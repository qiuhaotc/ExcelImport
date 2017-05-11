using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMain.Common;
using System.Data;
using System.IO;

namespace WebMain.Excel
{
    public partial class StepZero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DataTable"] = null;
                BindGrid();
            }
        }

        private void BindGrid()
        {
            ConfigModel configModel = ConfigOperater.GetConfigModel();

            SqlConnectionString.Text = configModel.SqlConnectionString;
            TableName.Text = configModel.TableName;
            UploadPath.Text = configModel.UploadPath;
            StartRow.Text = configModel.StartRow.ToString();
        }

        protected void SaveForm(object sender, EventArgs e)
        {
            try
            {
                ConfigModel configModel = ConfigOperater.GetConfigModel();
                configModel.SqlConnectionString = SqlConnectionString.Text;
                configModel.TableName = TableName.Text;
                configModel.UploadPath = UploadPath.Text;
                configModel.StartRow = Convert.ToInt32(StartRow.Text);

                ConfigOperater.UpdateConfigModel(configModel);
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }


        protected void UploadExcel(object sender, EventArgs e)
        {
            if (ExcelFile.HasFile)
            {
                string fileName = ExcelFile.FileName;
                FileInfo fileInfo = new FileInfo(fileName);

                if (fileInfo.Extension == ".xls" || fileInfo.Extension == ".xlsx")
                {
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileName;

                    ConfigModel modelConfig = ConfigOperater.GetConfigModel();

                    string filePath = Server.MapPath("/" + modelConfig.UploadPath + "/" + fileName);

                    ExcelFile.SaveAs(filePath);

                    try
                    {
                        DataTable dt = ExcelHelper.GetExcelToDataTable(url: filePath, startRow: (modelConfig.StartRow > 0 ? modelConfig.StartRow - 1 : 0));

                        Session["DataTable"] = dt;

                        //Context.Items.Add("SQLConnection", modelConfig.SqlConnectionString);

                        Response.Redirect("/Excel/StepOne.aspx");
                    }
                    catch (Exception ex)
                    {
                        RegisterScript("alert('导入错误！" + ex.ToString().Replace("\n", "").Replace("\r", "").Replace("'", "\"") + "')");
                    }
                    finally
                    {
                        File.Delete(filePath);
                    }
                }
                else
                {
                    RegisterScript("alert('错误的文件！')");
                }
            }
            else
            {
                RegisterScript("alert('请选择文件！')");
            }
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