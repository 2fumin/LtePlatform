using System;

namespace ZipLib.Streams
{
    public class StreamManipulator
    {
        private int _bitsInBuffer;
        private uint _buffer;
        private byte[] _window;
        private int _windowEnd;
        private int _windowStart;

        public int CopyBytes(byte[] output, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            if ((_bitsInBuffer & 7) != 0)
            {
                throw new InvalidOperationException("Bit buffer is not byte aligned!");
            }
            int num = 0;
            while ((_bitsInBuffer > 0) && (length > 0))
            {
                output[offset++] = (byte)_buffer;
                _buffer = _buffer >> 8;
                _bitsInBuffer -= 8;
                length--;
                num++;
            }
            if (length == 0)
            {
                return num;
            }
            int num2 = _windowEnd - _windowStart;
            if (length > num2)
            {
                length = num2;
            }
            Array.Copy(_window, _windowStart, output, offset, length);
            _windowStart += length;
            if (((_windowStart - _windowEnd) & 1) != 0)
            {
                _buffer = (uint)(_window[_windowStart++] & 0xff);
                _bitsInBuffer = 8;
            }
            return (num + length);
        }

        public void DropBits(int bitCount)
        {
            _buffer = _buffer >> bitCount;
            _bitsInBuffer -= bitCount;
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
            if (_bitsInBuffer < bitCount)
            {
                if (_windowStart == _windowEnd)
                {
                    return -1;
                }
                _buffer |= (uint)(((_window[_windowStart++] & 0xff) | ((_window[_windowStart++] & 0xff) << 8)) << _bitsInBuffer);
                _bitsInBuffer += 0x10;
            }
            return (((int)_buffer) & ((1 << bitCount) - 1));
        }

        public void Reset()
        {
            _buffer = 0;
            _windowStart = _windowEnd = _bitsInBuffer = 0;
        }

        public void SetInput(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            }
            if (_windowStart < _windowEnd)
            {
                throw new InvalidOperationException("Old input was not completely processed");
            }
            int num = offset + count;
            if ((offset > num) || (num > buffer.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if ((count & 1) != 0)
            {
                _buffer |= (uint)((buffer[offset++] & 0xff) << _bitsInBuffer);
                _bitsInBuffer += 8;
            }
            _window = buffer;
            _windowStart = offset;
            _windowEnd = num;
        }

        public void SkipToByteBoundary()
        {
            _buffer = _buffer >> (_bitsInBuffer & 7);
            _bitsInBuffer &= -8;
        }

        public int AvailableBits => _bitsInBuffer;

        public int AvailableBytes => ((_windowEnd - _windowStart) + (_bitsInBuffer >> 3));

        public bool IsNeedingInput => (_windowStart == _windowEnd);
    }
}

