using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace Tiu.Web
{
    /// <summary>
    /// QueryString帮助类
    /// </summary>
    public class QueryStringHelper
    {
        /// <summary>
        /// 当前QueryString
        /// </summary>
        private NameValueCollection _queryString;

        /// <summary>
        /// 构造方法
        /// </summary>
        public QueryStringHelper()
        {
            _queryString = System.Web.HttpContext.Current.Request.QueryString;
        }


        /// <summary>
        /// 获取键值对的值，返回结果为整形（获取不到返回0）
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public int GetValueAsInt(string key)
        {
            return GetValueAsInt(key, 0);
        }

        /// <summary>
        /// 获取键值对的值，返回结果为整形（获取不到返回自定义的默认值）
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">获取不到值时返回的默认值</param>
        /// <returns></returns>
        public int GetValueAsInt(string key, int defaultValue)
        {
            int re;
            if (_queryString[key] != null)
            {
                if (!int.TryParse(_queryString[key].ToString(), out re))
                {
                    re = defaultValue;
                }
            }
            else
            {
                re = defaultValue;
            }
            return re;
        }

        /// <summary>
        /// 获取键值对的值，返回结果为字符串类型（获取不到返回空字符串）
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public string GetValueAsString(string key)
        {
            return GetValueAsString(key, "");
        }

        /// <summary>
        /// 获取键值对的值，返回结果为字符串类型（获取不到返回空字符串）
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defalutValue">获取不到值时返回的默认值</param>
        /// <returns></returns>
        public string GetValueAsString(string key, string defalutValue)
        {
            string re = defalutValue;
            if (_queryString[key] != null)
            {
                re = _queryString[key].ToString();
            }
            return re;
        }

    }
}
