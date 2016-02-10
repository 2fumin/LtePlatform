using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Owin.Win32;

namespace Microsoft.Owin.Security
{
    public class CertificateSubjectPublicKeyInfoValidator : ICertificateValidator
    {
        private readonly SubjectPublicKeyInfoAlgorithm _algorithm;
        private readonly HashSet<string> _validBase64EncodedSubjectPublicKeyInfoHashes;

        public CertificateSubjectPublicKeyInfoValidator(IEnumerable<string> validBase64EncodedSubjectPublicKeyInfoHashes, SubjectPublicKeyInfoAlgorithm algorithm)
        {
            if (validBase64EncodedSubjectPublicKeyInfoHashes == null)
            {
                throw new ArgumentNullException(nameof(validBase64EncodedSubjectPublicKeyInfoHashes));
            }
            _validBase64EncodedSubjectPublicKeyInfoHashes = new HashSet<string>(validBase64EncodedSubjectPublicKeyInfoHashes);
            if (_validBase64EncodedSubjectPublicKeyInfoHashes.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(validBase64EncodedSubjectPublicKeyInfoHashes));
            }
            if ((_algorithm != SubjectPublicKeyInfoAlgorithm.Sha1) && (_algorithm != SubjectPublicKeyInfoAlgorithm.Sha256))
            {
                throw new ArgumentOutOfRangeException(nameof(algorithm));
            }
            _algorithm = algorithm;
        }

        private HashAlgorithm CreateHashAlgorithm()
        {
            if (_algorithm != SubjectPublicKeyInfoAlgorithm.Sha1)
            {
                return new SHA256CryptoServiceProvider();
            }
            return new SHA1CryptoServiceProvider();
        }

        private static byte[] ExtractSpkiBlob(X509Certificate2 certificate)
        {
            NativeMethods.CERT_CONTEXT cert_context = (NativeMethods.CERT_CONTEXT)Marshal.PtrToStructure(certificate.Handle, typeof(NativeMethods.CERT_CONTEXT));
            NativeMethods.CERT_INFO cert_info = (NativeMethods.CERT_INFO)Marshal.PtrToStructure(cert_context.pCertInfo, typeof(NativeMethods.CERT_INFO));
            NativeMethods.CERT_PUBLIC_KEY_INFO subjectPublicKeyInfo = cert_info.SubjectPublicKeyInfo;
            uint pcbEncoded = 0;
            IntPtr lpszStructType = new IntPtr(8);
            if (!NativeMethods.CryptEncodeObject(1, lpszStructType, ref subjectPublicKeyInfo, null, ref pcbEncoded))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            byte[] pbEncoded = new byte[pcbEncoded];
            if (!NativeMethods.CryptEncodeObject(1, lpszStructType, ref subjectPublicKeyInfo, pbEncoded, ref pcbEncoded))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return pbEncoded;
        }

        public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                if (chain == null)
                {
                    throw new ArgumentNullException(nameof(chain));
                }
                if (chain.ChainElements.Count < 2)
                {
                    return false;
                }
                using (HashAlgorithm algorithm = CreateHashAlgorithm())
                {
                    X509ChainElementEnumerator enumerator = chain.ChainElements.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        X509Certificate2 certificate2 = enumerator.Current.Certificate;
                        string item = Convert.ToBase64String(algorithm.ComputeHash(ExtractSpkiBlob(certificate2)));
                        if (_validBase64EncodedSubjectPublicKeyInfoHashes.Contains(item))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
