using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;
using System.Drawing;

namespace Tiu.ThirdpartyApi.Bing
{
    /// <summary>
    /// Bing图片信息集合
    /// </summary>
    public class BingImageCollection : List<BingImage>
    {
        #region 常量
        /// <summary>
        /// 请求地址的格式化字符串
        /// </summary>
        private const string REQUEST_URL_FORMAT_STR = "http://cn.bing.com/HPImageArchive.aspx?idx={0}&n={1}";
        /// <summary>
        /// 请求参数idx的最小值
        /// </summary>
        private const int MIN_INDEX = 0;
        /// <summary>
        /// 请求参数idx的最大值
        /// </summary>
        private const int MAX_INDEX = 24;
        /// <summary>
        /// 请求参数n的最小值
        /// </summary>
        private const int MIN_NUMBER = 0;
        /// <summary>
        /// 请求参数n的最大值
        /// </summary>
        private const int MAX_NUMBER = 8;
        #endregion

        #region 属性
        /// <summary>
        /// 图片加载中的提示信息
        /// </summary>
        public string LoadingMessage { get; set; }
        /// <summary>
        /// 上一张图片的链接文本
        /// </summary>
        public string PreviousImageText { get; set; }
        /// <summary>
        /// 下一张图片的链接文本
        /// </summary>
        public string NextImageText { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取Bing图片（集合）
        /// </summary>
        /// <param name="index">几天以前（默认为0，表示当天的图片，1就表示1天以前的图片）</param>
        /// <param name="number"> 图片数量，最大为8，图片返回顺序为由新到旧（默认为1）</param>
        /// <returns></returns>
        public static BingImageCollection GetBingImages(int index, int number)
        {
            #region 对传入参数进行修正
            if (index < MIN_INDEX) index = MIN_INDEX;
            else if (index > MAX_INDEX) index = MAX_INDEX;

            if (number < MIN_INDEX) number = MIN_INDEX;
            else if (number > MAX_NUMBER) number = MAX_NUMBER;
            #endregion

            XmlDocument xmlDoc = GetXmlDoc(index, number);
            BingImageCollection imgs = Xml2BingImgs(xmlDoc);

            return imgs;
        }

        /// <summary>
        /// 获取当天开始Bing图片（集合）
        /// </summary>
        /// <param name="number"> 图片数量，最大为8，图片返回顺序为由新到旧（默认为1）</param>
        /// <returns></returns>
        public static BingImageCollection GetBingImages(int number)
        {
            return GetBingImages(MIN_INDEX, number);
        }


        /// <summary>
        /// 转为图片对象集合
        /// </summary>
        /// <returns></returns>
        public ICollection<Image> ToImages()
        {
            ICollection<Image> _images = new List<Image>();

            ForEach((img) =>
            {
                _images.Add(img.ToImage());
            });

            return _images;
        }

        /// <summary>
        /// 转为图片对象集合，指定宽高
        /// </summary>
        /// <param name="width">宽（小于0表示保持默认宽度）</param>
        /// <param name="height">高（小于0表示保持默认高度）</param>
        /// <returns></returns>
        public ICollection<Image> ToImages(int width, int height)
        {
            ICollection<Image> _images = new List<Image>();

            ForEach((img) =>
            {
                _images.Add(img.ToImage(width,height));
            });

            return _images;
        }

        /// <summary>
        /// 转为图片对象集合，指定宽，高按照比例进行缩放
        /// </summary>
        /// <param name="width">宽</param>
        /// <returns></returns>
        public ICollection<Image> ToImagesAutoHeight(int width)
        {
            ICollection<Image> _images = new List<Image>();

            ForEach((img) =>
            {
                _images.Add(img.ToImageAutoHeight(width));
            });

            return _images;
        }

        /// <summary>
        /// 转为图片对象集合，指定高，宽按照比例进行缩放
        /// </summary>
        /// <param name="height">高</param>
        /// <returns></returns>
        public ICollection<Image> ToImagesAutoWidth(int height)
        {
            ICollection<Image> _images = new List<Image>();

            ForEach((img) =>
            {
                _images.Add(img.ToImageAutoWidth(height));
            });

            return _images;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// XmlDocument转为BingImageCollection
        /// </summary>
        /// <param name="xmlDoc">XmlDocument对象</param>
        /// <returns></returns>
        private static BingImageCollection Xml2BingImgs(XmlDocument xmlDoc)
        {
            BingImageCollection bic = new BingImageCollection();

            XmlNode rootNode = xmlDoc.GetElementsByTagName("images")[0];

            XmlNode loadMsgNode = rootNode.SelectSingleNode("tooltips/loadMessage/message");
            XmlNode preImgNode = rootNode.SelectSingleNode("tooltips/previousImage/text");
            XmlNode nextImgNode = rootNode.SelectSingleNode("tooltips/nextImage/text");
            bic.LoadingMessage = loadMsgNode != null ? loadMsgNode.InnerText : "";
            bic.PreviousImageText = preImgNode != null ? preImgNode.InnerText : "";
            bic.NextImageText = nextImgNode != null ? nextImgNode.InnerText : "";

            XmlNodeList imgNodeList = rootNode.SelectNodes("image");
            foreach (XmlNode item in imgNodeList)
            {
                BingImage bingImg = BingImage.CreateBingImage(item);
                bic.Add(bingImg);
            }

            return bic;
        }

        /// <summary>
        /// 获取Bing图片的XML信息流
        /// </summary>
        /// <param name="index">几天以前（默认为0，表示当天的图片，1就表示1天以前的图片）</param>
        /// <param name="number"> 图片数量，最大为8，图片返回顺序为由新到旧（默认为1）</param>
        /// <returns></returns>
        private static Stream GetXmlStream(int index, int number)
        {
            // 请求地址
            string requestUriString = string.Format(REQUEST_URL_FORMAT_STR, index, number);
            WebRequest request = WebRequest.Create(requestUriString);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            return responseStream;
        }

        /// <summary>
        /// 获取Bing图片的XML文档对象
        /// </summary>
        /// <param name="index">几天以前（默认为0，表示当天的图片，1就表示1天以前的图片）</param>
        /// <param name="number"> 图片数量，最大为8，图片返回顺序为由新到旧（默认为1）</param>
        /// <returns></returns>
        private static XmlDocument GetXmlDoc(int index, int number)
        {
            XmlDocument xmlDoc = new XmlDocument();

            // 获取XML信息流
            xmlDoc.Load(GetXmlStream(index, number));

            return xmlDoc;
        }
        #endregion
    }
}