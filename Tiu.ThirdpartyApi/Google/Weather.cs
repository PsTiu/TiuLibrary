using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

namespace Tiu.ThirdpartyApi.Google
{
    /// <summary>
    /// 天气信息
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        const string REQUEST_URL_FORMAT = "http://www.google.com/ig/api?weather={0}&hl={1}";


        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TabId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MobileRow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MobileZipped { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Row { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// 预报信息
        /// </summary>
        public ForecastInformation Forecast { get; set; }

        /// <summary>
        /// 当天天气情况
        /// </summary>
        public CurrentConditions Current { get; set; }

        /// <summary>
        /// 接下来几天的预报
        /// </summary>
        public ForecastConditionsCollection ForecastConditions { get; set; }

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        private Weather()
        {
            Forecast = new ForecastInformation();
            Current = new CurrentConditions();
            ForecastConditions = new ForecastConditionsCollection();
        }

        /// <summary>
        /// 获取天气情况
        /// </summary>
        /// <param name="place">地方名（拼音或者中文）</param>
        /// <param name="hl">语言（如："zh-cn"）</param>
        /// <returns></returns>
        public static Weather GetWeather(string place)
        {
            return GetWeather(place, null);
        }

        /// <summary>
        /// 获取天气情况
        /// </summary>
        /// <param name="place">地方名（拼音或者中文）</param>
        /// <param name="hl">语言（如："zh-cn"）</param>
        /// <returns></returns>
        public static Weather GetWeather(string place, string hl)
        {
            Weather weather = new Weather();

            if (place == null)
            {
                throw new Exception("地区不能为空");
            }
            if (string.IsNullOrEmpty(hl))
            {
                hl = "zh-cn";
            }

            XmlDocument xmlDoc = GetXmlDoc(place, hl);
            weather = Xml2Weather(xmlDoc);

            place = System.Web.HttpUtility.UrlEncode(place);

            return weather;
        }

        /// <summary>
        /// 把XML转成天气实例
        /// </summary>
        /// <param name="xmlDoc">XmlDocument</param>
        /// <returns></returns>
        private static Weather Xml2Weather(XmlDocument xmlDoc)
        {
            Weather weather = new Weather();

            XmlNode node_weather = xmlDoc.SelectSingleNode("xml_api_reply/weather");

            weather.ModuleId = node_weather.Attributes["module_id"].Value;
            weather.TabId = node_weather.Attributes["tab_id"].Value;
            weather.MobileRow = node_weather.Attributes["mobile_row"].Value;
            weather.MobileZipped = node_weather.Attributes["mobile_zipped"].Value;
            weather.Row = node_weather.Attributes["row"].Value;
            weather.Section = node_weather.Attributes["section"].Value;

            weather.Forecast = GetForecastInformation(node_weather);
            weather.Current = GetCurrentConditions(node_weather);
            weather.ForecastConditions = GetForecastConditions(node_weather);
            
            return weather;
        }

        /// <summary>
        /// 从XmlNode中解析出CurrentConditions对象并返回该对象
        /// </summary>
        /// <param name="node_weather"></param>
        /// <returns></returns>
        private static ForecastConditionsCollection GetForecastConditions(XmlNode node_weather)
        {
            ForecastConditionsCollection forcastConditions = new ForecastConditionsCollection();

            XmlNodeList node_currentConditions = node_weather.SelectNodes("forecast_conditions");
            if (node_currentConditions != null && node_currentConditions.Count > 0)
            {
                foreach (XmlNode item in node_currentConditions)
                {
                    forcastConditions.Add(new ForecastConditionscs()
                    {
                        DayOfWeek = item.SelectSingleNode("day_of_week").Attributes["data"].Value,
                        Low = Convert.ToInt32(item.SelectSingleNode("low").Attributes["data"].Value),
                        High = Convert.ToInt32(item.SelectSingleNode("high").Attributes["data"].Value),
                        Icon = item.SelectSingleNode("icon").Attributes["data"].Value,
                        Condition = item.SelectSingleNode("condition").Attributes["data"].Value
                    });
                }
            }

            return forcastConditions;
        }

        /// <summary>
        /// 从XmlNode中解析出CurrentConditions对象并返回该对象
        /// </summary>
        /// <param name="node_weather"></param>
        /// <returns></returns>
        private static CurrentConditions GetCurrentConditions(XmlNode node_weather)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            XmlNode node_currentConditions = node_weather.SelectSingleNode("current_conditions");

            currentConditions.Condition = node_currentConditions.SelectSingleNode("condition").Attributes["data"].Value;
            currentConditions.Temp_f = Convert.ToInt32(node_currentConditions.SelectSingleNode("temp_f").Attributes["data"].Value);
            currentConditions.Temp_c = Convert.ToInt32(node_currentConditions.SelectSingleNode("temp_c").Attributes["data"].Value);
            currentConditions.Humidity = node_currentConditions.SelectSingleNode("humidity").Attributes["data"].Value;
            currentConditions.Icon = node_currentConditions.SelectSingleNode("icon").Attributes["data"].Value;
            currentConditions.WindCondition = node_currentConditions.SelectSingleNode("wind_condition").Attributes["data"].Value;
            
            return currentConditions;
        }

        /// <summary>
        /// 从XmlNode中解析出ForecastInformation对象并返回该对象
        /// </summary>
        /// <param name="node_weather"></param>
        /// <returns></returns>
        private static ForecastInformation GetForecastInformation(XmlNode node_weather)
        {
            ForecastInformation forecast = new ForecastInformation();

            XmlNode node_forcast = node_weather.SelectSingleNode("forecast_information");

            forecast.City = node_forcast.SelectSingleNode("city").Attributes["data"].Value;
            forecast.PostalCode = node_forcast.SelectSingleNode("postal_code").Attributes["data"].Value;
            forecast.Latitude_e6 = node_forcast.SelectSingleNode("latitude_e6").Attributes["data"].Value;
            forecast.Longitude_e6 = node_forcast.SelectSingleNode("longitude_e6").Attributes["data"].Value;
            forecast.ForecastDate = Convert.ToDateTime(node_forcast.SelectSingleNode("forecast_date").Attributes["data"].Value);
            forecast.CurrentDateTime = Convert.ToDateTime(node_forcast.SelectSingleNode("current_date_time").Attributes["data"].Value);
            forecast.UnitSystem = node_forcast.SelectSingleNode("unit_system").Attributes["data"].Value;

            return forecast;
        }

        /// <summary>
        /// 获取Xml
        /// </summary>
        /// <param name="place">地方名（拼音或者中文）</param>
        /// <param name="hl">语言（如："zh-cn"）</param>
        /// <returns></returns>
        private static XmlDocument GetXmlDoc(string place, string hl)
        {
            XmlDocument xmlDoc = new XmlDocument();

            // 获取XML信息流
            Stream stream = GetXmlStream(place, hl);

            StreamReader readStream = new StreamReader(stream, Encoding.Default);
            string str = readStream.ReadToEnd();


            xmlDoc.LoadXml(str);
            return xmlDoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="place">地方名（拼音或者中文）</param>
        /// <param name="hl">语言（如："zh-cn"）</param>
        /// <returns></returns>
        private static Stream GetXmlStream(string place, string hl)
        {
            // 请求地址
            string requestUriString = string.Format(REQUEST_URL_FORMAT, place, hl);
            WebRequest request = WebRequest.Create(requestUriString);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            return responseStream;
        }
    }
}
