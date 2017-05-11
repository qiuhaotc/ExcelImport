using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMain.Excel
{
    public partial class Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Items["ResultSQL"] != null && Context.Items["SQLConnection"] != null)
            {
                string result = Context.Items["ResultSQL"].ToString();

                string connection = Context.Items["SQLConnection"].ToString();

                SQLResult.InnerText = result;
                sqlConnection.Value = connection;
            }
        }
    }
}