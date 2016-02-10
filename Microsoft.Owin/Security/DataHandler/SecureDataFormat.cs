using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.DataHandler
{
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using Microsoft.Owin.Security.DataHandler.Serializer;
    using Microsoft.Owin.Security.DataProtection;
    using System;

    public class SecureDataFormat<TData> : ISecureDataFormat<TData>
    {
        private readonly ITextEncoder _encoder;
        private readonly IDataProtector _protector;
        private readonly IDataSerializer<TData> _serializer;

        public SecureDataFormat(IDataSerializer<TData> serializer, IDataProtector protector, ITextEncoder encoder)
        {
            this._serializer = serializer;
            this._protector = protector;
            this._encoder = encoder;
        }

        public string Protect(TData data)
        {
            byte[] userData = this._serializer.Serialize(data);
            byte[] buffer2 = this._protector.Protect(userData);
            return this._encoder.Encode(buffer2);
        }

        public TData Unprotect(string protectedText)
        {
            try
            {
                if (protectedText == null)
                {
                    return default(TData);
                }
                byte[] protectedData = this._encoder.Decode(protectedText);
                if (protectedData == null)
                {
                    return default(TData);
                }
                byte[] data = this._protector.Unprotect(protectedData);
                if (data == null)
                {
                    return default(TData);
                }
                return this._serializer.Deserialize(data);
            }
            catch
            {
                return default(TData);
            }
        }
    }
}
