using System;
using System.IO;

namespace ZipLib.Tar
{
    public class TarEntry : ICloneable
    {
        private string _file;
        private TarHeader _header;

        private TarEntry()
        {
            _header = new TarHeader();
        }

        public TarEntry(byte[] headerBuffer)
        {
            _header = new TarHeader();
            _header.ParseBuffer(headerBuffer);
        }

        public TarEntry(TarHeader header)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }
            this._header = (TarHeader)header.Clone();
        }

        public static void AdjustEntryName(byte[] buffer, string newName)
        {
            TarHeader.GetNameBytes(newName, buffer, 0, 100);
        }

        public object Clone()
        {
            return new TarEntry { _file = _file, _header = (TarHeader)_header.Clone(), Name = Name };
        }

        public static TarEntry CreateEntryFromFile(string fileName)
        {
            TarEntry entry = new TarEntry();
            entry.GetFileTarHeader(entry._header, fileName);
            return entry;
        }

        public static TarEntry CreateTarEntry(string name)
        {
            TarEntry entry = new TarEntry();
            NameTarHeader(entry._header, name);
            return entry;
        }

        public override bool Equals(object obj)
        {
            TarEntry entry = obj as TarEntry;
            return ((entry != null) && Name.Equals(entry.Name));
        }

        public TarEntry[] GetDirectoryEntries()
        {
            if ((_file == null) || !Directory.Exists(_file))
            {
                return new TarEntry[0];
            }
            string[] fileSystemEntries = Directory.GetFileSystemEntries(_file);
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
                throw new ArgumentNullException(nameof(head));
            }
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }
            _file = f;
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
                throw new ArgumentNullException(nameof(toTest));
            }
            return toTest.Name.StartsWith(Name);
        }

        public static void NameTarHeader(TarHeader header, string name)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
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
            _header.WriteHeader(outBuffer);
        }

        public string File => _file;

        public int GroupId
        {
            get
            {
                return _header.GroupId;
            }
            set
            {
                _header.GroupId = value;
            }
        }

        public string GroupName
        {
            get
            {
                return _header.GroupName;
            }
            set
            {
                _header.GroupName = value;
            }
        }

        public bool IsDirectory
        {
            get
            {
                if (_file != null)
                {
                    return Directory.Exists(_file);
                }
                if ((_header == null) || ((_header.TypeFlag != 0x35) && !Name.EndsWith("/")))
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
                return _header.ModTime;
            }
            set
            {
                _header.ModTime = value;
            }
        }

        public string Name
        {
            get
            {
                return _header.Name;
            }
            set
            {
                _header.Name = value;
            }
        }

        public long Size
        {
            get
            {
                return _header.Size;
            }
            set
            {
                _header.Size = value;
            }
        }

        public TarHeader TarHeader => _header;

        public int UserId
        {
            get
            {
                return _header.UserId;
            }
            set
            {
                _header.UserId = value;
            }
        }

        public string UserName
        {
            get
            {
                return _header.UserName;
            }
            set
            {
                _header.UserName = value;
            }
        }
    }
}
