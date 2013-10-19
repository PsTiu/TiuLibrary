using System;
using System.Collections.Generic;
using System.Text;

using Tiu.Attributes;
using System.Reflection;
using Tiu.Tools;

namespace Tiu.Data
{
    /// <summary>
    /// 数据表映射类
    /// </summary>
    public class DbModel
    {
        /// <summary>
        /// 获取数据表名信息
        /// </summary>
        /// <returns></returns>
        public ModelTableAttribute GetTableAttribute()
        {
            return AttributesTool.GetAttributeByType<ModelTableAttribute>(this.GetType());
        }

        /// <summary>
        /// 获取数据表的所有列信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<ModelColumnAttribute, object> GetColumnsInfo()
        {
            Dictionary<ModelColumnAttribute, object> dicColumnsInfo = new Dictionary<ModelColumnAttribute, object>();

            Type thisType = this.GetType();
            PropertyInfo[] pis = thisType.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                ModelColumnAttribute columnAttr =  AttributesTool.GetAttributeByPropertyInfo<ModelColumnAttribute>(pi);
                if (columnAttr != null)
                {
                    dicColumnsInfo.Add(columnAttr, pi.GetValue(this, null));
                }
            }

            return dicColumnsInfo;
        }
    }
}
