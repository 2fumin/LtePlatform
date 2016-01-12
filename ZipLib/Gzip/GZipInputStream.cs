using System.IO;
using ZipLib.CheckSums;
using ZipLib.Comppression;
using ZipLib.Streams;

namespace ZipLib.Gzip
{
    public class GZipInputStream : InflaterInputStream
    {
        private Crc32 _crc;
        private bool _readGzipHeader;

        public GZipInputStream(Stream baseInputStream, int size = 0x1000)
            : base(baseInputStream, new Inflater(true), size)
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int num;
            do
            {
                if (!_readGzipHeader && !ReadHeader())
                {
                    return 0;
                }
                num = Read(buffer, offset, count);
                if (num > 0)
                {
                    _crc.Update(buffer, offset, num);
                }
                if (Inf.IsFinished)
                {
                    ReadFooter();
                }
            }
            while (num <= 0);
            return num;
        }

        private void ReadFooter()
        {
            int num3;
            byte[] outBuffer = new byte[8];
            long num = Inf.TotalOut & 0xffffffffL;
            InputBuffer.Available += Inf.RemainingInput;
            Inf.Reset();
            for (int i = 8; i > 0; i -= num3)
            {
                num3 = InputBuffer.ReadClearTextBuffer(outBuffer, 8 - i, i);
                if (num3 <= 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP footer");
                }
            }
            int num4 
                = (((outBuffer[0] & 0xff) | ((outBuffer[1] & 0xff) << 8))
                | ((outBuffer[2] & 0xff) << GZipConstants.FCOMMENT)) | (outBuffer[3] << 0x18);
            if (num4 != ((int)_crc.Value))
            {
                throw new GZipException(string.Concat(new object[]
                {
                    "GZIP crc sum mismatch, theirs \"", num4, "\" and ours \"", (int)_crc.Value
                }));
            }
            uint num5 = (uint)((((outBuffer[4] & 0xff) | ((outBuffer[5] & 0xff) << 8)) 
                | ((outBuffer[6] & 0xff) << GZipConstants.FCOMMENT)) | (outBuffer[7] << 0x18));
            if (num != num5)
            {
                throw new GZipException("Number of bytes mismatch in footer");
            }
            _readGzipHeader = false;
        }

        private bool ReadHeader()
        {
            _crc = new Crc32();
            if (InputBuffer.Available <= 0)
            {
                InputBuffer.Fill();
                if (InputBuffer.Available <= 0)
                {
                    return false;
                }
            }
            Crc32 crc32 = new Crc32();
            int num = InputBuffer.ReadLeByte();
            if (num < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }
            crc32.Update(num);
            if (num != 0x1f)
            {
                throw new GZipException("Error GZIP header, first magic byte doesn't match");
            }
            num = InputBuffer.ReadLeByte();
            if (num < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }
            if (num != 0x8b)
            {
                throw new GZipException("Error GZIP header,  second magic byte doesn't match");
            }
            crc32.Update(num);
            int num2 = InputBuffer.ReadLeByte();
            if (num2 < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }
            if (num2 != 8)
            {
                throw new GZipException("Error GZIP header, data not in deflate format");
            }
            crc32.Update(num2);
            int num3 = InputBuffer.ReadLeByte();
            if (num3 < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }
            crc32.Update(num3);
            if ((num3 & 0xe0) != 0)
            {
                throw new GZipException("Reserved flag bits in GZIP header != 0");
            }
            for (int i = 0; i < 6; i++)
            {
                int num5 = InputBuffer.ReadLeByte();
                if (num5 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                crc32.Update(num5);
            }
            if ((num3 & 4) != 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    int num7 = InputBuffer.ReadLeByte();
                    if (num7 < 0)
                    {
                        throw new EndOfStreamException("EOS reading GZIP header");
                    }
                    crc32.Update(num7);
                }
                if ((InputBuffer.ReadLeByte() < 0) || (InputBuffer.ReadLeByte() < 0))
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                int num8 = InputBuffer.ReadLeByte();
                int num9 = InputBuffer.ReadLeByte();
                if ((num8 < 0) || (num9 < 0))
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                crc32.Update(num8);
                crc32.Update(num9);
                int num10 = (num8 << 8) | num9;
                for (int k = 0; k < num10; k++)
                {
                    int num12 = InputBuffer.ReadLeByte();
                    if (num12 < 0)
                    {
                        throw new EndOfStreamException("EOS reading GZIP header");
                    }
                    crc32.Update(num12);
                }
            }
            if ((num3 & 8) != 0)
            {
                int num13;
                while ((num13 = InputBuffer.ReadLeByte()) > 0)
                {
                    crc32.Update(num13);
                }
                if (num13 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                crc32.Update(num13);
            }
            if ((num3 & GZipConstants.FCOMMENT) != 0)
            {
                int num14;
                while ((num14 = InputBuffer.ReadLeByte()) > 0)
                {
                    crc32.Update(num14);
                }
                if (num14 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                crc32.Update(num14);
            }
            if ((num3 & 2) != 0)
            {
                int num16 = InputBuffer.ReadLeByte();
                if (num16 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                int num15 = InputBuffer.ReadLeByte();
                if (num15 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                num16 = (num16 << 8) | num15;
                if (num16 != (((int)crc32.Value) & 0xffff))
                {
                    throw new GZipException("Header CRC value mismatch");
                }
            }
            _readGzipHeader = true;
            return true;
        }
    }
}
