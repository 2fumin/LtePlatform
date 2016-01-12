using System;
using System.IO;

namespace ZipLib.Zip
{
    internal class ZipHelperStream : Stream
    {
        private bool _isOwner;
        private Stream _stream;

        public ZipHelperStream(Stream stream)
        {
            _stream = stream;
        }

        public ZipHelperStream(string name)
        {
            _stream = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
            _isOwner = true;
        }

        public override void Close()
        {
            Stream stream = _stream;
            _stream = null;
            if (_isOwner && (stream != null))
            {
                _isOwner = false;
                stream.Close();
            }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
        {
            long offset = endLocation - minimumBlockSize;
            if (offset < 0L)
            {
                return -1L;
            }
            long num2 = Math.Max(offset - maximumVariableData, 0L);
            do
            {
                if (offset < num2)
                {
                    return -1L;
                }
                offset -= 1L;
                Seek(offset, SeekOrigin.Begin);
            }
            while (ReadLeInt() != signature);
            return Position;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public void ReadDataDescriptor(bool zip64, DescriptorData data)
        {
            if (ReadLeInt() != ZipConstants.DataDescriptorSignature)
            {
                throw new ZipException("Data descriptor signature not found");
            }
            data.Crc = ReadLeInt();
            if (zip64)
            {
                data.CompressedSize = ReadLeLong();
                data.Size = ReadLeLong();
            }
            else
            {
                data.CompressedSize = ReadLeInt();
                data.Size = ReadLeInt();
            }
        }

        public int ReadLeInt()
        {
            return (ReadLeShort() | (ReadLeShort() << 0x10));
        }

        public long ReadLeLong()
        {
            return (((long)((ulong)ReadLeInt())) | (ReadLeInt() << 0x20));
        }

        public int ReadLeShort()
        {
            int num = _stream.ReadByte();
            if (num < 0)
            {
                throw new EndOfStreamException();
            }
            int num2 = _stream.ReadByte();
            if (num2 < 0)
            {
                throw new EndOfStreamException();
            }
            return (num | (num2 << 8));
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public int WriteDataDescriptor(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            int num = 0;
            if ((entry.Flags & 8) == 0)
            {
                return num;
            }
            WriteLeInt(ZipConstants.DataDescriptorSignature);
            WriteLeInt((int)entry.Crc);
            num += 8;
            if (entry.LocalHeaderRequiresZip64)
            {
                WriteLeLong(entry.CompressedSize);
                WriteLeLong(entry.Size);
                return (num + 0x10);
            }
            WriteLeInt((int)entry.CompressedSize);
            WriteLeInt((int)entry.Size);
            return (num + 8);
        }

        public void WriteEndOfCentralDirectory(long noOfEntries, long sizeEntries, long startOfCentralDirectory, byte[] comment)
        {
            if (((noOfEntries >= 0xffffL) || (startOfCentralDirectory >= 0xffffffffL)) || (sizeEntries >= 0xffffffffL))
            {
                WriteZip64EndOfCentralDirectory(noOfEntries, sizeEntries, startOfCentralDirectory);
            }
            WriteLeInt(ZipConstants.EndOfCentralDirectorySignature);
            WriteLeShort(0);
            WriteLeShort(0);
            if (noOfEntries >= 0xffffL)
            {
                WriteLeUshort(0xffff);
                WriteLeUshort(0xffff);
            }
            else
            {
                WriteLeShort((short)noOfEntries);
                WriteLeShort((short)noOfEntries);
            }
            if (sizeEntries >= 0xffffffffL)
            {
                WriteLeUint(uint.MaxValue);
            }
            else
            {
                WriteLeInt((int)sizeEntries);
            }
            if (startOfCentralDirectory >= 0xffffffffL)
            {
                WriteLeUint(uint.MaxValue);
            }
            else
            {
                WriteLeInt((int)startOfCentralDirectory);
            }
            int num = (comment != null) ? comment.Length : 0;
            if (num > 0xffff)
            {
                throw new ZipException(string.Format("Comment length({0}) is too long can only be 64K", num));
            }
            WriteLeShort(num);
            if (num > 0)
            {
                if (comment != null) Write(comment, 0, comment.Length);
            }
        }

        public void WriteLeInt(int value)
        {
            WriteLeShort(value);
            WriteLeShort(value >> 0x10);
        }

        public void WriteLeLong(long value)
        {
            WriteLeInt((int)value);
            WriteLeInt((int)(value >> 0x20));
        }

        public void WriteLeShort(int value)
        {
            _stream.WriteByte((byte)(value & 0xff));
            _stream.WriteByte((byte)((value >> 8) & 0xff));
        }

        public void WriteLeUint(uint value)
        {
            WriteLeUshort((ushort)(value & 0xffff));
            WriteLeUshort((ushort)(value >> 0x10));
        }

        public void WriteLeUlong(ulong value)
        {
            WriteLeUint((uint)(value & 0xffffffffL));
            WriteLeUint((uint)(value >> 0x20));
        }

        public void WriteLeUshort(ushort value)
        {
            _stream.WriteByte((byte)(value & 0xff));
            _stream.WriteByte((byte)(value >> 8));
        }

        public void WriteZip64EndOfCentralDirectory(long noOfEntries, long sizeEntries, long centralDirOffset)
        {
            long position = _stream.Position;
            WriteLeInt(0x6064b50);
            WriteLeLong(0x2cL);
            WriteLeShort(0x33);
            WriteLeShort(0x2d);
            WriteLeInt(0);
            WriteLeInt(0);
            WriteLeLong(noOfEntries);
            WriteLeLong(noOfEntries);
            WriteLeLong(sizeEntries);
            WriteLeLong(centralDirOffset);

            WriteLeInt(ZipConstants.Zip64CentralDirLocatorSignature);
            WriteLeInt(0);
            WriteLeLong(position);
            WriteLeInt(1);
        }

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public override bool CanTimeout => _stream.CanTimeout;

        public override bool CanWrite => _stream.CanWrite;

        public bool IsStreamOwner
        {
            get
            {
                return _isOwner;
            }
            set
            {
                _isOwner = value;
            }
        }

        public override long Length => _stream.Length;

        public override long Position
        {
            get
            {
                return _stream.Position;
            }
            set
            {
                _stream.Position = value;
            }
        }
    }
}

