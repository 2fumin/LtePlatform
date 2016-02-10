using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
    public class CertificateSubjectKeyIdentifierValidator : ICertificateValidator
    {
        private readonly HashSet<string> _validSubjectKeyIdentifiers;

        public CertificateSubjectKeyIdentifierValidator(IEnumerable<string> validSubjectKeyIdentifiers)
        {
            if (validSubjectKeyIdentifiers == null)
            {
                throw new ArgumentNullException(nameof(validSubjectKeyIdentifiers));
            }
            _validSubjectKeyIdentifiers = new HashSet<string>(validSubjectKeyIdentifiers, StringComparer.OrdinalIgnoreCase);
            if (_validSubjectKeyIdentifiers.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(validSubjectKeyIdentifiers));
            }
        }

        private static string GetSubjectKeyIdentifier(X509Certificate2 certificate)
        {
            var extension = certificate.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
            return extension?.SubjectKeyIdentifier;
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
                var subjectKeyIdentifier = GetSubjectKeyIdentifier(enumerator.Current.Certificate);
                if (!string.IsNullOrWhiteSpace(subjectKeyIdentifier) && _validSubjectKeyIdentifiers.Contains(subjectKeyIdentifier))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
