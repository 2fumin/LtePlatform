using System;

namespace ZipLib.Streams
{
    public class StreamManipulator
    {
        private int bitsInBuffer_;
        private uint buffer_;
        private byte[] window_;
        private int windowEnd_;
        private int windowStart_;

        public int CopyBytes(byte[] output, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            if ((bitsInBuffer_ & 7) != 0)
            {
                throw new InvalidOperationException("Bit buffer is not byte aligned!");
            }
            int num = 0;
            while ((bitsInBuffer_ > 0) && (length > 0))
            {
                output[offset++] = (byte)buffer_;
                buffer_ = buffer_ >> 8;
                bitsInBuffer_ -= 8;
                length--;
                num++;
            }
            if (length == 0)
            {
                return num;
            }
            int num2 = windowEnd_ - windowStart_;
            if (length > num2)
            {
                length = num2;
            }
            Array.Copy(window_, windowStart_, output, offset, length);
            windowStart_ += length;
            if (((windowStart_ - windowEnd_) & 1) != 0)
            {
                buffer_ = (uint)(window_[windowStart_++] & 0xff);
                bitsInBuffer_ = 8;
            }
            return (num + length);
        }

        public void DropBits(int bitCount)
        {
            buffer_ = buffer_ >> bitCount;
            bitsInBuffer_ -= bitCount;
        }

        public int GetBits(int bitCount)
        {
            int num = PeekBits(bitCount);
            if (num >= 0)
            {
                DropBits(bitCount);
            }
            return num;
        }

        public int PeekBits(int bitCount)
        {
            if (bitsInBuffer_ < bitCount)
            {
                if (windowStart_ == windowEnd_)
                {
                    return -1;
                }
                buffer_ |= (uint)(((window_[windowStart_++] & 0xff) | ((window_[windowStart_++] & 0xff) << 8)) << bitsInBuffer_);
                bitsInBuffer_ += 0x10;
            }
            return (((int)buffer_) & ((1 << bitCount) - 1));
        }

        public void Reset()
        {
            buffer_ = 0;
            windowStart_ = windowEnd_ = bitsInBuffer_ = 0;
        }

        public void SetInput(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "Cannot be negative");
            }
            if (windowStart_ < windowEnd_)
            {
                throw new InvalidOperationException("Old input was not completely processed");
            }
            int num = offset + count;
            if ((offset > num) || (num > buffer.Length))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if ((count & 1) != 0)
            {
                buffer_ |= (uint)((buffer[offset++] & 0xff) << bitsInBuffer_);
                bitsInBuffer_ += 8;
            }
            window_ = buffer;
            windowStart_ = offset;
            windowEnd_ = num;
        }

        public void SkipToByteBoundary()
        {
            buffer_ = buffer_ >> (bitsInBuffer_ & 7);
            bitsInBuffer_ &= -8;
        }

        public int AvailableBits
        {
            get
            {
                return bitsInBuffer_;
            }
        }

        public int AvailableBytes
        {
            get
            {
                return ((windowEnd_ - windowStart_) + (bitsInBuffer_ >> 3));
            }
        }

        public bool IsNeedingInput
        {
            get
            {
                return (windowStart_ == windowEnd_);
            }
        }
    }
}

