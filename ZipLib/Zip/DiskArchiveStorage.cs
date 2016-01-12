using System;
using System.IO;

namespace ZipLib.Zip
{
    public class DiskArchiveStorage : BaseArchiveStorage
    {
        private readonly string _fileName;
        private string _temporaryName;
        private Stream _temporaryStream;

        public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode = FileUpdateMode.Safe)
            : base(updateMode)
        {
            if (file.Name == null)
            {
                throw new ZipException("Cant handle non file archives");
            }
            _fileName = file.Name;
        }

        public override Stream ConvertTemporaryToFinal()
        {
            if (_temporaryStream == null)
            {
                throw new ZipException("No temporary stream has been created");
            }
            Stream stream;
            string tempFileName = GetTempFileName(_fileName, false);
            bool flag = false;
            try
            {
                _temporaryStream.Close();
                File.Move(_fileName, tempFileName);
                File.Move(_temporaryName, _fileName);
                flag = true;
                File.Delete(tempFileName);
                stream = File.Open(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                if (!flag)
                {
                    File.Move(tempFileName, _fileName);
                    File.Delete(_temporaryName);
                }
                throw;
            }
            return stream;
        }

        public override void Dispose()
        {
            _temporaryStream?.Close();
        }

        private static string GetTempFileName(string original, bool makeTempFile)
        {
            string str = null;
            if (original == null)
            {
                return Path.GetTempFileName();
            }
            int num = 0;
            int second = DateTime.Now.Second;
            while (str == null)
            {
                num++;
                string path = $"{original}.{second}{num}.tmp";
                if (!File.Exists(path))
                {
                    if (makeTempFile)
                    {
                        try
                        {
                            using (File.Create(path))
                            {
                            }
                            str = path;
                        }
                        catch
                        {
                            second = DateTime.Now.Second;
                        }
                    }
                    else
                    {
                        str = path;
                    }
                }
            }
            return str;
        }

        public override Stream GetTemporaryOutput()
        {
            if (_temporaryName != null)
            {
                _temporaryName = GetTempFileName(_temporaryName, true);
                _temporaryStream = File.Open(_temporaryName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            }
            else
            {
                _temporaryName = Path.GetTempFileName();
                _temporaryStream = File.Open(_temporaryName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            }
            return _temporaryStream;
        }

        public override Stream MakeTemporaryCopy(Stream stream)
        {
            stream.Close();
            _temporaryName = GetTempFileName(_fileName, true);
            File.Copy(_fileName, _temporaryName, true);
            _temporaryStream = new FileStream(_temporaryName, FileMode.Open, FileAccess.ReadWrite);
            return _temporaryStream;
        }

        public override Stream OpenForDirectUpdate(Stream stream)
        {
            if ((stream == null) || !stream.CanWrite)
            {
                if (stream != null)
                {
                    stream.Close();
                }
                return new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite);
            }
            return stream;
        }
    }
}
