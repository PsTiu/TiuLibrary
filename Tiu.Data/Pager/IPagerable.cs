using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.Data
{
    /// <summary>
    /// 分页接口
    /// </summary>
    public interface IPagerable
    {
        #region 【分页属性】

        /// <summary>
        /// 查询的数据表的名称（或者是视图的名称）
        /// </summary>
        string PagerTableName { get; set; }

        /// <summary>
        /// 排序字段名（默认为"ID"）
        /// </summary>
        string OrderBy { get; set; }

        /// <summary>
        /// 排序方式（默认为DESC）
        /// </summary>
        SelectOrderWay OrderWay { get; set; }

        /// <summary>
        /// 分页查询条件（不用加"where"）
        /// </summary>
        string SeatchWhere { get; set; }

        /// <summary>
        /// 页码（第几页,默认值1）
        /// </summary>
        int PageNum { get; set; }

        /// <summary>
        /// 每页显示的行数（默认值10）
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 总页数（用于查询后返回数据）
        /// </summary>
        int PageCount { get; set; }

        /// <summary>
        /// 总记录数（用于查询后返回数据）
        /// </summary>
        int RecordCount { get; set; }

        #endregion
    }
}
