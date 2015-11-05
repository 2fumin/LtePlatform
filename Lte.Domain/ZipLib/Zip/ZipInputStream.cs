using System;
using System.IO;
using Lte.Domain.Lz4Net.Encryption;
using Lte.Domain.ZipLib.CheckSums;
using Lte.Domain.ZipLib.Compression;
using Lte.Domain.ZipLib.Streams;

namespace Lte.Domain.ZipLib.Zip
{
    public class ZipInputStream : InflaterInputStream
    {
        private Crc32 crc;
        private ZipEntry entry;
        private int flags;
        private ReadDataHandler internalReader;
        private int method;
        private string password;
        private long size;

        public ZipInputStream(Stream baseInputStream)
            : base(baseInputStream, new Inflater(true))
        {
            crc = new Crc32();
            internalReader = ReadingNotAvailable;
        }

        public ZipInputStream(Stream baseInputStream, int bufferSize)
            : base(baseInputStream, new Inflater(true), bufferSize)
        {
            crc = new Crc32();
            internalReader = ReadingNotAvailable;
        }

        private int BodyRead(byte[] buffer, int offset, int count)
        {
            if (crc == null)
            {
                throw new InvalidOperationException("Closed");
            }
            if ((entry == null) || (count <= 0))
            {
                return 0;
            }
            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException("Offset + count exceeds buffer size");
            }
            bool flag = false;
            switch (method)
            {
                case 0:
                    if ((count > base.csize) && (base.csize >= 0L))
                    {
                        count = (int)base.csize;
                    }
                    if (count > 0)
                    {
                        count = base.inputBuffer.ReadClearTextBuffer(buffer, offset, count);
                        if (count > 0)
                        {
                            base.csize -= count;
                            size -= count;
                        }
                    }
                    if (base.csize == 0L)
                    {
                        flag = true;
                    }
                    else if (count < 0)
                    {
                        throw new ZipException("EOF in stored block");
                    }
                    break;

                case 8:
                    count = base.Read(buffer, offset, count);
                    if (count <= 0)
                    {
                        if (!inf.IsFinished)
                        {
                            throw new ZipException("Inflater not finished!");
                        }
                        inputBuffer.Available = inf.RemainingInput;
                        if (((flags & 8) == 0) && ((((base.inf.TotalIn != base.csize) && (base.csize != 0xffffffffL)) && (base.csize != -1L)) || (base.inf.TotalOut != size)))
                        {
                            throw new ZipException(string.Concat(new object[] { "Size mismatch: ", base.csize, ";", size, " <-> ", base.inf.TotalIn, ";", base.inf.TotalOut }));
                        }
                        base.inf.Reset();
                        flag = true;
                    }
                    break;
            }
            if (count > 0)
            {
                crc.Update(buffer, offset, count);
            }
            if (flag)
            {
                CompleteCloseEntry(true);
            }
            return count;
        }

        public override void Close()
        {
            internalReader = ReadingNotAvailable;
            crc = null;
            entry = null;
            base.Close();
        }

        public void CloseEntry()
        {
            if (crc == null)
            {
                throw new InvalidOperationException("Closed");
            }
            if (entry != null)
            {
                if (method == 8)
                {
                    if ((flags & 8) != 0)
                    {
                        byte[] buffer = new byte[0x1000];
                        while (Read(buffer, 0, buffer.Length) > 0)
                        {
                        }
                        return;
                    }
                    base.csize -= base.inf.TotalIn;
                    base.inputBuffer.Available += base.inf.RemainingInput;
                }
                if ((base.inputBuffer.Available > base.csize) && (base.csize >= 0L))
                {
                    base.inputBuffer.Available -= (int)base.csize;
                }
                else
                {
                    base.csize -= base.inputBuffer.Available;
                    base.inputBuffer.Available = 0;
                    while (base.csize != 0L)
                    {
                        long num = base.Skip(base.csize);
                        if (num <= 0L)
                        {
                            throw new ZipException("Zip archive ends early.");
                        }
                        base.csize -= num;
                    }
                }
                CompleteCloseEntry(false);
            }
        }

        private void CompleteCloseEntry(bool testCrc)
        {
            base.StopDecrypting();
            if ((flags & 8) != 0)
            {
                ReadDataDescriptor();
            }
            size = 0L;
            if ((testCrc && ((((ulong)crc.Value) & 0xffffffffL) != (ulong)entry.Crc)) && (entry.Crc != -1L))
            {
                throw new ZipException("CRC mismatch");
            }
            crc.Reset();
            if (method == 8)
            {
                base.inf.Reset();
            }
            entry = null;
        }

        public ZipEntry GetNextEntry()
        {
            if (crc == null)
            {
                throw new InvalidOperationException("Closed.");
            }
            if (entry != null)
            {
                CloseEntry();
            }
            int num = base.inputBuffer.ReadLeInt();
            switch (num)
            {
                case 0x2014b50:
                case 0x6054b50:
                case 0x5054b50:
                case 0x7064b50:
                case 0x6064b50:
                    Close();
                    return null;

                case 0x30304b50:
                case 0x8074b50:
                    num = base.inputBuffer.ReadLeInt();
                    break;
            }
            if (num != 0x4034b50)
            {
                throw new ZipException("Wrong Local header signature: 0x" + string.Format("{0:X}", num));
            }
            short versionRequiredToExtract = (short)base.inputBuffer.ReadLeShort();
            flags = base.inputBuffer.ReadLeShort();
            method = base.inputBuffer.ReadLeShort();
            uint num3 = (uint)base.inputBuffer.ReadLeInt();
            int num4 = base.inputBuffer.ReadLeInt();
            base.csize = base.inputBuffer.ReadLeInt();
            size = base.inputBuffer.ReadLeInt();
            int num5 = base.inputBuffer.ReadLeShort();
            int num6 = base.inputBuffer.ReadLeShort();
            bool flag = (flags & 1) == 1;
            byte[] buffer = new byte[num5];
            base.inputBuffer.ReadRawBuffer(buffer);
            string name = ZipConstants.ConvertToStringExt(flags, buffer);
            entry = new ZipEntry(name, versionRequiredToExtract);
            entry.Flags = flags;
            entry.CompressionMethod = (CompressionMethod)method;
            if ((flags & 8) == 0)
            {
                entry.Crc = num4 & 0xffffffffL;
                entry.Size = size & 0xffffffffL;
                entry.CompressedSize = base.csize & 0xffffffffL;
                entry.CryptoCheckValue = (byte)((num4 >> 0x18) & 0xff);
            }
            else
            {
                if (num4 != 0)
                {
                    entry.Crc = num4 & 0xffffffffL;
                }
                if (size != 0L)
                {
                    entry.Size = size & 0xffffffffL;
                }
                if (csize != 0L)
                {
                    entry.CompressedSize = csize & 0xffffffffL;
                }
                entry.CryptoCheckValue = (byte)((num3 >> 8) & 0xff);
            }
            entry.DosTime = num3;
            if (num6 > 0)
            {
                byte[] buffer2 = new byte[num6];
                inputBuffer.ReadRawBuffer(buffer2);
                entry.ExtraData = buffer2;
            }
            entry.ProcessExtraData(true);
            if (entry.CompressedSize >= 0L)
            {
                base.csize = entry.CompressedSize;
            }
            if (entry.Size >= 0L)
            {
                size = entry.Size;
            }
            if ((method == 0) && ((!flag && (base.csize != size)) || (flag && ((base.csize - 12L) != size))))
            {
                throw new ZipException("Stored, but compressed != uncompressed");
            }
            if (entry.IsCompressionMethodSupported())
            {
                internalReader = InitialRead;
            }
            else
            {
                internalReader = ReadingNotSupported;
            }
            return entry;
        }

        private int InitialRead(byte[] destination, int offset, int count)
        {
            if (!CanDecompressEntry)
            {
                throw new ZipException("Library cannot extract this entry. Version required is (" 
                    + entry.Version.ToString() + ")");
            }
            if (entry.IsCrypted)
            {
                if (password == null)
                {
                    throw new ZipException("No password set.");
                }
                PkzipClassicManaged managed = new PkzipClassicManaged();
                byte[] rgbKey = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(password));
                base.inputBuffer.CryptoTransform = managed.CreateDecryptor(rgbKey, null);
                byte[] outBuffer = new byte[12];
                base.inputBuffer.ReadClearTextBuffer(outBuffer, 0, 12);
                if (outBuffer[11] != entry.CryptoCheckValue)
                {
                    throw new ZipException("Invalid password");
                }
                if (base.csize < 12L)
                {
                    if ((entry.Flags & 8) == 0)
                    {
                        throw new ZipException(string.Format("Entry compressed size {0} too small for encryption", base.csize));
                    }
                }
                else
                {
                    base.csize -= 12L;
                }
            }
            else
            {
                base.inputBuffer.CryptoTransform = null;
            }
            if ((base.csize > 0L) || ((flags & 8) != 0))
            {
                if ((method == 8) && (base.inputBuffer.Available > 0))
                {
                    base.inputBuffer.SetInflaterInput(base.inf);
                }
                internalReader = BodyRead;
                return BodyRead(destination, offset, count);
            }
            internalReader = ReadingNotAvailable;
            return 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "Cannot be negative");
            }
            if ((buffer.Length - offset) < count)
            {
                throw new ArgumentException("Invalid offset/count combination");
            }
            return internalReader(buffer, offset, count);
        }

        public override int ReadByte()
        {
            byte[] buffer = new byte[1];
            if (Read(buffer, 0, 1) <= 0)
            {
                return -1;
            }
            return (buffer[0] & 0xff);
        }

        private void ReadDataDescriptor()
        {
            if (base.inputBuffer.ReadLeInt() != 0x8074b50)
            {
                throw new ZipException("Data descriptor signature not found");
            }
            entry.Crc = base.inputBuffer.ReadLeInt() & 0xffffffffL;
            if (entry.LocalHeaderRequiresZip64)
            {
                base.csize = base.inputBuffer.ReadLeLong();
                size = base.inputBuffer.ReadLeLong();
            }
            else
            {
                base.csize = base.inputBuffer.ReadLeInt();
                size = base.inputBuffer.ReadLeInt();
            }
            entry.CompressedSize = base.csize;
            entry.Size = size;
        }

        private int ReadingNotAvailable(byte[] destination, int offset, int count)
        {
            throw new InvalidOperationException("Unable to read from this stream");
        }

        private int ReadingNotSupported(byte[] destination, int offset, int count)
        {
            throw new ZipException("The compression method for this entry is not supported");
        }

        public override int Available
        {
            get
            {
                if (entry == null)
                {
                    return 0;
                }
                return 1;
            }
        }

        public bool CanDecompressEntry
        {
            get
            {
                return ((entry != null) && entry.CanDecompress);
            }
        }

        public override long Length
        {
            get
            {
                if (entry == null)
                {
                    throw new InvalidOperationException("No current entry");
                }
                if (entry.Size < 0L)
                {
                    throw new ZipException("Length not available for the current entry");
                }
                return entry.Size;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        private delegate int ReadDataHandler(byte[] b, int offset, int length);
    }
}
