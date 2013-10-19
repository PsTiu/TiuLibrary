using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Models
{
    [Tiu.Attributes.ModelTable("tb_test")]
    public class MO_Test:Tiu.Data.DbModel
    {
        public enum TestType
        {
            Type1 = 1,
            Type2 = 2,
            Type3 = 3
        }

        [Tiu.EasyUI.Attributes.DataGridColumn("id")]
        [Tiu.Attributes.ModelColumn("id",true)]
        public int ID { get; set; }

        [Tiu.EasyUI.Attributes.DataGridColumn("parm_text")]
        [Tiu.Attributes.ModelColumn("parm_text")]
        public string Text { get; set; }

        [Tiu.Attributes.ModelColumn("parm_remark")]
        public string Remark { get; set; }

        [Tiu.EasyUI.Attributes.DataGridColumn("parm_time")]
        [Tiu.Attributes.ModelColumn("parm_time")]
        public DateTime Time { get; set; }

        [Tiu.EasyUI.Attributes.DataGridColumn("parm_enum",true)]
        [Tiu.Attributes.ModelColumn("parm_enum")]
        public TestType Enum { get; set; }
    }
}
