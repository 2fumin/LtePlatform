using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
    public class CertificateThumbprintValidator : ICertificateValidator
    {
        private readonly HashSet<string> _validCertificateThumbprints;

        public CertificateThumbprintValidator(IEnumerable<string> validThumbprints)
        {
            if (validThumbprints == null)
            {
                throw new ArgumentNullException(nameof(validThumbprints));
            }
            _validCertificateThumbprints = new HashSet<string>(validThumbprints, StringComparer.OrdinalIgnoreCase);
            if (_validCertificateThumbprints.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(validThumbprints));
            }
        }

        public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.None) return false;
            if (chain == null)
            {
                throw new ArgumentNullException(nameof(chain));
            }
            if (chain.ChainElements.Count < 2)
            {
                return false;
            }
            var enumerator = chain.ChainElements.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var thumbprint = enumerator.Current.Certificate.Thumbprint;
                if ((thumbprint != null) && _validCertificateThumbprints.Contains(thumbprint))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
