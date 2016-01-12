using System;

namespace ZipLib.Comppression
{
    public class PendingBuffer
    {
        private int _bitCount;
        private uint _bits;
        private readonly byte[] _buffer;
        private int _end;
        private int _start;

        protected PendingBuffer()
            : this(0x1000)
        {
        }

        protected PendingBuffer(int bufferSize)
        {
            _buffer = new byte[bufferSize];
        }

        public void AlignToByte()
        {
            if (_bitCount > 0)
            {
                _buffer[_end++] = (byte)_bits;
                if (_bitCount > 8)
                {
                    _buffer[_end++] = (byte)(_bits >> 8);
                }
            }
            _bits = 0;
            _bitCount = 0;
        }

        public int Flush(byte[] output, int offset, int length)
        {
            if (_bitCount >= 8)
            {
                _buffer[_end++] = (byte)_bits;
                _bits = _bits >> 8;
                _bitCount -= 8;
            }
            if (length > (_end - _start))
            {
                length = _end - _start;
                Array.Copy(_buffer, _start, output, offset, length);
                _start = 0;
                _end = 0;
                return length;
            }
            Array.Copy(_buffer, _start, output, offset, length);
            _start += length;
            return length;
        }

        public void Reset()
        {
            _start = _end = _bitCount = 0;
        }

        public byte[] ToByteArray()
        {
            byte[] destinationArray = new byte[_end - _start];
            Array.Copy(_buffer, _start, destinationArray, 0, destinationArray.Length);
            _start = 0;
            _end = 0;
            return destinationArray;
        }

        public void WriteBits(int b, int count)
        {
            _bits |= (uint)(b << _bitCount);
            _bitCount += count;
            if (_bitCount >= 0x10)
            {
                _buffer[_end++] = (byte)_bits;
                _buffer[_end++] = (byte)(_bits >> 8);
                _bits = _bits >> 0x10;
                _bitCount -= 0x10;
            }
        }

        public void WriteBlock(byte[] block, int offset, int length)
        {
            Array.Copy(block, offset, _buffer, _end, length);
            _end += length;
        }

        public void WriteByte(int value)
        {
            _buffer[_end++] = (byte)value;
        }

        public void WriteInt(int value)
        {
            _buffer[_end++] = (byte)value;
            _buffer[_end++] = (byte)(value >> 8);
            _buffer[_end++] = (byte)(value >> 0x10);
            _buffer[_end++] = (byte)(value >> 0x18);
        }

        public void WriteShort(int value)
        {
            _buffer[_end++] = (byte)value;
            _buffer[_end++] = (byte)(value >> 8);
        }

        public void WriteShortMsb(int s)
        {
            _buffer[_end++] = (byte)(s >> 8);
            _buffer[_end++] = (byte)s;
        }

        public int BitCount
        {
            get
            {
                return _bitCount;
            }
        }

        public bool IsFlushed
        {
            get
            {
                return (_end == 0);
            }
        }
    }
}

