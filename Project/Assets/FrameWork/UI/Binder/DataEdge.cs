using System;

namespace UIBinder
{
    public class DataEdge<T>
    {
        public DataNode<T> Src { get; set; }

        public DataNode<T> Dest { get; set; }

        public Action<DataEdge<T>> Accessor { get; set; }

        private Action<DataEdge<T>> defaultAccessor = (edge) =>
        {
            edge.Dest.Set(edge.Src.Get());
        };

        public DataEdge(DataNode<T> src, DataNode<T> dest, Action<DataEdge<T>> accessor = null)
        {
            Src = src;
            Dest = dest;
            Accessor = accessor == null ? defaultAccessor : accessor;
        }

    }
}
