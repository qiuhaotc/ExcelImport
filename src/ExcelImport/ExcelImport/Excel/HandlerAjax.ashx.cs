using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMain.Excel
{
    /// <summary>
    /// HandlerAjax 的摘要说明
    /// </summary>
    public class HandlerAjax : IHttpHandler
    {
        static DAL.TableConstruct colomn = new DAL.TableConstruct();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            string method = context.Request["method"];

            switch (method)
            {
                case "colomn":
                    RenderColomn(context);
                    break;
                default:
                    break;
            }
        }

        private void RenderColomn(HttpContext context)
        {
            string tableName = Common.StringHelper.DelSQLStr(context.Request["tablename"]);

            if (!string.IsNullOrEmpty(tableName))
            {
                List<DAL.TableColomns> tableColomn = colomn.GetColomns(tableName);

                context.Response.Write(Common.JsonHelper.ToJsonStr<List<DAL.TableColomns>>(tableColomn));
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}