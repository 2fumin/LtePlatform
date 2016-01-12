using System.IO;
using Lte.Domain.Lz4Net.Core;

namespace ZipLib.Zip
{
    public class MemoryArchiveStorage : BaseArchiveStorage
    {
        private MemoryStream _finalStream;
        private MemoryStream _temporaryStream;

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
            if (_temporaryStream == null)
            {
                throw new ZipException("No temporary stream has been created");
            }
            _finalStream = new MemoryStream(_temporaryStream.ToArray());
            return _finalStream;
        }

        public override void Dispose()
        {
            if (_temporaryStream != null)
            {
                _temporaryStream.Close();
            }
        }

        public override Stream GetTemporaryOutput()
        {
            _temporaryStream = new MemoryStream();
            return _temporaryStream;
        }

        public override Stream MakeTemporaryCopy(Stream stream)
        {
            _temporaryStream = new MemoryStream();
            stream.Position = 0L;
            StreamUtils.Copy(stream, _temporaryStream, new byte[0x1000]);
            return _temporaryStream;
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
                return _finalStream;
            }
        }
    }
}

