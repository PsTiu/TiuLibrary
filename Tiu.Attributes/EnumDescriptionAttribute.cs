using System;
using System.Collections.Generic;
using System.Text;

namespace Tiu.Attributes
{
    /// <summary>
    /// 枚举描述信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 枚举的中文名称
        /// </summary>
        private string _showInChinese;
        /// <summary>
        /// 枚举的中文名称
        /// </summary>
        public string ShowInChinese
        {
            get { return this._showInChinese; }
            set { this._showInChinese = value; }
        }

        /// <summary>
        /// 枚举要显示成的Web颜色（eg：#D3D3D3）
        /// </summary>
        private string _webColor;
        /// <summary>
        /// 枚举要显示成的Web颜色（eg：#D3D3D3）
        /// </summary>
        public string WebColor
        {
            get { return this._webColor; }
            set { this._webColor = value; }
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="showInChinese">枚举的中文名称</param>
        /// <param name="webColor">枚举要显示成的Web颜色（eg：#D3D3D3）</param>
        public EnumDescriptionAttribute(string showInChinese, string webColor)
        {
            this._showInChinese = showInChinese;
            this._webColor = webColor;
        }
    }
}
