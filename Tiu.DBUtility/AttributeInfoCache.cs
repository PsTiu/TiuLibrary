using System;
using System.Collections.Generic;
using System.Text;
using Tiu.Attributes;
using Tiu.Data;

namespace Tiu.DBUtility
{
    /// <summary>
    /// 用于保存实体类特性信息的类（对应一个实体类）
    /// </summary>
    public class AttributeInfoCache
    {
        /// <summary>
        /// 表信息
        /// </summary>
        public ModelTableAttribute TableAttribute { get; private set; }

        public AttributeInfoCache(DbModel model)
        {
            TableAttribute = model.GetTableAttribute();
        }
    }

    public static class AttributeInfos
    {
        /// <summary>
        /// 用来感觉类名，缓存实体类反射信息的变量
        /// </summary>
        private static IDictionary<string, AttributeInfoCache> _dic = new Dictionary<string, AttributeInfoCache>();

        /// <summary>
        /// 获取实体类的放射信息
        /// </summary>
        /// <typeparam name="T">实体类的类型</typeparam>
        /// <returns></returns>
        public static AttributeInfoCache GetAttributeInfo<T>() where T : DbModel
        {
            var tp = typeof(T);
            var classFullName = tp.FullName;
            if (!_dic.ContainsKey(classFullName))
            {
                var model = Activator.CreateInstance(tp) as T;
                var attInfo = new AttributeInfoCache(model);
                _dic[classFullName] = attInfo;
            }

            return _dic[classFullName];
        }
    }
}
