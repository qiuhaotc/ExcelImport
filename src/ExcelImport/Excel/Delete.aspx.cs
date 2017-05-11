using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMain.Common;

namespace WebMain.Excel
{
    public partial class Delete : System.Web.UI.Page
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
            ConfigModel model = ConfigOperater.GetConfigModel();

            if (SqlField != "")
            {
                FieldModel modelField = model.ListField.Find(u => u.SQLField == SqlField);
                if (modelField != null)
                {
                    model.ListField.Remove(modelField);

                    ConfigOperater.UpdateConfigModel(model);
                }

            }

            Response.Redirect("/excel/excelimport.aspx");
        }
    }
}