using System;
using System.IO;
using System.Text;

namespace ZipLib.Tar
{
    public class TarInputStream : Stream
    {
        private TarEntry _currentEntry;
        private IEntryFactory _entryFactory;
        private long _entryOffset;
        private long _entrySize;
        private bool _hasHitEof;
        private readonly Stream _inputStream;
        private byte[] _readBuffer;
        private readonly TarBuffer _tarBuffer;

        public TarInputStream(Stream inputStream)
            : this(inputStream, 20)
        {
        }

        public TarInputStream(Stream inputStream, int blockFactor)
        {
            _inputStream = inputStream;
            _tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
        }

        public override void Close()
        {
            _tarBuffer.Close();
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
            _inputStream.Flush();
        }

        public TarEntry GetNextEntry()
        {
            if (_hasHitEof)
            {
                return null;
            }
            if (_currentEntry != null)
            {
                SkipToNextEntry();
            }
            byte[] block = _tarBuffer.ReadBlock();
            if (block == null)
            {
                _hasHitEof = true;
            }
            else if (TarBuffer.IsEndOfArchiveBlock(block))
            {
                _hasHitEof = true;
            }
            if (_hasHitEof)
            {
                _currentEntry = null;
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
                    _entryOffset = 0L;
                    _entrySize = header.Size;
                    StringBuilder builder = null;
                    if (header.TypeFlag == 0x4c)
                    {
                        byte[] buffer = new byte[0x200];
                        long size = _entrySize;
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
                        block = _tarBuffer.ReadBlock();
                    }
                    else if (header.TypeFlag == 0x67)
                    {
                        SkipToNextEntry();
                        block = _tarBuffer.ReadBlock();
                    }
                    else if (header.TypeFlag == 120)
                    {
                        SkipToNextEntry();
                        block = _tarBuffer.ReadBlock();
                    }
                    else if (header.TypeFlag == 0x56)
                    {
                        SkipToNextEntry();
                        block = _tarBuffer.ReadBlock();
                    }
                    else if (((header.TypeFlag != 0x30) && (header.TypeFlag != 0)) && (header.TypeFlag != 0x35))
                    {
                        SkipToNextEntry();
                        block = _tarBuffer.ReadBlock();
                    }
                    if (_entryFactory == null)
                    {
                        _currentEntry = new TarEntry(block);
                        if (builder != null)
                        {
                            _currentEntry.Name = builder.ToString();
                        }
                    }
                    else
                    {
                        _currentEntry = _entryFactory.CreateEntry(block);
                    }
                    _entryOffset = 0L;
                    _entrySize = _currentEntry.Size;
                }
                catch (InvalidHeaderException exception)
                {
                    _entrySize = 0L;
                    _entryOffset = 0L;
                    _currentEntry = null;
                    throw new InvalidHeaderException(string.Format("Bad header in record {0} block {1} {2}", _tarBuffer.CurrentRecord, _tarBuffer.CurrentBlock, exception.Message));
                }
            }
            return _currentEntry;
        }

        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return _tarBuffer.RecordSize;
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
            if (_entryOffset >= _entrySize)
            {
                return 0;
            }
            long num2 = count;
            if ((num2 + _entryOffset) > _entrySize)
            {
                num2 = _entrySize - _entryOffset;
            }
            if (_readBuffer != null)
            {
                int length = (num2 > _readBuffer.Length) ? _readBuffer.Length : ((int)num2);
                Array.Copy(_readBuffer, 0, buffer, offset, length);
                if (length >= _readBuffer.Length)
                {
                    _readBuffer = null;
                }
                else
                {
                    int num4 = _readBuffer.Length - length;
                    byte[] destinationArray = new byte[num4];
                    Array.Copy(_readBuffer, length, destinationArray, 0, num4);
                    _readBuffer = destinationArray;
                }
                num += length;
                num2 -= length;
                offset += length;
            }
            while (num2 > 0L)
            {
                byte[] sourceArray = _tarBuffer.ReadBlock();
                if (sourceArray == null)
                {
                    throw new TarException("unexpected EOF with " + num2 + " bytes unread");
                }
                int num5 = (int)num2;
                int num6 = sourceArray.Length;
                if (num6 > num5)
                {
                    Array.Copy(sourceArray, 0, buffer, offset, num5);
                    _readBuffer = new byte[num6 - num5];
                    Array.Copy(sourceArray, num5, _readBuffer, 0, num6 - num5);
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
            _entryOffset += num;
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
            _entryFactory = factory;
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
            long skipCount = _entrySize - _entryOffset;
            if (skipCount > 0L)
            {
                Skip(skipCount);
            }
            _readBuffer = null;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("TarInputStream Write not supported");
        }

        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("TarInputStream WriteByte not supported");
        }

        public long Available => (_entrySize - _entryOffset);

        public override bool CanRead => _inputStream.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public bool IsMarkSupported => false;

        public bool IsStreamOwner
        {
            get
            {
                return _tarBuffer.IsStreamOwner;
            }
            set
            {
                _tarBuffer.IsStreamOwner = value;
            }
        }

        public override long Length => _inputStream.Length;

        public override long Position
        {
            get
            {
                return _inputStream.Position;
            }
            set
            {
                throw new NotSupportedException("TarInputStream Seek not supported");
            }
        }

        public int RecordSize => _tarBuffer.RecordSize;

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
