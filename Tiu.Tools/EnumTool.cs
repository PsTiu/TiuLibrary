using System;
using System.Collections.Generic;
using System.Text;
using Tiu.Attributes;

namespace Tiu.Tools
{
    /// <summary>
    /// 枚举工具
    /// </summary>
    public static class EnumTool
    {
        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="e">枚举值</param>
        /// <returns></returns>
        public static EnumDescriptionAttribute GetEnumDescription(object e)
        {
            // 获取枚举对象的类型
            Type objType = e.GetType();

            // 获取字段信息
            System.Reflection.FieldInfo[] fieldInfos = objType.GetFields();

            // 遍历字段信息
            foreach (System.Reflection.FieldInfo fieldInfo in fieldInfos)
            {
                // 判断名称是否相等
                if (fieldInfo.Name != e.ToString())
                {
                    continue;
                }
                else
                {
                    // 反射出特性
                    foreach (Attribute attr in fieldInfo.GetCustomAttributes(true))
                    {
                        // 类型转换找到一个Description，用Description作为成员名称
                        EnumDescriptionAttribute dscript = attr as EnumDescriptionAttribute;
                        if (dscript != null)
                        {
                            return dscript;
                        }
                    }
                }
            }

            //如果没有检测到合适的注释，则返回null
            return null;
        }

        /// <summary>
        /// 字符串转为枚举（没有异常处理）
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">字符串的值</param>
        /// <returns></returns>
        public static T Parst<T>(string value)
        {
            T re = (T)Enum.Parse(typeof(T), value);
            return re;
        }

        /// <summary>
        /// 对象串转为枚举（没有异常处理）
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">对象的值</param>
        /// <returns></returns>
        public static T Parst<T>(object value)
        {
            T re = Parst<T>(value);
            return re;
        }

        /// <summary>
        /// 字符串转为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">字符串的值</param>
        /// <param name="defauleValue">转换失败返回的默认值</param>
        /// <returns></returns>
        public static T Parst<T>(string value,T defauleValue)
        {
            try
            {
                T re = (T)Enum.Parse(typeof(T), value);
                return re;
            }
            catch
            {
                return defauleValue;
            }
        }

        /// <summary>
        /// 对象转为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">对象的值</param>
        /// <param name="defauleValue">转换失败返回的默认值</param>
        /// <returns></returns>
        public static T Parst<T>(object value, T defauleValue)
        {
            try
            {
                T re = Parst<T>(value.ToString(), defauleValue);
                return re;
            }
            catch
            {
                return defauleValue;
            }
        }
    }
}
