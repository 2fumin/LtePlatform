using System;
using System.IO;
using Lte.Domain.Common;

namespace Lte.Domain.Lz4Net.ExtraZip
{
    public class Lz4CompressStream : Stream
    {
        private readonly bool _closeStream;
        private byte[] _compressedBuffer;
        private readonly Lz4Mode _compressionMode;
        private bool _disposed;
        private long _length;
        private readonly Stream _targetStream;
        private readonly byte[] _writeBuffer;
        private int _writeBufferOffset;

        public Lz4CompressStream([NotNull] Stream targetStream, 
            int bufferSize = 0x100000, Lz4Mode compressionMode = 0, bool closeStream = false)
        {
            if (targetStream == null)
            {
                throw new ArgumentNullException("targetStream");
            }
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }
            _targetStream = targetStream;
            _compressionMode = compressionMode;
            _closeStream = closeStream;
            _writeBuffer = new byte[bufferSize];
            _compressedBuffer = new byte[Lz4.LZ4_compressBound(bufferSize)];
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    Flush();
                }
                byte[] buffer = new byte[8];
                _targetStream.Write(buffer, 0, buffer.Length);
                CompressedLength += 8L;
                base.Dispose(disposing);
                if (_closeStream)
                {
                    _targetStream.Dispose();
                }
            }
        }

        public override void Flush()
        {
            if (_writeBufferOffset > 0)
            {
                int count = Lz4.Compress(_writeBuffer, 0, _writeBufferOffset, ref _compressedBuffer, _compressionMode);
                _targetStream.Write(_compressedBuffer, 0, count);
                CompressedLength += count;
                _writeBufferOffset = 0;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("stream");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException();
            }
            if (count != 0)
            {
                int num = _writeBuffer.Length - _writeBufferOffset;
                if (count <= num)
                {
                    Buffer.BlockCopy(buffer, offset, _writeBuffer, _writeBufferOffset, count);
                    _length += count;
                    _writeBufferOffset += count;
                    if (_writeBufferOffset == _writeBuffer.Length)
                    {
                        Flush();
                    }
                }
                else
                {
                    Write(buffer, offset, num);
                    Write(buffer, offset + num, count - num);
                }
            }
        }

        public override void WriteByte(byte value)
        {
            _writeBuffer[_writeBufferOffset++] = value;
            _length += 1L;
            if (_writeBufferOffset == _writeBuffer.Length)
            {
                Flush();
            }
        }

        public override bool CanRead
        {
            get
            {
                return false;
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
                return true;
            }
        }

        public long CompressedLength { get; private set; }

        public override long Length
        {
            get
            {
                return _length;
            }
        }

        public override long Position
        {
            get
            {
                return _length;
            }
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}

