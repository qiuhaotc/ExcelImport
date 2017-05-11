using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class TableConstruct
    {
        /// <summary>
        /// 获取所有Table
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public List<string> GetTableNames(string dbName)
        {
            DataSet ds = SqlHelper.Query(" SELECT Name FROM SysObjects Where XType='U' ORDER BY Name");

            List<string> listTable = new List<string>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                listTable.Add(dr[0].ToString());
            }

            return listTable;
        }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="hasIdentity"></param>
        /// <returns></returns>
        public List<TableColomns> GetColomns(string tableName, bool hasIdentity = false)
        {
            DataSet ds = SqlHelper.Query(@"SELECT  CASE WHEN col.colorder = 1 THEN obj.name  
                  ELSE ''  
             END AS 表名,  
        col.colorder AS 序号 ,  
        col.name AS 列名 ,  
        ISNULL(ep.[value], '') AS 列说明 ,  
        t.name AS 数据类型 ,  
        col.length AS 长度 ,  
        ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS 小数位数 ,  
        CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN '√'  
             ELSE ''  
        END AS 标识 ,  
        CASE WHEN EXISTS ( SELECT   1  
                           FROM     dbo.sysindexes si  
                                    INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id  
                                                              AND si.indid = sik.indid  
                                    INNER JOIN dbo.syscolumns sc ON sc.id = sik.id  
                                                              AND sc.colid = sik.colid  
                                    INNER JOIN dbo.sysobjects so ON so.name = si.name  
                                                              AND so.xtype = 'PK'  
                           WHERE    sc.id = col.id  
                                    AND sc.colid = col.colid ) THEN '√'  
             ELSE ''  
        END AS 主键 ,  
        CASE WHEN col.isnullable = 1 THEN '√'  
             ELSE ''  
        END AS 允许空 ,  
        ISNULL(comm.text, '') AS 默认值  
FROM    dbo.syscolumns col  
        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype  
        inner JOIN dbo.sysobjects obj ON col.id = obj.id  
                                         AND obj.xtype = 'U'  
                                         AND obj.status >= 0  
        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id  
        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id  
                                                      AND col.colid = ep.minor_id  
                                                      AND ep.name = 'MS_Description'  
        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id  
                                                         AND epTwo.minor_id = 0  
                                                         AND epTwo.name = 'MS_Description'  
WHERE   obj.name = '"+tableName+@"'--表名  
ORDER BY col.colorder ;");

            List<TableColomns> tableColum = new List<TableColomns>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string name = dr["列名"].ToString();
                string describe = dr["列说明"].ToString();
                string typeStr = dr["数据类型"].ToString();
                string shiZhuJian = dr["主键"].ToString();

                if (shiZhuJian == "√")
                {
                    if (hasIdentity)
                    {
                        TableColomns colomn = new TableColomns() { ColomnName = name, ColomnType = WebMain.Common.BaseEnum.Enumeration.FieldTypes.TypeInt, Description = describe };
                        tableColum.Add(colomn);
                    }
                }
                else
                {
                    TableColomns colomn = new TableColomns() { ColomnName = name,Description = describe};

                    switch (typeStr)
                    {
                        case "int":
                            colomn.ColomnType = WebMain.Common.BaseEnum.Enumeration.FieldTypes.TypeInt;
                            break;

                        case "bit":
                            colomn.ColomnType = WebMain.Common.BaseEnum.Enumeration.FieldTypes.TypeBool;
                            break;

                        case "datetime":
                            colomn.ColomnType = WebMain.Common.BaseEnum.Enumeration.FieldTypes.TypeDateTime;
                            break;

                        case "float":
                        case "decimal":
                            colomn.ColomnType = WebMain.Common.BaseEnum.Enumeration.FieldTypes.TypeDouble;
                            break;

                        default:
                            colomn.ColomnType = WebMain.Common.BaseEnum.Enumeration.FieldTypes.TypeString;
                            break;
                    }

                    tableColum.Add(colomn);
                }
            }

            return tableColum;
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetColomnKey(string tableName)
        {
            DataSet ds = SqlHelper.Query("sp_columns " + tableName);

            List<TableColomns> tableColum = new List<TableColomns>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string name = dr["COLUMN_NAME"].ToString();
                string typeStr = dr["TYPE_NAME"].ToString();

                if (typeStr.Contains("identity"))
                {
                    return name;
                }
            }
            return "";
        }
    }

    public class TableColomns
    {
        public string ColomnName { get; set; }
        public string Description { get; set; }
        public WebMain.Common.BaseEnum.Enumeration.FieldTypes ColomnType { get; set; }
    }
}