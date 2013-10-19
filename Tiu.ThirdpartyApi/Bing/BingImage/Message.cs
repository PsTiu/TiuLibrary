using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace Tiu.ThirdpartyApi.Bing
{
    /// <summary>
    /// 图片相关信息
    /// </summary>
    public class Message
    {
        #region 属性
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 描述文本
        /// </summary>
        public string Text { get; set; }
        #endregion

        #region 构造函数
        protected Message()
        { 
            
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 创建Message对象
        /// </summary>
        /// <param name="item">XmlNode</param>
        /// <returns></returns>
        internal static Message CreateMessage(XmlNode item)
        {
            Message msg = new Message();

            msg.Title = item["msgtitle"].InnerText;
            msg.Link = item["msglink"].InnerText;
            msg.Text = item["msgtext"].InnerText;

            return msg;
        }
        #endregion
    }
}