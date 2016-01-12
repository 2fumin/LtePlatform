using System.IO;
using Lte.Domain.Lz4Net.Core;

namespace Lte.Domain.ZipLib.Zip
{
    public class MemoryArchiveStorage : BaseArchiveStorage
    {
        private MemoryStream finalStream_;
        private MemoryStream temporaryStream_;

        public MemoryArchiveStorage()
            : base(FileUpdateMode.Direct)
        {
        }

        public MemoryArchiveStorage(FileUpdateMode updateMode)
            : base(updateMode)
        {
        }

        public override Stream ConvertTemporaryToFinal()
        {
            if (temporaryStream_ == null)
            {
                throw new ZipException("No temporary stream has been created");
            }
            finalStream_ = new MemoryStream(temporaryStream_.ToArray());
            return finalStream_;
        }

        public override void Dispose()
        {
            if (temporaryStream_ != null)
            {
                temporaryStream_.Close();
            }
        }

        public override Stream GetTemporaryOutput()
        {
            temporaryStream_ = new MemoryStream();
            return temporaryStream_;
        }

        public override Stream MakeTemporaryCopy(Stream stream)
        {
            temporaryStream_ = new MemoryStream();
            stream.Position = 0L;
            StreamUtils.Copy(stream, temporaryStream_, new byte[0x1000]);
            return temporaryStream_;
        }

        public override Stream OpenForDirectUpdate(Stream stream)
        {
            if ((stream == null) || !stream.CanWrite)
            {
                Stream destination = new MemoryStream();
                if (stream != null)
                {
                    stream.Position = 0L;
                    StreamUtils.Copy(stream, destination, new byte[0x1000]);
                    stream.Close();
                }
                return destination;
            }
            return stream;
        }

        public MemoryStream FinalStream
        {
            get
            {
                return finalStream_;
            }
        }
    }
}

