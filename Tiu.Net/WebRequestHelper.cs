using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Tiu.Net
{
    /// <summary>
    /// web请求帮助类
    /// </summary>
    public class WebRequestHelper
    {
        /// <summary>
        /// 请求方式
        /// </summary>
        public enum WebRequestMethod
        {
            GET, POST
        }


        /// <summary>
        /// 请求url，返回字符串形式的请求结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string RequestGetString(string url,WebRequestMethod method)
        {
            // 创建Request和Response对象
            WebRequest request = WebRequest.Create(url);
            request.Method = method.ToString();
            if (method == WebRequestMethod.POST)
            {
                // POST提交的话，必须要这个属性，否则服务器端接收不到提交的参数
                request.ContentType = "application/x-www-form-urlencoded"; 
            }
            
            // 请求，获取Response
            WebResponse response = request.GetResponse();

            // 获取返回流
            System.IO.Stream stream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);

            // 把流转成字符串
            string re = reader.ReadToEnd();

            // 关闭流
            stream.Close();
            reader.Close();

            // 返回结果
            return re;
        }

        /// <summary>
        /// 请求url，返回字符串形式的请求结果（GET）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string RequestGetString(string url)
        {
            return RequestGetString(url, WebRequestMethod.GET);
        }
    }
}
