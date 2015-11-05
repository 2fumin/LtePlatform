using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Lz4Net
{

    public sealed class Lz4DecompressionStream : Stream
    {
        private const int HeaderSize = 8;
        private readonly bool m_closeStream;
        private readonly byte[] m_header = new byte[8];
        private byte[] m_readBuffer;
        private Stream m_targetStream;
        private byte[] m_unpackedBuffer;
        private int m_unpackedLength;
        private int m_unpackedOffset;

        public Lz4DecompressionStream(Stream sourceStream, bool closeStream = false)
        {
            m_closeStream = closeStream;
            m_targetStream = sourceStream;
            Fill();
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

        private void Fill()
        {
            int num = m_targetStream.Read(m_header, 0, 8);
            if (num == 0)
            {
                m_unpackedBuffer = null;
            }
            else
            {
                if (num != 8)
                {
                    throw new InvalidDataException("input buffer corrupted (header)");
                }
                int compressedSize = Lz4.GetCompressedSize(m_header);
                if ((m_readBuffer == null) || (m_readBuffer.Length < (compressedSize + 8)))
                {
                    m_readBuffer = new byte[compressedSize + 8];
                }
                Buffer.BlockCopy(m_header, 0, m_readBuffer, 0, 8);
                if (m_targetStream.Read(m_readBuffer, 8, compressedSize) != compressedSize)
                {
                    throw new InvalidDataException("input buffer corrupted (body)");
                }
                m_unpackedLength = Lz4.Decompress(m_readBuffer, 0, ref m_unpackedBuffer);
                m_unpackedOffset = 0;
            }
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if ((m_unpackedBuffer == null) || (m_unpackedOffset == m_unpackedLength))
            {
                Fill();
            }
            if (m_unpackedBuffer == null)
            {
                return 0;
            }
            if ((m_unpackedOffset + count) > m_unpackedLength)
            {
                int num = m_unpackedLength - m_unpackedOffset;
                int num2 = Read(buffer, offset, num);
                int num3 = Read(buffer, offset + num, count - num);
                return (num2 + num3);
            }
            Buffer.BlockCopy(m_unpackedBuffer, m_unpackedOffset, buffer, offset, count);
            m_unpackedOffset += count;
            return count;
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

