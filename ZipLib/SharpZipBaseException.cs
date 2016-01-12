using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.ZipLib
{
    [Serializable]
    public class SharpZipBaseException : ApplicationException
    {
        public SharpZipBaseException()
        {
        }

        public SharpZipBaseException(string message)
            : base(message)
        {
        }

        protected SharpZipBaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SharpZipBaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
