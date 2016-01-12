using System.IO;
using Lte.Domain.ZipLib.CheckSums;
using Lte.Domain.ZipLib.Compression;
using Lte.Domain.ZipLib.Streams;

namespace Lte.Domain.ZipLib.Gzip
{
    public class GZipInputStream : InflaterInputStream
    {
        protected Crc32 crc;
        private bool readGZIPHeader;

        public GZipInputStream(Stream baseInputStream, int size = 0x1000)
            : base(baseInputStream, new Inflater(true), size)
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int num;
            do
            {
                if (!readGZIPHeader && !ReadHeader())
                {
                    return 0;
                }
                num = Read(buffer, offset, count);
                if (num > 0)
                {
                    crc.Update(buffer, offset, num);
                }
                if (inf.IsFinished)
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
            long num = inf.TotalOut & 0xffffffffL;
            inputBuffer.Available += inf.RemainingInput;
            inf.Reset();
            for (int i = 8; i > 0; i -= num3)
            {
                num3 = inputBuffer.ReadClearTextBuffer(outBuffer, 8 - i, i);
                if (num3 <= 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP footer");
                }
            }
            int num4 
                = (((outBuffer[0] & 0xff) | ((outBuffer[1] & 0xff) << 8))
                | ((outBuffer[2] & 0xff) << GZipConstants.FCOMMENT)) | (outBuffer[3] << 0x18);
            if (num4 != ((int)crc.Value))
            {
                throw new GZipException(string.Concat(new object[]
                {
                    "GZIP crc sum mismatch, theirs \"", num4, "\" and ours \"", (int)crc.Value
                }));
            }
            uint num5 = (uint)((((outBuffer[4] & 0xff) | ((outBuffer[5] & 0xff) << 8)) 
                | ((outBuffer[6] & 0xff) << GZipConstants.FCOMMENT)) | (outBuffer[7] << 0x18));
            if (num != num5)
            {
                throw new GZipException("Number of bytes mismatch in footer");
            }
            readGZIPHeader = false;
        }

        private bool ReadHeader()
        {
            crc = new Crc32();
            if (inputBuffer.Available <= 0)
            {
                inputBuffer.Fill();
                if (inputBuffer.Available <= 0)
                {
                    return false;
                }
            }
            Crc32 crc32 = new Crc32();
            int num = inputBuffer.ReadLeByte();
            if (num < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }
            crc32.Update(num);
            if (num != 0x1f)
            {
                throw new GZipException("Error GZIP header, first magic byte doesn't match");
            }
            num = inputBuffer.ReadLeByte();
            if (num < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }
            if (num != 0x8b)
            {
                throw new GZipException("Error GZIP header,  second magic byte doesn't match");
            }
            crc32.Update(num);
            int num2 = inputBuffer.ReadLeByte();
            if (num2 < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }
            if (num2 != 8)
            {
                throw new GZipException("Error GZIP header, data not in deflate format");
            }
            crc32.Update(num2);
            int num3 = inputBuffer.ReadLeByte();
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
                int num5 = inputBuffer.ReadLeByte();
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
                    int num7 = inputBuffer.ReadLeByte();
                    if (num7 < 0)
                    {
                        throw new EndOfStreamException("EOS reading GZIP header");
                    }
                    crc32.Update(num7);
                }
                if ((inputBuffer.ReadLeByte() < 0) || (inputBuffer.ReadLeByte() < 0))
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                int num8 = inputBuffer.ReadLeByte();
                int num9 = inputBuffer.ReadLeByte();
                if ((num8 < 0) || (num9 < 0))
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                crc32.Update(num8);
                crc32.Update(num9);
                int num10 = (num8 << 8) | num9;
                for (int k = 0; k < num10; k++)
                {
                    int num12 = inputBuffer.ReadLeByte();
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
                while ((num13 = inputBuffer.ReadLeByte()) > 0)
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
                while ((num14 = inputBuffer.ReadLeByte()) > 0)
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
                int num16 = inputBuffer.ReadLeByte();
                if (num16 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }
                int num15 = inputBuffer.ReadLeByte();
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
            readGZIPHeader = true;
            return true;
        }
    }
}
