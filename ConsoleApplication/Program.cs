using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Tiu.Tools;
using Tiu.Attributes;


namespace ConsoleApplication
{
    class Program
    {

        public static string AccConStr
        {
            get
            {
                return "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + System.Environment.CurrentDirectory + @"\Database.mdb";
            }
        }

        public static string SqlServerConStr
        {
            get
            {
                return "Data Source=.;Initial Catalog=TiuTest;pooling=true;Persist Security Info=True;User ID=sa;Password=11166.tiu";
            }
        }


        static void Main(string[] args)
        {
            TryAction(Test013);
            
            Console.ReadKey();
        }

        private static void Test013()
        {
            var requestUriString = "ftp://10.20.126.150/ftp/IssueTest";
            Tiu.Net.FTPClient fc = new Tiu.Net.FTPClient(requestUriString);
            var re = fc.GetDirectoriesList("client/update_jxex/");
            Console.WriteLine(re.Count);
            foreach (var item in re)
            {
                Console.WriteLine(item);
            }
            
        }

        private static void Test012()
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = "Data Source=10.20.126.231;Initial Catalog=NewRequirementDB;User Id=sa;Password=issue.2008;";
            
            var comd = con.CreateCommand();
            comd.CommandType = System.Data.CommandType.Text;
            comd.CommandText = "select * from Category order by CategoryId desc for xml raw('Cate'),root('Cates')";

            con.Open();
            var xmlReader = comd.ExecuteXmlReader();            

            xmlReader.Read();
            Console.Write(xmlReader.ReadInnerXml());
            xmlReader.Close();
            
            con.Close();

            Console.ReadKey();

            
        }

        private static void Test011()
        {
            IList<Models.MO_Test> list = new List<Models.MO_Test>();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Enum");
            dt.Columns.Add("ID");
            dt.Columns.Add("Remark");
            dt.Columns.Add("Text");
            dt.Columns.Add("Time");
            for (int i = 0; i < 10; i++)
            {
                var m = new Models.MO_Test()
                {
                    Enum = Models.MO_Test.TestType.Type2,
                    ID = i,
                    Remark = "Remark" + i,
                    Text = "Text" + i,
                    Time = DateTime.Now
                };
                list.Add(m);

                var r = dt.NewRow();
                r["Enum"] = m.Enum.GetHashCode();
                r["ID"] = m.ID;
                r["Remark"] = m.Remark;
                r["Text"] = m.Text;
                r["Time"] = m.Time;
                dt.Rows.Add(r);
            }
            Tiu.EasyUI.IEasyUiJsonData jsonData1 = new Tiu.EasyUI.EasyUiDataGrid<Models.MO_Test>(list);
            Tiu.EasyUI.IEasyUiJsonData jsonData2 = new Tiu.EasyUI.EasyUiDataGrid(dt);
            Console.WriteLine(jsonData1.ToJson());
            Console.WriteLine("");
            Console.WriteLine(jsonData2.ToJson());
        }

        private static void Test010()
        {
            Tiu.DBUtility.DBHelper db;
            db = Tiu.DBUtility.DBHelperCreater.CreateDBHelper(Tiu.DBUtility.HelperType.Access, AccConStr);
            //db = Tiu.DBUtility.DBHelperCreater.CreateDBHelper(Tiu.DBUtility.HelperType.SqlServer, SqlServerConStr);
            
            Models.MO_Test test = new Models.MO_Test()
            {
                ID=1,
                Enum = Models.MO_Test.TestType.Type3,
                Remark = "123",
                Text = "321",
                Time = DateTime.Now
            };
            var re = db.UpdateModelToDB(test);
            db.DeleteByID<Models.MO_Test>(test);
            Console.WriteLine(re);
        }

        private static void Test009()
        {
            Tiu.DBUtility.DBHelper db;
            //db = Tiu.DBUtility.DBHelperCreater.CreateDBHelper(Tiu.DBUtility.HelperType.Access, AccConStr);
            db = Tiu.DBUtility.DBHelperCreater.CreateDBHelper(Tiu.DBUtility.HelperType.SqlServer, SqlServerConStr);
            
            Models.MO_Test test = new Models.MO_Test()
            {
                Enum = Models.MO_Test.TestType.Type2,
                Remark = null,
                Text = "二〇一一年一月十四日 11:05:30",
                Time = DateTime.Now
            };
            var re = db.AddModelToDB(test);
            Console.WriteLine(re);
        }

        private static void Test008()
        {
            Tiu.Net.WebRequestHelper requestHelper = new Tiu.Net.WebRequestHelper();
            var re = requestHelper.RequestGetString("http://www.youguanbumen.net");
            Console.WriteLine(re);
        }

        private static void Test007()
        {
            string str = "as123cs456as789cs";
            var re = Tiu.Tools.StringTool.GetMiddleStrings(str, "as", "cs");
            foreach (var item in re)
            {
                Console.WriteLine(item);
            }
        }

        private static void Test006()
        {
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + System.Environment.CurrentDirectory + @"\Database.mdb";
            Tiu.DBUtility.DBHelper dbHelper = Tiu.DBUtility.DBHelperCreater.CreateDBHelper(Tiu.DBUtility.HelperType.Access, conStr);

            // 分页查询-Table
            Tiu.Data.PagerTable pt = new Tiu.Data.PagerTable()
            {
                PagerTableName = "tb_test",
                PageNum = 1,
                PageSize = 3
            };
            dbHelper.FilePagerTable(pt);
            int allCount = pt.RecordCount;

            // 分页查询-List
            Tiu.Data.PagerList<Models.MO_Test> pl = new Tiu.Data.PagerList<Models.MO_Test>()
            {
                PagerTableName = "tb_test",
                PageNum = 1,
                PageSize = 3
            };
            dbHelper.FilePagerList<Models.MO_Test>(pl);
            int countAll = pl.RecordCount;
        }

        private static void Test005()
        {
            MyModel mm = new MyModel() { ID = 1 };
            string sqlStr = "select {0} from {1} where {2}";
            string tableName = AttributesTool.GetAttributeInClass<MyModel, ModelTableAttribute>().TableName;
            string selectCols = AttributesTool.GetAttributeInProperty<MyModel, ModelColumnAttribute>("Name").ColumnName;
            string where = AttributesTool.GetAttributeInProperty<MyModel, ModelColumnAttribute>("ID").ColumnName + "=" + mm.ID;
            sqlStr = string.Format(sqlStr, tableName, selectCols, where);
            Console.WriteLine(sqlStr);
        }

        private static void Test004()
        {
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + System.Environment.CurrentDirectory + @"\Database.mdb";
            Tiu.DBUtility.DBHelper dbHelper = Tiu.DBUtility.DBHelperCreater.CreateDBHelper(Tiu.DBUtility.HelperType.Access, conStr);

            var list = dbHelper.SelectListFromDB<Models.MO_Test>();
            foreach (var item in list)
            {
                Console.WriteLine(item.ID + " : " + item.Time);
            }
        }

        private static void Test003()
        {
            Models.MO_Test test = new Models.MO_Test()
            {
                ID = 1
            };

            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + System.Environment.CurrentDirectory + @"\Database.mdb";
            Tiu.DBUtility.DBHelper dbHelper = Tiu.DBUtility.DBHelperCreater.CreateDBHelper(Tiu.DBUtility.HelperType.Access, conStr);
            var re=  dbHelper.FillModelFromDB(test);

            if (re == false)
            {
                Console.WriteLine(re);
            }
            else
            {
                Console.WriteLine(test.Time);
            }
        }

        private static void Test002()
        {
            DateTime dtn = DateTime.Now;
            Models.MO_Test test = new Models.MO_Test()
            {
                ID=1,
                Remark = "' Remark_" + dtn.ToString()+" '",
                Enum = Models.MO_Test.TestType.Type2,
                Text = "Test_" + dtn.ToString(),
                Time = dtn
            };

            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + System.Environment.CurrentDirectory+@"\Database.mdb";
            Tiu.DBUtility.DBHelper dbHelper = Tiu.DBUtility.DBHelperCreater.CreateDBHelper(Tiu.DBUtility.HelperType.Access, conStr);
            int re = dbHelper.AddModelToDB(test);
            Console.WriteLine(re);
        }

        private static void Test001()
        {

            MyModel mm = new MyModel();
            mm.ID = 1;
            mm.CreateTime = DateTime.Now;
            mm.Name = "serafin'tiu";
            mm.ME = MyEnum.Enum2;

            Console.WriteLine(mm.GetTableAttribute().TableName);
            var dic = mm.GetColumnsInfo();
            foreach (var item in dic)
            {
                if (item.Value.GetType().BaseType != typeof(Enum))
                {
                    Console.WriteLine(item.Key.ColumnName + "  " + item.Value);
                }
                else
                {
                    Console.WriteLine(item.Key.ColumnName + "  " + item.Value.GetHashCode());
                }
            }

            //Console.WriteLine(mm.GetSelectAllSql());
            //Console.WriteLine("\r\n-------------------------------------------------------------------------------\r\n");
            //Console.WriteLine(mm.GetInsertSql());
            //Console.WriteLine("\r\n-------------------------------------------------------------------------------\r\n");
        }

        private static void TryAction(Action act)
        {
            try
            {
                act();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error! "+ex.Message);
            }
        }


    }

}
