using System;
using System.IO;
using ZipLib.CheckSums;
using ZipLib.Comppression;
using ZipLib.Encryption;
using ZipLib.Streams;

namespace ZipLib.Zip
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
                    if ((count > Csize) && (Csize >= 0L))
                    {
                        count = (int)Csize;
                    }
                    if (count > 0)
                    {
                        count = InputBuffer.ReadClearTextBuffer(buffer, offset, count);
                        if (count > 0)
                        {
                            Csize -= count;
                            _size -= count;
                        }
                    }
                    if (Csize == 0L)
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
                        if (!Inf.IsFinished)
                        {
                            throw new ZipException("Inflater not finished!");
                        }
                        InputBuffer.Available = Inf.RemainingInput;
                        if (((_flags & 8) == 0) && ((((Inf.TotalIn != Csize) && (Csize != 0xffffffffL)) && (Csize != -1L)) || (Inf.TotalOut != _size)))
                        {
                            throw new ZipException(string.Concat("Size mismatch: ", Csize, ";", _size, " <-> ", Inf.TotalIn, ";", Inf.TotalOut));
                        }
                        Inf.Reset();
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
                    Csize -= Inf.TotalIn;
                    InputBuffer.Available += Inf.RemainingInput;
                }
                if ((InputBuffer.Available > Csize) && (Csize >= 0L))
                {
                    InputBuffer.Available -= (int)Csize;
                }
                else
                {
                    Csize -= InputBuffer.Available;
                    InputBuffer.Available = 0;
                    while (Csize != 0L)
                    {
                        var num = Skip(Csize);
                        if (num <= 0L)
                        {
                            throw new ZipException("Zip archive ends early.");
                        }
                        Csize -= num;
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
                Inf.Reset();
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
            var num = InputBuffer.ReadLeInt();
            switch (num)
            {
                case ZipConstants.CentralHeaderSignature:
                case ZipConstants.EndOfCentralDirectorySignature:
                case ZipConstants.CentralHeaderDigitalSignature:
                case ZipConstants.ArchiveExtraDataSignature:
                case 0x6064b50:
                    Close();
                    return null;

                case 0x30304b50:
                case ZipConstants.SpanningSignature:
                    num = InputBuffer.ReadLeInt();
                    break;
            }
            if (num != ZipConstants.LocalHeaderSignature)
            {
                throw new ZipException("Wrong Local header signature: 0x" + $"{num:X}");
            }
            var versionRequiredToExtract = (short)InputBuffer.ReadLeShort();
            _flags = InputBuffer.ReadLeShort();
            _method = InputBuffer.ReadLeShort();
            var num3 = (uint)InputBuffer.ReadLeInt();
            var num4 = InputBuffer.ReadLeInt();
            Csize = InputBuffer.ReadLeInt();
            _size = InputBuffer.ReadLeInt();
            var num5 = InputBuffer.ReadLeShort();
            var num6 = InputBuffer.ReadLeShort();
            var flag = (_flags & 1) == 1;
            var buffer = new byte[num5];
            InputBuffer.ReadRawBuffer(buffer);
            var name = ZipConstants.ConvertToStringExt(_flags, buffer);
            _entry = new ZipEntry(name, versionRequiredToExtract)
            {
                Flags = _flags,
                CompressionMethod = (CompressionMethod) _method
            };
            if ((_flags & 8) == 0)
            {
                _entry.Crc = num4 & 0xffffffffL;
                _entry.Size = _size & 0xffffffffL;
                _entry.CompressedSize = Csize & 0xffffffffL;
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
                if (Csize != 0L)
                {
                    _entry.CompressedSize = Csize & 0xffffffffL;
                }
                _entry.CryptoCheckValue = (byte)((num3 >> 8) & 0xff);
            }
            _entry.DosTime = num3;
            if (num6 > 0)
            {
                var buffer2 = new byte[num6];
                InputBuffer.ReadRawBuffer(buffer2);
                _entry.ExtraData = buffer2;
            }
            _entry.ProcessExtraData(true);
            if (_entry.CompressedSize >= 0L)
            {
                Csize = _entry.CompressedSize;
            }
            if (_entry.Size >= 0L)
            {
                _size = _entry.Size;
            }
            if ((_method == 0) && ((!flag && (Csize != _size)) || (flag && ((Csize - 12L) != _size))))
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
                InputBuffer.CryptoTransform = managed.CreateDecryptor(rgbKey, null);
                var outBuffer = new byte[12];
                InputBuffer.ReadClearTextBuffer(outBuffer, 0, 12);
                if (outBuffer[11] != _entry.CryptoCheckValue)
                {
                    throw new ZipException("Invalid password");
                }
                if (Csize < 12L)
                {
                    if ((_entry.Flags & 8) == 0)
                    {
                        throw new ZipException($"Entry compressed size {Csize} too small for encryption");
                    }
                }
                else
                {
                    Csize -= 12L;
                }
            }
            else
            {
                InputBuffer.CryptoTransform = null;
            }
            if ((Csize > 0L) || ((_flags & 8) != 0))
            {
                if ((_method == 8) && (InputBuffer.Available > 0))
                {
                    InputBuffer.SetInflaterInput(Inf);
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
            if (InputBuffer.ReadLeInt() != ZipConstants.DataDescriptorSignature)
            {
                throw new ZipException("Data descriptor signature not found");
            }
            _entry.Crc = InputBuffer.ReadLeInt() & 0xffffffffL;
            if (_entry.LocalHeaderRequiresZip64)
            {
                Csize = InputBuffer.ReadLeLong();
                _size = InputBuffer.ReadLeLong();
            }
            else
            {
                Csize = InputBuffer.ReadLeInt();
                _size = InputBuffer.ReadLeInt();
            }
            _entry.CompressedSize = Csize;
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

        public bool CanDecompressEntry => ((_entry != null) && _entry.CanDecompress);

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
