using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Tiu.Web
{
    /// <summary>
    /// Cookie帮助类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 获取Cookie值，返回字符串
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="defaultValue">为空时返回的值</param>
        /// <returns></returns>
        public string GetValueAsString(string name, string defaultValue)
        {
            string re = "";
            if (System.Web.HttpContext.Current.Request.Cookies[name] != null)
            {
                re =  System.Web.HttpContext.Current.Request.Cookies[name].Value;
            }
            else
            {
                re = defaultValue;
            }
            return HttpUtility.UrlDecode(re);
        }

        /// <summary>
        /// 获取Cookie值，返回字符串
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        public string GetValueAsString(string name)
        {
            return GetValueAsString(name, string.Empty);
        }

        /// <summary>
        /// 获取Cookie值，返回整型
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="defaultValue">为空时返回的值</param>
        /// <returns></returns>
        public int GetValueAsInt(string name, int defaultValue)
        {
            if (System.Web.HttpContext.Current.Request.Cookies[name] != null)
            {
                int re;
                if (int.TryParse(GetValueAsString(name), out re))
                {
                    return re;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取Cookie值，返回整型
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        public int GetValueAsInt(string name)
        {
            return GetValueAsInt(name, 0);
        }


        /// <summary>
        /// 设置Cookie值，永久保存
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public void SetValue(string name, string value)
        {
            SetValue(name, value, DateTime.MaxValue);
        }

        /// <summary>
        /// 设置Cookie值
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public void SetValue(string name, string value, DateTime expires)
        {
            value = HttpUtility.UrlEncode(value); // 对Cookie值进行url编码
            System.Web.HttpContext.Current.Response.Cookies.Set(new System.Web.HttpCookie(name, value));
            System.Web.HttpContext.Current.Response.Cookies[name].Expires = expires;
        }

        #region 清除Cookie

        /// <summary>
        /// 清除Cookie
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        public void Remove(string name)
        {
            System.Web.HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
        }

        /// <summary>
        /// 清除所有Cookie
        /// </summary>
        /// <returns></returns>
        public void RemoveAll()
        {
            System.Web.HttpContext.Current.Response.Cookies.Clear();
        }

        #endregion
    }
}
