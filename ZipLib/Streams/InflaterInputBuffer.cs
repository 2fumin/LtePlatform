using System;
using System.IO;
using System.Security.Cryptography;
using ZipLib.Comppression;
using ZipLib.Zip;

namespace ZipLib.Streams
{
    public class InflaterInputBuffer
    {
        private int available;
        private byte[] clearText;
        private int clearTextLength;
        private ICryptoTransform cryptoTransform;
        private Stream inputStream;
        private byte[] internalClearText;
        private byte[] rawData;
        private int rawLength;

        public InflaterInputBuffer(Stream stream, int bufferSize = 0x1000)
        {
            inputStream = stream;
            if (bufferSize < 0x400)
            {
                bufferSize = 0x400;
            }
            rawData = new byte[bufferSize];
            clearText = rawData;
        }

        public void Fill()
        {
            int num2;
            rawLength = 0;
            for (int i = rawData.Length; i > 0; i -= num2)
            {
                num2 = inputStream.Read(rawData, rawLength, i);
                if (num2 <= 0)
                {
                    break;
                }
                rawLength += num2;
            }
            if (cryptoTransform != null)
            {
                clearTextLength = cryptoTransform.TransformBlock(rawData, 0, rawLength, clearText, 0);
            }
            else
            {
                clearTextLength = rawLength;
            }
            available = clearTextLength;
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
                if (available <= 0)
                {
                    Fill();
                    if (available <= 0)
                    {
                        return 0;
                    }
                }
                int num3 = Math.Min(num2, available);
                Array.Copy(clearText, clearTextLength - available, outBuffer, destinationIndex, num3);
                destinationIndex += num3;
                num2 -= num3;
                available -= num3;
            }
            return length;
        }

        public int ReadLeByte()
        {
            if (available <= 0)
            {
                Fill();
                if (available <= 0)
                {
                    throw new ZipException("EOF in header");
                }
            }
            byte num = rawData[rawLength - available];
            available--;
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
                if (available <= 0)
                {
                    Fill();
                    if (available <= 0)
                    {
                        return 0;
                    }
                }
                int num3 = Math.Min(num2, available);
                Array.Copy(rawData, rawLength - available, outBuffer, destinationIndex, num3);
                destinationIndex += num3;
                num2 -= num3;
                available -= num3;
            }
            return length;
        }

        public void SetInflaterInput(Inflater inflater)
        {
            if (available > 0)
            {
                inflater.SetInput(clearText, clearTextLength - available, available);
                available = 0;
            }
        }

        public int Available
        {
            get
            {
                return available;
            }
            set
            {
                available = value;
            }
        }

        public byte[] ClearText
        {
            get
            {
                return clearText;
            }
        }

        public int ClearTextLength
        {
            get
            {
                return clearTextLength;
            }
        }

        public ICryptoTransform CryptoTransform
        {
            set
            {
                cryptoTransform = value;
                if (cryptoTransform != null)
                {
                    if (rawData == clearText)
                    {
                        if (internalClearText == null)
                        {
                            internalClearText = new byte[rawData.Length];
                        }
                        clearText = internalClearText;
                    }
                    clearTextLength = rawLength;
                    if (available > 0)
                    {
                        cryptoTransform.TransformBlock(rawData, rawLength - available, available, clearText, rawLength - available);
                    }
                }
                else
                {
                    clearText = rawData;
                    clearTextLength = rawLength;
                }
            }
        }

        public byte[] RawData
        {
            get
            {
                return rawData;
            }
        }

        public int RawLength
        {
            get
            {
                return rawLength;
            }
        }
    }
}

