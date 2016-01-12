using System;
using System.IO;
using System.Text;

namespace ZipLib.Tar
{
    public class TarArchive : IDisposable
    {
        private bool applyUserInfoOverrides;
        private bool asciiTranslate;
        private int _groupId;
        private string groupName;
        private bool isDisposed;
        private bool keepOldFiles;
        private string pathPrefix;
        private string rootPath;
        private TarInputStream tarIn;
        private TarOutputStream tarOut;
        private int _userId;
        private string _userName;

        public event ProgressMessageHandler ProgressMessageEvent;

        protected TarArchive()
        {
            _userName = string.Empty;
            groupName = string.Empty;
        }

        protected TarArchive(TarInputStream stream)
        {
            _userName = string.Empty;
            groupName = string.Empty;
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            tarIn = stream;
        }

        protected TarArchive(TarOutputStream stream)
        {
            _userName = string.Empty;
            groupName = string.Empty;
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            tarOut = stream;
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
                throw new ArgumentNullException("inputStream");
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
                throw new ArgumentNullException("inputStream");
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
                throw new ArgumentNullException("outputStream");
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
                throw new ArgumentNullException("outputStream");
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
            if (!isDisposed)
            {
                isDisposed = true;
                if (disposing)
                {
                    if (tarOut != null)
                    {
                        tarOut.Flush();
                        tarOut.Close();
                    }
                    if (tarIn != null)
                    {
                        tarIn.Close();
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
            if (isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            while (true)
            {
                TarEntry nextEntry = tarIn.GetNextEntry();
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
                if (keepOldFiles)
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
            if (asciiTranslate)
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
            num = tarIn.Read(buffer, 0, buffer.Length);
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
            if (isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            while (true)
            {
                TarEntry nextEntry = tarIn.GetNextEntry();
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
            if (isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            asciiTranslate = translateAsciiFiles;
        }

        public void SetKeepOldFiles(bool keepExistingFiles)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            keepOldFiles = keepExistingFiles;
        }

        public void SetUserInfo(int userId, string userName, int groupId, string _groupName)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("TarArchive");
            }
            _userId = userId;
            _userName = userName;
            _groupId = groupId;
            groupName = _groupName;
            applyUserInfoOverrides = true;
        }

        public void WriteEntry(TarEntry sourceEntry, bool recurse)
        {
            if (sourceEntry == null)
            {
                throw new ArgumentNullException("sourceEntry");
            }
            if (isDisposed)
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
            if (applyUserInfoOverrides)
            {
                entry.GroupId = _groupId;
                entry.GroupName = groupName;
                entry.UserId = _userId;
                entry.UserName = _userName;
            }
            OnProgressMessageEvent(entry, null);
            if ((asciiTranslate && !entry.IsDirectory) && !IsBinary(file))
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
            if ((rootPath != null) && entry.Name.StartsWith(rootPath))
            {
                str4 = entry.Name.Substring(rootPath.Length + 1);
            }
            if (pathPrefix != null)
            {
                str4 = (str4 == null) ? (pathPrefix + "/" + entry.Name) : (pathPrefix + "/" + str4);
            }
            if (str4 != null)
            {
                entry.Name = str4;
            }
            tarOut.PutNextEntry(entry);
            if (entry.IsDirectory)
            {
                if (recurse)
                {
                    TarEntry[] directoryEntries = entry.GetDirectoryEntries();
                    for (int i = 0; i < directoryEntries.Length; i++)
                    {
                        WriteEntryCore(directoryEntries[i], true);
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
                    tarOut.Write(buffer, 0, count);
                }
            }
        Label_01F6:
            if (!string.IsNullOrEmpty(path))
            {
                File.Delete(path);
            }
            tarOut.CloseEntry();
        }

        public bool ApplyUserInfoOverrides
        {
            get
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return applyUserInfoOverrides;
            }
            set
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                applyUserInfoOverrides = value;
            }
        }

        public bool AsciiTranslate
        {
            get
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return asciiTranslate;
            }
            set
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                asciiTranslate = value;
            }
        }

        public int GroupId
        {
            get
            {
                if (isDisposed)
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
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return groupName;
            }
        }

        public bool IsStreamOwner
        {
            set
            {
                if (tarIn != null)
                {
                    tarIn.IsStreamOwner = value;
                }
                else
                {
                    tarOut.IsStreamOwner = value;
                }
            }
        }

        public string PathPrefix
        {
            get
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return pathPrefix;
            }
            set
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                pathPrefix = value;
            }
        }

        public int RecordSize
        {
            get
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                if (tarIn != null)
                {
                    return tarIn.RecordSize;
                }
                if (tarOut != null)
                {
                    return tarOut.RecordSize;
                }
                return 0x2800;
            }
        }

        public string RootPath
        {
            get
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return rootPath;
            }
            set
            {
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                rootPath = value;
            }
        }

        public int UserId
        {
            get
            {
                if (isDisposed)
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
                if (isDisposed)
                {
                    throw new ObjectDisposedException("TarArchive");
                }
                return _userName;
            }
        }
    }
}
