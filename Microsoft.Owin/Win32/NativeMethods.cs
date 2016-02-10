using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Owin.Win32
{
    [Localizable(false)]
    internal static class NativeMethods
    {
        public const int X509_ASN_ENCODING = 1;
        public const int X509_PUBLIC_KEY_INFO = 8;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("crypt32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        internal static extern bool CryptEncodeObject(uint dwCertEncodingType, IntPtr lpszStructType, ref CERT_PUBLIC_KEY_INFO pvStructInfo, byte[] pbEncoded, ref uint pcbEncoded);

        [StructLayout(LayoutKind.Sequential)]
        internal struct CERT_CONTEXT
        {
            public readonly int dwCertEncodingType;
            public IntPtr pbCertEncoded;
            public readonly int cbCertEncoded;
            public IntPtr pCertInfo;
            public IntPtr hCertStore;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class CERT_INFO
        {
            public int dwVersion;
            public CRYPT_BLOB SerialNumber;
            public CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;
            public CRYPT_BLOB Issuer;
            public System.Runtime.InteropServices.ComTypes.FILETIME NotBefore;
            public System.Runtime.InteropServices.ComTypes.FILETIME NotAfter;
            public CRYPT_BLOB Subject;
            public CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;
            public CRYPT_BIT_BLOB IssuerUniqueId;
            public CRYPT_BIT_BLOB SubjectUniqueId;
            public int cExtension;
            public IntPtr rgExtension;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_PUBLIC_KEY_INFO
        {
            public CRYPT_ALGORITHM_IDENTIFIER Algorithm;
            public CRYPT_BIT_BLOB PublicKey;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_ALGORITHM_IDENTIFIER
        {
            public readonly string pszObjId;
            public CRYPT_BLOB Parameters;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_BIT_BLOB
        {
            public readonly int cbData;
            public IntPtr pbData;
            public readonly int cUnusedBits;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPT_BLOB
        {
            public readonly int cbData;
            public IntPtr pbData;
        }
    }
}
