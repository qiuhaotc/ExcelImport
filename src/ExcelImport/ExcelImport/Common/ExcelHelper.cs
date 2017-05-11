using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Data;
using System.Reflection;
using System.Web;

namespace WebMain.Common
{
    #region Excel导入导出帮助类
    /// <summary>
    /// Excel导入导出帮助类 使用Aspose.Cells
    /// </summary>
    public class ExcelHelper
    {
        /**************************导入数据**************************/
        #region 4.0读取数据到DataTable + GetExcelToDataTable(string url, int sheetNum = 0, int startRow = 1, bool toString = true)
        /// <summary>
        /// 读取数据到DataTable
        /// </summary>
        /// <param name="url">文件地址</param>
        /// <param name="sheetNum">所在表格</param>
        /// <param name="startRow">第一行位置</param>
        /// <returns></returns>
        public static DataTable GetExcelToDataTable(string url, int sheetNum = 0, int startRow = 1, bool toString = true,bool columnName=true)
        {
            Workbook workbook = new Workbook(url);

            Cells cells = workbook.Worksheets[sheetNum].Cells;

            DataTable dataTableExcel;
            if (toString)
            {
                dataTableExcel = cells.ExportDataTableAsString(startRow, 0, cells.MaxDataRow + 1, cells.MaxColumn + 1, columnName);
            }
            else
            {
                dataTableExcel = cells.ExportDataTable(startRow, 0, cells.MaxDataRow + 1, cells.MaxColumn + 1, columnName);
            }
            return dataTableExcel;
        }
        #endregion

    }
    #endregion
}
