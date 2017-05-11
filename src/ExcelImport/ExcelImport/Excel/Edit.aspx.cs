using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMain.Common;
using WebMain.Common.BaseEnum;

namespace WebMain.Excel
{
    public partial class Edit : System.Web.UI.Page
    {
        public string SqlField
        {
            get
            {
                return Common.UrlHelper.GetQueryString("sqlfield");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
                BindData();
            }
        }

        private void InitData()
        {
            List<AttributesUtils.EnumItem> listFieldType = AttributesUtils.GetEnumList<Common.BaseEnum.Enumeration.FieldTypes>();

            ExcelFieldType.DataSource = listFieldType;
            ExcelFieldType.DataTextField = "Value";
            ExcelFieldType.DataValueField = "Key";
            ExcelFieldType.DataBind();

            SQLFieldType.DataSource = listFieldType;
            SQLFieldType.DataTextField = "Value";
            SQLFieldType.DataValueField = "Key";
            SQLFieldType.DataBind();
        }

        private void BindData()
        {
            if (SqlField != "")
            {
                ConfigModel confModel = ConfigOperater.GetConfigModel();
                FieldModel modelField = confModel.ListField.Find(u => u.SQLField == SqlField);
                if (modelField != null)
                {
                    FieldName.Text = modelField.FieldName;
                    ExcelColomn.Text = modelField.ExcelColomn.ToString();
                    ExcelFieldType.SelectedValue = ((int)modelField.ExcelFieldType).ToString();

                    SQLField.Text = modelField.SQLField.ToString();
                    SQLFieldType.SelectedValue = ((int)modelField.SQLFieldType).ToString();
                    IsForeignKey.SelectedValue = (modelField.IsForeignKey==false?0:1).ToString();
                    ForeignTable.Text = modelField.ForeignTable;
                    ForeignKey.Text = modelField.ForeignKey.ToString();
                    LinkField.Text = modelField.LinkField.ToString();

                }
            }
        }

        protected void SaveForm(object sender, EventArgs e)
        {
            try
            {
                FieldModel modelField = new FieldModel();

                modelField.SQLField = SqlField;

                modelField.FieldName = FieldName.Text;

                modelField.ExcelColomn = Convert.ToInt32(ExcelColomn.Text);

                modelField.ExcelFieldType = (WebMain.Common.BaseEnum.Enumeration.FieldTypes)Convert.ToInt32(ExcelFieldType.SelectedValue);

                modelField.SQLField = SQLField.Text;

                modelField.SQLFieldType = (WebMain.Common.BaseEnum.Enumeration.FieldTypes)Convert.ToInt32(SQLFieldType.SelectedValue);

                modelField.IsForeignKey = IsForeignKey.SelectedValue == "0" ? false : true;

                modelField.ForeignTable = ForeignTable.Text;

                modelField.ForeignKey = ForeignKey.Text;

                modelField.LinkField = LinkField.Text;

                ConfigOperater.AddOrUpdateField(modelField);

                Response.Redirect("/Excel/ExcelImport.aspx");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            
        }

    }
}