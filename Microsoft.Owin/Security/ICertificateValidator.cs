using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
    public interface ICertificateValidator
    {
        bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
    }

    public interface ISecureDataFormat<TData>
    {
        string Protect(TData data);

        TData Unprotect(string protectedText);
    }
} 
