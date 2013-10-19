using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.ThirdpartyApi.Google
{
    /// <summary>
    /// 天气预报（一天的天气情况）
    /// </summary>
    public class ForecastConditionscs
    {
        /// <summary>
        /// 天气图标的根路径
        /// </summary>
        const string ICON_ROOT_URL = "http://www.google.com/";

        /// <summary>
        /// 星期
        /// </summary>
        public string DayOfWeek { get; set; }

        /// <summary>
        /// 最低温度
        /// </summary>
        public int Low { get; set; }

        /// <summary>
        /// 最高温度
        /// </summary>
        public int High { get; set; }

        /// <summary>
        /// 天气图标（相对路径）
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 天气图标（全路径）
        /// </summary>
        public string IconFullRule
        {
            get
            {
                return string.Format(ICON_ROOT_URL + Icon);
            }
        }

        /// <summary>
        /// 天气情况
        /// </summary>
        public string Condition { get; set; }
    }
}
