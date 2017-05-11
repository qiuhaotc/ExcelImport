using System;

namespace WebMain.Common.BaseEnum
{
    public class DescriptionAttribute : Attribute
    {
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        //当使用获取枚举项的方法输出到下拉框，列表项的时候，该项是排除的。
        private bool _excludeWhenSelect;

        public bool ExcludeWhenSelect
        {
            get { return _excludeWhenSelect; }
            set { _excludeWhenSelect = value; }
        }

        public DescriptionAttribute(string desc)
        {
            _description = desc;
        }

        public DescriptionAttribute(string desc, bool exclude)
        {
            _description = desc;
            _excludeWhenSelect = exclude;
        }
    }
}