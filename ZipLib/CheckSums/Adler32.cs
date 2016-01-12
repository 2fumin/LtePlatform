using System;

namespace ZipLib.CheckSums
{
    public sealed class Adler32 : IChecksum
    {
        private const uint Base = 0xfff1;
        private uint _checksum;

        public Adler32()
        {
            Reset();
        }

        public void Reset()
        {
            _checksum = 1;
        }

        public void Update(int value)
        {
            uint num = _checksum & 0xffff;
            uint num2 = _checksum >> 0x10;
            num = (uint)((num + (value & 0xff)) % Base);
            num2 = (num + num2) % Base;
            _checksum = (num2 << 0x10) + num;
        }

        public void Update(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            Update(buffer, 0, buffer.Length);
        }

        public void Update(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "cannot be negative");
            }
            if (offset >= buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "not a valid index into buffer");
            }
            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "exceeds buffer size");
            }
            uint num = _checksum & 0xffff;
            uint num2 = _checksum >> 0x10;
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
                num = num % Base;
                num2 = num2 % Base;
            }
            _checksum = (num2 << 0x10) | num;
        }

        public long Value
        {
            get
            {
                return _checksum;
            }
        }
    }
}

