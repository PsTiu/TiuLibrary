using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication
{
    public enum MyEnum
    {
        [Tiu.Attributes.EnumDescription("枚举1", "red")]
        Enum1 = 1,
        [Tiu.Attributes.EnumDescription("枚举2", "blue")]
        Enum2 = 2
    }
}
