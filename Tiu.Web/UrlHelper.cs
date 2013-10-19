using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Tiu.Web
{
    /// <summary>
    /// Url帮助类
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        /// 获取当前绝对的根路径
        /// </summary>
        public string GetRootUrl()
        {
            HttpRequest request = HttpContext.Current.Request;
            String _rootUrl = string.Format("{0}://{1}{2}",
                request.Url.Scheme,
                request.Url.Authority,
                request.ApplicationPath).TrimEnd('/');

            return _rootUrl;
        }

        /// <summary>
        /// 获取虚拟路基在该网站中的路径
        /// </summary>
        /// <param name="vPath">虚拟路径(“~/”开头）</param>
        /// <returns></returns>
        public string GetUrlInSite(string vPath)
        {
            string url = GetRootUrl();
            if (vPath.IndexOf("~/") == 0)
            {
                vPath = vPath.Substring(1, vPath.Length - 1);
            }
            url = url + vPath;
            return url;
        }

    }
}
