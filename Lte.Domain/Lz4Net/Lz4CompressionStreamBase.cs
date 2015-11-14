using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Lz4Net
{
    public abstract class Lz4CompressionStreamBase : Stream
    {
        protected readonly bool _closeStream;
        protected byte[] _compressedBuffer;
        protected readonly Lz4Mode _compressionMode;
        protected Stream _targetStream;
        protected readonly byte[] _writeBuffer;
        protected int _writeBufferOffset;

        protected Lz4CompressionStreamBase(Stream targetStream, byte[] writeBuffer, byte[] compressionBuffer, Lz4Mode mode,
            bool closeStream)
        {
            _closeStream = closeStream;
            _targetStream = targetStream;
            _writeBuffer = writeBuffer;
            _compressedBuffer = compressionBuffer;
            _compressionMode = mode;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

    }
}
