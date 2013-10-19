using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Tiu.Tools
{
    /// <summary>
    /// 字符串工具
    /// </summary>
    public static class StringTool
    {
        #region 字符串替换

        /// <summary>
        /// 把字符串中的符号转为Html格式的字符
        /// </summary>
        /// <param name="strSrc">源字符串</param>
        /// <returns></returns>
        public static string ReplaceStrHtmlChar(string strSrc)
        {
            string s = strSrc;

            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("#", "&#35;");
                //s = s.Replace(";", "&#59;");

                s = s.Replace("<", "&#60;");
                s = s.Replace(">", "&#62;");
                s = s.Replace("/", "&#47;");

                s = s.Replace("\r\n", "<br>");
                s = s.Replace("\r", "<br>");
                s = s.Replace("\n", "<br>");

                s = s.Replace(" ", "&nbsp;");
                s = s.Replace("!", "&#33;");
                s = s.Replace("\"", "&#34;");
                s = s.Replace("$", "&#36;");
                s = s.Replace("%", "&#37;");

                s = s.Replace("`", "&#39;");
                s = s.Replace("(", "&#40;");
                s = s.Replace(")", "&#41;");
                s = s.Replace("*", "&#42;");
                s = s.Replace("+", "&#43;");
                s = s.Replace(",", "&#44;");
                s = s.Replace("-", "&#45;");
                s = s.Replace(".", "&#46;");
                s = s.Replace(":", "&#58;");

                s = s.Replace("=", "&#61;");
                s = s.Replace("?", "&#63;");
                s = s.Replace("@", "&#64;");
                s = s.Replace("[", "&#91;");
                s = s.Replace("\\", "&#92;");
                s = s.Replace("]", "&#93;");
                s = s.Replace("^", "&#94;");
                s = s.Replace("_", "&#95;");
                s = s.Replace("'", "&#96;");
                s = s.Replace("{", "&#123;");
                s = s.Replace("|", "&#124;");
                s = s.Replace("}", "&#125;");
                s = s.Replace("~", "&#126;");
            }

            return s;
        }

        /// <summary>
        /// 替换尖括号（左尖括号="&lt;" 右尖括号="&gt;"）
        /// </summary>
        /// <param name="strSrc">源字符串</param>
        /// <returns></returns>
        public static string ReplaceAngleBrackets(string strSrc)
        { 
             string s = strSrc;

             if (!string.IsNullOrEmpty(s))
             {
                 s = s.Replace("<", "&lt;");
                 s = s.Replace(">", "&gt;");
             }

             return s;
        }

        /// <summary>
        /// 过滤字符串中带有的关键字和符号（转为全角）
        /// </summary>
        /// <param name="strSrc">源字符串</param>
        /// <returns></returns>
        public static string FilterSqlChars(string strSrc)
        {
            string s = strSrc;

            //半角括号替换为全角括号
            s = s.Replace("'", "＇");
            s = s.Replace("\"", "“");
            s = s.Replace("|", "｜");
            s = s.Replace(";", "；");
            s = s.Replace("(", "（");
            s = s.Replace(")", "）");

            //去除执行存储过程的命令关键字
            s = s.Replace("exec", "ｅｘｅｃ");
            s = s.Replace("execute", "ｅｘｅｃｕｔｅ");
            s = s.Replace("inster", "ｉｎｓｅｒｔ");
            s = s.Replace("update", "ｕｐｄａｔｅ");
            s = s.Replace("delete", "ｄｅｌｅｔｅ");
            s = s.Replace("select", "ｓｅｌｅｃｔ");
            s = s.Replace("tr", "ｔｒ");

            //去除系统存储过程或扩展存储过程关键字
            s = s.Replace("xp_", "ｘｐ_");
            s = s.Replace("sp_", "ｓｐ_");

            //防止16进制注入
            s = s.Replace("0x", "０ｘ");

            return s;
        }

        #endregion


        #region 字符串加密

        /// <summary>
        /// 返回MD5加密后的字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns></returns>
        public static string ToMD5(string str)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(str));
            string re = BitConverter.ToString(s).Replace("-", "");
            return re;
        }

        #endregion


        #region 字符串查找

        /// <summary>
        /// 在源字符串中查找首个以a字符串开始、以b字符串结尾的字符串
        /// </summary>
        /// <param name="strSrc">源字符串</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="includeAandB">是否返回a字符串和b字符串</param>
        /// <returns></returns>
        public static string GetMiddleString(string strSrc, string a, string b, bool includeAandB)
        {
            var regex = new Regex(@"(?<=("+a+@"))[.\s\S]*?(?=("+b+"))", RegexOptions.Multiline | RegexOptions.Singleline);
            var re = regex.Match(strSrc);

            if (includeAandB)
            {
                return a + re.Value + b;
            }
            else
            {
                return re.Value;
            }
        }

        /// <summary>
        /// 在源字符串中查找首个以a字符串开始、以b字符串结尾的字符串（返回结果不包括a字符串和b字符串）
        /// </summary>
        /// <param name="strSrc">源字符串</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetMiddleString(string strSrc, string a, string b)
        {
            return GetMiddleString(strSrc, a, b, false);
        }

        /// <summary>
        /// 在源字符串中查找以a字符串开始、以b字符串结尾的字符串
        /// </summary>
        /// <param name="strSrc">源字符串</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="includeAandB">是否返回a字符串和b字符串</param>
        /// <returns></returns>
        public static IList<string> GetMiddleStrings(string strSrc, string a, string b, bool includeAandB)
        {
            var regex = new Regex(@"(?<=(" + a + @"))[.\s\S]*?(?=(" + b + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            var matchResult = regex.Matches(strSrc);
            IList<string> re = new List<string>();
            foreach (Match item in matchResult)
            {
                if (includeAandB)
                {
                    re.Add(a + item.Value + b);
                }
                else
                {
                    re.Add(item.Value);
                }
            }
            return re;
        }

        /// <summary>
        /// 在源字符串中查找以a字符串开始、以b字符串结尾的字符串
        /// </summary>
        /// <param name="strSrc">源字符串</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IList<string> GetMiddleStrings(string strSrc, string a, string b)
        {
            return GetMiddleStrings(strSrc, a, b, false);
        }

        #endregion


        #region 字符串生成
        
        /// <summary>
        /// 生成Guid字符串
        /// </summary>
        /// <param name="isUper">字母是否大写</param>
        /// <param name="onlyLetter">是否只生成字母（会生成36位，32为的数字和字母加四个横杠；设置为true的话就没有横杠）</param>
        /// <returns></returns>
        public static string CreateGuid(bool isUper,bool onlyLetter)
        {
            var guid = Guid.NewGuid().ToString();
            guid = isUper ? guid.ToUpper() : guid.ToLower();
            guid = onlyLetter ? guid.Replace("-", "") : guid;
            return guid;
        }

        /// <summary>
        /// 按照当前时间生成十八位字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateDateTimeString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }
        #endregion

        /// <summary>
        /// 截取字符串的前几个字符
        /// </summary>
        /// <param name="srcStr">源字符串</param>
        /// <param name="showCharNumber">截取出来的字符数</param>
        /// <param name="ellipsis">源字符串中超出部分要显示成的文本（如"…"）</param>
        /// <returns></returns>
        public static string CutChars(string srcStr,int showCharNumber, string ellipsis)
        {
            if (srcStr.Length <= showCharNumber)
            {
                return srcStr;
            }
            else
            {
                string re = srcStr.Substring(0, showCharNumber);
                re += ellipsis;
                return re;
            }
        }
    }
}
