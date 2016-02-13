using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
    internal class OwinHttpRequestContext : HttpRequestContext
    {
        private X509Certificate2 _clientCertificate;
        private bool _clientCertificateSet;
        private readonly IOwinContext _context;
        private bool _includeErrorDetail;
        private bool _includeErrorDetailSet;
        private bool _isLocal;
        private bool _isLocalSet;
        private readonly HttpRequestMessage _request;
        private UrlHelper _url;
        private bool _urlSet;
        private string _virtualPathRoot;
        private bool _virtualPathRootSet;

        public OwinHttpRequestContext(IOwinContext context, HttpRequestMessage request)
        {
            _context = context;
            _request = request;
        }

        public override X509Certificate2 ClientCertificate
        {
            get
            {
                if (!_clientCertificateSet)
                {
                    _clientCertificate = _context.Get<X509Certificate2>(OwinConstants.ClientCertifiateKey);
                    _clientCertificateSet = true;
                }
                return _clientCertificate;
            }
            set
            {
                _clientCertificate = value;
                _clientCertificateSet = true;
            }
        }

        public IOwinContext Context
        {
            get
            {
                return _context;
            }
        }

        public override bool IncludeErrorDetail
        {
            get
            {
                if (!_includeErrorDetailSet)
                {
                    IncludeErrorDetailPolicy includeErrorDetailPolicy;
                    bool isLocal;
                    HttpConfiguration configuration = Configuration;
                    if (configuration != null)
                    {
                        includeErrorDetailPolicy = configuration.IncludeErrorDetailPolicy;
                    }
                    else
                    {
                        includeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;
                    }
                    switch (includeErrorDetailPolicy)
                    {
                        case IncludeErrorDetailPolicy.Default:
                        case IncludeErrorDetailPolicy.LocalOnly:
                            isLocal = IsLocal;
                            break;

                        case IncludeErrorDetailPolicy.Always:
                            isLocal = true;
                            break;

                        default:
                            isLocal = false;
                            break;
                    }
                    _includeErrorDetail = isLocal;
                    _includeErrorDetailSet = true;
                }
                return _includeErrorDetail;
            }
            set
            {
                _includeErrorDetail = value;
                _includeErrorDetailSet = true;
            }
        }

        public override bool IsLocal
        {
            get
            {
                if (!_isLocalSet)
                {
                    _isLocal = _context.Get<bool>(OwinConstants.IsLocalKey);
                    _isLocalSet = true;
                }
                return _isLocal;
            }
            set
            {
                _isLocal = value;
                _isLocalSet = true;
            }
        }

        public override IPrincipal Principal
        {
            get
            {
                return _context.Request.User;
            }
            set
            {
                _context.Request.User = value;
                Thread.CurrentPrincipal = value;
            }
        }

        public HttpRequestMessage Request
        {
            get
            {
                return _request;
            }
        }

        public override UrlHelper Url
        {
            get
            {
                if (!_urlSet)
                {
                    _url = new UrlHelper(_request);
                    _urlSet = true;
                }
                return _url;
            }
            set
            {
                _url = value;
                _urlSet = true;
            }
        }

        public override string VirtualPathRoot
        {
            get
            {
                if (!_virtualPathRootSet)
                {
                    string str = _context.Request.PathBase.ToString();
                    _virtualPathRoot = string.IsNullOrEmpty(str) ? "/" : str;
                    _virtualPathRootSet = true;
                }
                return _virtualPathRoot;
            }
            set
            {
                _virtualPathRoot = value;
                _virtualPathRootSet = true;
            }
        }
    }
}
