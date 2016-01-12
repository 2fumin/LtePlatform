using System;
using System.IO;

namespace Lte.Domain.ZipLib.Zip
{
    public class ZipEntry : ICloneable
    {
        private int _aesEncryptionStrength;
        private string comment;
        private ulong compressedSize;
        private uint crc;
        private byte cryptoCheckValue_;
        private uint dosTime;
        private int externalFileAttributes;
        private byte[] extra;
        private int flags;
        private bool forceZip64_;
        private Known known;
        private CompressionMethod method;
        private string name;
        private long offset;
        private ulong size;
        private ushort versionMadeBy;
        private ushort versionToExtract;
        private long zipFileIndex;

        [Obsolete("Use Clone instead")]
        public ZipEntry(ZipEntry entry)
        {
            externalFileAttributes = -1;
            method = CompressionMethod.Deflated;
            zipFileIndex = -1L;
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }
            known = entry.known;
            name = entry.name;
            size = entry.size;
            compressedSize = entry.compressedSize;
            crc = entry.crc;
            dosTime = entry.dosTime;
            method = entry.method;
            comment = entry.comment;
            versionToExtract = entry.versionToExtract;
            versionMadeBy = entry.versionMadeBy;
            externalFileAttributes = entry.externalFileAttributes;
            flags = entry.flags;
            zipFileIndex = entry.zipFileIndex;
            offset = entry.offset;
            forceZip64_ = entry.forceZip64_;
            if (entry.extra != null)
            {
                extra = new byte[entry.extra.Length];
                Array.Copy(entry.extra, 0, extra, 0, entry.extra.Length);
            }
        }

        public ZipEntry(string name)
            : this(name, 0)
        {
        }

        internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo = 0x33, 
            CompressionMethod method = CompressionMethod.Deflated)
        {
            externalFileAttributes = -1;
            this.method = CompressionMethod.Deflated;
            zipFileIndex = -1L;
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name.Length > 0xffff)
            {
                throw new ArgumentException("Name is too long", "name");
            }
            if ((versionRequiredToExtract != 0) && (versionRequiredToExtract < 10))
            {
                throw new ArgumentOutOfRangeException("versionRequiredToExtract");
            }
            DateTime = DateTime.Now;
            this.name = name;
            versionMadeBy = (ushort)madeByInfo;
            versionToExtract = (ushort)versionRequiredToExtract;
            this.method = method;
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
            if (extra != null)
            {
                entry.extra = new byte[extra.Length];
                Array.Copy(extra, 0, entry.extra, 0, extra.Length);
            }
            return entry;
        }

        public void ForceZip64()
        {
            forceZip64_ = true;
        }

        private bool HasDosAttributes(int attributes)
        {
            bool flag = ((((byte)(known & Known.ExternalAttributes)) != 0) && ((HostSystem == 0) || (HostSystem == 10))) 
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
            return forceZip64_;
        }

        private void ProcessAESExtraData(ZipExtraData extraData)
        {
            if (!extraData.Find(0x9901))
            {
                throw new ZipException("AES Extra Data missing");
            }
            versionToExtract = 0x33;
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
            method = (CompressionMethod)num4;
        }

        internal void ProcessExtraData(bool localHeader)
        {
            ZipExtraData extraData = new ZipExtraData(extra);
            if (extraData.Find(1))
            {
                forceZip64_ = true;
                if (extraData.ValueLength < 4)
                {
                    throw new ZipException("Extra data extended Zip64 information length is invalid");
                }
                if (localHeader || (size == 0xffffffffL))
                {
                    size = (ulong)extraData.ReadLong();
                }
                if (localHeader || (compressedSize == 0xffffffffL))
                {
                    compressedSize = (ulong)extraData.ReadLong();
                }
                if (!localHeader && (offset == 0xffffffffL))
                {
                    offset = extraData.ReadLong();
                }
            }
            else if (((versionToExtract & 0xff) >= 0x2d) && ((size == 0xffffffffL) || (compressedSize == 0xffffffffL)))
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
            if (method == CompressionMethod.WinZipAES)
            {
                ProcessAESExtraData(extraData);
            }
        }

        public override string ToString()
        {
            return name;
        }

        internal byte AESEncryptionStrength
        {
            get
            {
                return (byte)_aesEncryptionStrength;
            }
        }

        public int AESKeySize
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

        internal int AESOverheadSize
        {
            get
            {
                return (12 + AESSaltLen);
            }
        }

        internal int AESSaltLen
        {
            get
            {
                return (AESKeySize / 0x10);
            }
        }

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
                    return (offset >= 0xffffffffL);
                }
                return true;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                if ((value != null) && (value.Length > 0xffff))
                {
                    throw new ArgumentOutOfRangeException("value", "cannot exceed 65535");
                }
                comment = value;
            }
        }

        public long CompressedSize
        {
            get
            {
                if (((byte)(known & Known.CompressedSize)) == 0)
                {
                    return -1L;
                }
                return (long)compressedSize;
            }
            set
            {
                compressedSize = (ulong)value;
                known = (Known)((byte)(known | Known.CompressedSize));
            }
        }

        public CompressionMethod CompressionMethod
        {
            get
            {
                return method;
            }
            set
            {
                if (!IsCompressionMethodSupported(value))
                {
                    throw new NotSupportedException("Compression method not supported");
                }
                method = value;
            }
        }

        internal CompressionMethod CompressionMethodForHeader
        {
            get
            {
                if (AESKeySize <= 0)
                {
                    return method;
                }
                return CompressionMethod.WinZipAES;
            }
        }

        public long Crc
        {
            get
            {
                if (((byte)(known & Known.Crc)) == 0)
                {
                    return -1L;
                }
                return crc & 0xffffffffL;
            }
            set
            {
                if ((crc & 18446744069414584320L) != 0L)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                crc = (uint)value;
                known = (Known)((byte)(known | Known.Crc));
            }
        }

        internal byte CryptoCheckValue
        {
            get
            {
                return cryptoCheckValue_;
            }
            set
            {
                cryptoCheckValue_ = value;
            }
        }

        public DateTime DateTime
        {
            get
            {
                uint num = Math.Min(0x3b, 2 * (dosTime & 0x1f));
                uint num2 = Math.Min(0x3b, (dosTime >> 5) & 0x3f);
                uint num3 = Math.Min(0x17, (dosTime >> 11) & 0x1f);
                uint num4 = Math.Max(1, Math.Min(12, (dosTime >> 0x15) & 15));
                uint num5 = ((dosTime >> 0x19) & 0x7f) + 0x7bc;
                return new DateTime((int)num5, (int)num4, Math.Max(1, Math.Min(DateTime.DaysInMonth((int)num5, (int)num4), ((int)(dosTime >> 0x10)) & 0x1f)), (int)num3, (int)num2, (int)num);
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
                if (((byte)(known & (Known.None | Known.Time))) == 0)
                {
                    return 0L;
                }
                return dosTime;
            }
            set
            {
                dosTime = (uint)value;
                known = (Known)((byte)(known | Known.None | Known.Time));
            }
        }

        public int ExternalFileAttributes
        {
            get
            {
                if (((byte)(known & Known.ExternalAttributes)) == 0)
                {
                    return -1;
                }
                return externalFileAttributes;
            }
            set
            {
                externalFileAttributes = value;
                known = (Known)((byte)(known | Known.ExternalAttributes));
            }
        }

        public byte[] ExtraData
        {
            get
            {
                return extra;
            }
            set
            {
                if (value == null)
                {
                    extra = null;
                }
                else
                {
                    if (value.Length > 0xffff)
                    {
                        throw new ArgumentOutOfRangeException("value");
                    }
                    extra = new byte[value.Length];
                    Array.Copy(value, 0, extra, 0, value.Length);
                }
            }
        }

        public int Flags
        {
            get
            {
                return flags;
            }
            set
            {
                flags = value;
            }
        }

        public bool HasCrc
        {
            get
            {
                return (((byte)(known & Known.Crc)) != 0);
            }
        }

        public int HostSystem
        {
            get
            {
                return ((versionMadeBy >> 8) & 0xff);
            }
            set
            {
                versionMadeBy = (ushort)(versionMadeBy & 0xff);
                versionMadeBy = (ushort)(versionMadeBy | ((ushort)((value & 0xff) << 8)));
            }
        }

        public bool IsCrypted
        {
            get
            {
                return ((flags & 1) != 0);
            }
            set
            {
                if (value)
                {
                    flags |= 1;
                }
                else
                {
                    flags &= -2;
                }
            }
        }

        public bool IsDirectory
        {
            get
            {
                int length = name.Length;
                return (((length > 0) && ((name[length - 1] == '/') || (name[length - 1] == '\\'))) || HasDosAttributes(0x10));
            }
        }

        public bool IsDOSEntry
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

        public bool IsFile
        {
            get
            {
                return (!IsDirectory && !HasDosAttributes(8));
            }
        }

        public bool IsUnicodeText
        {
            get
            {
                return ((flags & 0x800) != 0);
            }
            set
            {
                if (value)
                {
                    flags |= 0x800;
                }
                else
                {
                    flags &= -2049;
                }
            }
        }

        public bool LocalHeaderRequiresZip64
        {
            get
            {
                bool flag = forceZip64_;
                if (flag)
                {
                    return true;
                }
                if ((versionToExtract == 0) && IsCrypted)
                {
                    compressedSize += 12L;
                }
                return (((size >= 0xffffffffL) || (compressedSize >= 0xffffffffL)) && ((versionToExtract == 0) 
                    || (versionToExtract >= 0x2d)));
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public long Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }

        public long Size
        {
            get
            {
                if (((byte)(known & (Known.None | Known.Size))) == 0)
                {
                    return -1L;
                }
                return (long)size;
            }
            set
            {
                size = (ulong)value;
                known = (Known)((byte)(known | Known.None | Known.Size));
            }
        }

        public int Version
        {
            get
            {
                if (versionToExtract != 0)
                {
                    return versionToExtract;
                }
                int num = 10;
                if (AESKeySize > 0)
                {
                    return 0x33;
                }
                if (CentralHeaderRequiresZip64)
                {
                    return 0x2d;
                }
                if (CompressionMethod.Deflated == method)
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

        public int VersionMadeBy
        {
            get
            {
                return (versionMadeBy & 0xff);
            }
        }

        public long ZipFileIndex
        {
            get
            {
                return zipFileIndex;
            }
            set
            {
                zipFileIndex = value;
            }
        }

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

