using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common
{
    public sealed class BigEndianBinaryReader : BinaryReader
    {
        private char[] _charBuffer;
        private byte[] ByteBuffer;

        public BigEndianBinaryReader(Stream input)
            : base(input, Encoding.ASCII)
        {
            _charBuffer = null;
            GetInternalBuffer();
        }

        public BigEndianBinaryReader(Stream input, Encoding encoding)
            : base(input, encoding)
        {
            _charBuffer = null;
            GetInternalBuffer();
        }

        public bool Eof()
        {
            return (BaseStream.Position == BaseStream.Length);
        }

        protected override void FillBuffer(int numBytes)
        {
            base.FillBuffer(numBytes);
            switch (numBytes)
            {
                case 1:
                    return;

                case 2:
                    Swap(0, 1);
                    return;

                case 4:
                    Swap(0, 3);
                    Swap(1, 2);
                    return;

                case 8:
                    Swap(0, 7);
                    Swap(1, 6);
                    Swap(2, 5);
                    Swap(3, 4);
                    return;
            }
            throw new InvalidOperationException();
        }

        private void GetInternalBuffer()
        {
            var baseType = GetType().BaseType;
            if (baseType != null)
            {
                FieldInfo field = baseType.GetField("m_buffer", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null) ByteBuffer = (byte[])field.GetValue(this);
            }
        }

        public string ReadHex(int len)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                builder.Append(ReadByte().ToString("X2") + " ");
            }
            return builder.ToString();
        }

        public string ReadString(int len)
        {
            int num;
            _charBuffer = new char[0x100];
            if (len <= _charBuffer.Length)
            {
                num = Read(_charBuffer, 0, len);
                return ((num <= 0) ? null : new string(_charBuffer, 0, num));
            }
            StringBuilder builder = null;
            while (len > 0)
            {
                num = Read(_charBuffer, 0, Math.Min(_charBuffer.Length, len));
                if (num <= 0)
                {
                    break;
                }
                if (builder == null)
                {
                    builder = new StringBuilder(len);
                }
                builder.Append(_charBuffer, 0, num);
                len -= num;
            }
            if (builder == null)
            {
                return null;
            }
            for (int i = builder.Length - 1; i >= 0; i--)
            {
                if ((builder[i] != '\0') && (builder[i] != ' '))
                {
                    builder.Length = i + 1;
                    break;
                }
            }
            return builder.ToString();
        }

        public uint ReadUInts(int len)
        {
            switch (len)
            {
                case 1:
                    return ReadByte();

                case 2:
                    return ReadUInt16();

                case 3:
                    {
                        byte[] buffer = new byte[4];
                        Read(buffer, 1, 3);
                        return BitConverter.ToUInt32(buffer.Reverse().ToArray(), 0);
                    }
                case 4:
                    return ReadUInt32();
            }
            throw new Exception("无效的数值长度!");
        }

        private void Swap(int i, int j)
        {
            byte num = ByteBuffer[i];
            ByteBuffer[i] = ByteBuffer[j];
            ByteBuffer[j] = num;
        }
    }
}
