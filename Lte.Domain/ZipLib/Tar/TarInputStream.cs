using System;
using System.IO;
using System.Text;

namespace Lte.Domain.ZipLib.Tar
{
    public class TarInputStream : Stream
    {
        private TarEntry currentEntry;
        protected IEntryFactory entryFactory;
        protected long entryOffset;
        protected long entrySize;
        protected bool hasHitEOF;
        private readonly Stream inputStream;
        protected byte[] readBuffer;
        protected TarBuffer tarBuffer;

        public TarInputStream(Stream inputStream)
            : this(inputStream, 20)
        {
        }

        public TarInputStream(Stream inputStream, int blockFactor)
        {
            this.inputStream = inputStream;
            tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
        }

        public override void Close()
        {
            tarBuffer.Close();
        }

        public void CopyEntryContents(Stream outputStream)
        {
            byte[] buffer = new byte[0x8000];
            while (true)
            {
                int count = Read(buffer, 0, buffer.Length);
                if (count <= 0)
                {
                    return;
                }
                outputStream.Write(buffer, 0, count);
            }
        }

        public override void Flush()
        {
            inputStream.Flush();
        }

        public TarEntry GetNextEntry()
        {
            if (hasHitEOF)
            {
                return null;
            }
            if (currentEntry != null)
            {
                SkipToNextEntry();
            }
            byte[] block = tarBuffer.ReadBlock();
            if (block == null)
            {
                hasHitEOF = true;
            }
            else if (TarBuffer.IsEndOfArchiveBlock(block))
            {
                hasHitEOF = true;
            }
            if (hasHitEOF)
            {
                currentEntry = null;
            }
            else
            {
                try
                {
                    TarHeader header = new TarHeader();
                    header.ParseBuffer(block);
                    if (!header.IsChecksumValid)
                    {
                        throw new TarException("Header checksum is invalid");
                    }
                    entryOffset = 0L;
                    entrySize = header.Size;
                    StringBuilder builder = null;
                    if (header.TypeFlag == 0x4c)
                    {
                        byte[] buffer = new byte[0x200];
                        long size = entrySize;
                        builder = new StringBuilder();
                        while (size > 0L)
                        {
                            int length = Read(buffer, 0, (size > buffer.Length) ? buffer.Length : ((int)size));
                            if (length == -1)
                            {
                                throw new InvalidHeaderException("Failed to read long name entry");
                            }
                            builder.Append(TarHeader.ParseName(buffer, 0, length));
                            size -= length;
                        }
                        SkipToNextEntry();
                        block = tarBuffer.ReadBlock();
                    }
                    else if (header.TypeFlag == 0x67)
                    {
                        SkipToNextEntry();
                        block = tarBuffer.ReadBlock();
                    }
                    else if (header.TypeFlag == 120)
                    {
                        SkipToNextEntry();
                        block = tarBuffer.ReadBlock();
                    }
                    else if (header.TypeFlag == 0x56)
                    {
                        SkipToNextEntry();
                        block = tarBuffer.ReadBlock();
                    }
                    else if (((header.TypeFlag != 0x30) && (header.TypeFlag != 0)) && (header.TypeFlag != 0x35))
                    {
                        SkipToNextEntry();
                        block = tarBuffer.ReadBlock();
                    }
                    if (entryFactory == null)
                    {
                        currentEntry = new TarEntry(block);
                        if (builder != null)
                        {
                            currentEntry.Name = builder.ToString();
                        }
                    }
                    else
                    {
                        currentEntry = entryFactory.CreateEntry(block);
                    }
                    entryOffset = 0L;
                    entrySize = currentEntry.Size;
                }
                catch (InvalidHeaderException exception)
                {
                    entrySize = 0L;
                    entryOffset = 0L;
                    currentEntry = null;
                    throw new InvalidHeaderException(string.Format("Bad header in record {0} block {1} {2}", tarBuffer.CurrentRecord, tarBuffer.CurrentBlock, exception.Message));
                }
            }
            return currentEntry;
        }

        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return tarBuffer.RecordSize;
        }

        public void Mark(int markLimit)
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            int num = 0;
            if (entryOffset >= entrySize)
            {
                return 0;
            }
            long num2 = count;
            if ((num2 + entryOffset) > entrySize)
            {
                num2 = entrySize - entryOffset;
            }
            if (readBuffer != null)
            {
                int length = (num2 > readBuffer.Length) ? readBuffer.Length : ((int)num2);
                Array.Copy(readBuffer, 0, buffer, offset, length);
                if (length >= readBuffer.Length)
                {
                    readBuffer = null;
                }
                else
                {
                    int num4 = readBuffer.Length - length;
                    byte[] destinationArray = new byte[num4];
                    Array.Copy(readBuffer, length, destinationArray, 0, num4);
                    readBuffer = destinationArray;
                }
                num += length;
                num2 -= length;
                offset += length;
            }
            while (num2 > 0L)
            {
                byte[] sourceArray = tarBuffer.ReadBlock();
                if (sourceArray == null)
                {
                    throw new TarException("unexpected EOF with " + num2 + " bytes unread");
                }
                int num5 = (int)num2;
                int num6 = sourceArray.Length;
                if (num6 > num5)
                {
                    Array.Copy(sourceArray, 0, buffer, offset, num5);
                    readBuffer = new byte[num6 - num5];
                    Array.Copy(sourceArray, num5, readBuffer, 0, num6 - num5);
                }
                else
                {
                    num5 = num6;
                    Array.Copy(sourceArray, 0, buffer, offset, num6);
                }
                num += num5;
                num2 -= num5;
                offset += num5;
            }
            entryOffset += num;
            return num;
        }

        public override int ReadByte()
        {
            byte[] buffer = new byte[1];
            if (Read(buffer, 0, 1) <= 0)
            {
                return -1;
            }
            return buffer[0];
        }

        public void Reset()
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("TarInputStream Seek not supported");
        }

        public void SetEntryFactory(IEntryFactory factory)
        {
            entryFactory = factory;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("TarInputStream SetLength not supported");
        }

        public void Skip(long skipCount)
        {
            int num3;
            byte[] buffer = new byte[0x2000];
            for (long i = skipCount; i > 0L; i -= num3)
            {
                int count = (i > buffer.Length) ? buffer.Length : ((int)i);
                num3 = Read(buffer, 0, count);
                if (num3 == -1)
                {
                    return;
                }
            }
        }

        private void SkipToNextEntry()
        {
            long skipCount = entrySize - entryOffset;
            if (skipCount > 0L)
            {
                Skip(skipCount);
            }
            readBuffer = null;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("TarInputStream Write not supported");
        }

        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("TarInputStream WriteByte not supported");
        }

        public long Available
        {
            get
            {
                return (entrySize - entryOffset);
            }
        }

        public override bool CanRead
        {
            get
            {
                return inputStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public bool IsMarkSupported
        {
            get
            {
                return false;
            }
        }

        public bool IsStreamOwner
        {
            get
            {
                return tarBuffer.IsStreamOwner;
            }
            set
            {
                tarBuffer.IsStreamOwner = value;
            }
        }

        public override long Length
        {
            get
            {
                return inputStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return inputStream.Position;
            }
            set
            {
                throw new NotSupportedException("TarInputStream Seek not supported");
            }
        }

        public int RecordSize
        {
            get
            {
                return tarBuffer.RecordSize;
            }
        }

        public class EntryFactoryAdapter : IEntryFactory
        {
            public TarEntry CreateEntry(string name)
            {
                return TarEntry.CreateTarEntry(name);
            }

            public TarEntry CreateEntry(byte[] headerBuffer)
            {
                return new TarEntry(headerBuffer);
            }

            public TarEntry CreateEntryFromFile(string fileName)
            {
                return TarEntry.CreateEntryFromFile(fileName);
            }
        }

        public interface IEntryFactory
        {
            TarEntry CreateEntry(string name);
            TarEntry CreateEntry(byte[] headerBuffer);
            TarEntry CreateEntryFromFile(string fileName);
        }
    }
}
