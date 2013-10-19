using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Tiu.EasyUI
{
    /// <summary>
    /// EasyUI树节点集合
    /// </summary>
    public class EasyUiTreeNodeCollection<ModelT> : Collection<EasyUiTreeNode<ModelT>>, IEasyUiJsonData
    {
        /// <summary>
        /// 集合的持有者
        /// </summary>
        private EasyUiTreeNode<ModelT> _owner;

        /// <summary>
        /// 构造方法
        /// </summary>
        public EasyUiTreeNodeCollection() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="owner">集合的持有者</param>
        public EasyUiTreeNodeCollection(EasyUiTreeNode<ModelT> owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// 获取树状Json字符串
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            StringBuilder sbJson = new StringBuilder();

            sbJson.Append("[");

            if (Items != null && Items.Count > 0)
            {
                int loop = Items.Count;
                int lastIndex = loop - 1;
                for (int i = 0; i < loop; i++)
                {
                    sbJson.Append(Items[i].ToJson());
                    if (i < lastIndex)
                    {
                        sbJson.Append(",");
                    }
                }
            }
            sbJson.Append("]");

            return sbJson.ToString();
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="item"></param>
        public new void Add(EasyUiTreeNode<ModelT> item)
        {
            base.Add(item);

            if (_owner != null)
            {
                item.Parent = _owner;
            }
        }
    }
}
