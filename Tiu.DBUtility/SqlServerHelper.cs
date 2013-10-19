using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Tiu.Data;
using System.Reflection;
using Tiu.Attributes;

namespace Tiu.DBUtility
{
    /// <summary>
    /// SqlServer数据库访问类
    /// </summary>
    public class SqlServerHelper : DBHelper
    {

        #region 【构造函数】
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionStr">数据库链接字符串</param>
        public SqlServerHelper(string connectionStr)
            : base(connectionStr)
        {
            // 初始化数据库链接对象
            _connection = new SqlConnection(connectionStr);

            // 初始化数据库命令对象
            _command = new SqlCommand();
            _command.Connection = base._connection;

            // 初始化数据适配器对象
            _adapter = new SqlDataAdapter("", connectionStr);
            _adapter.SelectCommand = this._command;
        }
        #endregion


        #region 【override方法】

        #region 创建SQL参数的方法
        /// <summary>
        /// 创建传入的SQL参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public override DbParameter MakeInParam(string paramName, DbType dbType, int size, object value)
        {
            return base.MakeParam<SqlParameter>(paramName, ParameterDirection.Input, dbType, size, value);
        }

        /// <summary>
        /// 创建传出的SQL参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <returns></returns>
        public override DbParameter MakeOutParam(string paramName, DbType dbType, int size)
        {
            return base.MakeParam<SqlParameter>(paramName, ParameterDirection.Output, dbType, size, null);
        }
        #endregion

        #region 执行SQL语句的方法
        /// <summary>
        /// 执行SQL语句，返回DataReader（注意：使用完DataReader之后要调用Close()方法关闭数据库链接）
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public override DbDataReader ExecSqlReader(string sqlStr, IList<DbParameter> parameters)
        {
            return base.ExecuteReader(CommandType.Text, sqlStr, parameters);
        }

        /// <summary>
        /// 执行SQL语句返回DataSet
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="dataSetName">数据集名称</param>
        /// <returns></returns>
        public override DataSet ExecSqlDataSet(string sqlStr, IList<DbParameter> parameters,string dataSetName)
        {
            return base.ExecuteDataSet(CommandType.Text, sqlStr, parameters, dataSetName);       
        }

        /// <summary>
        /// 执行SQL语句返回DataTable
        /// </summary>
        /// <param name="sqlStr">SQL命令</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="tableName">Table名</param>
        /// <returns></returns>
        public override DataTable ExecSqlDataTable(string sqlStr, IList<DbParameter> parameters, string tableName)
        {
            return base.ExecuteDataTable(CommandType.Text, sqlStr, parameters, tableName);
        }

        /// <summary>
        /// 执行SQL语句返回首行首列
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public override object ExecSqlSingle(string sqlStr, IList<DbParameter> parameters)
        {
            return base.ExecuteSingle(CommandType.Text, sqlStr, parameters);
        }

        /// <summary>
        /// 执行SQL语句返回受影响行数
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public override int ExecSql(string sqlStr, IList<DbParameter> parameters)
        {
            return base.ExecuteSql(CommandType.Text, sqlStr, parameters);
        }
        #endregion

        #region 执行存储过程的方法
        /// <summary>
        /// 执行存储过程，返回DataReader（注意：使用完DataReader之后要调用Close()方法关闭数据库链接）
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public override DbDataReader ExecProcReader(string procName, IList<DbParameter> parameters)
        {
            return base.ExecuteReader(CommandType.StoredProcedure, procName, parameters);
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="dataSetName">数据集名称</param>
        /// <returns></returns>
        public override DataSet ExecProcDataSet(string procName, IList<DbParameter> parameters, string dataSetName)
        {
            return base.ExecuteDataSet(CommandType.StoredProcedure, procName, parameters, dataSetName);
        }

        /// <summary>
        /// 执行存储过程返回DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="tableName">Table名</param>
        /// <returns></returns>
        public override DataTable ExecProcDataTable(string procName, IList<DbParameter> parameters, string tableName)
        {
            return base.ExecuteDataTable(CommandType.StoredProcedure, procName, parameters, tableName);
        }

        /// <summary>
        /// 执行存储过程返回首行首列
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public override object ExecProcSingle(string procName, IList<DbParameter> parameters)
        {
            return base.ExecuteSingle(CommandType.StoredProcedure, procName, parameters);
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public override int ExecProc(string procName, IList<DbParameter> parameters)
        {
            return base.ExecuteSql(CommandType.StoredProcedure, procName, parameters);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 填充分页查询PagerTable结果
        /// </summary>
        /// <param name="pagerTable">PagerTable对象</param>
        public override void FilePagerTable(PagerTable pagerTable)
        {
            #region **************************

            //// 修正参数
            //if (pagerTable.PageSize < 1)
            //{
            //    pagerTable.PageSize = 10;
            //}
            //if (pagerTable.PageNum < 1)
            //{
            //    pagerTable.PageNum = 1;
            //}

            //// 获取分页数据
            //string tableName = pagerTable.PagerTableName; // 查询的表名
            //int pageSize = pagerTable.PageSize; // 每页显示的记录数
            //int pageNum = pagerTable.PageNum;     // 显示第几页
            //string orderBy = pagerTable.OrderBy; // 排序字段
            //string orderWay = string.IsNullOrEmpty(pagerTable.OrderBy.Trim())?"":pagerTable.OrderWay.ToString(); // 排序方式
            //string strWhere = string.IsNullOrEmpty(pagerTable.SeatchWhere) ? "" : "where " + pagerTable.SeatchWhere;
            
            //// 分页查询
            //StringBuilder sbSqlStr = new StringBuilder();
            //sbSqlStr.AppendLine("SELECT @RecordCount=COUNT(1) FROM Table1");
            //sbSqlStr.AppendLine(string.Format("SELECT TOP {0} * ", pageSize));
            //sbSqlStr.AppendLine(string.Format("FROM ( SELECT ROW_NUMBER() OVER (ORDER BY {0} {1}) AS RowNumber,* FROM {2} {3}) as T",
            //    orderBy, orderWay, tableName, strWhere));
            //sbSqlStr.AppendLine(string.Format("WHERE RowNumber > {0}*({1}-1)", pageSize, pageNum));

            #endregion


            // 获取分页查询语句
            string sqlStr = CreatePagerSqlStr(pagerTable);

            IList<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(this.MakeOutParam("@RecordCount", DbType.Int32));

            DataTable dt = this.ExecSqlDataTable(sqlStr, parameters);

            // 设置总记录数
            pagerTable.RecordCount = Convert.ToInt32(parameters[0].Value);
            // 计算总共的页数
            if (pagerTable.RecordCount % pagerTable.PageSize != 0)
            {
                pagerTable.PageCount = pagerTable.RecordCount / pagerTable.PageSize + 1;
            }
            else
            {
                pagerTable.PageCount = pagerTable.RecordCount / pagerTable.PageSize;
            }

            // 设置返回数据
            pagerTable.TableName = dt.TableName;
            foreach (DataColumn item in dt.Columns)
            {
                pagerTable.Columns.Add(item.ColumnName, item.DataType, item.Expression);
            }
            foreach (DataRow item in dt.Rows)
            {
                pagerTable.Rows.Add(item.ItemArray);
            }
        }

        /// <summary>
        /// 填充分页查询PagerList结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagerList">PagerList对象</param>
        public override void FilePagerList<T>(PagerList<T> pagerList)
        {
            // 获取分页查询语句
            string sqlStr = CreatePagerSqlStr(pagerList);

            IList<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(this.MakeOutParam("@RecordCount", DbType.Int32));

            DbDataReader reader = this.ExecSqlReader(sqlStr, parameters);
            // 设置总记录数
            pagerList.RecordCount = Convert.ToInt32(parameters[0].Value);

            // 创建一个实体类对象，用于获取信息
            T tempModel = System.Activator.CreateInstance<T>();

            // 获取对象的所有属性信息
            PropertyInfo[] modelPis = tempModel.GetType().GetProperties();
            while (reader.Read())
            {
                T model = System.Activator.CreateInstance<T>();

                // 遍历属性信息，给属性赋值
                foreach (PropertyInfo pi in modelPis)
                {
                    // 获取字段对应的列名
                    ModelColumnAttribute mca = Tiu.Tools.AttributesTool.GetAttributeByPropertyInfo<ModelColumnAttribute>(pi);
                    if (mca != null)
                    {
                        string columnName = mca.ColumnName;
                        // 赋值
                        if (typeof(System.DBNull) != reader[columnName].GetType())
                        {
                            pi.SetValue(model, reader[columnName], null);
                        }
                        else
                        {
                            pi.SetValue(model, null, null);
                        }
                    }
                }
                pagerList.Add(model);
            }
            reader.Close();
            this.Close();
        }

        /// <summary>
        /// 创建分页查询语句
        /// </summary>
        /// <param name="pager">分页接口对象</param>
        /// <returns></returns>
        protected override string CreatePagerSqlStr(IPagerable pager)
        {
            // 修正参数
            if (pager.PageSize < 1)
            {
                pager.PageSize = 10;
            }
            if (pager.PageNum < 1)
            {
                pager.PageNum = 1;
            }

            // 获取分页数据
            string tableName = pager.PagerTableName; // 查询的表名
            int pageSize = pager.PageSize; // 每页显示的记录数
            int pageNum = pager.PageNum;     // 显示第几页
            string orderBy = pager.OrderBy; // 排序字段
            string orderWay = string.IsNullOrEmpty(pager.OrderBy.Trim()) ? "" : pager.OrderWay.ToString(); // 排序方式
            string strWhere = string.IsNullOrEmpty(pager.SeatchWhere) ? "" : "where " + pager.SeatchWhere;

            // 创建分页查询语句
            StringBuilder sbSqlStr = new StringBuilder();
            sbSqlStr.AppendLine(string.Format("SELECT @RecordCount=COUNT(1) FROM {0} {1}", tableName, strWhere));
            sbSqlStr.AppendLine(string.Format("SELECT TOP {0} * ", pageSize));
            sbSqlStr.AppendLine(string.Format("FROM ( SELECT ROW_NUMBER() OVER (ORDER BY {0} {1}) AS RowNumber,* FROM {2} {3}) as T",
                orderBy, orderWay, tableName, strWhere));
            sbSqlStr.AppendLine(string.Format("WHERE RowNumber > {0}*({1}-1)", pageSize, pageNum));

            return sbSqlStr.ToString();
        }
        #endregion

        #region [实体类映射数据库操作]
        /// <summary>
        /// 添加实体到数据库
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="model">实体类对象</param>
        /// <returns></returns>
        public override int AddModelToDB<T>(T model)
        {
            return AddModelToDbByParms<T>(model, "@");
        }

        /// <summary>
        /// 更新实体到数据库
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="model">实体类对象</param>
        /// <returns></returns>
        public override int UpdateModelToDB<T>(T model)
        {
            return UpdateModelToDbByParms<T>(model, "@");
        }
        #endregion

        #endregion

    }
}
