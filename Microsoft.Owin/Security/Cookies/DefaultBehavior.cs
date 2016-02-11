using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Microsoft.Owin.Security.Cookies
{
    internal static class DefaultBehavior
    {
        internal static readonly Action<CookieApplyRedirectContext> ApplyRedirect;

        static DefaultBehavior()
        {
            ApplyRedirect = delegate (CookieApplyRedirectContext context) {
                if (IsAjaxRequest(context.Request))
                {
                    RespondedJson json2 = new RespondedJson
                    {
                        Status = context.Response.StatusCode
                    };
                    RespondedJson.RespondedJsonHeaders headers = new RespondedJson.RespondedJsonHeaders
                    {
                        Location = context.RedirectUri
                    };
                    json2.Headers = headers;
                    RespondedJson json = json2;
                    context.Response.StatusCode = 200;
                    context.Response.Headers.Append("X-Responded-JSON", json.ToString());
                }
                else
                {
                    context.Response.Redirect(context.RedirectUri);
                }
            };
        }

        private static bool IsAjaxRequest(IOwinRequest request)
        {
            IReadableStringCollection query = request.Query;
            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            IHeaderDictionary headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        [DataContract]
        private class RespondedJson
        {
            public static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(RespondedJson));

            public override string ToString()
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    Serializer.WriteObject(stream, this);
                    return Encoding.ASCII.GetString(stream.ToArray());
                }
            }

            [DataMember(Name = "headers", Order = 2)]
            public RespondedJsonHeaders Headers { get; set; }

            [DataMember(Name = "status", Order = 1)]
            public int Status { get; set; }

            [DataContract]
            public class RespondedJsonHeaders
            {
                [DataMember(Name = "location", Order = 1)]
                public string Location { get; set; }
            }
        }
    }
}
