using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Owin.Security
{
    public class AuthenticationProperties
    {
        private readonly IDictionary<string, string> _dictionary;
        internal const string ExpiresUtcKey = ".expires";
        internal const string IsPersistentKey = ".persistent";
        internal const string IssuedUtcKey = ".issued";
        internal const string RedirectUriKey = ".redirect";
        internal const string RefreshKey = ".refresh";
        internal const string UtcDateTimeFormat = "r";

        public AuthenticationProperties()
        {
            _dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
        }

        public AuthenticationProperties(IDictionary<string, string> dictionary)
        {
            _dictionary = dictionary ?? new Dictionary<string, string>(StringComparer.Ordinal);
        }

        public bool? AllowRefresh
        {
            get
            {
                string str;
                bool flag;
                if (_dictionary.TryGetValue(RefreshKey, out str) && bool.TryParse(str, out flag))
                {
                    return flag;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _dictionary[RefreshKey] = value.Value.ToString(CultureInfo.InvariantCulture);
                }
                else if (_dictionary.ContainsKey(RefreshKey))
                {
                    _dictionary.Remove(RefreshKey);
                }
            }
        }

        public IDictionary<string, string> Dictionary => _dictionary;

        public DateTimeOffset? ExpiresUtc
        {
            get
            {
                string str;
                DateTimeOffset offset;
                if (_dictionary.TryGetValue(ExpiresUtcKey, out str) &&
                    DateTimeOffset.TryParseExact(str, UtcDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind,
                        out offset))
                {
                    return offset;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _dictionary[ExpiresUtcKey] = value.Value.ToString(UtcDateTimeFormat, CultureInfo.InvariantCulture);
                }
                else if (_dictionary.ContainsKey(ExpiresUtcKey))
                {
                    _dictionary.Remove(ExpiresUtcKey);
                }
            }
        }

        public bool IsPersistent
        {
            get
            {
                return _dictionary.ContainsKey(IsPersistentKey);
            }
            set
            {
                if (_dictionary.ContainsKey(IsPersistentKey))
                {
                    if (!value)
                    {
                        _dictionary.Remove(IsPersistentKey);
                    }
                }
                else if (value)
                {
                    _dictionary.Add(IsPersistentKey, string.Empty);
                }
            }
        }

        public DateTimeOffset? IssuedUtc
        {
            get
            {
                string str;
                DateTimeOffset offset;
                if (_dictionary.TryGetValue(IssuedUtcKey, out str) &&
                    DateTimeOffset.TryParseExact(str, UtcDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind,
                        out offset))
                {
                    return offset;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    _dictionary[IssuedUtcKey] = value.Value.ToString(UtcDateTimeFormat, CultureInfo.InvariantCulture);
                }
                else if (_dictionary.ContainsKey(IssuedUtcKey))
                {
                    _dictionary.Remove(IssuedUtcKey);
                }
            }
        }

        public string RedirectUri
        {
            get
            {
                string str;
                return !_dictionary.TryGetValue(RedirectUriKey, out str) ? null : str;
            }
            set
            {
                if (value != null)
                {
                    _dictionary[RedirectUriKey] = value;
                }
                else if (_dictionary.ContainsKey(RedirectUriKey))
                {
                    _dictionary.Remove(RedirectUriKey);
                }
            }
        }
    }
}
