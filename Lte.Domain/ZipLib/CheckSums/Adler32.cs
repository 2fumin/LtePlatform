using System;

namespace Lte.Domain.ZipLib.CheckSums
{
    public sealed class Adler32 : IChecksum
    {
        private const uint BASE = 0xfff1;
        private uint checksum;

        public Adler32()
        {
            Reset();
        }

        public void Reset()
        {
            checksum = 1;
        }

        public void Update(int value)
        {
            uint num = checksum & 0xffff;
            uint num2 = checksum >> 0x10;
            num = (uint)((num + (value & 0xff)) % BASE);
            num2 = (num + num2) % BASE;
            checksum = (num2 << 0x10) + num;
        }

        public void Update(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            Update(buffer, 0, buffer.Length);
        }

        public void Update(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "cannot be negative");
            }
            if (offset >= buffer.Length)
            {
                throw new ArgumentOutOfRangeException("offset", "not a valid index into buffer");
            }
            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentOutOfRangeException("count", "exceeds buffer size");
            }
            uint num = checksum & 0xffff;
            uint num2 = checksum >> 0x10;
            while (count > 0)
            {
                int num3 = 0xed8;
                if (num3 > count)
                {
                    num3 = count;
                }
                count -= num3;
                while (--num3 >= 0)
                {
                    num += (uint)(buffer[offset++] & 0xff);
                    num2 += num;
                }
                num = num % BASE;
                num2 = num2 % BASE;
            }
            checksum = (num2 << 0x10) | num;
        }

        public long Value
        {
            get
            {
                return checksum;
            }
        }
    }
}

