using System;
using System.IO;
using System.Web;

namespace Microsoft.Owin.Host.SystemWeb.CallStreams
{
    internal class InputStream : DelegatingStream
    {
        private bool _bufferOnSeek;
        private bool _preferBuffered = true;
        private readonly HttpRequestBase _request;
        private Stream _stream;

        internal InputStream(HttpRequestBase request)
        {
            _request = request;
        }

        internal void DisableBuffering()
        {
            switch (_request.ReadEntityBodyMode)
            {
                case ReadEntityBodyMode.None:
                case ReadEntityBodyMode.Classic:
                case ReadEntityBodyMode.Buffered:
                    _preferBuffered = false;
                    _bufferOnSeek = false;
                    return;

                case ReadEntityBodyMode.Bufferless:
                    return;
            }
            throw new NotImplementedException(_request.ReadEntityBodyMode.ToString());
        }

        private void ResolveStream()
        {
            if (_stream != null) return;
            switch (_request.ReadEntityBodyMode)
            {
                case ReadEntityBodyMode.None:
                    _stream = _preferBuffered ? _request.GetBufferedInputStream() : _request.GetBufferlessInputStream();
                    _bufferOnSeek = _preferBuffered;
                    return;

                case ReadEntityBodyMode.Classic:
                    _stream = _request.InputStream;
                    return;

                case ReadEntityBodyMode.Bufferless:
                    _stream = _request.GetBufferlessInputStream();
                    return;

                case ReadEntityBodyMode.Buffered:
                    _stream = _request.GetBufferedInputStream();
                    try
                    {
                        _stream = _request.InputStream;
                    }
                    catch (InvalidOperationException)
                    {
                        _bufferOnSeek = _preferBuffered;
                    }
                    return;
            }
            throw new NotImplementedException(_request.ReadEntityBodyMode.ToString());
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            ResolveStream();
            if (!_bufferOnSeek) return base.Seek(offset, origin);
            if ((origin == SeekOrigin.Begin) && (offset == _stream.Position))
            {
                return _stream.Position;
            }
            var position = _stream.Position;
            var buffer = new byte[0x400];
            while (_stream.Read(buffer, 0, buffer.Length) > 0)
            {
            }
            _stream = _request.InputStream;
            _stream.Position = position;
            _bufferOnSeek = false;
            return base.Seek(offset, origin);
        }

        public override bool CanSeek
        {
            get
            {
                switch (_request.ReadEntityBodyMode)
                {
                    case ReadEntityBodyMode.None:
                    case ReadEntityBodyMode.Buffered:
                        return _preferBuffered;

                    case ReadEntityBodyMode.Classic:
                        return true;

                    case ReadEntityBodyMode.Bufferless:
                        return false;
                }
                throw new NotImplementedException(_request.ReadEntityBodyMode.ToString());
            }
        }

        public override long Position
        {
            get
            {
                return _stream == null ? 0L : base.Position;
            }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }

        protected override Stream Stream
        {
            get
            {
                ResolveStream();
                return _stream;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
