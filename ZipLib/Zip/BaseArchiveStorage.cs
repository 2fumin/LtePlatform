using System.IO;

namespace ZipLib.Zip
{
    public abstract class BaseArchiveStorage : IArchiveStorage
    {
        protected BaseArchiveStorage(FileUpdateMode updateMode)
        {
            UpdateMode = updateMode;
        }

        public abstract Stream ConvertTemporaryToFinal();

        public abstract void Dispose();

        public abstract Stream GetTemporaryOutput();

        public abstract Stream MakeTemporaryCopy(Stream stream);

        public abstract Stream OpenForDirectUpdate(Stream stream);

        public FileUpdateMode UpdateMode { get; }
    }
}

