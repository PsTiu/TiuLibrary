using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.Attributes
{
    /// <summary>
    /// 实体类对应数据列信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=false)]
    public class ModelColumnAttribute:Attribute
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
            get { return this._columnName; }
            set { this._columnName = value; }
        }

        /// <summary>
        /// 是否自增
        /// </summary>
        private bool _identity;
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool Identity
        {
            get { return this._identity; }
            set { this._identity = value; }
        }


        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="columnName">数据列名</param>
        public ModelColumnAttribute(string columnName)
            : this(columnName, false)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="columnName">数据列名</param>
        /// <param name="isAuto">是否自增</param>
        public ModelColumnAttribute(string columnName,bool isAuto)
        {
            this._identity = isAuto;
            this._columnName = columnName;
        }
    }
}
