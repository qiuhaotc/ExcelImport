using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMain.Common.BaseEnum.Enumeration;

namespace WebMain.Common
{
    /// <summary>
    /// 配置模型
    /// </summary>
    public class ConfigModel
    {
        public string SqlConnectionString { get; set; }
        public string TableName { get; set; }
        public string UploadPath { get; set; }
        public int StartRow { get; set; }
        public List<FieldModel> ListField { get; set; }
    }

    /// <summary>
    /// 字段模型
    /// </summary>
    public class FieldModel
    {
        public string FieldName { get; set; }
        public int ExcelColomn { get; set; }
        public FieldTypes ExcelFieldType { get; set; }
        public string SQLField { get; set; }
        public FieldTypes SQLFieldType { get; set; }
        public bool IsForeignKey { get; set; }
        public string ForeignTable { get; set; }
        public string ForeignKey { get; set; }
        public string LinkField { get; set; }
    }

    public struct ValueDataRow
    {
        public List<object> DataItem { get; set; }
    }

    public class SQLResultModel
    {
        public List<FieldModel> ListField { get; set; }
        public List<ValueDataRow> Data { get; set; }
    }
}