using System;
using System.IO;

namespace ZipLib.Tar
{
    public class TarOutputStream : Stream
    {
        private readonly byte[] _assemblyBuffer;
        private int _assemblyBufferLength;
        private readonly byte[] _blockBuffer;
        private readonly TarBuffer _buffer;
        private long _currBytes;
        private long _currSize;
        private bool _isClosed;
        private readonly Stream _outputStream;

        public TarOutputStream(Stream outputStream)
            : this(outputStream, 20)
        {
        }

        public TarOutputStream(Stream outputStream, int blockFactor)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }
            this._outputStream = outputStream;
            _buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
            _assemblyBuffer = new byte[0x200];
            _blockBuffer = new byte[0x200];
        }

        public override void Close()
        {
            if (!_isClosed)
            {
                _isClosed = true;
                Finish();
                _buffer.Close();
            }
        }

        public void CloseEntry()
        {
            if (_assemblyBufferLength > 0)
            {
                Array.Clear(_assemblyBuffer, _assemblyBufferLength, _assemblyBuffer.Length - _assemblyBufferLength);
                _buffer.WriteBlock(_assemblyBuffer);
                _currBytes += _assemblyBufferLength;
                _assemblyBufferLength = 0;
            }
            if (_currBytes < _currSize)
            {
                throw new TarException(
                    $"Entry closed at '{_currBytes}' before the '{_currSize}' bytes specified in the header were written");
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
            _outputStream.Flush();
        }

        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return _buffer.RecordSize;
        }

        public void PutNextEntry(TarEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
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
                header.WriteHeader(_blockBuffer);
                _buffer.WriteBlock(_blockBuffer);
                int nameOffset = 0;
                while (nameOffset < entry.TarHeader.Name.Length)
                {
                    Array.Clear(_blockBuffer, 0, _blockBuffer.Length);
                    TarHeader.GetAsciiBytes(entry.TarHeader.Name, nameOffset, _blockBuffer, 0, 0x200);
                    nameOffset += 0x200;
                    _buffer.WriteBlock(_blockBuffer);
                }
            }
            entry.WriteEntryHeader(_blockBuffer);
            _buffer.WriteBlock(_blockBuffer);
            _currBytes = 0L;
            _currSize = entry.IsDirectory ? 0L : entry.Size;
        }

        public override int Read(byte[] buf, int offset, int count)
        {
            return _outputStream.Read(buf, offset, count);
        }

        public override int ReadByte()
        {
            return _outputStream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _outputStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _outputStream.SetLength(value);
        }

        public override void Write(byte[] buf, int offset, int count)
        {
            if (buf == null)
            {
                throw new ArgumentNullException(nameof(buf));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be negative");
            }
            if ((buf.Length - offset) < count)
            {
                throw new ArgumentException("offset and count combination is invalid");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            }
            if ((_currBytes + count) > _currSize)
            {
                string message 
                    = $"request to write '{count}' bytes exceeds size in header of '{_currSize}' bytes";
                throw new ArgumentOutOfRangeException(nameof(count), message);
            }
            if (_assemblyBufferLength > 0)
            {
                if ((_assemblyBufferLength + count) >= _blockBuffer.Length)
                {
                    int length = _blockBuffer.Length - _assemblyBufferLength;
                    Array.Copy(_assemblyBuffer, 0, _blockBuffer, 0, _assemblyBufferLength);
                    Array.Copy(buf, offset, _blockBuffer, _assemblyBufferLength, length);
                    _buffer.WriteBlock(_blockBuffer);
                    _currBytes += _blockBuffer.Length;
                    offset += length;
                    count -= length;
                    _assemblyBufferLength = 0;
                }
                else
                {
                    Array.Copy(buf, offset, _assemblyBuffer, _assemblyBufferLength, count);
                    offset += count;
                    _assemblyBufferLength += count;
                    count -= count;
                }
            }
            while (count > 0)
            {
                if (count < _blockBuffer.Length)
                {
                    Array.Copy(buf, offset, _assemblyBuffer, _assemblyBufferLength, count);
                    _assemblyBufferLength += count;
                    return;
                }
                _buffer.WriteBlock(buf, offset);
                int num2 = _blockBuffer.Length;
                _currBytes += num2;
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
            Array.Clear(_blockBuffer, 0, _blockBuffer.Length);
            _buffer.WriteBlock(_blockBuffer);
        }

        public override bool CanRead => _outputStream.CanRead;

        public override bool CanSeek => _outputStream.CanSeek;

        public override bool CanWrite => _outputStream.CanWrite;

        private bool IsEntryOpen => (_currBytes < _currSize);

        public bool IsStreamOwner
        {
            get
            {
                return _buffer.IsStreamOwner;
            }
            set
            {
                _buffer.IsStreamOwner = value;
            }
        }

        public override long Length => _outputStream.Length;

        public override long Position
        {
            get
            {
                return _outputStream.Position;
            }
            set
            {
                _outputStream.Position = value;
            }
        }

        public int RecordSize => _buffer.RecordSize;
    }
}
