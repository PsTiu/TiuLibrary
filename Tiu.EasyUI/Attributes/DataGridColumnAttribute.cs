using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiu.EasyUI.Attributes
{
    /// <summary>
    /// DataGrid列特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DataGridColumnAttribute : Attribute
    {
        /// <summary>
        /// 数据列名
        /// </summary>
        private string _columnName;
        /// <summary>
        /// 数据列名
        /// </summary>
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        /// <summary>
        /// 是否显示哈希代码
        /// </summary>
        private bool _showHashCode;
        /// <summary>
        /// 是否显示哈希代码（默认为false，一般如果属性是枚举类型的话需要设置为true）
        /// </summary>
        public bool ShowHashCode
        {
            get { return _showHashCode; }
            set { _showHashCode = value; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public DataGridColumnAttribute():this(null,false) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="columnName">数据列名</param>
        public DataGridColumnAttribute(string columnName) : this(columnName, false) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="columnName">数据列名</param>
        /// <param name="showHashCode">是否显示哈希代码</param>
        public DataGridColumnAttribute(string columnName, bool showHashCode)
        {
            _columnName = columnName;
            _showHashCode = showHashCode;
        }
    }
}
