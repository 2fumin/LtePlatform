using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    public class Base64TextEncoder : ITextEncoder
    {
        public byte[] Decode(string text)
        {
            return Convert.FromBase64String(text);
        }

        public string Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
