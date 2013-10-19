using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.ThirdpartyApi.Google
{
    /// <summary>
    /// 当前天气情况
    /// </summary>
    public class CurrentConditions
    {
        /// <summary>
        /// 天气图标的根路径
        /// </summary>
        const string ICON_ROOT_URL = "http://www.google.com/";

        /// <summary>
        /// 气候情况（如“晴”，“阴”）
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 华氏°
        /// </summary>
        public int Temp_f { get; set; }

        /// <summary>
        /// 摄氏°
        /// </summary>
        public int Temp_c { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public string Humidity { get; set; }

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
        /// 风
        /// </summary>
        public string WindCondition { get; set; }
    }
}
