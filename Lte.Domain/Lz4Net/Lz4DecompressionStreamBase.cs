using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Lz4Net
{
    public abstract class Lz4DecompressionStreamBase : Stream
    {
        protected const int HeaderSize = 8;
        protected readonly bool _closeStream;
        protected readonly byte[] _header = new byte[HeaderSize];
        protected byte[] _readBuffer;
        protected Stream _targetStream;
        protected byte[] _unpackedBuffer;
        protected int _unpackedLength;
        protected int _unpackedOffset;

        protected Lz4DecompressionStreamBase(Stream targetStream, bool closeStream)
        {
            _targetStream = targetStream;
            _closeStream = closeStream;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

    }
}
