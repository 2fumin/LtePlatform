using System;
using System.Runtime.Serialization;

namespace Lte.Domain.ZipLib.Lzw
{
    [Serializable]
    public class LzwException : SharpZipBaseException
    {
        public LzwException()
        {
        }

        public LzwException(string message)
            : base(message)
        {
        }

        protected LzwException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public LzwException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
