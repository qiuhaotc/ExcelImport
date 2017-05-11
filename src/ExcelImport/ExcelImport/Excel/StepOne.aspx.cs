using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebMain.Excel
{
    public partial class StepOne : System.Web.UI.Page
    {
        private DAL.TableConstruct dalColomn = new DAL.TableConstruct();
        private int nowIndex = 0;

        public int NowIndex
        {
            get
            {
                return nowIndex;
            }
            set
            {
                nowIndex = value;
            }
        }

        private List<String> listExcelField = null;
        private List<String> listTables = null;
        private List<DAL.TableColomns> listColomn = null;

        public List<string> GetList()
        {
            if (listExcelField == null)
            {
                if (Session["DataTable"] != null)
                {
                    DataTable dt = Session["DataTable"] as DataTable;

                    listExcelField = new List<String>();

                    foreach (DataColumn item in dt.Columns)
                    {
                        listExcelField.Add(item.ColumnName);
                    }
                }
                else
                {
                    listExcelField = new List<string>();
                }
            }
            

            return listExcelField;
        }

        public List<string> GetTables()
        {
            if (listTables == null)
            {
                if (Session["DataTable"] != null)
                {
                    Common.ConfigModel modelConfig = Common.ConfigOperater.GetConfigModel();

                    DAL.SqlHelper.connectionString = modelConfig.SqlConnectionString;

                    listTables = dalColomn.GetTableNames(modelConfig.TableName);
                }
                else
                {
                    listExcelField = new List<string>();
                }
            }

            return listTables;
        }

        public List<DAL.TableColomns> GetColomn()
        {
            if (listColomn == null)
            {
                if (Session["DataTable"] != null)
                {
                    Common.ConfigModel modelConfig = Common.ConfigOperater.GetConfigModel();

                    DAL.SqlHelper.connectionString = modelConfig.SqlConnectionString;

                    listColomn = dalColomn.GetColomns(modelConfig.TableName);
                }
                else
                {
                    listColomn = new List<DAL.TableColomns>();
                }
            }

            return listColomn;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NowIndex = 0;

            if (!IsPostBack && Session["DataTable"] != null)
            {
                RepeaterItem.DataSource = GetList();
                RepeaterItem.DataBind();
            }
            else
            {
                Response.Redirect("/excel/stepzero.aspx");
            }
        }
    }
}