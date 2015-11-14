using System;
using System.IO;

namespace Lte.Domain.Lz4Net.ExtraZip
{
    internal class LzEntryOutputStream : Stream
    {
        private Stream _baseStream;
        private readonly Lz4PackageEntry _entry;
        private readonly ExtraCompressStream _lz;
        private readonly long _pos;

        public LzEntryOutputStream(Stream stream, Lz4PackageEntry entry, int bufferSize = 0x100000, Lz4Mode mode = 0)
        {
            _baseStream = stream;
            _lz = new ExtraCompressStream(stream, bufferSize, mode);
            try
            {
                _pos = stream.Position;
            }
            catch (Exception)
            {
                _pos = -1L;
            }
            _entry = entry;
            _entry.CompressedSize = 0L;
            _entry.OriginSize = 0L;
            _entry.Entry = stream.Position;
        }

        protected override void Dispose(bool disposing)
        {
            if (_baseStream != null)
            {
                _lz.Close();
                if (_pos != -1L)
                {
                    _entry.CompressedSize = _baseStream.Position - _pos;
                }
                _entry.Package.Save(_entry);
                _baseStream.Close();
                _baseStream = null;
            }
            base.Dispose(disposing);
        }

        public override void Flush()
        {
            _lz.Flush();
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
            _lz.Write(buffer, offset, count);
            _entry.OriginSize += count;
        }

        public override bool CanRead
        {
            get
            {
                return _lz.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return _lz.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return _lz.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                return _lz.Length;
            }
        }

        public override long Position
        {
            get
            {
                return _lz.Position;
            }
            set
            {
                _lz.Position = value;
            }
        }
    }
}

