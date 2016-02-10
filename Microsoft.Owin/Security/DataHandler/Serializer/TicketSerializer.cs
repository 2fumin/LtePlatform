using System;
using System.IdentityModel.Tokens;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
    public class TicketSerializer : IDataSerializer<AuthenticationTicket>
    {
        private const int FormatVersion = 3;

        public virtual AuthenticationTicket Deserialize(byte[] data)
        {
            AuthenticationTicket ticket;
            using (var stream = new MemoryStream(data))
            {
                using (var stream2 = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (var reader = new BinaryReader(stream2))
                    {
                        ticket = Read(reader);
                    }
                }
            }
            return ticket;
        }

        public static AuthenticationTicket Read(BinaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            if (reader.ReadInt32() != FormatVersion)
            {
                return null;
            }
            var authenticationType = reader.ReadString();
            var defaultValue = ReadWithDefault(reader, DefaultValues.NameClaimType);
            var roleType = ReadWithDefault(reader, DefaultValues.RoleClaimType);
            var num = reader.ReadInt32();
            var claims = new Claim[num];
            for (var i = 0; i != num; i++)
            {
                var type = ReadWithDefault(reader, defaultValue);
                var str5 = reader.ReadString();
                var valueType = ReadWithDefault(reader, DefaultValues.StringValueType);
                var str7 = ReadWithDefault(reader, DefaultValues.LocalAuthority);
                var originalIssuer = ReadWithDefault(reader, str7);
                claims[i] = new Claim(type, str5, valueType, str7, originalIssuer);
            }
            var identity = new ClaimsIdentity(claims, authenticationType, defaultValue, roleType);
            if (reader.ReadInt32() > 0)
            {
                identity.BootstrapContext = new BootstrapContext(reader.ReadString());
            }
            return new AuthenticationTicket(identity, PropertiesSerializer.Read(reader));
        }

        private static string ReadWithDefault(BinaryReader reader, string defaultValue)
        {
            var a = reader.ReadString();
            if (string.Equals(a, DefaultValues.DefaultStringPlaceholder, StringComparison.Ordinal))
            {
                return defaultValue;
            }
            return a;
        }

        public virtual byte[] Serialize(AuthenticationTicket model)
        {
            using (var stream = new MemoryStream())
            {
                using (var stream2 = new GZipStream(stream, CompressionLevel.Optimal))
                {
                    using (var writer = new BinaryWriter(stream2))
                    {
                        Write(writer, model);
                    }
                }
                return stream.ToArray();
            }
        }

        public static void Write(BinaryWriter writer, AuthenticationTicket model)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            writer.Write(3);
            var identity = model.Identity;
            writer.Write(identity.AuthenticationType);
            WriteWithDefault(writer, identity.NameClaimType, DefaultValues.NameClaimType);
            WriteWithDefault(writer, identity.RoleClaimType, DefaultValues.RoleClaimType);
            writer.Write(identity.Claims.Count());
            foreach (var claim in identity.Claims)
            {
                WriteWithDefault(writer, claim.Type, identity.NameClaimType);
                writer.Write(claim.Value);
                WriteWithDefault(writer, claim.ValueType, DefaultValues.StringValueType);
                WriteWithDefault(writer, claim.Issuer, DefaultValues.LocalAuthority);
                WriteWithDefault(writer, claim.OriginalIssuer, claim.Issuer);
            }
            var bootstrapContext = identity.BootstrapContext as BootstrapContext;
            if (string.IsNullOrWhiteSpace(bootstrapContext?.Token))
            {
                writer.Write(0);
            }
            else
            {
                writer.Write(bootstrapContext.Token.Length);
                writer.Write(bootstrapContext.Token);
            }
            PropertiesSerializer.Write(writer, model.Properties);
        }

        private static void WriteWithDefault(BinaryWriter writer, string value, string defaultValue)
        {
            writer.Write(string.Equals(value, defaultValue, StringComparison.Ordinal)
                ? DefaultValues.DefaultStringPlaceholder
                : value);
        }

        private static class DefaultValues
        {
            public const string DefaultStringPlaceholder = "\0";
            public const string LocalAuthority = "LOCAL AUTHORITY";
            public const string NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
            public const string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";
        }
    }
}
