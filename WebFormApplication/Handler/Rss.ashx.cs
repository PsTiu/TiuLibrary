using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormApplication.Handler
{
    /// <summary>
    /// Summary description for Rss
    /// </summary>
    public class Rss : IHttpHandler
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="context">当前上下文</param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            string rssStr = System.IO.File.ReadAllText(context.Server.MapPath("~/Files/Rss.xml"));
            context.Response.Write(rssStr);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}