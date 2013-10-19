using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.ThirdpartyApi.Google
{
    /// <summary>
    /// 预报信息
    /// </summary>
    public class ForecastInformation
    {
        /// <summary>
        /// 天气信息所属城市
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// 城市代码
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude_e6 { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude_e6 { get; set; }

        /// <summary>
        /// 预报时间
        /// </summary>
        public DateTime ForecastDate { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UnitSystem { get; set; }
    }
}
