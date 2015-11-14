using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Lz4Net
{
    public sealed class Lz4CompressionStream : Stream
    {
        private readonly bool _closeStream;
        private byte[] _compressedBuffer;
        private readonly Lz4Mode _compressionMode;
        private Stream _targetStream;
        private readonly byte[] _writeBuffer;
        private int _writeBufferOffset;

        public Lz4CompressionStream(Stream targetStream, Lz4Mode mode = 0, bool closeStream = false)
            : this(targetStream, 0x40000, mode, closeStream)
        {
        }

        public Lz4CompressionStream(Stream targetStream, int bufferSize, Lz4Mode mode = 0, bool closeStream = false)
            : this(targetStream, new byte[bufferSize], new byte[Lz4.LZ4_compressBound(bufferSize)], mode, closeStream)
        {
        }

        public Lz4CompressionStream(Stream targetStream, byte[] writeBuffer, byte[] compressionBuffer, Lz4Mode mode = 0, bool closeStream = false)
        {
            _closeStream = closeStream;
            _targetStream = targetStream;
            _writeBuffer = writeBuffer;
            _compressedBuffer = compressionBuffer;
            _compressionMode = mode;
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
            _targetStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            int num = _writeBuffer.Length - _writeBufferOffset;
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
                Write(buffer, offset + num, count - num);
            }
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

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

