using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.Attributes
{
    /// <summary>
    /// 实体类对应数据表信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=false)]
    public class ModelTableAttribute:Attribute
    {
        /// <summary>
        /// 数据表的表名
        /// </summary>
        private string _tableName;
        /// <summary>
        /// 数据表的表名
        /// </summary>
        public string TableName
        {
            get { return this._tableName; }
            set { this._tableName = value; }
        }


        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tableName">数据表的表名</param>
        public ModelTableAttribute(string tableName)
        {
            this._tableName = tableName;
        }
    }
}
