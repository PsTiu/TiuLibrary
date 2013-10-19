using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.Web
{
    /// <summary>
    /// AppSettings帮助类
    /// </summary>
    public class AppSettingsHelper
    {
        /// <summary>
        /// 获取AppSetting的值，返回字符串
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">为空时返回的值</param>
        /// <returns></returns>
        public string GetAppSettingAsString(string key,string defaultValue)
        {
            if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
            {
                return System.Configuration.ConfigurationManager.AppSettings[key];
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取AppSetting的值，返回字符串
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public string GetAppSettingAsString(string key)
        {
            return GetAppSettingAsString(key, string.Empty);
        }

        /// <summary>
        /// 获取AppSetting的值，返回整型
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">为空时返回的值</param>
        /// <returns></returns>
        public int GetAppSettingAsInt(string key, int defaultValue)
        {
            if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
            {
                int re;
                if (int.TryParse(GetAppSettingAsString(key,null), out re))
                {
                    return re;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// 获取AppSetting的值，返回整型
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public int GetAppSettingAsInt(string key)
        {
            return GetAppSettingAsInt(key, 0);
        }
    }
}
