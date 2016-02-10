using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Owin.Security
{
    public class AuthenticationDescription
    {
        private const string AuthenticationTypePropertyKey = "AuthenticationType";
        private const string CaptionPropertyKey = "Caption";

        public AuthenticationDescription()
        {
            Properties = new Dictionary<string, object>(StringComparer.Ordinal);
        }

        public AuthenticationDescription(IDictionary<string, object> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            Properties = properties;
        }

        private string GetString(string name)
        {
            object obj2;
            if (Properties.TryGetValue(name, out obj2))
            {
                return Convert.ToString(obj2, CultureInfo.InvariantCulture);
            }
            return null;
        }

        public string AuthenticationType
        {
            get
            {
                return GetString(AuthenticationTypePropertyKey);
            }
            set
            {
                Properties[AuthenticationTypePropertyKey] = value;
            }
        }

        public string Caption
        {
            get
            {
                return GetString(CaptionPropertyKey);
            }
            set
            {
                Properties[CaptionPropertyKey] = value;
            }
        }

        public IDictionary<string, object> Properties { get; }
    }
}
