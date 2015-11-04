using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common
{
    public sealed class BitArrayInputStream : Stream
    {
        public long bitPosition;
        private Stream byteStream;
        public int currentBit;
        public int currentByte;
        private StringBuilder haveReadBin = new StringBuilder();
        private byte[] streamArray;

        public BitArrayInputStream(Stream byteStream)
        {
            streamArray = new byte[byteStream.Length];
            byteStream.Read(streamArray, 0, streamArray.Length);
            byteStream.Position = 0L;
            bitPosition = 0L;
            this.byteStream = byteStream;
        }

        private string DecimalToBinary(int decimalNum)
        {
            string str = Convert.ToString(decimalNum, 2);
            int num = 8 - str.Length;
            for (int i = 0; i < num; i++)
            {
                str = '0' + str;
            }
            return str;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (currentBit == 0)
            {
                return byteStream.Read(buffer, offset, count);
            }
            int index = 0;
            while (((index < buffer.Length) && (index < byteStream.Length)) && (index < count))
            {
                buffer[index] = (byte)ReadByte();
                index++;
            }
            return index;
        }

        public int readBit()
        {
            if (bitPosition >= bitLength)
            {
                throw new Exception("越界");
            }
            if (currentBit == 0)
            {
                currentByte = byteStream.ReadByte();
            }
            currentBit++;
            int num = (currentByte >> (8 - currentBit)) & 1;
            if (currentBit > 7)
            {
                currentBit = 0;
            }
            bitPosition += 1L;
            return num;
        }

        public int readBits(int nBits)
        {
            int num = 0;
            for (int i = 0; (i < nBits) && (i <= 0x20); i++)
            {
                num = (num << 1) | readBit();
            }
            return num;
        }

        public void readBits(byte[] buffer, int offset, int bits)
        {
            int count = bits / 8;
            int nBits = bits % 8;
            Read(buffer, offset, count);
            if (nBits > 0)
            {
                buffer[count] = (byte)(readBits(nBits) << (8 - nBits));
            }
        }

        public string readBitString(int nBits)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nBits; i++)
            {
                builder.Append(readBit());
            }
            return (builder + "'B");
        }

        public override int ReadByte()
        {
            if (currentBit == 0)
            {
                return byteStream.ReadByte();
            }
            int num = byteStream.ReadByte();
            int num2 = ((currentByte << currentBit) | (num >> (8 - currentBit))) & 0xff;
            currentByte = num;
            return num2;
        }

        public string readByteString(int nBytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nBytes; i++)
            {
                builder.Append((char)ReadByte());
            }
            return (builder + "'B");
        }

        public string readOctetString(int nBits)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nBits; i++)
            {
                builder.Append(readBits(8).ToString("X2"));
            }
            return (builder + "'H");
        }

        public void Reverse()
        {
            byteStream.Position = 0L;
            bitPosition = 0L;
            currentBit = 0;
            currentByte = 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return -1L;
        }

        public override void SetLength(long value)
        {
        }

        public void skipUnreadedBits()
        {
            currentBit = 0;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
        }

        public Stream BaseStream
        {
            get
            {
                return byteStream;
            }
        }

        public string BinStr
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < streamArray.Length; i++)
                {
                    byte decimalNum = streamArray[i];
                    string str = string.Format("{0}[{1}]:{2}", i, decimalNum.ToString("X2"), DecimalToBinary(decimalNum));
                    builder.AppendLine(str);
                }
                return builder.ToString();
            }
        }

        public long bitLength
        {
            get
            {
                return (byteStream.Length * 8L);
            }
        }

        public override bool CanRead
        {
            get
            {
                return (bitPosition < bitLength);
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public string HaveReadBin
        {
            get
            {
                return haveReadBin.ToString();
            }
        }

        public string HexStr
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (byte num in streamArray)
                {
                    builder.Append(num.ToString("X2"));
                }
                return builder.ToString();
            }
        }

        public override long Length
        {
            get
            {
                return byteStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return byteStream.Position;
            }
            set
            {
                byteStream.Position = value;
            }
        }
    }
}
