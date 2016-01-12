using System;
using System.IO;
using System.Security.Cryptography;
using ZipLib.Comppression;
using ZipLib.Zip;

namespace ZipLib.Streams
{
    public class InflaterInputBuffer
    {
        private int _available;
        private byte[] _clearText;
        private int _clearTextLength;
        private ICryptoTransform _cryptoTransform;
        private readonly Stream _inputStream;
        private byte[] _internalClearText;
        private readonly byte[] _rawData;
        private int _rawLength;

        public InflaterInputBuffer(Stream stream, int bufferSize = 0x1000)
        {
            _inputStream = stream;
            if (bufferSize < 0x400)
            {
                bufferSize = 0x400;
            }
            _rawData = new byte[bufferSize];
            _clearText = _rawData;
        }

        public void Fill()
        {
            int num2;
            _rawLength = 0;
            for (int i = _rawData.Length; i > 0; i -= num2)
            {
                num2 = _inputStream.Read(_rawData, _rawLength, i);
                if (num2 <= 0)
                {
                    break;
                }
                _rawLength += num2;
            }
            if (_cryptoTransform != null)
            {
                _clearTextLength = _cryptoTransform.TransformBlock(_rawData, 0, _rawLength, _clearText, 0);
            }
            else
            {
                _clearTextLength = _rawLength;
            }
            _available = _clearTextLength;
        }

        public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            int destinationIndex = offset;
            int num2 = length;
            while (num2 > 0)
            {
                if (_available <= 0)
                {
                    Fill();
                    if (_available <= 0)
                    {
                        return 0;
                    }
                }
                int num3 = Math.Min(num2, _available);
                Array.Copy(_clearText, _clearTextLength - _available, outBuffer, destinationIndex, num3);
                destinationIndex += num3;
                num2 -= num3;
                _available -= num3;
            }
            return length;
        }

        public int ReadLeByte()
        {
            if (_available <= 0)
            {
                Fill();
                if (_available <= 0)
                {
                    throw new ZipException("EOF in header");
                }
            }
            byte num = _rawData[_rawLength - _available];
            _available--;
            return num;
        }

        public int ReadLeInt()
        {
            return (ReadLeShort() | (ReadLeShort() << 0x10));
        }

        public long ReadLeLong()
        {
            return (((long)((ulong)ReadLeInt())) | (ReadLeInt() << 0x20));
        }

        public int ReadLeShort()
        {
            return (ReadLeByte() | (ReadLeByte() << 8));
        }

        public int ReadRawBuffer(byte[] buffer)
        {
            return ReadRawBuffer(buffer, 0, buffer.Length);
        }

        public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            int destinationIndex = offset;
            int num2 = length;
            while (num2 > 0)
            {
                if (_available <= 0)
                {
                    Fill();
                    if (_available <= 0)
                    {
                        return 0;
                    }
                }
                int num3 = Math.Min(num2, _available);
                Array.Copy(_rawData, _rawLength - _available, outBuffer, destinationIndex, num3);
                destinationIndex += num3;
                num2 -= num3;
                _available -= num3;
            }
            return length;
        }

        public void SetInflaterInput(Inflater inflater)
        {
            if (_available > 0)
            {
                inflater.SetInput(_clearText, _clearTextLength - _available, _available);
                _available = 0;
            }
        }

        public int Available
        {
            get
            {
                return _available;
            }
            set
            {
                _available = value;
            }
        }

        public byte[] ClearText
        {
            get
            {
                return _clearText;
            }
        }

        public int ClearTextLength
        {
            get
            {
                return _clearTextLength;
            }
        }

        public ICryptoTransform CryptoTransform
        {
            set
            {
                _cryptoTransform = value;
                if (_cryptoTransform != null)
                {
                    if (_rawData == _clearText)
                    {
                        if (_internalClearText == null)
                        {
                            _internalClearText = new byte[_rawData.Length];
                        }
                        _clearText = _internalClearText;
                    }
                    _clearTextLength = _rawLength;
                    if (_available > 0)
                    {
                        _cryptoTransform.TransformBlock(_rawData, _rawLength - _available, _available, _clearText, _rawLength - _available);
                    }
                }
                else
                {
                    _clearText = _rawData;
                    _clearTextLength = _rawLength;
                }
            }
        }

        public byte[] RawData
        {
            get
            {
                return _rawData;
            }
        }

        public int RawLength
        {
            get
            {
                return _rawLength;
            }
        }
    }
}

