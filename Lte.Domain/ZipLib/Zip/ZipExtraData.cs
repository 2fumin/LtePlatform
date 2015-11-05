using System;
using System.IO;

namespace Lte.Domain.ZipLib.Zip
{
    public sealed class ZipExtraData : IDisposable
    {
        private byte[] _data;
        private int _index;
        private MemoryStream _newEntry;
        private int _readValueLength;
        private int _readValueStart;

        public ZipExtraData()
        {
            Clear();
        }

        public ZipExtraData(byte[] data)
        {
            if (data == null)
            {
                _data = new byte[0];
            }
            else
            {
                _data = data;
            }
        }

        public void AddData(byte data)
        {
            _newEntry.WriteByte(data);
        }

        public void AddData(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            _newEntry.Write(data, 0, data.Length);
        }

        public void AddEntry(ITaggedData taggedData)
        {
            if (taggedData == null)
            {
                throw new ArgumentNullException("taggedData");
            }
            AddEntry(taggedData.TagID, taggedData.GetData());
        }

        public void AddEntry(int headerID, byte[] fieldData)
        {
            if ((headerID > 0xffff) || (headerID < 0))
            {
                throw new ArgumentOutOfRangeException("headerID");
            }
            int source = (fieldData == null) ? 0 : fieldData.Length;
            if (source > 0xffff)
            {
                throw new ArgumentOutOfRangeException("fieldData", "exceeds maximum length");
            }
            int num2 = (_data.Length + source) + 4;
            if (Find(headerID))
            {
                num2 -= ValueLength + 4;
            }
            if (num2 > 0xffff)
            {
                throw new ZipException("Data exceeds maximum length");
            }
            Delete(headerID);
            byte[] array = new byte[num2];
            _data.CopyTo(array, 0);
            int length = _data.Length;
            _data = array;
            SetShort(ref length, headerID);
            SetShort(ref length, source);
            if (fieldData != null)
            {
                fieldData.CopyTo(array, length);
            }
        }

        public void AddLeInt(int toAdd)
        {
            AddLeShort((short)toAdd);
            AddLeShort((short)(toAdd >> 0x10));
        }

        public void AddLeLong(long toAdd)
        {
            AddLeInt((int)(((ulong)toAdd) & 0xffffffffL));
            AddLeInt((int)(toAdd >> 0x20));
        }

        public void AddLeShort(int toAdd)
        {
            _newEntry.WriteByte((byte)toAdd);
            _newEntry.WriteByte((byte)(toAdd >> 8));
        }

        public void AddNewEntry(int headerID)
        {
            byte[] fieldData = _newEntry.ToArray();
            _newEntry = null;
            AddEntry(headerID, fieldData);
        }

        public void Clear()
        {
            if ((_data == null) || (_data.Length != 0))
            {
                _data = new byte[0];
            }
        }

        private static ITaggedData Create(short tag, byte[] data, int offset, int count)
        {
            ITaggedData data2;
            switch (tag)
            {
                case 10:
                    data2 = new NTTaggedData();
                    break;

                case 0x5455:
                    data2 = new ExtendedUnixData();
                    break;

                default:
                    data2 = new RawTaggedData(tag);
                    break;
            }
            data2.SetData(data, offset, count);
            return data2;
        }

        public bool Delete(int headerID)
        {
            bool flag = false;
            if (Find(headerID))
            {
                flag = true;
                int length = _readValueStart - 4;
                byte[] destinationArray = new byte[_data.Length - (ValueLength + 4)];
                Array.Copy(_data, 0, destinationArray, 0, length);
                int sourceIndex = (length + ValueLength) + 4;
                Array.Copy(_data, sourceIndex, destinationArray, length, _data.Length - sourceIndex);
                _data = destinationArray;
            }
            return flag;
        }

        public void Dispose()
        {
            if (_newEntry != null)
            {
                _newEntry.Close();
            }
        }

        public bool Find(int headerID)
        {
            _readValueStart = _data.Length;
            _readValueLength = 0;
            _index = 0;
            int num = _readValueStart;
            int num2 = headerID - 1;
            while ((num2 != headerID) && (_index < (_data.Length - 3)))
            {
                num2 = ReadShortInternal();
                num = ReadShortInternal();
                if (num2 != headerID)
                {
                    _index += num;
                }
            }
            bool flag = (num2 == headerID) && ((_index + num) <= _data.Length);
            if (flag)
            {
                _readValueStart = _index;
                _readValueLength = num;
            }
            return flag;
        }

        public byte[] GetEntryData()
        {
            if (Length > 0xffff)
            {
                throw new ZipException("Data exceeds maximum length");
            }
            return (byte[])_data.Clone();
        }

        public Stream GetStreamForTag(int tag)
        {
            Stream stream = null;
            if (Find(tag))
            {
                stream = new MemoryStream(_data, _index, _readValueLength, false);
            }
            return stream;
        }

        public int ReadByte()
        {
            int num = -1;
            if ((_index < _data.Length) && ((_readValueStart + _readValueLength) > _index))
            {
                num = _data[_index];
                _index++;
            }
            return num;
        }

        private void ReadCheck(int length)
        {
            if ((_readValueStart > _data.Length) || (_readValueStart < 4))
            {
                throw new ZipException("Find must be called before calling a Read method");
            }
            if (_index > ((_readValueStart + _readValueLength) - length))
            {
                throw new ZipException("End of extra data");
            }
            if ((_index + length) < 4)
            {
                throw new ZipException("Cannot read before start of tag");
            }
        }

        public int ReadInt()
        {
            ReadCheck(4);
            int num = ((_data[_index] + (_data[_index + 1] << 8)) + (_data[_index + 2] << 0x10)) + (_data[_index + 3] << 0x18);
            _index += 4;
            return num;
        }

        public long ReadLong()
        {
            ReadCheck(8);
            return ((ReadInt() & 0xffffffffL) | (ReadInt() << 0x20));
        }

        public int ReadShort()
        {
            ReadCheck(2);
            int num = _data[_index] + (_data[_index + 1] << 8);
            _index += 2;
            return num;
        }

        private int ReadShortInternal()
        {
            if (_index > (_data.Length - 2))
            {
                throw new ZipException("End of extra data");
            }
            int num = _data[_index] + (_data[_index + 1] << 8);
            _index += 2;
            return num;
        }

        private void SetShort(ref int index, int source)
        {
            _data[index] = (byte)source;
            _data[index + 1] = (byte)(source >> 8);
            index += 2;
        }

        public void Skip(int amount)
        {
            ReadCheck(amount);
            _index += amount;
        }

        public void StartNewEntry()
        {
            _newEntry = new MemoryStream();
        }

        public int CurrentReadIndex
        {
            get
            {
                return _index;
            }
        }

        public int Length
        {
            get
            {
                return _data.Length;
            }
        }

        public int UnreadCount
        {
            get
            {
                if ((_readValueStart > _data.Length) || (_readValueStart < 4))
                {
                    throw new ZipException("Find must be called before calling a Read method");
                }
                return ((_readValueStart + _readValueLength) - _index);
            }
        }

        public int ValueLength
        {
            get
            {
                return _readValueLength;
            }
        }
    }
}

