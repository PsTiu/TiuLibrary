using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Tiu.EasyUI
{
    /// <summary>
    /// EasyUI树节点
    /// </summary>
    /// <typeparam name="ModelT">Model类</typeparam>
    public class EasyUiTreeNode<ModelT> : TreeNode, IEasyUiJsonData
    {
        private const string NODESTATE_OPEN = "open";
        private const string NODESTATE_CLOSED = "closed";

        /// <summary>
        /// 构造方法
        /// </summary>
        public EasyUiTreeNode()
        {
            // 初始化
            _childNodes = new EasyUiTreeNodeCollection<ModelT>(this);
        }

        #region 属性

        /// <summary>
        /// 节点id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 节点图标
        /// </summary>
        public string IconCls { get; set; }

        /// <summary>
        /// 节点对应的模型对象
        /// </summary>
        public ModelT Model { get; set; }

        /// <summary>
        /// 父级节点
        /// </summary>
        public new EasyUiTreeNode<ModelT> Parent { get; set; }

        /// <summary>
        /// 子级节点集合
        /// </summary>
        private EasyUiTreeNodeCollection<ModelT> _childNodes;
        /// <summary>
        /// 子级节点集合
        /// </summary>
        public new EasyUiTreeNodeCollection<ModelT> ChildNodes
        {
            get
            {
                return _childNodes;
            }
            set
            {
                _childNodes = value;
                if (value != null && value.Count >= 0)
                {
                    foreach (var item in value)
                    {
                        item.Parent = this;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 获取节点Json
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            StringBuilder sbJson = new StringBuilder();

            sbJson.Append("{");
            sbJson.AppendFormat("\"id\":\"{0}\",", Id);
            sbJson.AppendFormat("\"text\":\"{0}\",", Text);
            sbJson.AppendFormat("\"iconCls\":\"{0}\",", IconCls);
            sbJson.AppendFormat("\"checked\":{0},", Checked.ToString().ToLower());
            sbJson.AppendFormat("\"attributes\":{0}", GetAttributesJson());
            if (_childNodes != null && _childNodes.Count > 0)
            {
                sbJson.AppendFormat(",\"state\":\"{0}\"", (Expanded == null || Expanded == true) ? NODESTATE_OPEN : NODESTATE_CLOSED);
                sbJson.AppendFormat(",\"children\":{0}", _childNodes.ToJson());
            }
            sbJson.Append("}");

            return sbJson.ToString();
        }

        /// <summary>
        /// 获取实体类属性Json
        /// </summary>
        /// <returns></returns>
        private string GetAttributesJson()
        {
            StringBuilder sbAttributesJson = new StringBuilder();
            PropertyInfo[] pi = typeof(ModelT).GetProperties();
            string re = "";

            // 遍历对象中的每一个属性，生成Json字符串
            sbAttributesJson.Append("{");
            for (int i = 0; i < pi.Length; i++)
            {
                // 添加键值对
                string key = pi[i].Name.ToString();
                if (pi[i].GetValue(Model, null) == null)
                {
                    sbAttributesJson.AppendFormat("\"{0}\":\"\"", key);
                    // 如果不是最后一个属性的话，添加','
                    if (i < pi.Length - 1)
                    {
                        sbAttributesJson.Append(",");
                    }
                }
                else
                {
                    Type type = pi[i].GetValue(Model, null).GetType();
                    if (type.Namespace == "System")
                    {
                        string value = pi[i].GetValue(Model, null).ToString().Replace("\r\n", "<br>").Replace("\n", "<br>");
                        sbAttributesJson.AppendFormat("\"{0}\":\"{1}\"", key, value);
                        // 如果不是最后一个属性的话，添加','
                        if (i < pi.Length - 1)
                        {
                            sbAttributesJson.Append(",");
                        }
                    }
                    else if (type.BaseType == typeof(Enum))
                    {
                        string value = pi[i].GetValue(Model, null).GetHashCode().ToString();
                        sbAttributesJson.AppendFormat("\"{0}\":\"{1}\"", key, value);
                        // 如果不是最后一个属性的话，添加','
                        if (i < pi.Length - 1)
                        {
                            sbAttributesJson.Append(",");
                        }
                    }
                }
            }
            re = sbAttributesJson.ToString().TrimEnd(',');
            re = re + "}";

            return re;
        }

    }
}
