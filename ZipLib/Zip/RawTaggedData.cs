using System;

namespace ZipLib.Zip
{
    public class RawTaggedData : ITaggedData
    {
        private byte[] _data;
        private short _tag;

        public RawTaggedData(short tag)
        {
            _tag = tag;
        }

        public byte[] GetData()
        {
            return _data;
        }

        public void SetData(byte[] data, int offset, int count)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            _data = new byte[count];
            Array.Copy(data, offset, _data, 0, count);
        }

        public byte[] Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public short TagId
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }
    }
}

