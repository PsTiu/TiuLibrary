using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;
using System.Web;

namespace Tiu.Web
{
    /// <summary>
    /// Session帮助类
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// 当前Session
        /// </summary>
        private HttpSessionState _session;

        /// <summary>
        /// 构造方法
        /// </summary>
        public SessionHelper()
        { 
            _session = HttpContext.Current.Session;
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
            if (_session[key] != null)
            {
                if (!int.TryParse(_session[key].ToString(), out re))
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
            if (_session[key] != null)
            {
                re = _session[key].ToString();
            }
            return re;
        }
        
        /// <summary>
        /// 设置键值对的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetValue(string key, object value)
        {
            _session[key] = value;
        }
    }
}
