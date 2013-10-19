using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiu.Attributes;

namespace Tiu.Extent
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="theObj">扩展对象，枚举</param>
        /// <returns></returns>
        public static EnumDescriptionAttribute GetDescription(this Enum theObj)
        {
            return Tiu.Tools.EnumTool.GetEnumDescription(theObj);
        }
    }
}
