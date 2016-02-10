using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    public class Base64UrlTextEncoder : ITextEncoder
    {
        public byte[] Decode(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            return Convert.FromBase64String(Pad(text.Replace('-', '+').Replace('_', '/')));
        }

        public string Encode(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            return Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        private static string Pad(string text)
        {
            var count = 3 - ((text.Length + 3) % 4);
            if (count == 0)
            {
                return text;
            }
            return (text + new string('=', count));
        }
    }
}
