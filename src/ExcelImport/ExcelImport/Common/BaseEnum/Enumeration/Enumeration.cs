namespace WebMain.Common.BaseEnum.Enumeration
{
    #region 字段类型

    /// <summary>
    /// 字段类型
    /// </summary>
    public enum FieldTypes
    {
        /// <summary>
        /// Int
        /// </summary>
        [Description("Int型")]
        TypeInt = 1,

        /// <summary>
        /// String
        /// </summary>
         [Description("String型")]
        TypeString= 2,

        /// <summary>
        ///Bool
        /// </summary>
         [Description("Bool型")]
        TypeBool = 3,

        /// <summary>
        ///DateTime
        /// </summary>
         [Description("DateTime型")]
        TypeDateTime = 4,

         /// <summary>
         ///DateTime
         /// </summary>
         [Description("Double型")]
         TypeDouble = 5
    }

    #endregion 状态代码
}