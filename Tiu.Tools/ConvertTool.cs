using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace Tiu.Tools
{
    /// <summary>
    /// 类型转换工具
    /// </summary>
    public static class ConvertTool
    {
        /// <summary>
        /// DataTable转为Json字符串（格式{"total":0,"rows":[]"）
        /// </summary>
        /// <param name="dataTable">DataTable对象</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dataTable)
        {
            return DataTableToJson(dataTable.Rows.Count, dataTable);
        }

        /// <summary>
        /// DataTable转为Json字符串（格式{"total":0,"rows":[]"）
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="dataTable">DataTable对象</param>
        /// <returns></returns>
        public static string DataTableToJson(int total, DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                StringBuilder JsonString = new StringBuilder();
                JsonString.Append("{\"total\":" + total + ",\"rows\":[");

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        if (j < dataTable.Columns.Count - 1)
                        {
                            JsonString.AppendFormat("\"{0}\":\"{1}\",", dataTable.Columns[j].ColumnName, dataTable.Rows[i][j].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"));

                        }
                        else if (j == dataTable.Columns.Count - 1)
                        {
                            JsonString.AppendFormat("\"{0}\":\"{1}\"", dataTable.Columns[j].ColumnName, dataTable.Rows[i][j].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"));

                        }
                    }

                    if (i == dataTable.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return "{\"total\":0,\"rows\":[]}";
            }
        }

        
    }
}
