using System;

namespace Lte.Domain.ZipLib.Streams
{
    public class OutputWindow
    {
        private byte[] window = new byte[0x8000];
        private int windowEnd;
        private int windowFilled;

        public void CopyDict(byte[] dictionary, int offset, int length)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (windowFilled > 0)
            {
                throw new InvalidOperationException();
            }
            if (length > 0x8000)
            {
                offset += length - 0x8000;
                length = 0x8000;
            }
            Array.Copy(dictionary, offset, window, 0, length);
            windowEnd = length & 0x7fff;
        }

        public int CopyOutput(byte[] output, int offset, int len)
        {
            int localEnd = windowEnd;
            if (len > windowFilled)
            {
                len = windowFilled;
            }
            else
            {
                localEnd = ((windowEnd - windowFilled) + len) & 0x7fff;
            }
            int num2 = len;
            int length = len - localEnd;
            if (length > 0)
            {
                Array.Copy(window, 0x8000 - length, output, offset, length);
                offset += length;
                len = localEnd;
            }
            Array.Copy(window, localEnd - len, output, offset, len);
            windowFilled -= num2;
            if (windowFilled < 0)
            {
                throw new InvalidOperationException();
            }
            return num2;
        }

        public int CopyStored(StreamManipulator input, int length)
        {
            int num;
            length = Math.Min(Math.Min(length, 0x8000 - windowFilled), input.AvailableBytes);
            int num2 = 0x8000 - windowEnd;
            if (length > num2)
            {
                num = input.CopyBytes(window, windowEnd, num2);
                if (num == num2)
                {
                    num += input.CopyBytes(window, 0, length - num2);
                }
            }
            else
            {
                num = input.CopyBytes(window, windowEnd, length);
            }
            windowEnd = (windowEnd + num) & 0x7fff;
            windowFilled += num;
            return num;
        }

        public int GetAvailable()
        {
            return windowFilled;
        }

        public int GetFreeSpace()
        {
            return (0x8000 - windowFilled);
        }

        public void Repeat(int length, int distance)
        {
            windowFilled += length;
            if (windowFilled > 0x8000)
            {
                throw new InvalidOperationException("Window full");
            }
            int repStart = (windowEnd - distance) & 0x7fff;
            int num2 = 0x8000 - length;
            if ((repStart > num2) || (windowEnd >= num2))
            {
                SlowRepeat(repStart, length);
            }
            else if (length > distance)
            {
                while (length-- > 0)
                {
                    window[windowEnd++] = window[repStart++];
                }
            }
            else
            {
                Array.Copy(window, repStart, window, windowEnd, length);
                windowEnd += length;
            }
        }

        public void Reset()
        {
            windowFilled = windowEnd = 0;
        }

        private void SlowRepeat(int repStart, int length)
        {
            while (length-- > 0)
            {
                window[windowEnd++] = window[repStart++];
                windowEnd &= 0x7fff;
                repStart &= 0x7fff;
            }
        }

        public void Write(int value)
        {
            if (windowFilled++ == 0x8000)
            {
                throw new InvalidOperationException("Window full");
            }
            window[windowEnd++] = (byte)value;
            windowEnd &= 0x7fff;
        }
    }
}

