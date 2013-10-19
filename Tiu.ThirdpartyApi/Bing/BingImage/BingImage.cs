using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Drawing;
using System.Net;
using System.IO;

namespace Tiu.ThirdpartyApi.Bing
{
    /// <summary>
    /// Bing图片信息
    /// </summary>
    public class BingImage
    {
        #region 常量
        /// <summary>
        /// Bing的地址
        /// </summary>
        private const string BING_URL = "http://www.bing.com";
        /// <summary>
        /// 图片默认宽度
        /// </summary>
        public const int DEFAULT_WIDTH = 958;
        /// <summary>
        /// 图片默认高度
        /// </summary>
        public const int DEFAULT_HEIGHT = 512;
        #endregion

        #region 属性
        /// <summary>
        /// 图片开始在首页显示的时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 图片结束在首页显示的时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 图片的Url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图片的版权信息
        /// </summary>
        public string Copyright { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Drk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Bot { get; set; }

        /// <summary>
        /// 图片热点集合
        /// </summary>
        public HotSpotCollection HotSpots { get; set; }

        /// <summary>
        /// 图片相关信息集合
        /// </summary>
        public MessageCollection Messages { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        protected BingImage()
        {
            HotSpots = new HotSpotCollection();
            Messages = new MessageCollection();
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 创建一个BingImage
        /// </summary>
        /// <param name="imageNode">XmlNode</param>
        /// <returns></returns>
        internal static BingImage CreateBingImage(XmlNode imageNode)
        {
            BingImage bingImg = new BingImage();

            // 获取图片的基本属性
            bingImg.StartDate = DateTime.ParseExact(imageNode["fullstartdate"].InnerText, "yyyyMMddHHmm", null);
            bingImg.EndDate = DateTime.ParseExact(imageNode["enddate"].InnerText, "yyyyMMdd", null);
            bingImg.Url = BING_URL + "/" + imageNode["url"].InnerText.Trim('/');
            bingImg.Copyright = imageNode["copyright"].InnerText;
            bingImg.Drk = Convert.ToInt32(imageNode["drk"].InnerText);
            bingImg.Top = Convert.ToInt32(imageNode["top"].InnerText);
            bingImg.Bot = Convert.ToInt32(imageNode["bot"].InnerText);

            // 获取图片热点信息
            XmlNodeList hotSportNodes = imageNode.SelectNodes("hotspots/hotspot");
            foreach (XmlNode item in hotSportNodes)
            {
                HotSpot hotSpot = HotSpot.CreateHotSpot(item);
                bingImg.HotSpots.Add(hotSpot);
            }

            // 获取图片相关信息
            XmlNodeList msgNodes = imageNode.SelectNodes("messages/message");
            foreach (XmlNode item in msgNodes)
            {
                Message msg = Message.CreateMessage(item);
                bingImg.Messages.Add(msg);
            }

            return bingImg;
        }

        /// <summary>
        /// 获取Bing图片
        /// </summary>
        /// <param name="index">几天以前（默认为0，表示当天的图片，1就表示1天以前的图片）</param>
        /// <returns></returns>
        public static BingImage GetBingImage(int index)
        {
            var imgs = BingImageCollection.GetBingImages(index, 1);
            if (imgs != null && imgs.Count > 0)
            {
                return imgs[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取当天的Bing图片
        /// </summary>
        /// <returns></returns>
        public static BingImage GetBingImageOfCurrentDay()
        {
            return GetBingImage(0);
        }
        #endregion

        #region 公共方法
        private Image _image;
        /// <summary>
        /// 转为图片对象（保持原宽高）
        /// </summary>
        /// <returns></returns>
        public Image ToImage()
        {
            if (_image == null)
            {
                WebRequest request = WebRequest.Create(this.Url);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                _image = Image.FromStream(stream);
                stream.Close();
            }
            return _image;
        }

        /// <summary>
        /// 转为图片对象，指定宽高
        /// </summary>
        /// <param name="width">宽（小于0表示保持默认宽度）</param>
        /// <param name="height">高（小于0表示保持默认高度）</param>
        /// <returns></returns>
        public Image ToImage(int width, int height)
        {
            Image orgImg = ToImage();
            Image newImg = new System.Drawing.Bitmap(orgImg, 
                width > 0 ? width : orgImg.Width, 
                height > 0 ? height : orgImg.Height);
            return newImg;
        }

        /// <summary>
        /// 转为图片对象，指定宽，高按照比例进行缩放
        /// </summary>
        /// <param name="width">宽</param>
        /// <returns></returns>
        public Image ToImageAutoHeight(int width)
        {
            Image orgImg = ToImage();

            int height = orgImg.Height;
            if (width <= 0 || width == orgImg.Width)
            {
                width = orgImg.Width;
            }
            else
            {
                height = (int)((((double)width) / orgImg.Width) * orgImg.Height + 0.5);
            }

            Image newImg = new System.Drawing.Bitmap(orgImg,width,height);
            return newImg;
        }

        /// <summary>
        /// 转为图片对象，指定高，宽按照比例进行缩放
        /// </summary>
        /// <param name="height">高</param>
        /// <returns></returns>
        public Image ToImageAutoWidth(int height)
        {
            Image orgImg = ToImage();

            int width = orgImg.Width;
            if (height <= 0 || height == orgImg.Height)
            {
                height = orgImg.Height;
            }
            else
            {
                width = (int)((((double)height) / orgImg.Height) * orgImg.Width + 0.5);
            }

            Image newImg = new System.Drawing.Bitmap(orgImg, width, height);
            return newImg;
        }

        #endregion
    }
}