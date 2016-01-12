using System;
using System.IO;
using System.Text;

namespace ZipLib.Tar
{
    public class TarArchive : IDisposable
    {
        private bool _applyUserInfoOverrides;
        private bool _asciiTranslate;
        private int _groupId;
        private string _groupName;
        private bool _isDisposed;
        private bool _keepOldFiles;
        private string _pathPrefix;
        private string _rootPath;
        private readonly TarInputStream _tarIn;
        private readonly TarOutputStream _tarOut;
        private int _userId;
        private string _userName;

        public event ProgressMessageHandler ProgressMessageEvent;

        protected TarArchive()
        {
            _userName = string.Empty;
            _groupName = string.Empty;
        }

        protected TarArchive(TarInputStream stream)
        {
            _userName = string.Empty;
            _groupName = string.Empty;
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            _tarIn = stream;
        }

        protected TarArchive(TarOutputStream stream)
        {
            _userName = string.Empty;
            _groupName = string.Empty;
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            _tarOut = stream;
        }

        public virtual void Close()
        {
            Dispose(true);
        }

        [Obsolete("Use Close instead")]
        public void CloseArchive()
        {
            Close();
        }

        public static TarArchive CreateInputTarArchive(Stream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            TarInputStream stream = inputStream as TarInputStream;
            if (stream != null)
            {
                return new TarArchive(stream);
            }
            return CreateInputTarArchive(inputStream, 20);
        }

        public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            if (inputStream is TarInputStream)
            {
                throw new ArgumentException("TarInputStream not valid");
            }
            return new TarArchive(new TarInputStream(inputStream, blockFactor));
        }

        public static TarArchive CreateOutputTarArchive(Stream outputStream)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }
            TarOutputStream stream = outputStream as TarOutputStream;
            if (stream != null)
            {
                return new TarArchive(stream);
            }
            return CreateOutputTarArchive(outputStream, 20);
        }

        public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }
            if (outputStream is TarOutputStream)
            {
                throw new ArgumentException("TarOutputStream is not valid");
            }
            return new TarArchive(new TarOutputStream(outputStream, blockFactor));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    if (_tarOut != null)
                    {
                        _tarOut.Flush();
                        _tarOut.Close();
                    }
                    if (_tarIn != null)
                    {
                        _tarIn.Close();
                    }
                }
            }
        }

        private static void EnsureDirectoryExists(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                try
                {
                    Directory.CreateDirectory(directoryName);
                }
                catch (Exception exception)
                {
                    throw new TarException("Exception creating directory '" + directoryName + "', " + exception.Message, exception);
                }
            }
        }

        public void ExtractContents(string destinationDirectory)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            while (true)
            {
                TarEntry nextEntry = _tarIn.GetNextEntry();
                if (nextEntry == null)
                {
                    return;
                }
                ExtractEntry(destinationDirectory, nextEntry);
            }
        }

        private void ExtractEntry(string destDir, TarEntry entry)
        {
            int num;
            OnProgressMessageEvent(entry, null);
            string name = entry.Name;
            if (Path.IsPathRooted(name))
            {
                name = name.Substring(Path.GetPathRoot(name).Length);
            }
            name = name.Replace('/', Path.DirectorySeparatorChar);
            string directoryName = Path.Combine(destDir, name);
            if (entry.IsDirectory)
            {
                EnsureDirectoryExists(directoryName);
                return;
            }
            EnsureDirectoryExists(Path.GetDirectoryName(directoryName));
            bool flag = true;
            FileInfo info = new FileInfo(directoryName);
            if (info.Exists)
            {
                if (_keepOldFiles)
                {
                    OnProgressMessageEvent(entry, "Destination file already exists");
                    flag = false;
                }
                else if ((info.Attributes & FileAttributes.ReadOnly) != 0)
                {
                    OnProgressMessageEvent(entry, "Destination file already exists, and is read-only");
                    flag = false;
                }
            }
            if (!flag)
            {
                return;
            }
            bool flag2 = false;
            Stream stream = File.Create(directoryName);
            if (_asciiTranslate)
            {
                flag2 = !IsBinary(directoryName);
            }
            StreamWriter writer = null;
            if (flag2)
            {
                writer = new StreamWriter(stream);
            }
            byte[] buffer = new byte[0x8000];
        Label_00DF:
            num = _tarIn.Read(buffer, 0, buffer.Length);
            if (num > 0)
            {
                if (flag2)
                {
                    int index = 0;
                    for (int i = 0; i < num; i++)
                    {
                        if (buffer[i] == 10)
                        {
                            string str4 = Encoding.ASCII.GetString(buffer, index, i - index);
                            writer.WriteLine(str4);
                            index = i + 1;
                        }
                    }
                }
                else
                {
                    stream.Write(buffer, 0, num);
                }
                goto Label_00DF;
            }
            if (flag2)
            {
                writer.Close();
            }
            else
            {
                stream.Close();
            }
        }

        ~TarArchive()
        {
            Dispose(false);
        }

        private static bool IsBinary(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                int count = Math.Min(0x1000, (int)stream.Length);
                byte[] buffer = new byte[count];
                int num2 = stream.Read(buffer, 0, count);
                for (int i = 0; i < num2; i++)
                {
                    byte num4 = buffer[i];
                    if (((num4 < 8) || ((num4 > 13) && (num4 < 0x20))) || (num4 == 0xff))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ListContents()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            while (true)
            {
                TarEntry nextEntry = _tarIn.GetNextEntry();
                if (nextEntry == null)
                {
                    return;
                }
                OnProgressMessageEvent(nextEntry, null);
            }
        }

        protected virtual void OnProgressMessageEvent(TarEntry entry, string message)
        {
            ProgressMessageHandler progressMessageEvent = ProgressMessageEvent;
            if (progressMessageEvent != null)
            {
                progressMessageEvent(this, entry, message);
            }
        }

        [Obsolete("Use the AsciiTranslate property")]
        public void SetAsciiTranslation(bool translateAsciiFiles)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            _asciiTranslate = translateAsciiFiles;
        }

        public void SetKeepOldFiles(bool keepExistingFiles)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            _keepOldFiles = keepExistingFiles;
        }

        public void SetUserInfo(int userId, string userName, int groupId, string _groupName)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            _userId = userId;
            _userName = userName;
            _groupId = groupId;
            this._groupName = _groupName;
            _applyUserInfoOverrides = true;
        }

        public void WriteEntry(TarEntry sourceEntry, bool recurse)
        {
            if (sourceEntry == null)
            {
                throw new ArgumentNullException(nameof(sourceEntry));
            }
            if (_isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            try
            {
                if (recurse)
                {
                    TarHeader.SetValueDefaults(sourceEntry.UserId, sourceEntry.UserName, sourceEntry.GroupId, sourceEntry.GroupName);
                }
                WriteEntryCore(sourceEntry, recurse);
            }
            finally
            {
                if (recurse)
                {
                    TarHeader.RestoreSetValues();
                }
            }
        }

        private void WriteEntryCore(TarEntry sourceEntry, bool recurse)
        {
            string path = null;
            string file = sourceEntry.File;
            TarEntry entry = (TarEntry)sourceEntry.Clone();
            if (_applyUserInfoOverrides)
            {
                entry.GroupId = _groupId;
                entry.GroupName = _groupName;
                entry.UserId = _userId;
                entry.UserName = _userName;
            }
            OnProgressMessageEvent(entry, null);
            if ((_asciiTranslate && !entry.IsDirectory) && !IsBinary(file))
            {
                path = Path.GetTempFileName();
                using (StreamReader reader = File.OpenText(file))
                {
                    using (Stream stream = File.Create(path))
                    {
                        Label_0088:
                        string str3 = reader.ReadLine();
                        if (str3 != null)
                        {
                            byte[] bytes = Encoding.ASCII.GetBytes(str3);
                            stream.Write(bytes, 0, bytes.Length);
                            stream.WriteByte(10);
                            goto Label_0088;
                        }
                        stream.Flush();
                    }
                }
                entry.Size = new FileInfo(path).Length;
                file = path;
            }
            string str4 = null;
            if ((_rootPath != null) && entry.Name.StartsWith(_rootPath))
            {
                str4 = entry.Name.Substring(_rootPath.Length + 1);
            }
            if (_pathPrefix != null)
            {
                str4 = (str4 == null) ? (_pathPrefix + "/" + entry.Name) : (_pathPrefix + "/" + str4);
            }
            if (str4 != null)
            {
                entry.Name = str4;
            }
            _tarOut.PutNextEntry(entry);
            if (entry.IsDirectory)
            {
                if (recurse)
                {
                    TarEntry[] directoryEntries = entry.GetDirectoryEntries();
                    foreach (TarEntry t in directoryEntries)
                    {
                        WriteEntryCore(t, true);
                    }
                    return;
                }
                return;
            }
            using (Stream stream2 = File.OpenRead(file))
            {
                byte[] buffer = new byte[0x8000];
                while (true)
                {
                    int count = stream2.Read(buffer, 0, buffer.Length);
                    if (count <= 0)
                    {
                        goto Label_01F6;
                    }
                    _tarOut.Write(buffer, 0, count);
                }
            }
        Label_01F6:
            if (!string.IsNullOrEmpty(path))
            {
                File.Delete(path);
            }
            _tarOut.CloseEntry();
        }

        public bool ApplyUserInfoOverrides
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _applyUserInfoOverrides;
            }
            set
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                _applyUserInfoOverrides = value;
            }
        }

        public bool AsciiTranslate
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _asciiTranslate;
            }
            set
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                _asciiTranslate = value;
            }
        }

        public int GroupId
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _groupId;
            }
        }

        public string GroupName
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _groupName;
            }
        }

        public bool IsStreamOwner
        {
            set
            {
                if (_tarIn != null)
                {
                    _tarIn.IsStreamOwner = value;
                }
                else
                {
                    _tarOut.IsStreamOwner = value;
                }
            }
        }

        public string PathPrefix
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _pathPrefix;
            }
            set
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                _pathPrefix = value;
            }
        }

        public int RecordSize
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                if (_tarIn != null)
                {
                    return _tarIn.RecordSize;
                }
                if (_tarOut != null)
                {
                    return _tarOut.RecordSize;
                }
                return 0x2800;
            }
        }

        public string RootPath
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _rootPath;
            }
            set
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                _rootPath = value;
            }
        }

        public int UserId
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _userId;
            }
        }

        public string UserName
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _userName;
            }
        }
    }
}
