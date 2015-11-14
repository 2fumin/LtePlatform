using System;
using System.IO;
using Lte.Domain.Common;

namespace Lte.Domain.Lz4Net.ExtraZip
{
    public class ExtraCompressStream : Lz4CompressionStreamBase
    {
        private bool _disposed;
        private long _length;

        public ExtraCompressStream([NotNull] Stream targetStream,
            int bufferSize = 0x100000, Lz4Mode compressionMode = 0, bool closeStream = false)
            : base(
                targetStream, new byte[bufferSize], new byte[Lz4.LZ4_compressBound(bufferSize)], compressionMode,
                closeStream)
        {
            if (targetStream == null)
            {
                throw new ArgumentNullException(nameof(targetStream));
            }
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (disposing)
            {
                Flush();
            }
            var buffer = new byte[8];
            _targetStream.Write(buffer, 0, buffer.Length);
            CompressedLength += 8L;
            base.Dispose(disposing);
            if (_closeStream)
            {
                _targetStream.Dispose();
            }
        }

        public override void Flush()
        {
            if (_writeBufferOffset <= 0) return;
            var count = Lz4.Compress(_writeBuffer, 0, _writeBufferOffset, ref _compressedBuffer, _compressionMode);
            _targetStream.Write(_compressedBuffer, 0, count);
            CompressedLength += count;
            _writeBufferOffset = 0;
        }
        
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            while (true)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("stream");
                }
                if (buffer == null)
                {
                    throw new ArgumentNullException(nameof(buffer));
                }
                if (offset < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }
                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(count));
                }
                if ((offset + count) > buffer.Length)
                {
                    throw new ArgumentException();
                }
                if (count == 0) return;
                var num = _writeBuffer.Length - _writeBufferOffset;
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
                    offset = offset + num;
                    count = count - num;
                    continue;
                }
                break;
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

        public long CompressedLength { get; private set; }

        public override long Length => _length;

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

