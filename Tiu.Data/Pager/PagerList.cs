using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.Data
{
    /// <summary>
    /// 分页List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagerList<T> : List<T>, IPagerable
    {
        #region 【分页属性】

        /// <summary>
        /// 查询的数据表的名称（或者是视图的名称）
        /// </summary>
        public string PagerTableName { get; set; }

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
        public PagerList()
            : base()
        {
            this.PageNum = 1;
            this.PageSize = 10;
            this.OrderBy = "ID";
            this.OrderWay = SelectOrderWay.DESC;
        }    
        #endregion
    }
}
