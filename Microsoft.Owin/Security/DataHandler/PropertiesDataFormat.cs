using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.DataHandler
{
    using Microsoft.Owin.Security.DataProtection;
    using System;

    public class PropertiesDataFormat : SecureDataFormat<AuthenticationProperties>
    {
        public PropertiesDataFormat(IDataProtector protector) : base(DataSerializers.Properties, protector, TextEncodings.Base64Url)
        {
        }
    }
}
