using System;
using System.Collections.Generic;

using System.Text;

using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using Tiu.Data;
using System.Reflection;
using Tiu.Attributes;

namespace Tiu.DBUtility
{
    /// <summary>
    /// Access数据库访问类
    /// </summary>
    public class AccessHelper : DBHelper
    {

        #region 【构造函数】
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionStr">数据库链接字符串</param>
        public AccessHelper(string connectionStr)
            : base(connectionStr)
        {
            // 初始化数据库链接对象
            _connection = new OleDbConnection(connectionStr);

            // 初始化数据库命令对象
            _command = new OleDbCommand();
            _command.Connection = base._connection;

            // 初始化数据适配器对象
            _adapter = new OleDbDataAdapter("", connectionStr);
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
            return base.MakeParam<OleDbParameter>(paramName, ParameterDirection.Input, dbType, size, value);
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
            return base.MakeParam<OleDbParameter>(paramName, ParameterDirection.InputOutput, dbType, size, null);
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
        public override DataSet ExecSqlDataSet(string sqlStr, IList<DbParameter> parameters, string dataSetName)
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
        /// <param name="pagerTable">用于填充数据的PagerTable对象</param>
        public override void FilePagerTable(PagerTable pagerTable)
        {
            DataTable dt = new DataTable();
            int recordCount = 0;
            try
            {
                // 创建分页的SQL语句
                string sqlStr = CreatePagerSqlStr(pagerTable);

                // 打开数据库链接
                this.Open();
                // 创建DataSet
                DataSet dataSet = new DataSet(base._deafultDataSetName);
                // 设置SQL命令
                this._command.CommandText = sqlStr;
                this._command.CommandType = CommandType.Text;
                // 填充DataSet
                this._adapter.Fill(dataSet, (pagerTable.PageNum - 1) * pagerTable.PageSize, pagerTable.PageSize, base._defaultDataTableName);
                dt = dataSet.Tables[base._defaultDataTableName];

                // 获取总记录数
                string strWhere = string.IsNullOrEmpty(pagerTable.SeatchWhere) ? "" : "where " + pagerTable.SeatchWhere;
                sqlStr=string.Format("select count(1) from {0} {1}",pagerTable.PagerTableName,strWhere);
                recordCount = Convert.ToInt32(base.ExecSqlSingle(sqlStr));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                // 关闭数据库链接
                base.Close();
            }

            // 设置总记录数
            pagerTable.RecordCount = recordCount;
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
        /// <param name="pagerList">用于填充数据的PagerList对象</param>
        public override void FilePagerList<T>(PagerList<T> pagerList)
        {
            try
            {
                // 获取分页查询的SQL语句
                string sqlStr = CreatePagerSqlStr(pagerList);

                // 打开数据库链接
                this.Open();
                // 创建DataSet
                DataSet dataSet = new DataSet(base._deafultDataSetName);
                // 设置SQL命令
                this._command.CommandText = sqlStr;
                this._command.CommandType = CommandType.Text;
                // 填充DataSet
                this._adapter.Fill(dataSet, (pagerList.PageNum - 1) * pagerList.PageSize, pagerList.PageSize, base._defaultDataTableName);
                DataTable dt = dataSet.Tables[base._defaultDataTableName];

                // 创建一个实体类对象，用于获取信息
                T tempModel = System.Activator.CreateInstance<T>();
                // 获取对象的所有属性信息
                PropertyInfo[] modelPis = tempModel.GetType().GetProperties();
                // 遍历查询到的记录
                foreach (DataRow item in dt.Rows)
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
                            if (typeof(System.DBNull) != item[columnName].GetType())
                            {
                                pi.SetValue(model, item[columnName], null);
                            }
                            else
                            {
                                pi.SetValue(model, null, null);
                            }
                        }
                    }
                    pagerList.Add(model);
                }

                // 获取总记录数
                string strWhere = string.IsNullOrEmpty(pagerList.SeatchWhere) ? "" : "where " + pagerList.SeatchWhere;
                sqlStr = string.Format("select count(1) from {0} {1}", pagerList.PagerTableName, strWhere);
                pagerList.RecordCount = Convert.ToInt32(base.ExecSqlSingle(sqlStr));

                // 计算总共的页数
                if (pagerList.RecordCount % pagerList.PageSize != 0)
                {
                    pagerList.PageCount = pagerList.RecordCount / pagerList.PageSize + 1;
                }
                else
                {
                    pagerList.PageCount = pagerList.RecordCount / pagerList.PageSize;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // 关闭数据库链接
                base.Close();
            }
        }

        /// <summary>
        /// 创建分页查询的SQL语句
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
            int pageSize = pager.PageSize;   // 每页显示的记录数
            int pageNum = pager.PageNum;     // 显示第几页
            string orderBy = pager.OrderBy;  // 排序字段
            string orderWay = string.IsNullOrEmpty(pager.OrderBy.Trim()) ? "" : pager.OrderWay.ToString(); // 排序方式
            string strWhere = string.IsNullOrEmpty(pager.SeatchWhere) ? "" : "where " + pager.SeatchWhere; // 查询条件

            string sqlStr = string.Format("select * from {0} {1} order by {2} {3}", tableName, strWhere, orderBy, orderWay);

            return sqlStr;
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
