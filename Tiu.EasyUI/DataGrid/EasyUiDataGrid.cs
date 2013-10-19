using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Data;
using Tiu.EasyUI.Attributes;

namespace Tiu.EasyUI
{
    /// <summary>
    /// EasyUiDataGrid
    /// </summary>
    public class EasyUiDataGrid : IEasyUiJsonData
    {
        private DataTable _dataSource;
        private int _total;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dataSource">数据源</param>
        public EasyUiDataGrid(DataTable dataSource)
        {
            _dataSource = dataSource;
            _total = dataSource.Rows.Count;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="total">数据源</param>
        public EasyUiDataGrid(DataTable dataSource, int total)
        {
            _dataSource = dataSource;
            _total = total;
        }

        /// <summary>
        /// 转成Json数据
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            StringBuilder re = new StringBuilder();
            var rowCount = _dataSource.Rows.Count;
            var colCount = _dataSource.Columns.Count;

            if (rowCount > 0)
            {
                // 加入总记录数
                re.Append("{\"total\":" + _total + ",\"rows\":[");

                // 遍历集合里的每一个对象
                for (int i = 0; i < rowCount; i++)
                {
                    System.Data.DataRow row = _dataSource.Rows[i];

                    StringBuilder sbItemJson = new StringBuilder();

                    // 遍历对象中的每一个属性，生成Json字符串
                    sbItemJson.Append("{");
                    for (int j = 0; j < colCount; j++)
                    {
                        // 添加键值对
                        string key = _dataSource.Columns[j].ColumnName;
                        if (row[j] == null)
                        {
                            sbItemJson.AppendFormat("\"{0}\":\"\"", key);
                            // 如果不是最后一个属性的话，添加','
                            if (j < colCount - 1)
                            {
                                sbItemJson.Append(",");
                            }
                        }
                        else
                        {
                            string value = row[j].ToString();
                            sbItemJson.AppendFormat("\"{0}\":\"{1}\"", key, value.Replace("\r\n", "<br>").Replace("\n", "<br>"));
                            // 如果不是最后一个属性的话，添加','
                            if (j < colCount - 1)
                            {
                                sbItemJson.Append(",");
                            }
                        }
                    }

                    // 如果最后一个是逗号，那么去掉
                    if (sbItemJson.ToString().LastIndexOf(",") == sbItemJson.ToString().Length - 1)
                    {
                        string temp = sbItemJson.ToString();
                        sbItemJson = new StringBuilder();
                        sbItemJson.Append(temp.Substring(0, temp.Length - 1));
                    }

                    sbItemJson.Append("}");

                    // 如果不是最后一个，那么加上','
                    if (i < rowCount - 1)
                    {
                        sbItemJson.Append(",");
                    }

                    re.Append(sbItemJson.ToString());
                }

                re.Append("]}");
            }
            else
            {
                re.Append("{\"total\":0,\"rows\":[]}");
            }
            return re.ToString();
        }
    }

    /// <summary>
    /// EasyUiDataGrid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EasyUiDataGrid<T>:IEasyUiJsonData
    {
        private IEnumerable<T> _dataSource;
        private int _total;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dataSource">数据源</param>
        public EasyUiDataGrid(IEnumerable<T> dataSource)
        {
            _dataSource = dataSource;
            _total = dataSource.Count();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="total">数据源</param>
        public EasyUiDataGrid(IEnumerable<T> dataSource, int total)
        {
            _dataSource=dataSource;
            _total = total >= 0 ? total : 0;
        }

        /// <summary>
        /// 根据PropertyInfo获取列名，没有设置DataGridColumnAttribute特性的话返回null
        /// </summary>
        /// <param name="property">PropertyInfo</param>
        /// <returns></returns>
        private DataGridColumnAttribute GetDataGridColumnAttribute(PropertyInfo property)
        {
            DataGridColumnAttribute attr = Tiu.Tools.AttributesTool.GetAttributeInProperty<T, DataGridColumnAttribute>(property.Name);
            return attr;
        }

        /// <summary>
        /// 转成Json数据
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            string returnStringFormat = "{{\"total\":{0},\"rows\":[{1}]}}";
            string totalValue = _total.ToString();
            string rowsValue = "";

            if (_total > 0)
            {
                Type modelType = typeof(T);
                PropertyInfo[] modelPropertyInfos = modelType.GetProperties();
                IDictionary<PropertyInfo, DataGridColumnAttribute> propertyInfo_and_dataGridColumnName = new Dictionary<PropertyInfo, DataGridColumnAttribute>();
                foreach (var item in modelPropertyInfos)
                {
                    DataGridColumnAttribute dataGridColumn = GetDataGridColumnAttribute(item);
                    if (dataGridColumn!=null)
                    {
                        propertyInfo_and_dataGridColumnName.Add(item, dataGridColumn);
                    }
                }

                if (propertyInfo_and_dataGridColumnName.Count > 0)
                {
                    StringBuilder sbRows = new StringBuilder();

                    // 遍历集合里的每一个对象
                    for (int i = 0; i < _total; i++)
                    {
                        T model = _dataSource.ElementAt(i);
                        var columnCount = propertyInfo_and_dataGridColumnName.Count;

                        string rowFormat = "{{{0}}}";
                        if (i < _total - 1)
                        {
                            rowFormat += ",";
                        }

                        StringBuilder sbRow = new StringBuilder();

                        // 遍历要要生成列的属性
                        for (int j = 0; j < columnCount; j++)
                        {
                            var dicItem = propertyInfo_and_dataGridColumnName.ElementAt(j);
                            PropertyInfo thePropertyInfo = dicItem.Key;
                            DataGridColumnAttribute theDataGridColumnAttribute = dicItem.Value;

                            string cellFormat = "\"{0}\":\"{1}\"";
                            if (j < columnCount - 1)
                            {
                                cellFormat += ",";
                            }
                            string cellKey = string.IsNullOrEmpty(theDataGridColumnAttribute.ColumnName) ? thePropertyInfo.Name : theDataGridColumnAttribute.ColumnName;
                            string cellValue = "";

                            var thePropertyValue = thePropertyInfo.GetValue(model, null);
                            if (theDataGridColumnAttribute.ShowHashCode)
                            {
                                cellValue = thePropertyValue.GetHashCode().ToString();
                            }
                            else
                            {
                                cellValue = thePropertyValue.ToString();
                            }

                            sbRow.AppendFormat(cellFormat, cellKey, cellValue);
                        }

                        sbRows.AppendFormat(rowFormat, sbRow.ToString());
                        rowsValue = sbRows.ToString();
                    }
                }
            }

            return string.Format(returnStringFormat, totalValue, rowsValue);
        }
    }
}
