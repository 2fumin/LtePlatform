using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Microsoft.Owin.Host.SystemWeb.CallStreams
{
    internal class OutputStream : DelegatingStream
    {
        private Action _faulted;
        private readonly HttpResponseBase _response;
        private volatile Action _start;

        internal OutputStream(HttpResponseBase response, Stream stream, Action start, Action faulted) : base(stream)
        {
            _response = response;
            _start = start;
            _faulted = faulted;
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback,
            object state)
        {
            IAsyncResult result;
            try
            {
                Start(false);
                result = base.BeginWrite(buffer, offset, count, callback, state);
            }
            catch (HttpException)
            {
                Faulted();
                throw;
            }
            return result;
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            try
            {
                base.EndWrite(asyncResult);
            }
            catch (HttpException)
            {
                Faulted();
                throw;
            }
        }

        private void Faulted()
        {
            Interlocked.Exchange(ref _faulted, delegate {
            })();
        }

        public override void Flush()
        {
            try
            {
                Start(true);
                _response.Flush();
            }
            catch (HttpException)
            {
                Faulted();
                throw;
            }
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            try
            {
                Start(true);
                await
                    Task.Factory.FromAsync(_response.BeginFlush,
                        _response.EndFlush, TaskCreationOptions.None);
            }
            catch (HttpException)
            {
                Faulted();
                throw;
            }
        }

        private void Start(bool force)
        {
            Action action = _start;
            if ((action != null) && (force || !_response.BufferOutput))
            {
                action();
                _start = null;
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                Start(false);
                base.Write(buffer, offset, count);
            }
            catch (HttpException)
            {
                Faulted();
                throw;
            }
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Task task;
            try
            {
                Start(false);
                task = base.WriteAsync(buffer, offset, count, cancellationToken);
            }
            catch (HttpException)
            {
                Faulted();
                throw;
            }
            return task;
        }

        public override void WriteByte(byte value)
        {
            try
            {
                Start(false);
                base.WriteByte(value);
            }
            catch (HttpException)
            {
                Faulted();
                throw;
            }
        }

    }
}
