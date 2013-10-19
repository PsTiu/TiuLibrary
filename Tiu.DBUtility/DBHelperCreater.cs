using System;
using System.Collections.Generic;

using System.Text;

namespace Tiu.DBUtility
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum HelperType
    { 
        /// <summary>
        /// SqlServer
        /// </summary>
        SqlServer,
        /// <summary>
        /// Access
        /// </summary>
        Access,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,
        /// <summary>
        /// MySql
        /// </summary>
        MySql,
    }

    /// <summary>
    /// 数据库访问类创建者
    /// </summary>
    public static class DBHelperCreater
    {
        /// <summary>
        /// 获取一个数据库帮助类
        /// </summary>
        /// <param name="helperType">数据库类型</param>
        /// <param name="connectionStr">链接字符串</param>
        /// <returns></returns>
        public static DBHelper CreateDBHelper(HelperType helperType, string connectionStr)
        {
            DBHelper dbHelper;

            switch (helperType)
            {
                case HelperType.SqlServer:
                    {
                        dbHelper = new SqlServerHelper(connectionStr);
                        break;
                    }
                case HelperType.Access:
                    {
                        dbHelper = new AccessHelper(connectionStr);
                        break;
                    }
                default:
                    {
                        dbHelper = null;
                        break;
                    }
            }
            return dbHelper;
        }
    }
}
