using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Lz4Net
{

    public sealed class Lz4DecompressionStream : Lz4DecompressionStreamBase
    {
        public Lz4DecompressionStream(Stream sourceStream, bool closeStream = false) : base(sourceStream, closeStream)
        {
            Fill();
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

        private void Fill()
        {
            int num = _targetStream.Read(_header, 0, HeaderSize);
            if (num == 0)
            {
                _unpackedBuffer = null;
            }
            else
            {
                if (num != 8)
                {
                    throw new InvalidDataException("input buffer corrupted (header)");
                }
                int compressedSize = Lz4.GetCompressedSize(_header);
                if ((_readBuffer == null) || (_readBuffer.Length < (compressedSize + HeaderSize)))
                {
                    _readBuffer = new byte[compressedSize + HeaderSize];
                }
                Buffer.BlockCopy(_header, 0, _readBuffer, 0, HeaderSize);
                if (_targetStream.Read(_readBuffer, HeaderSize, compressedSize) != compressedSize)
                {
                    throw new InvalidDataException("input buffer corrupted (body)");
                }
                _unpackedLength = Lz4.Decompress(_readBuffer, 0, ref _unpackedBuffer);
                _unpackedOffset = 0;
            }
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if ((_unpackedBuffer == null) || (_unpackedOffset == _unpackedLength))
            {
                Fill();
            }
            if (_unpackedBuffer == null)
            {
                return 0;
            }
            if ((_unpackedOffset + count) > _unpackedLength)
            {
                int num = _unpackedLength - _unpackedOffset;
                int num2 = Read(buffer, offset, num);
                int num3 = Read(buffer, offset + num, count - num);
                return (num2 + num3);
            }
            Buffer.BlockCopy(_unpackedBuffer, _unpackedOffset, buffer, offset, count);
            _unpackedOffset += count;
            return count;
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
            throw new NotSupportedException();
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

