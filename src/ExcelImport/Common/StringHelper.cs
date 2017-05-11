using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebMain.Common
{
    public class StringHelper
    {
        /// <summary>
        /// 特殊字符过滤
        /// </summary>
        public static string DelSQLStr(string str)
        {
            if (str == null || str == "")
                return "";
            str = str.Trim();
            str = str.Replace(";", "");
            str = str.Replace("'", "");
            str = str.Replace("&", "");
            str = str.Replace("%20", "");
            str = str.Replace("-", "");
            str = str.Replace("=", "");
            str = str.Replace("==", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("%", "");
            str = str.Replace(" or", "");
            str = str.Replace("or ", "");
            str = str.Replace(" and", "");
            str = str.Replace("and ", "");
            str = str.Replace(" not", "");
            str = str.Replace("not ", "");
            str = str.Replace("!", "");
            str = str.Replace("{", "");
            str = str.Replace("}", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("|", "");
            str = str.Replace("_", "");
            //不够可以加
            return str;
        }
    }
}