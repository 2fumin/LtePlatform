using System;
using System.IO;
using ZipLib.Comppression;
using ZipLib.Zip;

namespace ZipLib.Streams
{
    public class InflaterInputStream : Stream
    {
        private readonly Stream _baseInputStream;
        protected long Csize;
        protected readonly Inflater Inf;
        protected readonly InflaterInputBuffer InputBuffer;
        private bool _isClosed;
        private bool _isStreamOwner;

        public InflaterInputStream(Stream baseInputStream)
            : this(baseInputStream, new Inflater(), 0x1000)
        {
        }

        public InflaterInputStream(Stream baseInputStream, Inflater inf)
            : this(baseInputStream, inf, 0x1000)
        {
        }

        public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
        {
            _isStreamOwner = true;
            if (baseInputStream == null)
            {
                throw new ArgumentNullException(nameof(baseInputStream));
            }
            if (inflater == null)
            {
                throw new ArgumentNullException(nameof(inflater));
            }
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }
            this._baseInputStream = baseInputStream;
            Inf = inflater;
            InputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException("InflaterInputStream BeginWrite not supported");
        }

        public override void Close()
        {
            if (!_isClosed)
            {
                _isClosed = true;
                if (_isStreamOwner)
                {
                    _baseInputStream.Close();
                }
            }
        }

        protected void Fill()
        {
            if (InputBuffer.Available <= 0)
            {
                InputBuffer.Fill();
                if (InputBuffer.Available <= 0)
                {
                    throw new SharpZipBaseException("Unexpected EOF");
                }
            }
            InputBuffer.SetInflaterInput(Inf);
        }

        public override void Flush()
        {
            _baseInputStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (Inf.IsNeedingDictionary)
            {
                throw new SharpZipBaseException("Need a dictionary");
            }
            int num = count;
            while (true)
            {
                int num2 = Inf.Inflate(buffer, offset, num);
                offset += num2;
                num -= num2;
                if ((num == 0) || Inf.IsFinished)
                {
                    return (count - num);
                }
                if (!Inf.IsNeedingInput)
                {
                    if (num2 == 0)
                    {
                        throw new ZipException("Dont know what to do");
                    }
                }
                else
                {
                    Fill();
                }
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seek not supported");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("InflaterInputStream SetLength not supported");
        }

        public long Skip(long count)
        {
            if (count <= 0L)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (_baseInputStream.CanSeek)
            {
                _baseInputStream.Seek(count, SeekOrigin.Current);
                return count;
            }
            int num = 0x800;
            if (count < num)
            {
                num = (int)count;
            }
            byte[] buffer = new byte[num];
            int num2 = 1;
            long num3 = count;
            while ((num3 > 0L) && (num2 > 0))
            {
                if (num3 < num)
                {
                    num = (int)num3;
                }
                num2 = _baseInputStream.Read(buffer, 0, num);
                num3 -= num2;
            }
            return (count - num3);
        }

        protected void StopDecrypting()
        {
            InputBuffer.CryptoTransform = null;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("InflaterInputStream Write not supported");
        }

        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("InflaterInputStream WriteByte not supported");
        }

        public virtual int Available
        {
            get
            {
                if (!Inf.IsFinished)
                {
                    return 1;
                }
                return 0;
            }
        }

        public override bool CanRead
        {
            get
            {
                return _baseInputStream.CanRead;
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

        public bool IsStreamOwner
        {
            get
            {
                return _isStreamOwner;
            }
            set
            {
                _isStreamOwner = value;
            }
        }

        public override long Length
        {
            get
            {
                return InputBuffer.RawLength;
            }
        }

        public override long Position
        {
            get
            {
                return _baseInputStream.Position;
            }
            set
            {
                throw new NotSupportedException("InflaterInputStream Position not supported");
            }
        }
    }
}
