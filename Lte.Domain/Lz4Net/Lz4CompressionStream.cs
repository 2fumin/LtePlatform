using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Lz4Net
{
    public sealed class Lz4CompressionStream : Lz4CompressionStreamBase
    {
        public Lz4CompressionStream(Stream targetStream, Lz4Mode mode = 0, bool closeStream = false)
            : this(targetStream, 0x40000, mode, closeStream)
        {
        }

        public Lz4CompressionStream(Stream targetStream, int bufferSize, Lz4Mode mode = 0, bool closeStream = false)
            : this(targetStream, new byte[bufferSize], new byte[Lz4.LZ4_compressBound(bufferSize)], mode, closeStream)
        {
        }

        public Lz4CompressionStream(Stream targetStream, byte[] writeBuffer, byte[] compressionBuffer, Lz4Mode mode = 0,
            bool closeStream = false)
            : base(targetStream, writeBuffer, compressionBuffer, mode, closeStream)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Flush();
            }
            base.Dispose(disposing);
            if (_closeStream)
            {
                _targetStream?.Dispose();
            }
            _targetStream = null;
        }

        public override void Flush()
        {
            if (_writeBufferOffset <= 0) return;
            var count = Lz4.Compress(_writeBuffer, 0, _writeBufferOffset, ref _compressedBuffer, _compressionMode);
            _targetStream.Write(_compressedBuffer, 0, count);
            _writeBufferOffset = 0;
        }

        public override void SetLength(long value)
        {
            _targetStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            while (true)
            {
                var num = _writeBuffer.Length - _writeBufferOffset;
                if (count <= num)
                {
                    Buffer.BlockCopy(buffer, offset, _writeBuffer, _writeBufferOffset, count);
                    _writeBufferOffset += count;
                    if (num == 0)
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
        
        public override long Length
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}

