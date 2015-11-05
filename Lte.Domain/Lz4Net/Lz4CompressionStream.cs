using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Lz4Net
{
    public sealed class Lz4CompressionStream : Stream
    {
        private readonly bool m_closeStream;
        private byte[] m_compressedBuffer;
        private Lz4Mode m_compressionMode;
        private Stream m_targetStream;
        private readonly byte[] m_writeBuffer;
        private int m_writeBufferOffset;

        public Lz4CompressionStream(Stream targetStream, Lz4Mode mode = 0, bool closeStream = false)
            : this(targetStream, 0x40000, mode, closeStream)
        {
        }

        public Lz4CompressionStream(Stream targetStream, int bufferSize, Lz4Mode mode = 0, bool closeStream = false)
            : this(targetStream, new byte[bufferSize], new byte[Lz4.LZ4_compressBound(bufferSize)], mode, closeStream)
        {
        }

        public Lz4CompressionStream(Stream targetStream, byte[] writeBuffer, byte[] compressionBuffer, Lz4Mode mode = 0, bool closeStream = false)
        {
            m_closeStream = closeStream;
            m_targetStream = targetStream;
            m_writeBuffer = writeBuffer;
            m_compressedBuffer = compressionBuffer;
            m_compressionMode = mode;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Flush();
            }
            base.Dispose(disposing);
            if (m_closeStream && (m_targetStream != null))
            {
                m_targetStream.Dispose();
            }
            m_targetStream = null;
        }

        public override void Flush()
        {
            if (m_writeBufferOffset > 0)
            {
                int count = Lz4.Compress(m_writeBuffer, 0, m_writeBufferOffset, ref m_compressedBuffer, m_compressionMode);
                m_targetStream.Write(m_compressedBuffer, 0, count);
                m_writeBufferOffset = 0;
            }
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
            m_targetStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            int num = m_writeBuffer.Length - m_writeBufferOffset;
            if (count <= num)
            {
                Buffer.BlockCopy(buffer, offset, m_writeBuffer, m_writeBufferOffset, count);
                m_writeBufferOffset += count;
                if (num == 0)
                {
                    Flush();
                }
            }
            else
            {
                Write(buffer, offset, num);
                Write(buffer, offset + num, count - num);
            }
        }

        public override bool CanRead
        {
            get
            {
                return false;
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
                return true;
            }
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

