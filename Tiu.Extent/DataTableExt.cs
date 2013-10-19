using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data
{
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public static class DataTableExt
    {
        /// <summary>
        /// 转为Json字符串（格式{"total":0,"rows":[]"）
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="theObj">扩展对象，DataTable</param>
        /// <returns></returns>
        public static string ToJson(this DataTable theObj,int total)
        {
            return Tiu.Tools.ConvertTool.DataTableToJson(total,theObj);
        }

        /// <summary>
        /// 转为Json字符串（格式{"total":0,"rows":[]"）
        /// </summary>
        /// <param name="theObj">扩展对象，DataTable</param>
        /// <returns></returns>
        public static string ToJson(this DataTable theObj)
        {
            return Tiu.Tools.ConvertTool.DataTableToJson(theObj);
        }
    }
}
