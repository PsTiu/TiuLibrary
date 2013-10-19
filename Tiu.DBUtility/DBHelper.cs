using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using Tiu.Data;
using System.Data.OleDb;
using System.Reflection;
using Tiu.Attributes;

namespace Tiu.DBUtility
{
    /// <summary>
    /// 数据库访问类基类
    /// </summary>
    public abstract class DBHelper
    {
        #region 【字段|属性】

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        private string _connectionStr;
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnectionStr { get { return this._connectionStr; } }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        protected DbConnection _connection;

        /// <summary>
        /// 数据库命令对象
        /// </summary>
        protected DbCommand _command;

        /// <summary>
        /// 数据适配器对象
        /// </summary>
        protected DbDataAdapter _adapter;

        /// <summary>
        /// 默认的DataSet名称
        /// </summary>
        protected string _deafultDataSetName = "Tiu_DataSet";

        /// <summary>
        /// 默认的DataTable名称
        /// </summary>
        protected string _defaultDataTableName = "Tiu_DataTable";

        #endregion


        #region 【构造函数】
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionStr">数据库链接字符串</param>
        public DBHelper(string connectionStr)
        {
            // 设置数据库链接字符串
            this.SetConnectionStr(connectionStr);
        }
        #endregion


        #region 【公共方法】

        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        public void Close()
        {
            if (this._connection.State != System.Data.ConnectionState.Closed)
            {
                this._connection.Close();
            }
        }


        #region [创建SQL参数的方法]
        #region |----创建传入的SQL参数
        /// <summary>
        /// 创建传入的SQL参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public abstract DbParameter MakeInParam(string paramName, DbType dbType, int size, object value);

        /// <summary>
        /// 创建传入的SQL参数（推荐使用）
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public DbParameter MakeInParam(string paramName,  DbType dbType, object value)
        {
            return this.MakeInParam(paramName, dbType, 0, value);
        }

        /// <summary>
        /// 创建传入的SQL参数
        /// </summary>
        /// <param name="paramName">参数名</param>       
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public DbParameter MakeInParam(string paramName, object value)
        {
            return this.MakeInParam(paramName, DbType.String, 0, value);
        }
        #endregion
        #region |----创建传出的SQL参数
        /// <summary>
        /// 创建传出的SQL参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <returns></returns>
        public abstract DbParameter MakeOutParam(string paramName, DbType dbType, int size);

        /// <summary>
        /// 创建传出的SQL参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <returns></returns>
        public DbParameter MakeOutParam(string paramName, DbType dbType)
        {
            return this.MakeOutParam(paramName, dbType, 0);
        }
        #endregion

        #endregion

        #region [执行SQL语句的方法]
        #region |----执行SQL语句返回DataReader
        /// <summary>
        /// 执行SQL语句，返回DataReader（注意：使用完DataReader之后要调用Close()方法关闭数据库链接）
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns></returns>
        public DbDataReader ExecSqlReader(string sqlStr)
        {
            return this.ExecSqlReader(sqlStr, null);
        }

        /// <summary>
        /// 执行SQL语句，返回DataReader（注意：使用完DataReader之后要调用Close()方法关闭数据库链接）
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public abstract DbDataReader ExecSqlReader(string sqlStr, IList<DbParameter> parameters);
        #endregion
        #region |----执行SQL语句返回DataSet
        /// <summary>
        /// 执行SQL语句返回DataSet
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns></returns>
        public DataSet ExecSqlDataSet(string sqlStr)
        {
            return this.ExecSqlDataSet(sqlStr, null, this._deafultDataSetName);
        }

        /// <summary>
        /// 执行SQL语句返回DataSet
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="dataSetName">数据集名称</param>
        /// <returns></returns>
        public DataSet ExecSqlDataSet(string sqlStr, string dataSetName)
        {
            return this.ExecSqlDataSet(sqlStr, null, dataSetName);
        }

        /// <summary>
        /// 执行SQL语句返回DataSet
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public DataSet ExecSqlDataSet(string sqlStr, IList<DbParameter> parameters)
        {
            return this.ExecSqlDataSet(sqlStr, parameters, this._deafultDataSetName);
        }

        /// <summary>
        /// 执行SQL语句返回DataSet
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="dataSetName">数据集名称</param>
        /// <returns></returns>
        public abstract DataSet ExecSqlDataSet(string sqlStr, IList<DbParameter> parameters, string dataSetName);
        #endregion
        #region |----执行SQL语句返回DataTable
        /// <summary>
        /// 执行SQL语句返回DataTable
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns></returns>
        public DataTable ExecSqlDataTable(string sqlStr)
        {
            return this.ExecSqlDataTable(sqlStr, null, this._defaultDataTableName);
        }

        /// <summary>
        /// 执行SQL语句返回DataTable
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="tableName">Table名</param>
        /// <returns></returns>
        public DataTable ExecSqlDataTable(string sqlStr, string tableName)
        {
            return ExecSqlDataTable(sqlStr, null, tableName);
        }

        /// <summary>
        /// 执行SQL语句返回DataTable
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public DataTable ExecSqlDataTable(string sqlStr, IList<DbParameter> parameters)
        {
            return this.ExecSqlDataTable(sqlStr, parameters, this._defaultDataTableName);
        }

        /// <summary>
        /// 执行SQL返回DataTable
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="tableName">Table名</param>
        /// <returns></returns>
        public abstract DataTable ExecSqlDataTable(string sqlStr, IList<DbParameter> parameters, string tableName);
        #endregion
        #region |----执行SQL语句返回首行首列
        /// <summary>
        /// 执行SQL语句返回首行首列
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns></returns>
        public object ExecSqlSingle(string sqlStr)
        {
            return this.ExecSqlSingle(sqlStr,null);
        }

        /// <summary>
        /// 执行SQL语句返回首行首列
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public abstract object ExecSqlSingle(string sqlStr, IList<DbParameter> parameters);
        #endregion
        #region |----执行SQL语句返回受影响行数
        /// <summary>
        /// 执行SQL语句返回受影响行数
        /// </summary>
        /// <param name="sqlStr">SQL参数</param>
        /// <returns></returns>
        public int ExecSql(string sqlStr)
        {
            return this.ExecSql(sqlStr,null);
        }

        /// <summary>
        /// 执行SQL语句返回受影响行数
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public abstract int ExecSql(string sqlStr, IList<DbParameter> parameters);
        #endregion
        #region |----执行多条SQL语句事务，返回是否成功执行全部SQL
        /// <summary>
        /// 执行多条SQL语句事务（非查询语句），（返回受影响行数；0=执行失败，回滚成功；-1=执行失败，回滚失败）
        /// </summary>
        /// <param name="sqlStrs">SQL语句</param>
        /// <returns></returns>
        public int ExecSqls(IList<string> sqlStrs)
        {
            IList<KeyValuePair<string, IList<DbParameter>>> sqlList = new List<KeyValuePair<string, IList<DbParameter>>>();
            foreach (var item in sqlStrs)
            {
                sqlList.Add(new KeyValuePair<string, IList<DbParameter>>(item, null));
            }
            return this.ExecSqls(sqlList);
        }

        /// <summary>
        /// 执行多条SQL语句事务（非查询语句），（返回受影响行数；0=执行失败，回滚成功；-1=执行失败，回滚失败）
        /// </summary>
        /// <param name="sqlList">批量执行的SQL语句（key：SQL语句，value：SQL语句的变量）</param>
        /// <returns></returns>
        public int ExecSqls(IList<KeyValuePair<string, IList<DbParameter>>> sqlList)
        {
            // 创建一个布尔型变量，用于返回执行结果
            int result = 0;

            // 创建一个事务对象
            DbTransaction transaction = this._connection.BeginTransaction();
            this._command.Transaction = transaction;

            // 打开数据库连接
            this.Open();

            try
            {
                // 遍历SQL语句并执行SQL
                foreach (var item in sqlList)
                {
                    // 设置SQL命令
                    this._command.CommandText = item.Key;
                    this._command.CommandType = CommandType.Text;
                    // 添加SQL参数
                    this.AddDbParameters(item.Value);
                    // 执行SQL
                    result += this._command.ExecuteNonQuery();
                }
                // 提交事务
                transaction.Commit();
            }
            catch
            {
                try
                {
                    // 事务回滚
                    transaction.Rollback();
                    result = 0;
                }
                catch
                {
                    // 回滚失败
                    result = -1;
                }
            }
            finally
            {
                this.Close();
            }

            // 返回结果
            return result;
        }
        #endregion
        #region |----执行SQL语句，通过DateReader填充List
        /// <summary>
        /// 执行SQL语句，通过DateReader填充List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="list">List对象</param>
        public void ExecSqlReaderFillList<T>(string sqlStr, List<T> list)
        {
            // 创建一个实体类对象，用于获取信息
            T tempModel = System.Activator.CreateInstance<T>();

            DbDataReader reader = ExecSqlReader(sqlStr);

            // 获取对象的所有属性信息
            PropertyInfo[] modelPis = tempModel.GetType().GetProperties();
            while (reader.Read())
            {
                // 创建Model
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

                list.Add(model);
            }
            reader.Close();
            this.Close();
        }
        #endregion
        #endregion

        #region [执行存储过程的方法]
        #region |----执行存储过程，返回DataReader
        /// <summary>
        /// 执行存储过程，返回DataReader（注意：使用完DataReader之后要调用Close()方法关闭数据库链接）
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns></returns>
        public DbDataReader ExecProcReader(string procName)
        {
            return this.ExecProcReader(procName, null);
        }

        /// <summary>
        /// 执行存储过程，返回DataReader（注意：使用完DataReader之后要调用Close()方法关闭数据库链接）
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public abstract DbDataReader ExecProcReader(string procName, IList<DbParameter> parameters);
        #endregion
        #region |----执行存储过程返回DataSet
        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns></returns>
        public DataSet ExecProcDataSet(string procName)
        {
            return this.ExecProcDataSet(procName, null, this._deafultDataSetName);
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="dataSetName">数据集名称</param>
        /// <returns></returns>
        public DataSet ExecProcDataSet(string procName, string dataSetName)
        {
            return this.ExecProcDataSet(procName, null, dataSetName);
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public DataSet ExecProcDataSet(string procName, IList<DbParameter> parameters)
        {
            return this.ExecProcDataSet(procName, parameters, this._deafultDataSetName);
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="dataSetName">数据集名称</param>
        /// <returns></returns>
        public abstract DataSet ExecProcDataSet(string procName, IList<DbParameter> parameters, string dataSetName);
        #endregion
        #region |----执行存储过程返回DataTable
        /// <summary>
        /// 执行存储过程返回DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns></returns>
        public DataTable ExecProcDataTable(string procName)
        {
            return this.ExecProcDataTable(procName, null, this._defaultDataTableName);
        }

        /// <summary>
        /// 执行存储过程返回DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="tableName">Table名</param>
        /// <returns></returns>
        public DataTable ExecProcDataTable(string procName, string tableName)
        {
            return this.ExecProcDataTable(procName, null, this._defaultDataTableName);
        }

        /// <summary>
        /// 执行存储过程返回DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public DataTable ExecProcDataTable(string procName, IList<DbParameter> parameters)
        {
            return this.ExecProcDataTable(procName, parameters, this._defaultDataTableName);
        }

        /// <summary>
        /// 执行存储过程返回DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="tableName">Table名</param>
        /// <returns></returns>
        public abstract DataTable ExecProcDataTable(string procName, IList<DbParameter> parameters, string tableName);
        #endregion
        #region |----执行存储过程返回首行首列
        /// <summary>
        /// 执行存储过程返回首行首列
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns></returns>
        public object ExecProcSingle(string procName)
        {
            return this.ExecProcSingle(procName, null);
        }

        /// <summary>
        /// 执行存储过程返回首行首列
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public abstract object ExecProcSingle(string procName, IList<DbParameter> parameters);
        #endregion

        #region 执行存储过程返回受影响行数
        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns></returns>
        public int ExecProc(string procName)
        {
            return this.ExecProc(procName, null);
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        public abstract int ExecProc(string procName, IList<DbParameter> parameters);
        #endregion

        #endregion

        #region [分页查询]
        #region |----填充分页查询PagerTable结果
        /// <summary>
        /// 填充分页查询PagerTable结果
        /// </summary>
        /// <param name="pageTable">用于填充数据的PagerTable对象</param>
        public abstract void FilePagerTable(PagerTable pageTable);        
        #endregion
        #region |----填充分页查询PagerList结果
        /// <summary>
        /// 填充分页查询PagerList结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagerList">用于填充数据的PagerList对象</param>
        public abstract void FilePagerList<T>(PagerList<T> pagerList);
        #endregion
        #region |----创建分页查询语句
        /// <summary>
        /// 创建分页查询语句
        /// </summary>
        /// <param name="pager">分页接口对象</param>
        /// <returns></returns>
        protected abstract string CreatePagerSqlStr(IPagerable pager);       
        #endregion
        #endregion

        #region [实体类映射数据库操作]
        #region |----添加
        /// <summary>
        /// 添加实体到数据库
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="model">实体类对象</param>
        /// <returns></returns>
        public virtual int AddModelToDB<T>(T model) where T : DbModel
        {
            var attInfos = AttributeInfos.GetAttributeInfo<T>();
            // 获取数据表名信息
            ModelTableAttribute tbAttr = attInfos.TableAttribute; //model.GetTableAttribute();
            if (tbAttr == null)
            {
                throw new Exception("没有设置实体类的ModelTableAttribute特性哇！");
            }

            // 获取数据表的所有列信息
            Dictionary<ModelColumnAttribute, object> dicColsAttr = model.GetColumnsInfo();
            if (dicColsAttr.Count <= 0)
            {
                throw new Exception("没有设置实体类字段的ModelColumnAttribute特性哇！");
            }

            string colNamesStr = string.Empty;      // 插入数据的字段
            string colValuesStr = string.Empty;     // 插入数据的字段的值
            foreach (KeyValuePair<ModelColumnAttribute, object> item in dicColsAttr)
            {
                // 忽略自增字段
                if (item.Key.Identity)
                {
                    continue;
                }
                colNamesStr += item.Key.ColumnName + ",";
                if (item.Value != null)
                {
                    Type valueType = item.Value.GetType();
                    if (valueType == typeof(string) || valueType == typeof(DateTime))
                    {
                        colValuesStr += string.Format("'{0}',", item.Value.ToString().Replace("'", "''"));
                    }
                    else
                    {
                        colValuesStr += string.Format("{0},", item.Value.GetHashCode());
                    }
                }
                else
                {
                    colValuesStr += "'',";
                }
            }
            colNamesStr = colNamesStr.Trim(',');
            colValuesStr = colValuesStr.Trim(',');

            string sqlStr = string.Format("insert into {0}({1}) values({2})"
                , tbAttr.TableName
                , colNamesStr
                , colValuesStr);
            return this.ExecSql(sqlStr);
        }

        /// <summary>
        /// 通过SQL参数添加实体到数据库
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="model">实体类对象</param>
        /// <param name="parmPrefix">参数前缀（如MsSqla、Access是"@"，Oracle是":",Mysql是"?"）</param>
        /// <returns></returns>
        protected int AddModelToDbByParms<T>(T model, string parmPrefix) where T : DbModel
        {
            var attInfos = AttributeInfos.GetAttributeInfo<T>();
            // 获取数据表名信息
            ModelTableAttribute tbAttr = attInfos.TableAttribute; //model.GetTableAttribute();
            if (tbAttr == null)
            {
                throw new Exception("没有设置实体类的ModelTableAttribute特性哇！");
            }

            // 获取数据表的所有列信息
            Dictionary<ModelColumnAttribute, object> dicColsAttr = model.GetColumnsInfo();
            if (dicColsAttr.Count <= 0)
            {
                throw new Exception("没有设置实体类字段的ModelColumnAttribute特性哇！");
            }

            string colNamesStr = string.Empty;      // 插入数据的字段
            string colValuesStr = string.Empty;     // 插入数据的字段的值
            IList<DbParameter> parameters = new List<DbParameter>(); // 参数列表
            foreach (KeyValuePair<ModelColumnAttribute, object> item in dicColsAttr)
            {
                // 忽略自增字段
                if (item.Key.Identity)
                {
                    continue;
                }

                string parmName = parmPrefix + item.Key.ColumnName; // 参数名（格式："@parmName"）
                object parmVal = null; // 参数值

                colNamesStr += item.Key.ColumnName + ",";
                colValuesStr += parmName + ",";

                if (item.Value != null)
                {
                    Type valueType = item.Value.GetType();
                    if (valueType == typeof(string) || valueType == typeof(DateTime))
                    {
                        parmVal = item.Value;
                    }
                    else
                    {
                        parmVal = item.Value.GetHashCode();
                    }
                }

                parameters.Add(MakeInParam(parmName, parmVal));
            }
            colNamesStr = colNamesStr.TrimEnd(',');
            colValuesStr = colValuesStr.TrimEnd(',');

            string sqlStr = string.Format("insert into {0}({1}) values({2})"
                , tbAttr.TableName
                , colNamesStr
                , colValuesStr);
            return this.ExecSql(sqlStr, parameters);
        }
        #endregion
        #region |----修改
        /// <summary>
        /// 更新实体到数据库
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="model">实体类对象</param>
        /// <returns></returns>
        public virtual int UpdateModelToDB<T>(T model) where T : DbModel
        {
            var attInfos = AttributeInfos.GetAttributeInfo<T>();
            // 获取数据表名信息
            ModelTableAttribute tbAttr = attInfos.TableAttribute; //model.GetTableAttribute();
            if (tbAttr == null)
            {
                throw new Exception("没有设置实体类的ModelTableAttribute特性哇！");
            }

            // 获取数据表的所有列信息
            Dictionary<ModelColumnAttribute, object> dicColsAttr = model.GetColumnsInfo();
            if (dicColsAttr.Count <= 0)
            {
                throw new Exception("没有设置实体类字段的ModelColumnAttribute特性哇！");
            }

            string colNamesAndValues = string.Empty;
            string where = string.Empty;
            foreach (KeyValuePair<ModelColumnAttribute, object> item in dicColsAttr)
            {
                if (!item.Key.Identity)
                {
                    if (item.Value != null)
                    {
                        Type valueType = item.Value.GetType();

                        if (valueType == typeof(string) || valueType == typeof(DateTime))
                        {
                            colNamesAndValues += string.Format("{0}='{1}',", item.Key.ColumnName, item.Value.ToString().Replace("'", "''"));
                        }
                        else
                        {
                            colNamesAndValues += string.Format("{0}={1},", item.Key.ColumnName, item.Value.GetHashCode());
                        }
                    }
                    else
                    {
                        colNamesAndValues += string.Format("{0}='',", item.Key.ColumnName);
                    }
                }
                else
                {
                    where = string.Format("where {0}={1}", item.Key.ColumnName, item.Value);
                }
            }
            colNamesAndValues = colNamesAndValues.Trim(',');

            // 创建SQL语句
            string sqlStr = string.Format("update {0} set {1} {2}"
                , tbAttr.TableName, 
                colNamesAndValues
                , where);

            // 执行SQL语句返回结果
            return this.ExecSql(sqlStr);
        }

        /// <summary>
        /// 通过SQL参数更新实体到数据库
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="model">实体类对象</param>
        /// <param name="parmPrefix">参数前缀（如MsSqla、Access是"@"，Oracle是":",Mysql是"?"）</param>
        /// <returns></returns>
        public int UpdateModelToDbByParms<T>(T model, string parmPrefix) where T : DbModel
        {
            var attInfos = AttributeInfos.GetAttributeInfo<T>();
            // 获取数据表名信息
            ModelTableAttribute tbAttr = attInfos.TableAttribute; //model.GetTableAttribute();
            if (tbAttr == null)
            {
                throw new Exception("没有设置实体类的ModelTableAttribute特性哇！");
            }

            // 获取数据表的所有列信息
            Dictionary<ModelColumnAttribute, object> dicColsAttr = model.GetColumnsInfo();
            if (dicColsAttr.Count <= 0)
            {
                throw new Exception("没有设置实体类字段的ModelColumnAttribute特性哇！");
            }

            string colNamesAndValues = string.Empty;
            string where = string.Empty;

            // 参数列表
            string pkParmName = "";  // 用户缓存主键参数名的变量
            object pkParmVal = null; // 用户缓存主键参数值的变量
            IList<DbParameter> parameters = new List<DbParameter>();
            foreach (KeyValuePair<ModelColumnAttribute, object> item in dicColsAttr)
            {
                string parameterName = item.Key.ColumnName;
                string parameterVal = parmPrefix + item.Key.ColumnName;

                if (!item.Key.Identity)
                {
                    colNamesAndValues += string.Format("{0}={1},", parameterName, parameterVal);
                    if (item.Value != null)
                    {
                        Type valueType = item.Value.GetType();
                        if (valueType == typeof(string) || valueType == typeof(DateTime))
                        {
                            parameters.Add(MakeInParam(parameterVal, item.Value));
                        }
                        else
                        {
                            parameters.Add(MakeInParam(parameterVal, item.Value.GetHashCode()));
                        }
                    }
                    else
                    {
                        parameters.Add(MakeInParam(parameterVal, null));
                    }
                }
                else
                {
                    where = string.Format("where {0}={1}", parameterName, parameterVal);
                    pkParmName = parameterName;
                    pkParmVal = item.Value;
                }
            }
            colNamesAndValues = colNamesAndValues.Trim(',');

            // 添加主键参数和值
            parameters.Add(MakeInParam(pkParmName, pkParmVal));

            // 创建SQL语句
            string sqlStr = string.Format("update {0} set {1} {2}"
                , tbAttr.TableName
                , colNamesAndValues
                , where);

            // 执行SQL语句返回结果
            return this.ExecSql(sqlStr,parameters);
        }
        #endregion
        #region |----查询单条记录

        /// <summary>
        /// 填充实体类
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="model">被填充的实体类对象（要预先设置好ID）</param>
        /// <returns></returns>
        public virtual bool FillModelFromDB<T>(T model) where T : DbModel
        {
            bool operResult = false;

            var attInfos = AttributeInfos.GetAttributeInfo<T>();
            // 获取数据表名信息
            ModelTableAttribute tbAttr = attInfos.TableAttribute; //model.GetTableAttribute();
            if (tbAttr == null)
            {
                throw new Exception("没有设置实体类的ModelTableAttribute特性哇！");
            }

            // 获取数据表的所有列信息
            Dictionary<ModelColumnAttribute, object> dicColsAttr = model.GetColumnsInfo();
            if (dicColsAttr.Count <= 0)
            {
                throw new Exception("没有设置实体类字段的ModelColumnAttribute特性哇！");
            }

            // 创建查询条件的SQL语句（根据主键查询）
            string where = string.Empty;
            foreach (KeyValuePair<ModelColumnAttribute, object> item in dicColsAttr)
            {
                if (item.Key.Identity)
                {
                    where = string.Format("{0}={1}", item.Key.ColumnName, item.Value);
                }
            }
            if (string.IsNullOrEmpty(where))
            {
                throw new Exception("没有设置自增字段哇！");
            }

            // 创建SQL语句
            string sqlStr = string.Format("select top 1 {0} from {1} where {2}"
                , "*"
                , tbAttr.TableName
                , where);

            // 执行SQL返回Reader
            DbDataReader reader = ExecSqlReader(sqlStr);
            if (reader.Read())
            {
                // 获取对象的所有属性信息
                PropertyInfo[] modelPis = model.GetType().GetProperties();

                // 遍历属性信息，给属性赋值
                foreach (PropertyInfo pi in modelPis)
                {
                    // 获取字段对应的列名
                    ModelColumnAttribute mca = Tiu.Tools.AttributesTool.GetAttributeByPropertyInfo<ModelColumnAttribute>(pi);
                    if (mca != null)
                    {
                        string columnName = mca.ColumnName;
                        // 赋值
                        if (reader[columnName] != null)
                        {
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
                }
                operResult = true;
            }

            // 关闭链接
            reader.Close();
            this.Close();

            // 返回结果
            return operResult;
        }

        #endregion
        #region |----查询多条记录
        /// <summary>
        /// 获取实体类列表
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <returns></returns>
        public IList<T> SelectListFromDB<T>() where T : DbModel
        {
            return SelectListFromDB<T>(0, string.Empty, string.Empty);
        }

        /// <summary>
        /// 获取实体类列表
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="top">前top条记录（设置为小于1表示查询全部记录）</param>
        /// <returns></returns>
        public IList<T> SelectListFromDB<T>(int top) where T : DbModel
        {
            return SelectListFromDB<T>(top, string.Empty, string.Empty);
        }

        /// <summary>
        /// 获取实体类列表
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="where">查询条件（不用带where）</param>
        /// <returns></returns>
        public IList<T> SelectListFromDB<T>(string where) where T : DbModel
        {
            return SelectListFromDB<T>(0, where, string.Empty);
        }

        /// <summary>
        /// 获取实体类列表
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="top">前top条记录（设置为小于1表示查询全部记录）</param>
        /// <param name="orderby">排序语句（不用带order by，需要表名字段名和排序方式，如"colName desc"）</param>
        /// <returns></returns>
        public IList<T> SelectListFromDB<T>(int top, string orderby) where T : DbModel
        {
            return SelectListFromDB<T>(top, string.Empty, orderby);
        }


        /// <summary>
        /// 获取实体类列表
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="where">查询条件（不用带where）</param>
        /// <param name="orderby">排序语句（不用带order by，需要表名字段名和排序方式，如"colName desc"）</param>
        /// <returns></returns>
        public IList<T> SelectListFromDB<T>(string where, string orderby) where T : DbModel
        {
            return SelectListFromDB<T>(0, where, orderby);
        }

        /// <summary>
        /// 获取实体类列表
        /// </summary>
        /// <typeparam name="T">数据表实体类</typeparam>
        /// <param name="top">前top条记录（设置为小于1表示查询全部记录）</param>
        /// <param name="where">查询条件（不用带where）</param>
        /// <param name="orderby">排序语句（不用带order by，需要表名字段名和排序方式，如"colName desc"）</param>
        /// <returns></returns>
        public virtual IList<T> SelectListFromDB<T>(int top, string where, string orderby) where T : DbModel
        {
            // 实例化返回的对象
            List<T> re = new List<T>();

            // 创建一个实体类对象，用于获取信息
            T tempModel = System.Activator.CreateInstance<T>();

            var attInfos = AttributeInfos.GetAttributeInfo<T>();
            // 获取数据表名信息
            ModelTableAttribute tbAttr = attInfos.TableAttribute; //model.GetTableAttribute();
            if (tbAttr == null)
            {
                throw new Exception("没有设置实体类的ModelTableAttribute特性哇！");
            }

            // 获取数据表的所有列信息
            Dictionary<ModelColumnAttribute, object> dicColsAttr = tempModel.GetColumnsInfo();
            if (dicColsAttr.Count <= 0)
            {
                throw new Exception("没有设置实体类字段的ModelColumnAttribute特性哇！");
            }

            // 创建SQL语句
            string sqlStr = string.Format("select {0} {1} from {2} {3} {4}"
                , top > 0 ? string.Format(" top {0} ", top) : string.Empty
                , "*"
                , tbAttr.TableName
                , string.IsNullOrEmpty(where) ? string.Empty : string.Format(" where {0} ", where)
                , string.IsNullOrEmpty(orderby) ? string.Empty : string.Format(" order by {0} ", orderby));

            // 执行SQL语句填充列表
            ExecSqlReaderFillList(sqlStr,re);

            // 返回结果
            return re;
        }
        #endregion
        #region |----删除记录
        public int DeleteByID<T>(T model) where T : DbModel
        { 
            // 获取数据表名信息
            ModelTableAttribute tbAttr = model.GetTableAttribute();
            if (tbAttr == null)
            {
                throw new Exception("没有设置实体类的ModelTableAttribute特性哇！");
            }

            // 获取数据表的所有列信息
            Dictionary<ModelColumnAttribute, object> dicColsAttr = model.GetColumnsInfo();
            if (dicColsAttr.Count <= 0)
            {
                throw new Exception("没有设置实体类字段的ModelColumnAttribute特性哇！");
            }

            string colNamesAndValues = string.Empty;
            string where = string.Empty;
            foreach (KeyValuePair<ModelColumnAttribute, object> item in dicColsAttr)
            {
                if (item.Key.Identity)
                {
                    where = string.Format("{0}={1}", item.Key.ColumnName, item.Value);
                    break;
                }
            }
            if (!string.IsNullOrEmpty(where))
            {
                var sqlStr = string.Format("delete from {0} where {1}",tbAttr.TableName,where);
                return ExecSql(sqlStr);
            }
            else
            {
                throw new Exception("没有设置自增字段（主键）哇！");
            }
        }
        #endregion
        #endregion

        #endregion


        #region 【保护方法】

        /// <summary>
        /// 设置数据库链接字符串
        /// </summary>
        /// <param name="connectionStr">数据库链接字符串</param>
        protected void SetConnectionStr(string connectionStr)
        {
            this._connectionStr = connectionStr;
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        protected void Open()
        {
            if (this._connection.State != System.Data.ConnectionState.Open)
            {
                this._connection.Open();
            }
        }

        /// <summary>
        /// 添加SQL参数（添加到数据库命令对象中）
        /// </summary>
        /// <param name="parameters"></param>
        protected void AddDbParameters(IList<DbParameter> parameters)
        {
            this._command.Parameters.Clear();
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var param in parameters)
                {
                    this._command.Parameters.Add(param);
                }
            }
        }

        /// <summary>
        /// 创建SQL参数
        /// </summary>
        /// <typeparam name="TParam">SQL参数类型</typeparam>
        /// <param name="paramName">参数名</param>
        /// <param name="direction">参数传送方向</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值（作为Output参数该值没作用）</param>
        /// <returns></returns>
        protected DbParameter MakeParam<TParam>(string paramName, ParameterDirection direction, DbType dbType, int size, object value)
            where TParam : DbParameter
        {
            // 判断SQL参数类型，创建SQL参数对象
            var paramType = typeof(TParam);
            DbParameter param = Activator.CreateInstance<TParam>();

            // 设置参数名
            param.ParameterName = paramName;

            // 设置参数类型
            param.DbType = dbType;

            // 设置参数大小
            if (size > 0)
            {
                param.Size = size;
            }

            // 设置参数值
            if (direction != ParameterDirection.Output)
            {
                param.Value = value ?? "";
            }

            // 设置参数传送方向
            param.Direction = direction;

            return param;
        }

        /// <summary>
        /// 执行SQL，返回DataReader（注意：使用完DataReader之后要调用Close()方法关闭数据库链接）
        /// </summary>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="commandText">SQL命令</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        protected DbDataReader ExecuteReader(CommandType commandType, string commandText, IList<DbParameter> parameters)
        {
            // 设置SQL命令
            this._command.CommandText = commandText;
            this._command.CommandType = commandType;
            // 添加SQL参数
            this.AddDbParameters(parameters);
            // 打开数据库链接
            this.Open();
            // 获取DataReade
            DbDataReader reader = this._command.ExecuteReader();
            // 返回结果
            return reader;
        }

        /// <summary>
        /// 执行SQL返回DataSet
        /// </summary>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="commandText">SQL命令</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="dataSetName">数据集名称</param>
        /// <returns></returns>
        protected DataSet ExecuteDataSet(CommandType commandType, string commandText, IList<DbParameter> parameters, string dataSetName)
        {
            try
            {
                // 创建DataSet
                DataSet dataSet = new DataSet(dataSetName);
                // 设置SQL命令
                this._command.CommandText = commandText;
                this._command.CommandType = commandType;
                // 添加SQL参数
                this.AddDbParameters(parameters);
                // 打开数据库链接
                this.Open();
                // 填充DataSet
                this._adapter.Fill(dataSet);
                // 返回结果
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // 关闭数据库链接
                this.Close();
            }
        }

        /// <summary>
        /// 执行SQL返回DataTable
        /// </summary>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="commandText">SQL命令</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="tableName">Table名</param>
        /// <returns></returns>
        protected DataTable ExecuteDataTable(CommandType commandType, string commandText, IList<DbParameter> parameters, string tableName)
        {
            try
            {
                // 创建DataSet
                DataSet dataSet = new DataSet(this._deafultDataSetName);
                // 设置SQL命令
                this._command.CommandText = commandText;
                this._command.CommandType = commandType;
                // 添加SQL参数
                this.AddDbParameters(parameters);
                // 打开数据库链接
                this.Open();
                // 填充DataSet
                this._adapter.Fill(dataSet, tableName);
                // 返回结果
                return dataSet.Tables[tableName];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // 关闭数据库链接
                this.Close();
            }
        }

        /// <summary>
        /// 执行SQL返回首行首列
        /// </summary>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="commandText">SQL命令</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        protected object ExecuteSingle(CommandType commandType, string commandText, IList<DbParameter> parameters)
        {
            try
            {
                // 设置SQL命令
                this._command.CommandText = commandText;
                this._command.CommandType = commandType;
                // 添加SQL参数
                this.AddDbParameters(parameters);
                // 打开链接
                this.Open();
                // 执行SQL
                object obj = this._command.ExecuteScalar();
                // 返回结果
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// 执行SQL语句返回受影响行数
        /// </summary>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="commandText">SQL命令</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns></returns>
        protected int ExecuteSql(CommandType commandType, string commandText, IList<DbParameter> parameters)
        {
            try
            {
                // 设置SQL命令
                this._command.CommandText = commandText;
                this._command.CommandType = commandType;
                // 添加SQL参数
                this.AddDbParameters(parameters);
                // 打开链接
                this.Open();
                // 执行SQL
                int obj = this._command.ExecuteNonQuery();
                // 返回结果
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Close();
            }
        }

        #endregion

    }
}
