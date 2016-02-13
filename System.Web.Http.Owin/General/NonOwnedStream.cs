using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http
{
    internal class NonOwnedStream : Stream
    {
        protected NonOwnedStream()
        {
        }

        public NonOwnedStream(Stream innerStream)
        {
            if (innerStream == null)
            {
                throw new ArgumentNullException(nameof(innerStream));
            }
            InnerStream = innerStream;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            ThrowIfDisposed();
            return InnerStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            ThrowIfDisposed();
            return InnerStream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void Close()
        {
            base.Close();
        }

        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            return InnerStream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                base.Dispose(disposing);
                IsDisposed = true;
            }
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            ThrowIfDisposed();
            return InnerStream.EndRead(asyncResult);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            ThrowIfDisposed();
            InnerStream.EndWrite(asyncResult);
        }

        public override void Flush()
        {
            ThrowIfDisposed();
            InnerStream.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            return InnerStream.FlushAsync(cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            return InnerStream.Read(buffer, offset, count);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            return InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            ThrowIfDisposed();
            return InnerStream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            ThrowIfDisposed();
            return InnerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            ThrowIfDisposed();
            InnerStream.SetLength(value);
        }

        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(null);
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            InnerStream.Write(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            return InnerStream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            ThrowIfDisposed();
            InnerStream.WriteByte(value);
        }

        public override bool CanRead => !IsDisposed && InnerStream.CanRead;

        public override bool CanSeek => !IsDisposed && InnerStream.CanSeek;

        public override bool CanTimeout => InnerStream.CanTimeout;

        public override bool CanWrite => !IsDisposed && InnerStream.CanWrite;

        protected Stream InnerStream { get; set; }

        protected bool IsDisposed { get; private set; }

        public override long Length
        {
            get
            {
                ThrowIfDisposed();
                return InnerStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                ThrowIfDisposed();
                return InnerStream.Position;
            }
            set
            {
                ThrowIfDisposed();
                InnerStream.Position = value;
            }
        }

        public override int ReadTimeout
        {
            get
            {
                ThrowIfDisposed();
                return InnerStream.ReadTimeout;
            }
            set
            {
                ThrowIfDisposed();
                InnerStream.ReadTimeout = value;
            }
        }

        public override int WriteTimeout
        {
            get
            {
                ThrowIfDisposed();
                return InnerStream.WriteTimeout;
            }
            set
            {
                ThrowIfDisposed();
                InnerStream.WriteTimeout = value;
            }
        }
    }
}
