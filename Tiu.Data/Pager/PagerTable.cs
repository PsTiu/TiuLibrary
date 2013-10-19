using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Tiu.Data
{
    /// <summary>
    /// 分页DataTable
    /// </summary>
    public class PagerTable : DataTable, IPagerable
    {
        #region 【分页属性】

        /// <summary>
        /// 查询的数据表的名称（或者是视图的名称）
        /// </summary>
        public string PagerTableName{ get; set; }

        /// <summary>
        /// 排序字段名（默认为"ID"）
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 排序方式（默认为DESC）
        /// </summary>
        public SelectOrderWay OrderWay { get; set; }

        /// <summary>
        /// 分页查询条件（不用加"where"）
        /// </summary>
        public string SeatchWhere { get; set; }

        /// <summary>
        /// 页码（第几页,默认值1）
        /// </summary>
        public int PageNum { get; set; }

        /// <summary>
        /// 每页显示的行数（默认值10）
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数（用于查询后返回数据）
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总记录数（用于查询后返回数据）
        /// </summary>
        public int RecordCount { get; set; }

        #endregion

        #region 【构造函数】
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagerTable()
            : base()
        {
            this.PageNum = 1;
            this.PageSize = 10;
            this.OrderBy = "ID";
            this.OrderWay = SelectOrderWay.DESC;
        }    
        #endregion

        #region 【公共方法】
        /// <summary>
        /// 转为EasyUI的DataGrid数据源（格式{"total":0,"rows":[]"）
        /// </summary>
        /// <returns></returns>
        public string ToEasyUiDataGridJson()
        {
            if (this.Rows.Count > 0)
            {
                StringBuilder JsonString = new StringBuilder();
                JsonString.Append("{\"total\":" + this.RecordCount.ToString() + ",\"rows\":[");

                for (int i = 0; i < this.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < this.Columns.Count; j++)
                    {
                        if (j < this.Columns.Count - 1)
                        {
                            JsonString.AppendFormat("\"{0}\":\"{1}\",", this.Columns[j].ColumnName, RepalceHtml(this.Rows[i][j].ToString()));

                        }
                        else if (j == this.Columns.Count - 1)
                        {
                            JsonString.AppendFormat("\"{0}\":\"{1}\"", this.Columns[j].ColumnName, RepalceHtml(this.Rows[i][j].ToString()));

                        }
                    }

                    if (i == this.Rows.Count - 1)
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
        #endregion

        #region 【私有方法】
        /// <summary>
        /// 转义json传递时导致错误的符号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string RepalceHtml(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            string re = str;
            re = re.Replace("\"", "'");
            re = re.Replace("\r\n", "<br>");
            re = re.Replace("\r", "<br>");
            return re;
        }
        #endregion

    }
}
