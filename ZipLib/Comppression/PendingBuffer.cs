using System;

namespace ZipLib.Comppression
{
    public class PendingBuffer
    {
        private int bitCount;
        private uint bits;
        private byte[] buffer_;
        private int end;
        private int start;

        protected PendingBuffer()
            : this(0x1000)
        {
        }

        protected PendingBuffer(int bufferSize)
        {
            buffer_ = new byte[bufferSize];
        }

        public void AlignToByte()
        {
            if (bitCount > 0)
            {
                buffer_[end++] = (byte)bits;
                if (bitCount > 8)
                {
                    buffer_[end++] = (byte)(bits >> 8);
                }
            }
            bits = 0;
            bitCount = 0;
        }

        public int Flush(byte[] output, int offset, int length)
        {
            if (bitCount >= 8)
            {
                buffer_[end++] = (byte)bits;
                bits = bits >> 8;
                bitCount -= 8;
            }
            if (length > (end - start))
            {
                length = end - start;
                Array.Copy(buffer_, start, output, offset, length);
                start = 0;
                end = 0;
                return length;
            }
            Array.Copy(buffer_, start, output, offset, length);
            start += length;
            return length;
        }

        public void Reset()
        {
            start = end = bitCount = 0;
        }

        public byte[] ToByteArray()
        {
            byte[] destinationArray = new byte[end - start];
            Array.Copy(buffer_, start, destinationArray, 0, destinationArray.Length);
            start = 0;
            end = 0;
            return destinationArray;
        }

        public void WriteBits(int b, int count)
        {
            bits |= (uint)(b << bitCount);
            bitCount += count;
            if (bitCount >= 0x10)
            {
                buffer_[end++] = (byte)bits;
                buffer_[end++] = (byte)(bits >> 8);
                bits = bits >> 0x10;
                bitCount -= 0x10;
            }
        }

        public void WriteBlock(byte[] block, int offset, int length)
        {
            Array.Copy(block, offset, buffer_, end, length);
            end += length;
        }

        public void WriteByte(int value)
        {
            buffer_[end++] = (byte)value;
        }

        public void WriteInt(int value)
        {
            buffer_[end++] = (byte)value;
            buffer_[end++] = (byte)(value >> 8);
            buffer_[end++] = (byte)(value >> 0x10);
            buffer_[end++] = (byte)(value >> 0x18);
        }

        public void WriteShort(int value)
        {
            buffer_[end++] = (byte)value;
            buffer_[end++] = (byte)(value >> 8);
        }

        public void WriteShortMSB(int s)
        {
            buffer_[end++] = (byte)(s >> 8);
            buffer_[end++] = (byte)s;
        }

        public int BitCount
        {
            get
            {
                return bitCount;
            }
        }

        public bool IsFlushed
        {
            get
            {
                return (end == 0);
            }
        }
    }
}

