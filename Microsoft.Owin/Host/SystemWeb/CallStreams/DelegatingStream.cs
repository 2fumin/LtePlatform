using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.SystemWeb.CallStreams
{
    internal abstract class DelegatingStream : Stream
    {
        protected DelegatingStream()
        {
        }

        protected DelegatingStream(Stream stream)
        {
            Stream = stream;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return Stream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return Stream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void Close()
        {
            Stream.Close();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    Stream.Close();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return Stream.EndRead(asyncResult);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            Stream.EndWrite(asyncResult);
        }

        public override void Flush()
        {
            Stream.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return Stream.FlushAsync(cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return Stream.Read(buffer, offset, count);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return Stream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            return Stream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return Stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            Stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Stream.Write(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return Stream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            Stream.WriteByte(value);
        }

        public override bool CanRead => Stream.CanRead;

        public override bool CanSeek => Stream.CanSeek;

        public override bool CanTimeout => Stream.CanTimeout;

        public override bool CanWrite => Stream.CanWrite;

        public override long Length => Stream.Length;

        public override long Position
        {
            get
            {
                return Stream.Position;
            }
            set
            {
                Stream.Position = value;
            }
        }

        public override int ReadTimeout
        {
            get
            {
                return Stream.ReadTimeout;
            }
            set
            {
                Stream.ReadTimeout = value;
            }
        }

        protected virtual Stream Stream { get; set; }

        public override int WriteTimeout
        {
            get
            {
                return Stream.WriteTimeout;
            }
            set
            {
                Stream.WriteTimeout = value;
            }
        }
    }
}
