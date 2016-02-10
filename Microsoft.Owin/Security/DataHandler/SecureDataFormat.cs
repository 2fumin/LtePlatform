using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.Owin.Security.DataHandler
{
    public class SecureDataFormat<TData> : ISecureDataFormat<TData>
    {
        private readonly ITextEncoder _encoder;
        private readonly IDataProtector _protector;
        private readonly IDataSerializer<TData> _serializer;

        public SecureDataFormat(IDataSerializer<TData> serializer, IDataProtector protector, ITextEncoder encoder)
        {
            _serializer = serializer;
            _protector = protector;
            _encoder = encoder;
        }

        public string Protect(TData data)
        {
            var userData = _serializer.Serialize(data);
            var buffer2 = _protector.Protect(userData);
            return _encoder.Encode(buffer2);
        }

        public TData Unprotect(string protectedText)
        {
            try
            {
                if (protectedText == null)
                {
                    return default(TData);
                }
                var protectedData = _encoder.Decode(protectedText);
                if (protectedData == null)
                {
                    return default(TData);
                }
                var data = _protector.Unprotect(protectedData);
                return data == null ? default(TData) : _serializer.Deserialize(data);
            }
            catch
            {
                return default(TData);
            }
        }
    }
}
