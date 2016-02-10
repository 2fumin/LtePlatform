using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
    using System;

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
