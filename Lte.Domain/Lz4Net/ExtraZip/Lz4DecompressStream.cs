using System;
using System.IO;

namespace Lte.Domain.Lz4Net.ExtraZip
{
    public class Lz4DecompressStream : Stream
    {
        private readonly bool _closeStream;
        private bool _disposed;
        private bool _eof;
        private readonly byte[] _header = new byte[8];
        private long _position;
        private byte[] _readBuffer;
        private readonly Stream _targetStream;
        private byte[] _unpackedBuffer;
        private int _unpackedLength;
        private int _unpackedOffset;

        public Lz4DecompressStream(Stream targetStream, bool closeStream = false)
        {
            _targetStream = targetStream;
            _closeStream = closeStream;
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
                base.Dispose(disposing);
                if (_closeStream)
                {
                    _targetStream.Dispose();
                }
            }
        }

        private void Fill()
        {
            if (!_eof)
            {
                int num = _targetStream.Read(_header, 0, 8);
                if (num == 0)
                {
                    _unpackedBuffer = null;
                    _eof = true;
                }
                else
                {
                    if (num != 8)
                    {
                        throw new InvalidDataException("input buffer corrupted (header)");
                    }
                    int compressedSize = Lz4.GetCompressedSize(_header);
                    if (compressedSize == 0)
                    {
                        _unpackedBuffer = null;
                        _eof = true;
                    }
                    else
                    {
                        if ((_readBuffer == null) || (_readBuffer.Length < (compressedSize + 8)))
                        {
                            _readBuffer = new byte[compressedSize + 8];
                        }
                        Buffer.BlockCopy(_header, 0, _readBuffer, 0, 8);
                        if (_targetStream.Read(_readBuffer, 8, compressedSize) != compressedSize)
                        {
                            throw new InvalidDataException("input buffer corrupted (body)");
                        }
                        _unpackedLength = Lz4.Decompress(_readBuffer, 0, ref _unpackedBuffer);
                        _unpackedOffset = 0;
                        CompressedLength += _unpackedLength;
                    }
                }
            }
        }

        public override void Flush()
        {
            _targetStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
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
            if (_eof)
            {
                return 0;
            }
            if (count == 0)
            {
                return 0;
            }
            if ((_unpackedBuffer == null) || (_unpackedOffset == _unpackedLength))
            {
                Fill();
            }
            if (_eof)
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
            _position += count;
            return count;
        }

        public override int ReadByte()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("stream");
            }
            if (_eof)
            {
                return -1;
            }
            if (_unpackedOffset == _unpackedLength)
            {
                Fill();
            }
            if (_eof)
            {
                return -1;
            }
            _position += 1L;
            return _unpackedBuffer[_unpackedOffset++];
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
            throw new NotSupportedException();
        }

        public override bool CanRead
        {
            get
            {
                return true;
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

        public long CompressedLength { get; private set; }

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
                return _position;
            }
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}

