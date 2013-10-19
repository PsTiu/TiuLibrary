using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.ThirdpartyApi
{
    /// <summary>
    /// Gravatar头像的API
    /// </summary>
    public class Gravatar
    {
        /// <summary>
        /// 默认头像
        /// </summary>
        public enum DefaultImage
        {
            MM, Identicon, Monsterid, Wavatar, Retro
        }

        /// <summary>
        /// 头像分级
        /// </summary>
        public enum Rating
        {
            G, PG, R, X
        }

        private const string GRAVATAR_URL = "http://www.gravatar.com/avatar";
        private const string DEFAULT_IMAGE_GUID = "00000000000000000000000000000000";


        #region 获取默认头像地
        /// <summary>
        /// 获取默认头像地址
        /// </summary>
        /// <returns></returns>
        public string GetDefaultAvatarUrl()
        {
            return string.Format("{0}/{1}", GRAVATAR_URL, DEFAULT_IMAGE_GUID);
        }

        /// <summary>
        /// 获取默认头像地址
        /// </summary>
        /// <param name="defaultImage">缺省默认头像</param>
        /// <returns></returns>
        public string GetDefaultAvatarUrl(DefaultImage defaultImage)
        {
            string url = GetDefaultAvatarUrl();
            string d = defaultImage.ToString().ToLower();

            return string.Format("{0}?d={1}", url, d);
        }
        #endregion


        #region 获取GravatarUrl头像地址

        /// <summary>
        /// 获取GravatarUrl头像地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <param name="defaultImgUrl">默认图片地址（为null的话使用系统设置的默认头像地址）</param>
        /// <param name="size">大小（为null表示使用默认大小）</param>
        /// <returns></returns>
        public string GetGravatarUrl(string email, string defaultImgUrl, int? size)
        {
            return GetGravatarUrl(email, defaultImgUrl, size, false, null);
        }

        /// <summary>
        /// 获取GravatarUrl头像地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <param name="defaultImgUrl">默认图片地址（为null的话使用系统设置的默认头像地址）</param>
        /// <returns></returns>
        public string GetGravatarUrl(string email, string defaultImgUrl)
        {
            return GetGravatarUrl(email, defaultImgUrl, null, false, null);
        }

        /// <summary>
        /// 获取GravatarUrl头像地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <param name="size">大小（为null表示使用默认大小）</param>
        /// <returns></returns>
        public string GetGravatarUrl(string email, int? size)
        {
            return GetGravatarUrl(email, DEFAULT_IMAGE_GUID, size, false, null);
        }

        /// <summary>
        /// 获取GravatarUrl头像地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        public string GetGravatarUrl(string email)
        {
            return GetGravatarUrl(email, DEFAULT_IMAGE_GUID, null, false, null);
        }

        /// <summary>
        /// 获取GravatarUrl头像地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <param name="defaultImgUrl">默认图片地址（为null的话使用系统设置的默认头像地址）</param>
        /// <param name="size">大小（为null表示使用默认大小）</param>
        /// <param name="forceDefault">是否强制显示默认头像</param>
        /// <param name="rating">头像分级</param>
        /// <returns></returns>
        public string GetGravatarUrl(string email, string defaultImgUrl, int? size, bool forceDefault, Rating? rating)
        {
            // 对email进行MD5加密
            string emailMd5 = Tiu.Tools.StringTool.ToMD5(email.Trim()).ToLower();
            string url = string.Format("{0}/{1}?temp=0", GRAVATAR_URL, emailMd5);

            if (!string.IsNullOrEmpty(defaultImgUrl))
            {
                url = string.Format("{0}&d={1}", url, System.Web.HttpUtility.UrlEncode(defaultImgUrl));
            }
            if (size != null && size > 0)
            {
                url = string.Format("{0}&s={1}", url, size);
            }
            if (forceDefault == true)
            {
                url = string.Format("{0}&f={1}", url, "y");
            }
            if (rating != null)
            {
                url = string.Format("{0}&r={1}", url, rating.ToString().ToLower());
            }

            return url;
        }

        /// <summary>
        /// 获取GravatarUrl头像地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <param name="defaultImgUrl">默认图片地址（为null的话使用系统设置的默认头像地址）</param>
        /// <param name="size">大小（为null表示使用默认大小）</param>
        /// <param name="forceDefault">是否强制显示默认头像</param>
        /// <param name="rating">头像分级</param>
        /// <returns></returns>
        public string GetGravatarUrl(string email, DefaultImage defaultImg, int? size, bool forceDefault, Rating? rating)
        {
            return GetGravatarUrl(email, defaultImg.ToString().ToLower(), size, forceDefault, rating);
        }

        #endregion
    }
}
