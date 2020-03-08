using System;
using System.Collections.Generic;

namespace UIBinder
{
    public class DataNode<T>
    {
        /// <summary>
        /// 存储所有的数据边
        /// </summary>
        public Dictionary<DataNode<T>, DataEdge<T>> DataEdges = new Dictionary<DataNode<T>, DataEdge<T>>();
        ///// <summary>
        ///// 存储所有绑定自身的数据节点
        ///// </summary>
        //public List<DataNode> OberverNodes = new List<DataNode>();

        private T data;

        public T Data
        {
            get { return data; }
            set
            {
                if (!data.Equals(value))
                {
                    data = value;
                    OnValueChanged(data);
                    SyncAllDataNode();
                }
            }
        }

        public Func<T> Get;
        public Action<T> Set;

        public DataNode(T data)
        {
            this.data = data;
            Get = () => this.data;
            Set = (value) => this.data = value;
        }

        //public virtual T Get()
        //{
        //    return data;
        //}

        //public virtual void Set(T data)
        //{
        //    this.data = data;
        //}

        protected virtual void OnValueChanged(T data)
        {
            
        }

        public void Bind(DataNode<T> node, Action<DataEdge<T>> accrssor, bool isInitData = false)
        {
            BindByOneWay(node, accrssor, isInitData);
        }

        public void UnBind(DataNode<T> node)
        {
            DataEdges[node] = null;
        }

        public void UnAllBind()
        {
            DataEdges.Clear();
        }


        public void BindByOneWay(DataNode<T> node, Action<DataEdge<T>> toAccrssor, bool isInitData = false)
        {
            To(node, toAccrssor, isInitData);
        }

        public void BindByTwoWay(DataNode<T> node, Action<DataEdge<T>> toAccrssor, Action<DataEdge<T>> formAccrssor, bool isInitData = false)
        {
            To(node, toAccrssor, isInitData);
            From(node, formAccrssor, isInitData);
        }

        protected void To(DataNode<T> node, Action<DataEdge<T>> accrssor, bool isInitData = false)
        {
            node.DataEdges[this] = new DataEdge<T>(node, this, accrssor);
            if (isInitData)
            {
                node.SyncAllDataNode();
            }

        }

        protected void From(DataNode<T> node, Action<DataEdge<T>> accrssor, bool isInitData = false)
        {
            DataEdges[node] = new DataEdge<T>(this, node, accrssor);
            if (isInitData)
            {
                SyncAllDataNode();
            }
        }

        protected void SyncAllDataNode()
        {
            //BFS
            Queue<DataEdge<T>> queue = new Queue<DataEdge<T>>();
            foreach (var edge in DataEdges)
            {
                queue.Enqueue(edge.Value);
            }

            while (queue.Count > 0)
            {
                var currentEdge = queue.Dequeue();
                foreach (var edge in currentEdge.Dest.DataEdges)
                {
                    queue.Enqueue(edge.Value);
                }

                currentEdge.Accessor.Invoke(currentEdge);
            }
        }


    }
}
