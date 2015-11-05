using System.IO;

namespace Lte.Domain.ZipLib.Zip
{
    public abstract class BaseArchiveStorage : IArchiveStorage
    {
        private FileUpdateMode updateMode_;

        protected BaseArchiveStorage(FileUpdateMode updateMode)
        {
            updateMode_ = updateMode;
        }

        public abstract Stream ConvertTemporaryToFinal();
        public abstract void Dispose();
        public abstract Stream GetTemporaryOutput();
        public abstract Stream MakeTemporaryCopy(Stream stream);
        public abstract Stream OpenForDirectUpdate(Stream stream);

        public FileUpdateMode UpdateMode
        {
            get
            {
                return updateMode_;
            }
        }
    }
}

