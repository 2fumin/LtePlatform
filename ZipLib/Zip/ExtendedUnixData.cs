using System;
using System.IO;

namespace ZipLib.Zip
{
    public class ExtendedUnixData : ITaggedData
    {
        private DateTime _createTime = new DateTime(0x7b2, 1, 1);
        private Flags _flags;
        private DateTime _lastAccessTime = new DateTime(0x7b2, 1, 1);
        private DateTime _modificationTime = new DateTime(0x7b2, 1, 1);

        public byte[] GetData()
        {
            byte[] buffer;
            using (MemoryStream stream = new MemoryStream())
            {
                using (ZipHelperStream stream2 = new ZipHelperStream(stream))
                {
                    stream2.IsStreamOwner = false;
                    stream2.WriteByte((byte)_flags);
                    if (((byte)(_flags & Flags.ModificationTime)) != 0)
                    {
                        DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0);
                        TimeSpan span = _modificationTime.ToUniversalTime() - time.ToUniversalTime();
                        int totalSeconds = (int)span.TotalSeconds;
                        stream2.WriteLeInt(totalSeconds);
                    }
                    if (((byte)(_flags & Flags.AccessTime)) != 0)
                    {
                        DateTime time2 = new DateTime(0x7b2, 1, 1, 0, 0, 0);
                        TimeSpan span2 = _lastAccessTime.ToUniversalTime() - time2.ToUniversalTime();
                        int num2 = (int)span2.TotalSeconds;
                        stream2.WriteLeInt(num2);
                    }
                    if (((byte)(_flags & Flags.CreateTime)) != 0)
                    {
                        DateTime time3 = new DateTime(0x7b2, 1, 1, 0, 0, 0);
                        TimeSpan span3 = _createTime.ToUniversalTime() - time3.ToUniversalTime();
                        int num3 = (int)span3.TotalSeconds;
                        stream2.WriteLeInt(num3);
                    }
                    buffer = stream.ToArray();
                }
            }
            return buffer;
        }

        public static bool IsValidValue(DateTime value)
        {
            if (value < new DateTime(0x76d, 12, 13, 20, 0x2d, 0x34))
            {
                return (value <= new DateTime(0x7f6, 1, 0x13, 3, 14, 7));
            }
            return true;
        }

        public void SetData(byte[] data, int index, int count)
        {
            using (MemoryStream stream = new MemoryStream(data, index, count, false))
            {
                using (ZipHelperStream stream2 = new ZipHelperStream(stream))
                {
                    _flags = (Flags)((byte)stream2.ReadByte());
                    if ((((byte)(_flags & Flags.ModificationTime)) != 0) && (count >= 5))
                    {
                        int seconds = stream2.ReadLeInt();
                        DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0);
                        _modificationTime = (time.ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0)).ToLocalTime();
                    }
                    if (((byte)(_flags & Flags.AccessTime)) != 0)
                    {
                        int num2 = stream2.ReadLeInt();
                        DateTime time3 = new DateTime(0x7b2, 1, 1, 0, 0, 0);
                        _lastAccessTime = (time3.ToUniversalTime() + new TimeSpan(0, 0, 0, num2, 0)).ToLocalTime();
                    }
                    if (((byte)(_flags & Flags.CreateTime)) != 0)
                    {
                        int num3 = stream2.ReadLeInt();
                        DateTime time5 = new DateTime(0x7b2, 1, 1, 0, 0, 0);
                        _createTime = (time5.ToUniversalTime() + new TimeSpan(0, 0, 0, num3, 0)).ToLocalTime();
                    }
                }
            }
        }

        public DateTime AccessTime
        {
            get
            {
                return _lastAccessTime;
            }
            set
            {
                if (!IsValidValue(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _flags = (Flags)((byte)(_flags | Flags.AccessTime));
                _lastAccessTime = value;
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
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _flags = (Flags)((byte)(_flags | Flags.CreateTime));
                _createTime = value;
            }
        }

        public DateTime ModificationTime
        {
            get
            {
                return _modificationTime;
            }
            set
            {
                if (!IsValidValue(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _flags = (Flags)((byte)(_flags | Flags.ModificationTime));
                _modificationTime = value;
            }
        }

        public short TagId => 0x5455;

        [Flags]
        public enum Flags : byte
        {
            AccessTime = 2,
            CreateTime = 4,
            ModificationTime = 1
        }
    }
}

