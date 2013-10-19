using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Models
{
    [Tiu.Attributes.ModelTable("table1")]
    class Table1
    {
        [Tiu.Attributes.ModelColumn("ID", true)]
        public int ID { get; set; }
        [Tiu.Attributes.ModelColumn("P_Tinyint")]
        public int P_Tinyint { get; set; }
        [Tiu.Attributes.ModelColumn("P_Int")]
        public int P_Int { get; set; }
        [Tiu.Attributes.ModelColumn("P_BigInt")]
        public int P_BigInt { get; set; }
        [Tiu.Attributes.ModelColumn("P_Nchar")]
        public string P_Nchar { get; set; }
        [Tiu.Attributes.ModelColumn("P_Char")]
        public string P_Char { get; set; }
        [Tiu.Attributes.ModelColumn("P_Ncarchar")]
        public string P_Ncarchar { get; set; }
        [Tiu.Attributes.ModelColumn("P_Varchar")]
        public string P_Varchar { get; set; }
        [Tiu.Attributes.ModelColumn("P_DataTime")]
        public DateTime P_DataTime { get; set; }
        [Tiu.Attributes.ModelColumn("P_Decimal")]
        public decimal P_Decimal { get; set; }
    }
}
