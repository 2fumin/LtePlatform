using System;
using System.IO;

namespace ZipLib.Zip
{
    public class NTTaggedData : ITaggedData
    {
        private DateTime _createTime = DateTime.FromFileTime(0L);
        private DateTime _lastAccessTime = DateTime.FromFileTime(0L);
        private DateTime _lastModificationTime = DateTime.FromFileTime(0L);

        public byte[] GetData()
        {
            byte[] buffer;
            using (MemoryStream stream = new MemoryStream())
            {
                using (ZipHelperStream stream2 = new ZipHelperStream(stream))
                {
                    stream2.IsStreamOwner = false;
                    stream2.WriteLeInt(0);
                    stream2.WriteLeShort(1);
                    stream2.WriteLeShort(0x18);
                    stream2.WriteLeLong(_lastModificationTime.ToFileTime());
                    stream2.WriteLeLong(_lastAccessTime.ToFileTime());
                    stream2.WriteLeLong(_createTime.ToFileTime());
                    buffer = stream.ToArray();
                }
            }
            return buffer;
        }

        public static bool IsValidValue(DateTime value)
        {
            bool flag = true;
            try
            {
                value.ToFileTimeUtc();
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public void SetData(byte[] data, int index, int count)
        {
            using (MemoryStream stream = new MemoryStream(data, index, count, false))
            {
                using (ZipHelperStream stream2 = new ZipHelperStream(stream))
                {
                    stream2.ReadLeInt();
                    while (stream2.Position < stream2.Length)
                    {
                        int num = stream2.ReadLeShort();
                        int num2 = stream2.ReadLeShort();
                        if (num == 1)
                        {
                            if (num2 >= 0x18)
                            {
                                long fileTime = stream2.ReadLeLong();
                                _lastModificationTime = DateTime.FromFileTime(fileTime);
                                long num4 = stream2.ReadLeLong();
                                _lastAccessTime = DateTime.FromFileTime(num4);
                                long num5 = stream2.ReadLeLong();
                                _createTime = DateTime.FromFileTime(num5);
                            }
                            return;
                        }
                        stream2.Seek(num2, SeekOrigin.Current);
                    }
                }
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                if (!IsValidValue(value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _createTime = value;
            }
        }

        public DateTime LastAccessTime
        {
            get
            {
                return _lastAccessTime;
            }
            set
            {
                if (!IsValidValue(value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _lastAccessTime = value;
            }
        }

        public DateTime LastModificationTime
        {
            get
            {
                return _lastModificationTime;
            }
            set
            {
                if (!IsValidValue(value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _lastModificationTime = value;
            }
        }

        public short TagID
        {
            get
            {
                return 10;
            }
        }
    }
}

