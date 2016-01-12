using System;
using System.IO;

namespace ZipLib.Tar
{
    public class TarOutputStream : Stream
    {
        protected byte[] assemblyBuffer;
        private int assemblyBufferLength;
        protected byte[] blockBuffer;
        protected TarBuffer buffer;
        private long currBytes;
        protected long currSize;
        private bool isClosed;
        protected Stream outputStream;

        public TarOutputStream(Stream outputStream)
            : this(outputStream, 20)
        {
        }

        public TarOutputStream(Stream outputStream, int blockFactor)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream");
            }
            this.outputStream = outputStream;
            buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
            assemblyBuffer = new byte[0x200];
            blockBuffer = new byte[0x200];
        }

        public override void Close()
        {
            if (!isClosed)
            {
                isClosed = true;
                Finish();
                buffer.Close();
            }
        }

        public void CloseEntry()
        {
            if (assemblyBufferLength > 0)
            {
                Array.Clear(assemblyBuffer, assemblyBufferLength, assemblyBuffer.Length - assemblyBufferLength);
                buffer.WriteBlock(assemblyBuffer);
                currBytes += assemblyBufferLength;
                assemblyBufferLength = 0;
            }
            if (currBytes < currSize)
            {
                throw new TarException(string.Format("Entry closed at '{0}' before the '{1}' bytes specified in the header were written", currBytes, currSize));
            }
        }

        public void Finish()
        {
            if (IsEntryOpen)
            {
                CloseEntry();
            }
            WriteEofBlock();
        }

        public override void Flush()
        {
            outputStream.Flush();
        }

        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return buffer.RecordSize;
        }

        public void PutNextEntry(TarEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }
            if (entry.TarHeader.Name.Length >= 100)
            {
                TarHeader header = new TarHeader
                {
                    TypeFlag = 0x4c,  
                    UserId = 0, GroupId = 0, 
                    GroupName = "", 
                    UserName = "", 
                    LinkName = "", 
                    Size = entry.TarHeader.Name.Length
                };
                header.Name += "././@LongLink";
                header.WriteHeader(blockBuffer);
                buffer.WriteBlock(blockBuffer);
                int nameOffset = 0;
                while (nameOffset < entry.TarHeader.Name.Length)
                {
                    Array.Clear(blockBuffer, 0, blockBuffer.Length);
                    TarHeader.GetAsciiBytes(entry.TarHeader.Name, nameOffset, blockBuffer, 0, 0x200);
                    nameOffset += 0x200;
                    buffer.WriteBlock(blockBuffer);
                }
            }
            entry.WriteEntryHeader(blockBuffer);
            buffer.WriteBlock(blockBuffer);
            currBytes = 0L;
            currSize = entry.IsDirectory ? 0L : entry.Size;
        }

        public override int Read(byte[] buf, int offset, int count)
        {
            return outputStream.Read(buf, offset, count);
        }

        public override int ReadByte()
        {
            return outputStream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return outputStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            outputStream.SetLength(value);
        }

        public override void Write(byte[] buf, int offset, int count)
        {
            if (buf == null)
            {
                throw new ArgumentNullException("buf");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
            }
            if ((buf.Length - offset) < count)
            {
                throw new ArgumentException("offset and count combination is invalid");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "Cannot be negative");
            }
            if ((currBytes + count) > currSize)
            {
                string message 
                    = string.Format("request to write '{0}' bytes exceeds size in header of '{1}' bytes", 
                    count, currSize);
                throw new ArgumentOutOfRangeException("count", message);
            }
            if (assemblyBufferLength > 0)
            {
                if ((assemblyBufferLength + count) >= blockBuffer.Length)
                {
                    int length = blockBuffer.Length - assemblyBufferLength;
                    Array.Copy(assemblyBuffer, 0, blockBuffer, 0, assemblyBufferLength);
                    Array.Copy(buf, offset, blockBuffer, assemblyBufferLength, length);
                    buffer.WriteBlock(blockBuffer);
                    currBytes += blockBuffer.Length;
                    offset += length;
                    count -= length;
                    assemblyBufferLength = 0;
                }
                else
                {
                    Array.Copy(buf, offset, assemblyBuffer, assemblyBufferLength, count);
                    offset += count;
                    assemblyBufferLength += count;
                    count -= count;
                }
            }
            while (count > 0)
            {
                if (count < blockBuffer.Length)
                {
                    Array.Copy(buf, offset, assemblyBuffer, assemblyBufferLength, count);
                    assemblyBufferLength += count;
                    return;
                }
                buffer.WriteBlock(buf, offset);
                int num2 = blockBuffer.Length;
                currBytes += num2;
                count -= num2;
                offset += num2;
            }
        }

        public override void WriteByte(byte value)
        {
            Write(new[] { value }, 0, 1);
        }

        private void WriteEofBlock()
        {
            Array.Clear(blockBuffer, 0, blockBuffer.Length);
            buffer.WriteBlock(blockBuffer);
        }

        public override bool CanRead
        {
            get
            {
                return outputStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return outputStream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return outputStream.CanWrite;
            }
        }

        private bool IsEntryOpen
        {
            get
            {
                return (currBytes < currSize);
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return buffer.IsStreamOwner;
            }
            set
            {
                buffer.IsStreamOwner = value;
            }
        }

        public override long Length
        {
            get
            {
                return outputStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return outputStream.Position;
            }
            set
            {
                outputStream.Position = value;
            }
        }

        public int RecordSize
        {
            get
            {
                return buffer.RecordSize;
            }
        }
    }
}
