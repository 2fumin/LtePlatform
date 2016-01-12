using System;
using System.Runtime.Serialization;

namespace ZipLib.Tar
{
    [Serializable]
    public class TarException : SharpZipBaseException
    {
        protected TarException()
        {
        }

        public TarException(string message)
            : base(message)
        {
        }

        protected TarException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public TarException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }

    [Serializable]
    public class InvalidHeaderException : TarException
    {
        public InvalidHeaderException()
        {
        }

        public InvalidHeaderException(string message)
            : base(message)
        {
        }

        protected InvalidHeaderException(SerializationInfo information, StreamingContext context)
            : base(information, context)
        {
        }

        public InvalidHeaderException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }

    public delegate void ProgressMessageHandler(TarArchive archive, TarEntry entry, string message);

}
