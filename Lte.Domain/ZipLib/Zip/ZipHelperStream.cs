using System;
using System.IO;

namespace Lte.Domain.ZipLib.Zip
{
    internal class ZipHelperStream : Stream
    {
        private bool isOwner_;
        private Stream stream_;

        public ZipHelperStream(Stream stream)
        {
            stream_ = stream;
        }

        public ZipHelperStream(string name)
        {
            stream_ = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
            isOwner_ = true;
        }

        public override void Close()
        {
            Stream stream = stream_;
            stream_ = null;
            if (isOwner_ && (stream != null))
            {
                isOwner_ = false;
                stream.Close();
            }
        }

        public override void Flush()
        {
            stream_.Flush();
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
            while (ReadLEInt() != signature);
            return Position;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return stream_.Read(buffer, offset, count);
        }

        public void ReadDataDescriptor(bool zip64, DescriptorData data)
        {
            if (ReadLEInt() != 0x8074b50)
            {
                throw new ZipException("Data descriptor signature not found");
            }
            data.Crc = ReadLEInt();
            if (zip64)
            {
                data.CompressedSize = ReadLELong();
                data.Size = ReadLELong();
            }
            else
            {
                data.CompressedSize = ReadLEInt();
                data.Size = ReadLEInt();
            }
        }

        public int ReadLEInt()
        {
            return (ReadLEShort() | (ReadLEShort() << 0x10));
        }

        public long ReadLELong()
        {
            return (((long)((ulong)ReadLEInt())) | (ReadLEInt() << 0x20));
        }

        public int ReadLEShort()
        {
            int num = stream_.ReadByte();
            if (num < 0)
            {
                throw new EndOfStreamException();
            }
            int num2 = stream_.ReadByte();
            if (num2 < 0)
            {
                throw new EndOfStreamException();
            }
            return (num | (num2 << 8));
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return stream_.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            stream_.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            stream_.Write(buffer, offset, count);
        }

        public int WriteDataDescriptor(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }
            int num = 0;
            if ((entry.Flags & 8) == 0)
            {
                return num;
            }
            WriteLEInt(0x8074b50);
            WriteLEInt((int)entry.Crc);
            num += 8;
            if (entry.LocalHeaderRequiresZip64)
            {
                WriteLELong(entry.CompressedSize);
                WriteLELong(entry.Size);
                return (num + 0x10);
            }
            WriteLEInt((int)entry.CompressedSize);
            WriteLEInt((int)entry.Size);
            return (num + 8);
        }

        public void WriteEndOfCentralDirectory(long noOfEntries, long sizeEntries, long startOfCentralDirectory, byte[] comment)
        {
            if (((noOfEntries >= 0xffffL) || (startOfCentralDirectory >= 0xffffffffL)) || (sizeEntries >= 0xffffffffL))
            {
                WriteZip64EndOfCentralDirectory(noOfEntries, sizeEntries, startOfCentralDirectory);
            }
            WriteLEInt(0x6054b50);
            WriteLEShort(0);
            WriteLEShort(0);
            if (noOfEntries >= 0xffffL)
            {
                WriteLEUshort(0xffff);
                WriteLEUshort(0xffff);
            }
            else
            {
                WriteLEShort((short)noOfEntries);
                WriteLEShort((short)noOfEntries);
            }
            if (sizeEntries >= 0xffffffffL)
            {
                WriteLEUint(uint.MaxValue);
            }
            else
            {
                WriteLEInt((int)sizeEntries);
            }
            if (startOfCentralDirectory >= 0xffffffffL)
            {
                WriteLEUint(uint.MaxValue);
            }
            else
            {
                WriteLEInt((int)startOfCentralDirectory);
            }
            int num = (comment != null) ? comment.Length : 0;
            if (num > 0xffff)
            {
                throw new ZipException(string.Format("Comment length({0}) is too long can only be 64K", num));
            }
            WriteLEShort(num);
            if (num > 0)
            {
                if (comment != null) Write(comment, 0, comment.Length);
            }
        }

        public void WriteLEInt(int value)
        {
            WriteLEShort(value);
            WriteLEShort(value >> 0x10);
        }

        public void WriteLELong(long value)
        {
            WriteLEInt((int)value);
            WriteLEInt((int)(value >> 0x20));
        }

        public void WriteLEShort(int value)
        {
            stream_.WriteByte((byte)(value & 0xff));
            stream_.WriteByte((byte)((value >> 8) & 0xff));
        }

        public void WriteLEUint(uint value)
        {
            WriteLEUshort((ushort)(value & 0xffff));
            WriteLEUshort((ushort)(value >> 0x10));
        }

        public void WriteLEUlong(ulong value)
        {
            WriteLEUint((uint)(value & 0xffffffffL));
            WriteLEUint((uint)(value >> 0x20));
        }

        public void WriteLEUshort(ushort value)
        {
            stream_.WriteByte((byte)(value & 0xff));
            stream_.WriteByte((byte)(value >> 8));
        }

        public void WriteZip64EndOfCentralDirectory(long noOfEntries, long sizeEntries, long centralDirOffset)
        {
            long position = stream_.Position;
            WriteLEInt(0x6064b50);
            WriteLELong(0x2cL);
            WriteLEShort(0x33);
            WriteLEShort(0x2d);
            WriteLEInt(0);
            WriteLEInt(0);
            WriteLELong(noOfEntries);
            WriteLELong(noOfEntries);
            WriteLELong(sizeEntries);
            WriteLELong(centralDirOffset);
            WriteLEInt(0x7064b50);
            WriteLEInt(0);
            WriteLELong(position);
            WriteLEInt(1);
        }

        public override bool CanRead
        {
            get
            {
                return stream_.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return stream_.CanSeek;
            }
        }

        public override bool CanTimeout
        {
            get
            {
                return stream_.CanTimeout;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return stream_.CanWrite;
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return isOwner_;
            }
            set
            {
                isOwner_ = value;
            }
        }

        public override long Length
        {
            get
            {
                return stream_.Length;
            }
        }

        public override long Position
        {
            get
            {
                return stream_.Position;
            }
            set
            {
                stream_.Position = value;
            }
        }
    }
}

