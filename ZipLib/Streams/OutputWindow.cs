using System;

namespace ZipLib.Streams
{
    public class OutputWindow
    {
        private readonly byte[] _window = new byte[0x8000];
        private int _windowEnd;
        private int _windowFilled;

        public void CopyDict(byte[] dictionary, int offset, int length)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            if (_windowFilled > 0)
            {
                throw new InvalidOperationException();
            }
            if (length > 0x8000)
            {
                offset += length - 0x8000;
                length = 0x8000;
            }
            Array.Copy(dictionary, offset, _window, 0, length);
            _windowEnd = length & 0x7fff;
        }

        public int CopyOutput(byte[] output, int offset, int len)
        {
            int localEnd = _windowEnd;
            if (len > _windowFilled)
            {
                len = _windowFilled;
            }
            else
            {
                localEnd = ((_windowEnd - _windowFilled) + len) & 0x7fff;
            }
            int num2 = len;
            int length = len - localEnd;
            if (length > 0)
            {
                Array.Copy(_window, 0x8000 - length, output, offset, length);
                offset += length;
                len = localEnd;
            }
            Array.Copy(_window, localEnd - len, output, offset, len);
            _windowFilled -= num2;
            if (_windowFilled < 0)
            {
                throw new InvalidOperationException();
            }
            return num2;
        }

        public int CopyStored(StreamManipulator input, int length)
        {
            int num;
            length = Math.Min(Math.Min(length, 0x8000 - _windowFilled), input.AvailableBytes);
            int num2 = 0x8000 - _windowEnd;
            if (length > num2)
            {
                num = input.CopyBytes(_window, _windowEnd, num2);
                if (num == num2)
                {
                    num += input.CopyBytes(_window, 0, length - num2);
                }
            }
            else
            {
                num = input.CopyBytes(_window, _windowEnd, length);
            }
            _windowEnd = (_windowEnd + num) & 0x7fff;
            _windowFilled += num;
            return num;
        }

        public int GetAvailable()
        {
            return _windowFilled;
        }

        public int GetFreeSpace()
        {
            return (0x8000 - _windowFilled);
        }

        public void Repeat(int length, int distance)
        {
            _windowFilled += length;
            if (_windowFilled > 0x8000)
            {
                throw new InvalidOperationException("Window full");
            }
            int repStart = (_windowEnd - distance) & 0x7fff;
            int num2 = 0x8000 - length;
            if ((repStart > num2) || (_windowEnd >= num2))
            {
                SlowRepeat(repStart, length);
            }
            else if (length > distance)
            {
                while (length-- > 0)
                {
                    _window[_windowEnd++] = _window[repStart++];
                }
            }
            else
            {
                Array.Copy(_window, repStart, _window, _windowEnd, length);
                _windowEnd += length;
            }
        }

        public void Reset()
        {
            _windowFilled = _windowEnd = 0;
        }

        private void SlowRepeat(int repStart, int length)
        {
            while (length-- > 0)
            {
                _window[_windowEnd++] = _window[repStart++];
                _windowEnd &= 0x7fff;
                repStart &= 0x7fff;
            }
        }

        public void Write(int value)
        {
            if (_windowFilled++ == 0x8000)
            {
                throw new InvalidOperationException("Window full");
            }
            _window[_windowEnd++] = (byte)value;
            _windowEnd &= 0x7fff;
        }
    }
}

