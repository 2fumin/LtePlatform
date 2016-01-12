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
        private Crc32 _crc;
        private ZipEntry _entry;
        private int _flags;
        private ReadDataHandler _internalReader;
        private int _method;
        private string _password;
        private long _size;

        public ZipInputStream(Stream baseInputStream)
            : base(baseInputStream, new Inflater(true))
        {
            _crc = new Crc32();
            _internalReader = ReadingNotAvailable;
        }

        public ZipInputStream(Stream baseInputStream, int bufferSize)
            : base(baseInputStream, new Inflater(true), bufferSize)
        {
            _crc = new Crc32();
            _internalReader = ReadingNotAvailable;
        }

        private int BodyRead(byte[] buffer, int offset, int count)
        {
            if (_crc == null)
            {
                throw new InvalidOperationException("Closed");
            }
            if ((_entry == null) || (count <= 0))
            {
                return 0;
            }
            if ((offset + count) > buffer.Length)
            {
                throw new ArgumentException("Offset + count exceeds buffer size");
            }
            var flag = false;
            switch (_method)
            {
                case 0:
                    if ((count > csize) && (csize >= 0L))
                    {
                        count = (int)csize;
                    }
                    if (count > 0)
                    {
                        count = inputBuffer.ReadClearTextBuffer(buffer, offset, count);
                        if (count > 0)
                        {
                            csize -= count;
                            _size -= count;
                        }
                    }
                    if (csize == 0L)
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
                        if (((_flags & 8) == 0) && ((((inf.TotalIn != csize) && (csize != 0xffffffffL)) && (csize != -1L)) || (inf.TotalOut != _size)))
                        {
                            throw new ZipException(string.Concat("Size mismatch: ", csize, ";", _size, " <-> ", inf.TotalIn, ";", inf.TotalOut));
                        }
                        inf.Reset();
                        flag = true;
                    }
                    break;
            }
            if (count > 0)
            {
                _crc.Update(buffer, offset, count);
            }
            if (flag)
            {
                CompleteCloseEntry(true);
            }
            return count;
        }

        public override void Close()
        {
            _internalReader = ReadingNotAvailable;
            _crc = null;
            _entry = null;
            base.Close();
        }

        public void CloseEntry()
        {
            if (_crc == null)
            {
                throw new InvalidOperationException("Closed");
            }
            if (_entry != null)
            {
                if (_method == 8)
                {
                    if ((_flags & 8) != 0)
                    {
                        var buffer = new byte[0x1000];
                        while (Read(buffer, 0, buffer.Length) > 0)
                        {
                        }
                        return;
                    }
                    csize -= inf.TotalIn;
                    inputBuffer.Available += inf.RemainingInput;
                }
                if ((inputBuffer.Available > csize) && (csize >= 0L))
                {
                    inputBuffer.Available -= (int)csize;
                }
                else
                {
                    csize -= inputBuffer.Available;
                    inputBuffer.Available = 0;
                    while (csize != 0L)
                    {
                        var num = Skip(csize);
                        if (num <= 0L)
                        {
                            throw new ZipException("Zip archive ends early.");
                        }
                        csize -= num;
                    }
                }
                CompleteCloseEntry();
            }
        }

        private void CompleteCloseEntry(bool testCrc = false)
        {
            StopDecrypting();
            if ((_flags & 8) != 0)
            {
                ReadDataDescriptor();
            }
            _size = 0L;
            if ((testCrc && ((((ulong)_crc.Value) & 0xffffffffL) != (ulong)_entry.Crc)) && (_entry.Crc != -1L))
            {
                throw new ZipException("CRC mismatch");
            }
            _crc.Reset();
            if (_method == 8)
            {
                inf.Reset();
            }
            _entry = null;
        }

        public ZipEntry GetNextEntry()
        {
            if (_crc == null)
            {
                throw new InvalidOperationException("Closed.");
            }
            if (_entry != null)
            {
                CloseEntry();
            }
            var num = inputBuffer.ReadLeInt();
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
                    num = inputBuffer.ReadLeInt();
                    break;
            }
            if (num != 0x4034b50)
            {
                throw new ZipException("Wrong Local header signature: 0x" + string.Format("{0:X}", num));
            }
            var versionRequiredToExtract = (short)inputBuffer.ReadLeShort();
            _flags = inputBuffer.ReadLeShort();
            _method = inputBuffer.ReadLeShort();
            var num3 = (uint)inputBuffer.ReadLeInt();
            var num4 = inputBuffer.ReadLeInt();
            csize = inputBuffer.ReadLeInt();
            _size = inputBuffer.ReadLeInt();
            var num5 = inputBuffer.ReadLeShort();
            var num6 = inputBuffer.ReadLeShort();
            var flag = (_flags & 1) == 1;
            var buffer = new byte[num5];
            inputBuffer.ReadRawBuffer(buffer);
            var name = ZipConstants.ConvertToStringExt(_flags, buffer);
            _entry = new ZipEntry(name, versionRequiredToExtract);
            _entry.Flags = _flags;
            _entry.CompressionMethod = (CompressionMethod)_method;
            if ((_flags & 8) == 0)
            {
                _entry.Crc = num4 & 0xffffffffL;
                _entry.Size = _size & 0xffffffffL;
                _entry.CompressedSize = csize & 0xffffffffL;
                _entry.CryptoCheckValue = (byte)((num4 >> 0x18) & 0xff);
            }
            else
            {
                if (num4 != 0)
                {
                    _entry.Crc = num4 & 0xffffffffL;
                }
                if (_size != 0L)
                {
                    _entry.Size = _size & 0xffffffffL;
                }
                if (csize != 0L)
                {
                    _entry.CompressedSize = csize & 0xffffffffL;
                }
                _entry.CryptoCheckValue = (byte)((num3 >> 8) & 0xff);
            }
            _entry.DosTime = num3;
            if (num6 > 0)
            {
                var buffer2 = new byte[num6];
                inputBuffer.ReadRawBuffer(buffer2);
                _entry.ExtraData = buffer2;
            }
            _entry.ProcessExtraData(true);
            if (_entry.CompressedSize >= 0L)
            {
                csize = _entry.CompressedSize;
            }
            if (_entry.Size >= 0L)
            {
                _size = _entry.Size;
            }
            if ((_method == 0) && ((!flag && (csize != _size)) || (flag && ((csize - 12L) != _size))))
            {
                throw new ZipException("Stored, but compressed != uncompressed");
            }
            if (_entry.IsCompressionMethodSupported())
            {
                _internalReader = InitialRead;
            }
            else
            {
                _internalReader = ReadingNotSupported;
            }
            return _entry;
        }

        private int InitialRead(byte[] destination, int offset, int count)
        {
            if (!CanDecompressEntry)
            {
                throw new ZipException("Library cannot extract this entry. Version required is (" 
                    + _entry.Version + ")");
            }
            if (_entry.IsCrypted)
            {
                if (_password == null)
                {
                    throw new ZipException("No password set.");
                }
                var managed = new PkzipClassicManaged();
                var rgbKey = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(_password));
                inputBuffer.CryptoTransform = managed.CreateDecryptor(rgbKey, null);
                var outBuffer = new byte[12];
                inputBuffer.ReadClearTextBuffer(outBuffer, 0, 12);
                if (outBuffer[11] != _entry.CryptoCheckValue)
                {
                    throw new ZipException("Invalid password");
                }
                if (csize < 12L)
                {
                    if ((_entry.Flags & 8) == 0)
                    {
                        throw new ZipException($"Entry compressed size {csize} too small for encryption");
                    }
                }
                else
                {
                    csize -= 12L;
                }
            }
            else
            {
                inputBuffer.CryptoTransform = null;
            }
            if ((csize > 0L) || ((_flags & 8) != 0))
            {
                if ((_method == 8) && (inputBuffer.Available > 0))
                {
                    inputBuffer.SetInflaterInput(inf);
                }
                _internalReader = BodyRead;
                return BodyRead(destination, offset, count);
            }
            _internalReader = ReadingNotAvailable;
            return 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
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
            if ((buffer.Length - offset) < count)
            {
                throw new ArgumentException("Invalid offset/count combination");
            }
            return _internalReader(buffer, offset, count);
        }

        public override int ReadByte()
        {
            var buffer = new byte[1];
            if (Read(buffer, 0, 1) <= 0)
            {
                return -1;
            }
            return (buffer[0] & 0xff);
        }

        private void ReadDataDescriptor()
        {
            if (inputBuffer.ReadLeInt() != 0x8074b50)
            {
                throw new ZipException("Data descriptor signature not found");
            }
            _entry.Crc = inputBuffer.ReadLeInt() & 0xffffffffL;
            if (_entry.LocalHeaderRequiresZip64)
            {
                csize = inputBuffer.ReadLeLong();
                _size = inputBuffer.ReadLeLong();
            }
            else
            {
                csize = inputBuffer.ReadLeInt();
                _size = inputBuffer.ReadLeInt();
            }
            _entry.CompressedSize = csize;
            _entry.Size = _size;
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
                if (_entry == null)
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
                return ((_entry != null) && _entry.CanDecompress);
            }
        }

        public override long Length
        {
            get
            {
                if (_entry == null)
                {
                    throw new InvalidOperationException("No current entry");
                }
                if (_entry.Size < 0L)
                {
                    throw new ZipException("Length not available for the current entry");
                }
                return _entry.Size;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private delegate int ReadDataHandler(byte[] b, int offset, int length);
    }
}
