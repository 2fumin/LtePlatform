using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microsoft.Owin.BuilderProperties
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AddressCollection : IEnumerable<Address>, IEnumerable
    {
        private readonly IList<IDictionary<string, object>> _list;

        public AddressCollection(IList<IDictionary<string, object>> list)
        {
            _list = list;
        }

        public IEnumerable<IDictionary<string, object>> List => _list;

        public int Count => _list.Count;

        public Address this[int index]
        {
            get { return new Address(_list[index]); }
            set { _list[index] = value.Dictionary; }
        }

        public void Add(Address address)
        {
            _list.Add(address.Dictionary);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Address> GetEnumerator()
        {
            return List.Select(iteratorVariable0 => new Address(iteratorVariable0)).GetEnumerator();
        }

        public static AddressCollection Create()
        {
            return new AddressCollection(new List<IDictionary<string, object>>());
        }

        public bool Equals(AddressCollection other)
        {
            return Equals(_list, other._list);
        }

        public override bool Equals(object obj)
        {
            return ((obj is AddressCollection) && Equals((AddressCollection) obj));
        }

        public override int GetHashCode()
        {
            return _list?.GetHashCode() ?? 0;
        }

        public static bool operator ==(AddressCollection left, AddressCollection right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AddressCollection left, AddressCollection right)
        {
            return !left.Equals(right);
        }
    }
}
