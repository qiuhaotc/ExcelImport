using System;
using System.Collections.Generic;

namespace WebMain.Common.BaseEnum
{
    public class AttributesUtils
    {
        /// <summary>
        /// 获取枚举的Description字典
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IDictionary<int, string> GetEnumValueDesc<TEnum>()
        {
            Type enumType = typeof(TEnum);
            string[] names = Enum.GetNames(enumType);
            IDictionary<int, string> kv = new Dictionary<int, string>();
            foreach (string name in names)
            {
                object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                }
                else
                {
                    DescriptionAttribute attr = objs[0] as DescriptionAttribute;
                    if (!attr.ExcludeWhenSelect)
                    {
                        int value = Convert.ToInt32(Enum.Parse(enumType, name));
                        kv.Add(value, attr.Description);
                    }
                }
            }
            return kv;
        }

        /// <summary>
        /// 获取某个枚举某个值的Description
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription<TEnum>(object enumValue)
        {
            IDictionary<int, string> kv = GetEnumValueDesc<TEnum>();
            int value = Convert.ToInt32(enumValue);
            if (kv.ContainsKey(value))
            {
                return kv[value];
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取枚举集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumItem> GetEnumList<T>()
        {
            List<EnumItem> enumList = new List<EnumItem>();
            Type enumType = typeof(T);
            string[] names = Enum.GetNames(enumType);
            int[] values = (int[])Enum.GetValues(enumType);
            for (int i = 0; i < values.Length; i++)
            {
                object[] objs = enumType.GetField(names[i]).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                }
                else
                {
                    DescriptionAttribute attr = objs[0] as DescriptionAttribute;
                    string strName = attr.Description;
                    int value = values[i];
                    enumList.Add(new EnumItem() { Key = value, Value = strName });
                }
            }
            return enumList;
        }

        public class EnumItem
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
    }
}