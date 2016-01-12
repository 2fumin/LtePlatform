using System;
using System.IO;

namespace ZipLib.Tar
{
    public class TarEntry : ICloneable
    {
        private string file;
        private TarHeader header;

        private TarEntry()
        {
            header = new TarHeader();
        }

        public TarEntry(byte[] headerBuffer)
        {
            header = new TarHeader();
            header.ParseBuffer(headerBuffer);
        }

        public TarEntry(TarHeader header)
        {
            if (header == null)
            {
                throw new ArgumentNullException("header");
            }
            this.header = (TarHeader)header.Clone();
        }

        public static void AdjustEntryName(byte[] buffer, string newName)
        {
            TarHeader.GetNameBytes(newName, buffer, 0, 100);
        }

        public object Clone()
        {
            return new TarEntry { file = file, header = (TarHeader)header.Clone(), Name = Name };
        }

        public static TarEntry CreateEntryFromFile(string fileName)
        {
            TarEntry entry = new TarEntry();
            entry.GetFileTarHeader(entry.header, fileName);
            return entry;
        }

        public static TarEntry CreateTarEntry(string name)
        {
            TarEntry entry = new TarEntry();
            NameTarHeader(entry.header, name);
            return entry;
        }

        public override bool Equals(object obj)
        {
            TarEntry entry = obj as TarEntry;
            return ((entry != null) && Name.Equals(entry.Name));
        }

        public TarEntry[] GetDirectoryEntries()
        {
            if ((file == null) || !Directory.Exists(file))
            {
                return new TarEntry[0];
            }
            string[] fileSystemEntries = Directory.GetFileSystemEntries(file);
            TarEntry[] entryArray = new TarEntry[fileSystemEntries.Length];
            for (int i = 0; i < fileSystemEntries.Length; i++)
            {
                entryArray[i] = CreateEntryFromFile(fileSystemEntries[i]);
            }
            return entryArray;
        }

        public void GetFileTarHeader(TarHeader head, string f)
        {
            if (head == null)
            {
                throw new ArgumentNullException("head");
            }
            if (f == null)
            {
                throw new ArgumentNullException("f");
            }
            file = f;
            string str = f;
            if (str.IndexOf(Environment.CurrentDirectory, StringComparison.Ordinal) == 0)
            {
                str = str.Substring(Environment.CurrentDirectory.Length);
            }
            str = str.Replace(Path.DirectorySeparatorChar, '/');
            while (str.StartsWith("/"))
            {
                str = str.Substring(1);
            }
            head.LinkName = string.Empty;
            head.Name = str;
            if (Directory.Exists(f))
            {
                head.Mode = 0x3eb;
                head.TypeFlag = 0x35;
                if ((head.Name.Length == 0) || (head.Name[head.Name.Length - 1] != '/'))
                {
                    head.Name = head.Name + "/";
                }
                head.Size = 0L;
            }
            else
            {
                head.Mode = 0x81c0;
                head.TypeFlag = 0x30;
                head.Size = new FileInfo(f.Replace('/', Path.DirectorySeparatorChar)).Length;
            }
            head.ModTime = System.IO.File.GetLastWriteTime(f.Replace('/', Path.DirectorySeparatorChar)).ToUniversalTime();
            head.DevMajor = 0;
            head.DevMinor = 0;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool IsDescendent(TarEntry toTest)
        {
            if (toTest == null)
            {
                throw new ArgumentNullException("toTest");
            }
            return toTest.Name.StartsWith(Name);
        }

        public static void NameTarHeader(TarHeader header, string name)
        {
            if (header == null)
            {
                throw new ArgumentNullException("header");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            bool flag = name.EndsWith("/");
            header.Name = name;
            header.Mode = flag ? 0x3eb : 0x81c0;
            header.UserId = 0;
            header.GroupId = 0;
            header.Size = 0L;
            header.ModTime = DateTime.UtcNow;
            header.TypeFlag = flag ? ((byte)0x35) : ((byte)0x30);
            header.LinkName = string.Empty;
            header.UserName = string.Empty;
            header.GroupName = string.Empty;
            header.DevMajor = 0;
            header.DevMinor = 0;
        }

        public void SetIds(int userId, int groupId)
        {
            UserId = userId;
            GroupId = groupId;
        }

        public void SetNames(string userName, string groupName)
        {
            UserName = userName;
            GroupName = groupName;
        }

        public void WriteEntryHeader(byte[] outBuffer)
        {
            header.WriteHeader(outBuffer);
        }

        public string File
        {
            get
            {
                return file;
            }
        }

        public int GroupId
        {
            get
            {
                return header.GroupId;
            }
            set
            {
                header.GroupId = value;
            }
        }

        public string GroupName
        {
            get
            {
                return header.GroupName;
            }
            set
            {
                header.GroupName = value;
            }
        }

        public bool IsDirectory
        {
            get
            {
                if (file != null)
                {
                    return Directory.Exists(file);
                }
                if ((header == null) || ((header.TypeFlag != 0x35) && !Name.EndsWith("/")))
                {
                    return false;
                }
                return true;
            }
        }

        public DateTime ModTime
        {
            get
            {
                return header.ModTime;
            }
            set
            {
                header.ModTime = value;
            }
        }

        public string Name
        {
            get
            {
                return header.Name;
            }
            set
            {
                header.Name = value;
            }
        }

        public long Size
        {
            get
            {
                return header.Size;
            }
            set
            {
                header.Size = value;
            }
        }

        public TarHeader TarHeader
        {
            get
            {
                return header;
            }
        }

        public int UserId
        {
            get
            {
                return header.UserId;
            }
            set
            {
                header.UserId = value;
            }
        }

        public string UserName
        {
            get
            {
                return header.UserName;
            }
            set
            {
                header.UserName = value;
            }
        }
    }
}
