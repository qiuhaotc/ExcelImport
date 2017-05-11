using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DAL;

namespace WebMain.Common
{
    public class SetExcelToSQL
    {
        /// <summary>
        /// DataTable转SQLResultModel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="config"></param>
        public static string RenderDataTableToSQLResult(DataTable dt, ConfigModel config, ref string errorStr)
        {
            SQLResultModel modelResult = new SQLResultModel();
            modelResult.ListField = new List<FieldModel>();
            modelResult.Data = new List<ValueDataRow>();

            int colomnNum = 0;

            foreach (DataColumn item in dt.Columns)
            {
                FieldModel modelField = config.ListField.Find(u => u.FieldName == item.ColumnName);
                if (modelField != null)
                {
                    modelField.ExcelColomn = colomnNum;
                    modelResult.ListField.Add(modelField);
                }

                colomnNum++;
            }

            try
            {
                foreach (DataRow dr in dt.Rows)
                {

                    ValueDataRow valueDataRow = new ValueDataRow();
                    valueDataRow.DataItem = new List<object>();

                    foreach (var field in modelResult.ListField)
                    {
                        object value = DBNull.Value;

                        switch (field.ExcelFieldType)
                        {
                            case Common.BaseEnum.Enumeration.FieldTypes.TypeInt:
                                value = Convert.ToInt32(dr[field.FieldName]);
                                break;

                            case Common.BaseEnum.Enumeration.FieldTypes.TypeString:
                                value = Convert.ToString(dr[field.FieldName]).Replace("'","");
                                break;

                            case Common.BaseEnum.Enumeration.FieldTypes.TypeBool:
                                value = Convert.ToBoolean(dr[field.FieldName]);
                                break;

                            case Common.BaseEnum.Enumeration.FieldTypes.TypeDateTime:
                                value = Convert.ToDateTime(dr[field.FieldName]);
                                break;
                            case Common.BaseEnum.Enumeration.FieldTypes.TypeDouble:
                                value = Convert.ToDouble(dr[field.FieldName]);
                                break;
                        }
                        valueDataRow.DataItem.Add(value);
                    }

                    modelResult.Data.Add(valueDataRow);
                }
               return  RenderToSQLStr(modelResult, config, ref errorStr);
            }
            catch (Exception ex)
            {
                errorStr += ex.ToString();
                return "";
            }
        }

        /// <summary>
        /// SQLResultModel转SQLString
        /// </summary>
        /// <param name="modelResult"></param>
        private static string RenderToSQLStr(SQLResultModel modelResult, ConfigModel config, ref string errorStr)
        {
            try
            {
                SqlHelper.connectionString = config.SqlConnectionString;

                string temp = "INSERT INTO " + config.TableName + "({0}) VALUES({1})\r\n";

                string tempForeign = "select {0} from {1} where {2}";

                string field = "", values = "";

                foreach (var item in modelResult.ListField)
                {
                    if (field == "")
                    {
                        field += item.SQLField;
                    }
                    else
                    {
                        field += "," + item.SQLField;
                    }
                }

                StringBuilder sb = new StringBuilder();

                //循环赋值
                for (int row = 0; row < modelResult.Data.Count; row++)
                {
                    for (int colomn = 0; colomn < modelResult.ListField.Count; colomn++)
                    {
                        List<object> dc = modelResult.Data[row].DataItem;
                        FieldModel model = modelResult.ListField[colomn];

                        //如果是外键字段
                        if (modelResult.ListField[colomn].IsForeignKey == true)
                        {
                            DataSet ds = SqlHelper.Query(string.Format(tempForeign, model.ForeignKey, model.ForeignTable, model.LinkField + "='" + dc[colomn] + "'"));

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dc[colomn] = ds.Tables[0].Rows[0][0].ToString();
                            }
                            else
                            {
                                //赋初值
                                dc[colomn] = 0;
                                //switch (model.SQLFieldType)
                                //{
                                //    case Common.BaseEnum.Enumeration.FieldTypes.TypeInt:
                                //        dc[colomn] = 0;
                                //        break;
                                //    case Common.BaseEnum.Enumeration.FieldTypes.TypeBool:
                                //        dc[colomn] = false;
                                //        break;
                                //}
                            }
                        }

                        if (values != "")
                        {
                            values += ",";
                        }

                        switch (model.SQLFieldType)
                        {
                            case Common.BaseEnum.Enumeration.FieldTypes.TypeInt:
                            case Common.BaseEnum.Enumeration.FieldTypes.TypeDouble:
                                values += dc[colomn];
                                break;
                            case Common.BaseEnum.Enumeration.FieldTypes.TypeString:
                                values += "'" + dc[colomn] + "'";
                                break;
                            case Common.BaseEnum.Enumeration.FieldTypes.TypeBool:
                                values += ((bool)dc[colomn]) == false ? 0 : 1;
                                break;
                            case Common.BaseEnum.Enumeration.FieldTypes.TypeDateTime:
                                values += "'" + dc[colomn] + "'";
                                break;
                            default:
                                values += "'" + dc[colomn] + "'";
                                break;
                        }
                    }
                    sb.Append(String.Format(temp, field, values));

                    values = "";
                }

                string resultTemp = @"BEGIN TRY 
BEGIN TRANSACTION 

"+sb.ToString()+@"
PRINT '导入成功！'
COMMIT TRANSACTION 
END TRY 
BEGIN CATCH 
SELECT ERROR_NUMBER() AS ERRORNUMBER 
PRINT '导入错误！'
ROLLBACK TRANSACTION 
END CATCH";

                return resultTemp;
            }
            catch (Exception ex)
            {
                errorStr = ex.ToString();
                return "";
            }
        }
    }
}