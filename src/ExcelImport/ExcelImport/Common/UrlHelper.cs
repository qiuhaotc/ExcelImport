using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebMain.Common
{
    public class UrlHelper
    {
        /// <summary>
        /// 返回Int型参数，不是或不存在则返回0
        /// </summary>
        /// <returns>参数值</returns>
        public static int GetQueryToInt(string queryName)
        {
            try
            {
                int queryValue = Convert.ToInt32(HttpContext.Current.Request[queryName]);
                return queryValue;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回Double型参数，不是或不存在则返回0
        /// </summary>
        /// <returns>参数值</returns>
        public static decimal GetQueryToDecimal(string queryName)
        {
            try
            {
                decimal queryValue = Convert.ToDecimal(HttpContext.Current.Request[queryName]);
                return queryValue;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 返回String型参数，不是或不存在则返回""
        /// </summary>
        /// <returns>参数值</returns>
        public static string GetQueryToStr(string queryName)
        {
            try
            {
                if (HttpContext.Current.Request[queryName] != null)
                {
                    string queryValue = GetValiParms(HttpContext.Current.Request[queryName]);
                    return queryValue;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 返回Boolean型参数，true/false不是或不存在则返回false
        /// </summary>
        /// <returns>参数值</returns>
        public static bool GetQueryToBoolean(string queryName)
        {
            try
            {
                bool queryValue;

                bool.TryParse(HttpContext.Current.Request[queryName], out queryValue);

                return queryValue;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 添加URL参数
        /// </summary>
        public static string AddParam(string url, string paramName, string value)
        {
            Uri uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Query))
            {
                string eval = HttpContext.Current.Server.UrlEncode(value);
                return String.Concat(url, "?" + paramName + "=" + eval);
            }
            else
            {
                string eval = HttpContext.Current.Server.UrlEncode(value);
                return String.Concat(url, "&" + paramName + "=" + eval);
            }
        }

        /// <summary>
        /// 更新URL参数
        /// </summary>
        public static string UpdateParam(string url, string paramName, string value)
        {
            string keyWord = paramName + "=";
            int index = url.IndexOf(keyWord) + keyWord.Length;
            int index1 = url.IndexOf("&", index);
            if (index1 == -1)
            {
                url = url.Remove(index, url.Length - index);
                url = string.Concat(url, value);
                return url;
            }
            url = url.Remove(index, index1 - index);
            url = url.Insert(index, value);
            return url;
        }

        /// <summary>
        /// 分析 url 字符串中的参数信息
        /// </summary>
        /// <param name="url">输入的 URL</param>
        /// <param name="baseUrl">输出 URL 的基础部分</param>
        /// <param name="nvc">输出分析后得到的 (参数名,参数值) 的集合</param>
        public static void ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            nvc = new NameValueCollection();
            baseUrl = "";

            if (url == "")
                return;

            int questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return;
            string ps = url.Substring(questionMarkIndex + 1);

            // 开始分析参数对
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);

            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }

        /// <summary>
        /// 获得是页面名
        /// </summary>
        /// <returns>页面名</returns>
        public static string GetPageName()
        {
            string[] urlStr = GetPath().Split('/');
            if (urlStr.Count() > 0)
            {
                return urlStr[urlStr.Length - 1];
            }
            return "";
        }

        /// <summary>
        /// 获得站点名+页面名
        /// </summary>
        /// <returns>站点名+页面名</returns>
        public static string GetPath()
        {
            return HttpContext.Current.Request.Path;
        }

        /// <summary>
        /// 向URL添加参数字符串
        /// </summary>
        /// <param name="url">主URL</param>
        /// <param name="queryStr">参数"query=value&query=value..."</param>
        /// <returns></returns>
        public static string AddQuery(string url, string queryStr)
        {
            if (url.Contains("?"))
            {
                return url + "&" + queryStr;
            }
            else
            {
                return url + "?" + queryStr;
            }
        }

        /// <summary>
        /// 从URL删除一个参数
        /// </summary>
        /// <param name="url">主URL</param>
        /// <param name="queryStr">参数"query"</param>
        /// <returns></returns>
        public static string RemoveQuery(string url, string queryStr)
        {
            if (url.Contains("?"))
            {
                string[] arrStr = url.Split('?')[1].Split('&');
                foreach (var str in arrStr)
                {
                    if (str.StartsWith(queryStr + "="))
                    {
                        url = url.Replace("&" + str, "");
                    }
                }
            }

            return url;
        }

        /// <summary>
        /// 读取QueryString值
        /// </summary>
        /// <param name="queryStringName">QueryString名称</param>
        /// <returns>QueryString值</returns>
        public static string GetQueryString(string queryStringName)
        {
            if ((HttpContext.Current.Request[queryStringName] != null) &&
                (HttpContext.Current.Request[queryStringName] != "undefined"))
            {
                return HttpContext.Current.Request[queryStringName].Trim();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获得当前访问页面的用户IP
        /// </summary>
        /// <returns>IP</returns>
        public static string GetUserHostNameIP()
        {
            //if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            //    return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            //else
            //    return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (null == result || result == String.Empty || !IsIP(result))
            {
                return "0.0.0.0";
            }
            return result;
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        #region 获得用户IP

        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp()
        {
            string ip;
            string[] temp;
            bool isErr = false;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
            if (ip.Length > 15)
                isErr = true;
            else
            {
                temp = ip.Split('.');
                if (temp.Length == 4)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].Length > 3) isErr = true;
                    }
                }
                else
                    isErr = true;
            }

            if (isErr)
                return "1.1.1.1";
            else
                return ip;
        }

        #endregion 获得用户IP

        #region 判断对象是否为空

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        #endregion 判断对象是否为空

        #region 检测客户的输入中是否有危险字符串

        /// <summary>
        /// 检测客户输入的字符串是否有效,并将原始字符串修改为有效字符串或空字符串。
        /// 当检测到客户的输入中有攻击性危险字符串,则返回false,有效返回true。
        /// </summary>
        /// <param name="input">要检测的字符串</param>
        public static bool IsValidInput(ref string input)
        {
            try
            {
                if (IsNullOrEmpty(input))
                {
                    //如果是空值,则跳出
                    return true;
                }
                else
                {
                    //替换单引号
                    input = input.Replace("'", "''").Trim();
                    //检测攻击性危险字符串
                    string testString = "and |or |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                    string[] testArray = testString.Split('|');
                    foreach (string testStr in testArray)
                    {
                        if (input.ToLower().IndexOf(testStr) != -1)
                        {
                            //检测到攻击字符串,清空传入的值
                            input = "";
                            return false;
                        }
                    }

                    //未检测到攻击字符串
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion 检测客户的输入中是否有危险字符串

        /// <summary>
        /// 过滤参数中的SQL字
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static string GetValiParms(string parms)
        {
            parms = parms.Replace("sp_", "sp-");
            parms = parms.Replace("'", "");
            parms = parms.Replace("create ", "");
            parms = parms.Replace("drop ", "");
            parms = parms.Replace("select ", "");
            parms = parms.Replace("\"", "");
            parms = parms.Replace("exec ", "");
            parms = parms.Replace("xp_", "xp-");
            parms = parms.Replace("insert ", "");
            parms = parms.Replace("update ", "");
            return parms;
        }

        //随机生成字符串（数字和字母混和）
        private int rep = 0;

        public string GenerateCheckCode(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + this.rep;
            this.rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> this.rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
}