using System;
using System.Collections.Generic;
using System.Web;

namespace Tiu.ThirdpartyApi.Bing
{
    /// <summary>
    /// 图片热点
    /// </summary>
    public class HotSpot
    {
        #region 属性
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public string Query { get; set; }
        /// <summary>
        /// 热点的X坐标（相对于图片左上角）
        /// </summary>
        public int LocX { get; set; }
        /// <summary>
        /// 热点的Y坐标（相对于图片左上角）
        /// </summary>
        public int LocY { get; set; }
        #endregion

        #region 构造方法
        protected HotSpot()
        { 
        
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 创建HotSpot对象
        /// </summary>
        /// <param name="hopSpotNode">XmlNode</param>
        /// <returns></returns>
        internal static HotSpot CreateHotSpot(System.Xml.XmlNode hopSpotNode)
        {
            HotSpot hopSport = new HotSpot();

            hopSport.Desc = hopSpotNode["desc"].InnerText;
            hopSport.Link = hopSpotNode["link"].InnerText;
            hopSport.Query = hopSpotNode["query"].InnerText;
            hopSport.LocX = Convert.ToInt32(hopSpotNode["LocX"].InnerText);
            hopSport.LocY = Convert.ToInt32(hopSpotNode["LocY"].InnerText);

            return hopSport;
        }
        #endregion
    }
}