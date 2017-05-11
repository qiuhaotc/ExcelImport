using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMain.Common;
using System.Data;

namespace WebMain.Excel
{
    public partial class StepResult : System.Web.UI.Page
    {

        DAL.TableConstruct dalTable = new DAL.TableConstruct();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["DataTable"] != null)
            {
                AnalysisData();
            }
            else
            {
                Response.Redirect("/excel/stepzero.aspx");
            }
        }

        /// <summary>
        /// 根据传入参数生成SQL
        /// </summary>
        private void AnalysisData()
        {
            try
            {
                string errorStr = "";

                DataTable dt = Session["DataTable"] as DataTable;

                Common.ConfigModel modelConfig = Common.ConfigOperater.GetConfigModel();

                DAL.SqlHelper.connectionString = modelConfig.SqlConnectionString;

                InitConfig(modelConfig);

                string result = SetExcelToSQL.RenderDataTableToSQLResult(dt, modelConfig, ref errorStr);

                if (errorStr != "")
                {
                    SQLResult.InnerText = errorStr;
                    sqlConnection.Value = modelConfig.SqlConnectionString;
                }
                else
                {
                    SQLResult.InnerText = result;
                    sqlConnection.Value = modelConfig.SqlConnectionString;
                }
            }
            catch (Exception ex)
            {
                SQLResult.InnerText = ex.ToString();
            }
        }

        /// <summary>
        /// 根据传入的配置字段修改 ConfigModel
        /// </summary>
        /// <param name="modelConfig"></param>
        private void InitConfig(ConfigModel modelConfig)
        {
            modelConfig.ListField = new List<FieldModel>();

            string fromField = Request["FromField"];
            string toField = Request["ToField"];
            string isForeign = Request["IsForeign"];
            string foreignField = Request["ForeignField"];
            string foreignTable = Request["ForeignTable"];

            string[] fromFields = fromField.Split(',');
            string[] toFields = toField.Split(',');
            string[] isForeigns = isForeign.Split(',');
            string[] foreignFields = foreignField.Split(',');
            string[] foreignTables = foreignTable.Split(',');

            List<DAL.TableColomns> colomns = dalTable.GetColomns(modelConfig.TableName);

            try
            {
                for (int i = 0; i < fromFields.Length; i++)
                {
                    if (fromFields[i] != "" && toFields[i] != "")
                    {
                        DAL.TableColomns colomn = colomns.Find(u => u.ColomnName == toFields[i]);

                        if (colomn != null)
                        {
                            FieldModel modelField = new FieldModel();

                            if (isForeigns[i] == "true" && !string.IsNullOrEmpty(foreignFields[i]) && !string.IsNullOrEmpty(foreignTables[i]))
                            {
                                string foreignKey = GetForeignKey(foreignTables[i]);

                                if (!string.IsNullOrEmpty(foreignKey))
                                {
                                    modelField.ExcelFieldType =Common.BaseEnum.Enumeration.FieldTypes.TypeString;
                                    modelField.FieldName = fromFields[i];
                                    modelField.ForeignKey = foreignKey;
                                    modelField.ForeignTable = foreignTables[i];
                                    modelField.IsForeignKey = true;
                                    modelField.LinkField = foreignFields[i];
                                    modelField.SQLField = colomn.ColomnName;
                                    modelField.SQLFieldType = Common.BaseEnum.Enumeration.FieldTypes.TypeInt;
                                    modelField.ExcelColomn = 0;

                                    modelConfig.ListField.Add(modelField);
                                }
                            }
                            else
                            {
                                modelField.ExcelFieldType = colomn.ColomnType;
                                modelField.FieldName = fromFields[i];
                                modelField.ForeignKey = "";
                                modelField.ForeignTable = "";
                                modelField.IsForeignKey = false;
                                modelField.LinkField = "";
                                modelField.SQLField = colomn.ColomnName;
                                modelField.SQLFieldType = colomn.ColomnType;
                                modelField.ExcelColomn = 0;

                                modelConfig.ListField.Add(modelField);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }

           
        }

        private string GetForeignKey(string p)
        {
            return dalTable.GetColomnKey(p);
        }

        #region 注册客户端js + RegistScript(string scriptStr)
        /// <summary>
        /// 注册客户端js
        /// </summary>
        /// <param name="scriptStr"></param>
        public void RegisterScript(string scriptStr)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>" + scriptStr + "</script>");
        }
        #endregion
    }
}