using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Tiu.Tools
{
    /// <summary>
    /// 类型工具类
    /// </summary>
    public static class AttributesTool
    {
        /// <summary>
        /// 获取指定类的指定特性（包括基类的特性）
        /// </summary>
        /// <typeparam name="ClassT">指定的类</typeparam>
        /// <typeparam name="AttT">要获取的特性</typeparam>
        /// <returns></returns>
        public static AttT GetAttributeInClass<ClassT, AttT>() where AttT : Attribute
        {
            return GetAttributeByType<AttT>(typeof(ClassT), true);
        }

        /// <summary>
        /// 获取指定类的指定特性
        /// </summary>
        /// <typeparam name="ClassT">指定的类</typeparam>
        /// <typeparam name="AttT">要获取的特性</typeparam>
        /// <param name="inherit">是否继承，即是否包括基类的特性</param>
        /// <returns></returns>
        public static AttT GetAttributeInClass<ClassT, AttT>(bool inherit) where AttT : Attribute
        {
            return GetAttributeByType<AttT>(typeof(ClassT), inherit);
        }

        /// <summary>
        /// 获取指定类型的指定特性（包括基类的特性）
        /// </summary>
        /// <typeparam name="AttT">要获取的特性</typeparam>
        /// <param name="objType">类型</param>
        /// <returns></returns>
        public static AttT GetAttributeByType<AttT>(Type objType) where AttT : Attribute
        {
            return GetAttributeByType<AttT>(objType, true);
        }

        /// <summary>
        /// 获取指定类型的指定特性
        /// </summary>
        /// <typeparam name="AttT">要获取的特性</typeparam>
        /// <param name="objType">类型</param>
        /// <param name="inherit">是否继承，即是否包括基类的特性</param>
        /// <returns></returns>
        public static AttT GetAttributeByType<AttT>(Type objType, bool inherit) where AttT : Attribute
        {
            AttT re = null;

            object[] classAttrs = objType.GetCustomAttributes(true);
            foreach (Attribute item in classAttrs)
            {
                if (item.GetType() == typeof(AttT))
                {
                    re = item as AttT;
                }
            }

            return re;
        }

        /// <summary>
        /// 通过属性信息PropertyInfo获取类型特性
        /// </summary>
        /// <typeparam name="AttT">要获取的特性</typeparam>
        /// <param name="pi">属性信息</param>
        /// <returns></returns>
        public static AttT GetAttributeByPropertyInfo<AttT>(PropertyInfo pi) where AttT : Attribute
        {
            return GetAttributeByPropertyInfo<AttT>(pi, true);
        }

        /// <summary>
        /// 通过属性信息PropertyInfo获取类型特性
        /// </summary>
        /// <typeparam name="AttT">要获取的特性</typeparam>
        /// <param name="pi">属性信息</param>
        /// <param name="inherit">是否继承</param>
        /// <returns></returns>
        public static AttT GetAttributeByPropertyInfo<AttT>(PropertyInfo pi, bool inherit) where AttT : Attribute
        {
            AttT re = null;

            // 获取属性类型的所有特性，遍历找出ModelColumnAttribute，获取数据表的列名
            object[] attrs = pi.GetCustomAttributes(true);
            foreach (Attribute item in attrs)
            {
                if (item.GetType() == typeof(AttT))
                {
                    re = item as AttT;
                }
            }

            return re;
        }


        /// <summary>
        /// 获取指定类型指定属性的指定特性（既：在ClassT类中，获取名为propertyName的字段的AttT特性）
        /// </summary>
        /// <typeparam name="ClassT">指定的类</typeparam>
        /// <typeparam name="AttT">要获取的特性</typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public static AttT GetAttributeInProperty<ClassT,AttT>(string propertyName) where AttT : Attribute
        {
            AttT re = null;

            Type tt = typeof(ClassT);
            PropertyInfo[] pis = tt.GetProperties();
            PropertyInfo pi = null;
            foreach (PropertyInfo item in pis)
            {
                if (item.Name == propertyName)
                {
                    pi = item;
                    break;
                }
            }
            if (pi != null)
            {
                re = GetAttributeByPropertyInfo<AttT>(pi);
            }

            return re;
        }
    }
}
