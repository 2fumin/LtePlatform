using System;
using System.IO;

namespace ZipLib.Zip
{
    public class ZipEntry : ICloneable
    {
        private int _aesEncryptionStrength;
        private string _comment;
        private ulong _compressedSize;
        private uint _crc;
        private uint _dosTime;
        private int _externalFileAttributes;
        private byte[] _extra;
        private bool _forceZip64;
        private Known _known;
        private CompressionMethod _method;
        private ulong _size;
        private ushort _versionMadeBy;
        private ushort _versionToExtract;

        [Obsolete("Use Clone instead")]
        public ZipEntry(ZipEntry entry)
        {
            _externalFileAttributes = -1;
            _method = CompressionMethod.Deflated;
            ZipFileIndex = -1L;
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            _known = entry._known;
            Name = entry.Name;
            _size = entry._size;
            _compressedSize = entry._compressedSize;
            _crc = entry._crc;
            _dosTime = entry._dosTime;
            _method = entry._method;
            _comment = entry._comment;
            _versionToExtract = entry._versionToExtract;
            _versionMadeBy = entry._versionMadeBy;
            _externalFileAttributes = entry._externalFileAttributes;
            Flags = entry.Flags;
            ZipFileIndex = entry.ZipFileIndex;
            Offset = entry.Offset;
            _forceZip64 = entry._forceZip64;
            if (entry._extra != null)
            {
                _extra = new byte[entry._extra.Length];
                Array.Copy(entry._extra, 0, _extra, 0, entry._extra.Length);
            }
        }

        public ZipEntry(string name)
            : this(name, 0)
        {
        }

        internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo = 0x33, 
            CompressionMethod method = CompressionMethod.Deflated)
        {
            _externalFileAttributes = -1;
            this._method = CompressionMethod.Deflated;
            ZipFileIndex = -1L;
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (name.Length > 0xffff)
            {
                throw new ArgumentException("Name is too long", nameof(name));
            }
            if ((versionRequiredToExtract != 0) && (versionRequiredToExtract < 10))
            {
                throw new ArgumentOutOfRangeException(nameof(versionRequiredToExtract));
            }
            DateTime = DateTime.Now;
            this.Name = name;
            _versionMadeBy = (ushort)madeByInfo;
            _versionToExtract = (ushort)versionRequiredToExtract;
            this._method = method;
        }

        public static string CleanName(string name)
        {
            if (name == null)
            {
                return string.Empty;
            }
            if (Path.IsPathRooted(name))
            {
                name = name.Substring(Path.GetPathRoot(name).Length);
            }
            name = name.Replace(@"\", "/");
            while ((name.Length > 0) && (name[0] == '/'))
            {
                name = name.Remove(0, 1);
            }
            return name;
        }

        public object Clone()
        {
            ZipEntry entry = (ZipEntry)MemberwiseClone();
            if (_extra != null)
            {
                entry._extra = new byte[_extra.Length];
                Array.Copy(_extra, 0, entry._extra, 0, _extra.Length);
            }
            return entry;
        }

        public void ForceZip64()
        {
            _forceZip64 = true;
        }

        private bool HasDosAttributes(int attributes)
        {
            bool flag = ((((byte)(_known & Known.ExternalAttributes)) != 0) && ((HostSystem == 0) || (HostSystem == 10))) 
                && ((ExternalFileAttributes & attributes) == attributes);
            return flag;
        }

        public bool IsCompressionMethodSupported()
        {
            return IsCompressionMethodSupported(CompressionMethod);
        }

        public static bool IsCompressionMethodSupported(CompressionMethod method)
        {
            if (method != CompressionMethod.Deflated)
            {
                return (method == CompressionMethod.Stored);
            }
            return true;
        }

        public bool IsZip64Forced()
        {
            return _forceZip64;
        }

        private void ProcessAesExtraData(ZipExtraData extraData)
        {
            if (!extraData.Find(0x9901))
            {
                throw new ZipException("AES Extra Data missing");
            }
            _versionToExtract = 0x33;
            Flags |= 0x40;
            int valueLength = extraData.ValueLength;
            if (valueLength < 7)
            {
                throw new ZipException("AES Extra Data Length " + valueLength + " invalid.");
            }
            extraData.ReadShort();
            extraData.ReadShort();
            int num3 = extraData.ReadByte();
            int num4 = extraData.ReadShort();
            _aesEncryptionStrength = num3;
            _method = (CompressionMethod)num4;
        }

        internal void ProcessExtraData(bool localHeader)
        {
            ZipExtraData extraData = new ZipExtraData(_extra);
            if (extraData.Find(1))
            {
                _forceZip64 = true;
                if (extraData.ValueLength < 4)
                {
                    throw new ZipException("Extra data extended Zip64 information length is invalid");
                }
                if (localHeader || (_size == 0xffffffffL))
                {
                    _size = (ulong)extraData.ReadLong();
                }
                if (localHeader || (_compressedSize == 0xffffffffL))
                {
                    _compressedSize = (ulong)extraData.ReadLong();
                }
                if (!localHeader && (Offset == 0xffffffffL))
                {
                    Offset = extraData.ReadLong();
                }
            }
            else if (((_versionToExtract & 0xff) >= 0x2d) && ((_size == 0xffffffffL) || (_compressedSize == 0xffffffffL)))
            {
                throw new ZipException("Zip64 Extended information required but is missing.");
            }
            if (extraData.Find(10))
            {
                if (extraData.ValueLength < 4)
                {
                    throw new ZipException("NTFS Extra data invalid");
                }
                extraData.ReadInt();
                while (extraData.UnreadCount >= 4)
                {
                    int num = extraData.ReadShort();
                    int amount = extraData.ReadShort();
                    if (num == 1)
                    {
                        if (amount >= 0x18)
                        {
                            long fileTime = extraData.ReadLong();
                            extraData.ReadLong();
                            extraData.ReadLong();
                            DateTime = DateTime.FromFileTime(fileTime);
                        }
                        break;
                    }
                    extraData.Skip(amount);
                }
            }
            else if (extraData.Find(0x5455))
            {
                int valueLength = extraData.ValueLength;
                if (((extraData.ReadByte() & 1) != 0) && (valueLength >= 5))
                {
                    int seconds = extraData.ReadInt();
                    DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0);
                    DateTime = (time.ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0)).ToLocalTime();
                }
            }
            if (_method == CompressionMethod.WinZipAes)
            {
                ProcessAesExtraData(extraData);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        internal byte AesEncryptionStrength => (byte)_aesEncryptionStrength;

        public int AesKeySize
        {
            get
            {
                switch (_aesEncryptionStrength)
                {
                    case 0:
                        return 0;

                    case 1:
                        return 0x80;

                    case 2:
                        return 0xc0;

                    case 3:
                        return 0x100;
                }
                throw new ZipException("Invalid AESEncryptionStrength " + _aesEncryptionStrength);
            }
            set
            {
                switch (value)
                {
                    case 0:
                        _aesEncryptionStrength = 0;
                        return;

                    case 0x80:
                        _aesEncryptionStrength = 1;
                        return;

                    case 0x100:
                        _aesEncryptionStrength = 3;
                        return;
                }
                throw new ZipException("AESKeySize must be 0, 128 or 256: " + value);
            }
        }

        internal int AesOverheadSize => (12 + AesSaltLen);

        internal int AesSaltLen => (AesKeySize / 0x10);

        public bool CanDecompress
        {
            get
            {
                if ((Version > 0x33) || (((Version != 10) && (Version != 11)) && (((Version != 20) && (Version != 0x2d)) && (Version != 0x33))))
                {
                    return false;
                }
                return IsCompressionMethodSupported();
            }
        }

        public bool CentralHeaderRequiresZip64
        {
            get
            {
                if (!LocalHeaderRequiresZip64)
                {
                    return (Offset >= 0xffffffffL);
                }
                return true;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if ((value != null) && (value.Length > 0xffff))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "cannot exceed 65535");
                }
                _comment = value;
            }
        }

        public long CompressedSize
        {
            get
            {
                if (((byte)(_known & Known.CompressedSize)) == 0)
                {
                    return -1L;
                }
                return (long)_compressedSize;
            }
            set
            {
                _compressedSize = (ulong)value;
                _known = (Known)((byte)(_known | Known.CompressedSize));
            }
        }

        public CompressionMethod CompressionMethod
        {
            get
            {
                return _method;
            }
            set
            {
                if (!IsCompressionMethodSupported(value))
                {
                    throw new NotSupportedException("Compression method not supported");
                }
                _method = value;
            }
        }

        internal CompressionMethod CompressionMethodForHeader
        {
            get
            {
                if (AesKeySize <= 0)
                {
                    return _method;
                }
                return CompressionMethod.WinZipAes;
            }
        }

        public long Crc
        {
            get
            {
                if (((byte)(_known & Known.Crc)) == 0)
                {
                    return -1L;
                }
                return _crc & 0xffffffffL;
            }
            set
            {
                if ((_crc & 18446744069414584320L) != 0L)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _crc = (uint)value;
                _known = (Known)((byte)(_known | Known.Crc));
            }
        }

        internal byte CryptoCheckValue { get; set; }

        public DateTime DateTime
        {
            get
            {
                uint num = Math.Min(0x3b, 2 * (_dosTime & 0x1f));
                uint num2 = Math.Min(0x3b, (_dosTime >> 5) & 0x3f);
                uint num3 = Math.Min(0x17, (_dosTime >> 11) & 0x1f);
                uint num4 = Math.Max(1, Math.Min(12, (_dosTime >> 0x15) & 15));
                uint num5 = ((_dosTime >> 0x19) & 0x7f) + 0x7bc;
                return new DateTime((int)num5, (int)num4, Math.Max(1, Math.Min(DateTime.DaysInMonth((int)num5, (int)num4), ((int)(_dosTime >> 0x10)) & 0x1f)), (int)num3, (int)num2, (int)num);
            }
            set
            {
                uint year = (uint)value.Year;
                uint month = (uint)value.Month;
                uint day = (uint)value.Day;
                uint hour = (uint)value.Hour;
                uint minute = (uint)value.Minute;
                uint second = (uint)value.Second;
                if (year < 0x7bc)
                {
                    year = 0x7bc;
                    month = 1;
                    day = 1;
                    hour = 0;
                    minute = 0;
                    second = 0;
                }
                else if (year > 0x83b)
                {
                    year = 0x83b;
                    month = 12;
                    day = 0x1f;
                    hour = 0x17;
                    minute = 0x3b;
                    second = 0x3b;
                }
                DosTime = (((((((year - 0x7bc) & 0x7f) << 0x19) | (month << 0x15)) | (day << 0x10)) | (hour << 11)) 
                    | (minute << 5)) | (second >> 1);
            }
        }

        public long DosTime
        {
            get
            {
                if (((byte)(_known & (Known.None | Known.Time))) == 0)
                {
                    return 0L;
                }
                return _dosTime;
            }
            set
            {
                _dosTime = (uint)value;
                _known = (Known)((byte)(_known | Known.None | Known.Time));
            }
        }

        public int ExternalFileAttributes
        {
            get
            {
                if (((byte)(_known & Known.ExternalAttributes)) == 0)
                {
                    return -1;
                }
                return _externalFileAttributes;
            }
            set
            {
                _externalFileAttributes = value;
                _known = (Known)((byte)(_known | Known.ExternalAttributes));
            }
        }

        public byte[] ExtraData
        {
            get
            {
                return _extra;
            }
            set
            {
                if (value == null)
                {
                    _extra = null;
                }
                else
                {
                    if (value.Length > 0xffff)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }
                    _extra = new byte[value.Length];
                    Array.Copy(value, 0, _extra, 0, value.Length);
                }
            }
        }

        public int Flags { get; set; }

        public bool HasCrc => (((byte)(_known & Known.Crc)) != 0);

        public int HostSystem
        {
            get
            {
                return ((_versionMadeBy >> 8) & 0xff);
            }
            set
            {
                _versionMadeBy = (ushort)(_versionMadeBy & 0xff);
                _versionMadeBy = (ushort)(_versionMadeBy | ((ushort)((value & 0xff) << 8)));
            }
        }

        public bool IsCrypted
        {
            get
            {
                return ((Flags & 1) != 0);
            }
            set
            {
                if (value)
                {
                    Flags |= 1;
                }
                else
                {
                    Flags &= -2;
                }
            }
        }

        public bool IsDirectory
        {
            get
            {
                int length = Name.Length;
                return (((length > 0) && ((Name[length - 1] == '/') || (Name[length - 1] == '\\'))) || HasDosAttributes(0x10));
            }
        }

        public bool IsDosEntry
        {
            get
            {
                if (HostSystem != 0)
                {
                    return (HostSystem == 10);
                }
                return true;
            }
        }

        public bool IsFile => (!IsDirectory && !HasDosAttributes(8));

        public bool IsUnicodeText
        {
            get
            {
                return ((Flags & 0x800) != 0);
            }
            set
            {
                if (value)
                {
                    Flags |= 0x800;
                }
                else
                {
                    Flags &= -2049;
                }
            }
        }

        public bool LocalHeaderRequiresZip64
        {
            get
            {
                bool flag = _forceZip64;
                if (flag)
                {
                    return true;
                }
                if ((_versionToExtract == 0) && IsCrypted)
                {
                    _compressedSize += 12L;
                }
                return (((_size >= 0xffffffffL) || (_compressedSize >= 0xffffffffL)) && ((_versionToExtract == 0) 
                    || (_versionToExtract >= 0x2d)));
            }
        }

        public string Name { get; }

        public long Offset { get; set; }

        public long Size
        {
            get
            {
                if (((byte)(_known & (Known.None | Known.Size))) == 0)
                {
                    return -1L;
                }
                return (long)_size;
            }
            set
            {
                _size = (ulong)value;
                _known = (Known)((byte)(_known | Known.None | Known.Size));
            }
        }

        public int Version
        {
            get
            {
                if (_versionToExtract != 0)
                {
                    return _versionToExtract;
                }
                int num = 10;
                if (AesKeySize > 0)
                {
                    return 0x33;
                }
                if (CentralHeaderRequiresZip64)
                {
                    return 0x2d;
                }
                if (CompressionMethod.Deflated == _method)
                {
                    return 20;
                }
                if (IsDirectory)
                {
                    return 20;
                }
                if (IsCrypted)
                {
                    return 20;
                }
                if (HasDosAttributes(8))
                {
                    num = 11;
                }
                return num;
            }
        }

        public int VersionMadeBy => (_versionMadeBy & 0xff);

        public long ZipFileIndex { get; set; }

        [Flags]
        private enum Known : byte
        {
            CompressedSize = 2,
            Crc = 4,
            ExternalAttributes = 0x10,
            None = 0,
            Size = 1,
            Time = 8
        }
    }
}

