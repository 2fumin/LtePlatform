using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
    internal sealed class ListWrapperCollection<T> : Collection<T>
    {
        internal ListWrapperCollection() : this(new List<T>())
        {
        }

        internal ListWrapperCollection(List<T> list) : base(list)
        {
            ItemsList = list;
        }

        internal List<T> ItemsList { get; }
    }
}
