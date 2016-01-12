﻿using System;
using System.Text;

namespace ZipLib.Tar
{
    public class TarHeader : ICloneable
    {
        private int checksum;
        public const int CHKSUMLEN = 8;
        public const int CHKSUMOFS = 0x94;
        private static readonly DateTime dateTime1970 = new DateTime(0x7b2, 1, 1, 0, 0, 0, 0);
        internal static int defaultGroupId;
        internal static string defaultGroupName = "None";
        internal static string defaultUser;
        internal static int defaultUserId;
        public const int DEVLEN = 8;
        private int devMajor;
        private int devMinor;
        public const int GIDLEN = 8;
        public const int GNAMELEN = 0x20;
        public const string GNU_TMAGIC = "ustar  ";
        private int groupId;
        internal static int groupIdAsSet;
        private string groupName;
        internal static string groupNameAsSet = "None";
        private bool isChecksumValid;
        public const byte LF_ACL = 0x41;
        public const byte LF_BLK = 0x34;
        public const byte LF_CHR = 0x33;
        public const byte LF_CONTIG = 0x37;
        public const byte LF_DIR = 0x35;
        public const byte LF_EXTATTR = 0x45;
        public const byte LF_FIFO = 0x36;
        public const byte LF_GHDR = 0x67;
        public const byte LF_GNU_DUMPDIR = 0x44;
        public const byte LF_GNU_LONGLINK = 0x4b;
        public const byte LF_GNU_LONGNAME = 0x4c;
        public const byte LF_GNU_MULTIVOL = 0x4d;
        public const byte LF_GNU_NAMES = 0x4e;
        public const byte LF_GNU_SPARSE = 0x53;
        public const byte LF_GNU_VOLHDR = 0x56;
        public const byte LF_LINK = 0x31;
        public const byte LF_META = 0x49;
        public const byte LF_NORMAL = 0x30;
        public const byte LF_OLDNORM = 0;
        public const byte LF_SYMLINK = 50;
        public const byte LF_XHDR = 120;
        private string linkName;
        private string magic;
        public const int MAGICLEN = 6;
        private int mode;
        public const int MODELEN = 8;
        private DateTime modTime;
        public const int MODTIMELEN = 12;
        private string name;
        public const int NAMELEN = 100;
        private long size;
        public const int SIZELEN = 12;
        private const long timeConversionFactor = 0x989680L;
        public const string TMAGIC = "ustar ";
        private byte typeFlag;
        public const int UIDLEN = 8;
        public const int UNAMELEN = 0x20;
        private int userId;
        internal static int userIdAsSet;
        private string userName;
        internal static string userNameAsSet;
        private string version;
        public const int VERSIONLEN = 2;

        public TarHeader()
        {
            Magic = "ustar ";
            Version = " ";
            Name = "";
            LinkName = "";
            UserId = defaultUserId;
            GroupId = defaultGroupId;
            UserName = defaultUser;
            GroupName = defaultGroupName;
            Size = 0L;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        private static int ComputeCheckSum(byte[] buffer)
        {
            int num = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                num += buffer[i];
            }
            return num;
        }

        public override bool Equals(object obj)
        {
            TarHeader header = obj as TarHeader;
            return ((header != null) && ((((((name == header.name) && (mode == header.mode)) && ((UserId == header.UserId) && (GroupId == header.GroupId))) && (((Size == header.Size) && (ModTime == header.ModTime)) && ((Checksum == header.Checksum) && (TypeFlag == header.TypeFlag)))) && ((((LinkName == header.LinkName) && (Magic == header.Magic)) && ((Version == header.Version) && (UserName == header.UserName))) && ((GroupName == header.GroupName) && (DevMajor == header.DevMajor)))) && (DevMinor == header.DevMinor)));
        }

        public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (toAdd == null)
            {
                throw new ArgumentNullException("toAdd");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            for (int i = 0; (i < length) && ((nameOffset + i) < toAdd.Length); i++)
            {
                buffer[bufferOffset + i] = (byte)toAdd[nameOffset + i];
            }
            return (bufferOffset + length);
        }

        private static int GetCheckSumOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            GetOctalBytes(value, buffer, offset, length - 1);
            return (offset + length);
        }

        private static int GetCTime(DateTime dateTime)
        {
            return (int)((dateTime.Ticks - dateTime1970.Ticks) / timeConversionFactor);
        }

        private static DateTime GetDateTimeFromCTime(long ticks)
        {
            try
            {
                return new DateTime(dateTime1970.Ticks + (ticks * timeConversionFactor));
            }
            catch (ArgumentOutOfRangeException)
            {
                return dateTime1970;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static int GetLongOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            return GetOctalBytes(value, buffer, offset, length);
        }

        [Obsolete("Use the Name property instead", true)]
        public string GetName()
        {
            return name;
        }

        public static int GetNameBytes(string name, byte[] buffer, int offset, int length)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            return GetNameBytes(name, 0, buffer, offset, length);
        }

        public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            return GetNameBytes(name.ToString(), 0, buffer, offset, length);
        }

        public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            int num = 0;
            while ((num < (length - 1)) && ((nameOffset + num) < name.Length))
            {
                buffer[bufferOffset + num] = (byte)name[nameOffset + num];
                num++;
            }
            while (num < length)
            {
                buffer[bufferOffset + num] = 0;
                num++;
            }
            return (bufferOffset + length);
        }

        public static int GetNameBytes(StringBuilder name, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            return GetNameBytes(name.ToString(), nameOffset, buffer, bufferOffset, length);
        }

        public static int GetOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            int num = length - 1;
            buffer[offset + num] = 0;
            num--;
            if (value > 0L)
            {
                long num2 = value;
                while ((num >= 0) && (num2 > 0L))
                {
                    buffer[offset + num] = (byte)(0x30 + ((byte)(num2 & 7L)));
                    num2 = num2 >> 3;
                    num--;
                }
            }
            while (num >= 0)
            {
                buffer[offset + num] = 0x30;
                num--;
            }
            return (offset + length);
        }

        private static int MakeCheckSum(byte[] buffer)
        {
            int num = 0;
            for (int i = 0; i < 0x94; i++)
            {
                num += buffer[i];
            }
            for (int j = 0; j < 8; j++)
            {
                num += 0x20;
            }
            for (int k = 0x9c; k < buffer.Length; k++)
            {
                num += buffer[k];
            }
            return num;
        }

        public void ParseBuffer(byte[] header)
        {
            if (header == null)
            {
                throw new ArgumentNullException("header");
            }
            int offset = 0;
            name = ParseName(header, offset, 100).ToString();
            offset += 100;
            mode = (int)ParseOctal(header, offset, 8);
            offset += 8;
            UserId = (int)ParseOctal(header, offset, 8);
            offset += 8;
            GroupId = (int)ParseOctal(header, offset, 8);
            offset += 8;
            Size = ParseOctal(header, offset, 12);
            offset += 12;
            ModTime = GetDateTimeFromCTime(ParseOctal(header, offset, 12));
            offset += 12;
            checksum = (int)ParseOctal(header, offset, 8);
            offset += 8;
            TypeFlag = header[offset++];
            LinkName = ParseName(header, offset, 100).ToString();
            offset += 100;
            Magic = ParseName(header, offset, 6).ToString();
            offset += 6;
            Version = ParseName(header, offset, 2).ToString();
            offset += 2;
            UserName = ParseName(header, offset, 0x20).ToString();
            offset += 0x20;
            GroupName = ParseName(header, offset, 0x20).ToString();
            offset += 0x20;
            DevMajor = (int)ParseOctal(header, offset, 8);
            offset += 8;
            DevMinor = (int)ParseOctal(header, offset, 8);
            isChecksumValid = Checksum == MakeCheckSum(header);
        }

        public static StringBuilder ParseName(byte[] header, int offset, int length)
        {
            if (header == null)
            {
                throw new ArgumentNullException("header");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "Cannot be less than zero");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Cannot be less than zero");
            }
            if ((offset + length) > header.Length)
            {
                throw new ArgumentException("Exceeds header size", "length");
            }
            StringBuilder builder = new StringBuilder(length);
            for (int i = offset; i < (offset + length); i++)
            {
                if (header[i] == 0)
                {
                    return builder;
                }
                builder.Append((char)header[i]);
            }
            return builder;
        }

        public static long ParseOctal(byte[] header, int offset, int length)
        {
            if (header == null)
            {
                throw new ArgumentNullException("header");
            }
            long num = 0L;
            bool flag = true;
            int num2 = offset + length;
            for (int i = offset; i < num2; i++)
            {
                if (header[i] == 0)
                {
                    return num;
                }
                if ((header[i] == 0x20) || (header[i] == 0x30))
                {
                    if (flag)
                    {
                        continue;
                    }
                    if (header[i] == 0x20)
                    {
                        return num;
                    }
                }
                flag = false;
                num = (num << 3) + (header[i] - 0x30);
            }
            return num;
        }

        internal static void RestoreSetValues()
        {
            defaultUserId = userIdAsSet;
            defaultUser = userNameAsSet;
            defaultGroupId = groupIdAsSet;
            defaultGroupName = groupNameAsSet;
        }

        internal static void SetValueDefaults(int userId, string userName, int groupId, string groupName)
        {
            defaultUserId = userIdAsSet = userId;
            defaultUser = userNameAsSet = userName;
            defaultGroupId = groupIdAsSet = groupId;
            defaultGroupName = groupNameAsSet = groupName;
        }

        public void WriteHeader(byte[] outBuffer)
        {
            if (outBuffer == null)
            {
                throw new ArgumentNullException("outBuffer");
            }
            int offset = 0;
            offset = GetNameBytes(Name, outBuffer, offset, 100);
            offset = GetOctalBytes(mode, outBuffer, offset, 8);
            offset = GetOctalBytes(UserId, outBuffer, offset, 8);
            offset = GetOctalBytes(GroupId, outBuffer, offset, 8);
            offset = GetLongOctalBytes(Size, outBuffer, offset, 12);
            offset = GetLongOctalBytes(GetCTime(ModTime), outBuffer, offset, 12);
            int num2 = offset;
            for (int i = 0; i < 8; i++)
            {
                outBuffer[offset++] = 0x20;
            }
            outBuffer[offset++] = TypeFlag;
            offset = GetNameBytes(LinkName, outBuffer, offset, 100);
            offset = GetAsciiBytes(Magic, 0, outBuffer, offset, 6);
            offset = GetNameBytes(Version, outBuffer, offset, 2);
            offset = GetNameBytes(UserName, outBuffer, offset, 0x20);
            offset = GetNameBytes(GroupName, outBuffer, offset, 0x20);
            if ((TypeFlag == 0x33) || (TypeFlag == 0x34))
            {
                offset = GetOctalBytes(DevMajor, outBuffer, offset, 8);
                offset = GetOctalBytes(DevMinor, outBuffer, offset, 8);
            }
            while (offset < outBuffer.Length)
            {
                outBuffer[offset++] = 0;
            }
            checksum = ComputeCheckSum(outBuffer);
            GetCheckSumOctalBytes(checksum, outBuffer, num2, 8);
            isChecksumValid = true;
        }

        public int Checksum
        {
            get
            {
                return checksum;
            }
        }

        public int DevMajor
        {
            get
            {
                return devMajor;
            }
            set
            {
                devMajor = value;
            }
        }

        public int DevMinor
        {
            get
            {
                return devMinor;
            }
            set
            {
                devMinor = value;
            }
        }

        public int GroupId
        {
            get
            {
                return groupId;
            }
            set
            {
                groupId = value;
            }
        }

        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                if (value == null)
                {
                    groupName = "None";
                }
                else
                {
                    groupName = value;
                }
            }
        }

        public bool IsChecksumValid
        {
            get
            {
                return isChecksumValid;
            }
        }

        public string LinkName
        {
            get
            {
                return linkName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                linkName = value;
            }
        }

        public string Magic
        {
            get
            {
                return magic;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                magic = value;
            }
        }

        public int Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public DateTime ModTime
        {
            get
            {
                return modTime;
            }
            set
            {
                if (value < dateTime1970)
                {
                    throw new ArgumentOutOfRangeException("value", "ModTime cannot be before Jan 1st 1970");
                }
                modTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                name = value;
            }
        }

        public long Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value < 0L)
                {
                    throw new ArgumentOutOfRangeException("value", "Cannot be less than zero");
                }
                size = value;
            }
        }

        public byte TypeFlag
        {
            get
            {
                return typeFlag;
            }
            set
            {
                typeFlag = value;
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (value != null)
                {
                    userName = value.Substring(0, Math.Min(0x20, value.Length));
                }
                else
                {
                    string localName = Environment.UserName;
                    if (localName.Length > 0x20)
                    {
                        localName = localName.Substring(0, 0x20);
                    }
                    userName = localName;
                }
            }
        }

        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                version = value;
            }
        }
    }
}
