using System;
using System.Collections.Generic;
using System.Text;
using Tiu.Data;

namespace ConsoleApplication
{
    [Tiu.Attributes.ModelTable("MyTable")]
    public class MyModel : DbModel
    {
        [Tiu.Attributes.ModelColumn("MyTable_ID", true)]
        public int ID { get; set; }

        [Tiu.Attributes.ModelColumn("MyTable_Name")]
        public string Name { get; set; }

        [Tiu.Attributes.ModelColumn("MyTable_CreateTime")]
        public DateTime CreateTime { get; set; }

        [Tiu.Attributes.ModelColumn("MyTable_ME")]
        public MyEnum ME { get; set; }
    }
}
