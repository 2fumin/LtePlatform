using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
    public class PropertiesSerializer : IDataSerializer<AuthenticationProperties>
    {
        private const int FormatVersion = 1;

        public AuthenticationProperties Deserialize(byte[] data)
        {
            AuthenticationProperties properties;
            using (var stream = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(stream))
                {
                    properties = Read(reader);
                }
            }
            return properties;
        }

        public static AuthenticationProperties Read(BinaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            if (reader.ReadInt32() != FormatVersion)
            {
                return null;
            }
            var capacity = reader.ReadInt32();
            var dictionary = new Dictionary<string, string>(capacity);
            for (var i = 0; i != capacity; i++)
            {
                var key = reader.ReadString();
                var str2 = reader.ReadString();
                dictionary.Add(key, str2);
            }
            return new AuthenticationProperties(dictionary);
        }

        public byte[] Serialize(AuthenticationProperties model)
        {
            byte[] buffer;
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    Write(writer, model);
                    writer.Flush();
                    buffer = stream.ToArray();
                }
            }
            return buffer;
        }

        public static void Write(BinaryWriter writer, AuthenticationProperties properties)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            writer.Write(1);
            writer.Write(properties.Dictionary.Count);
            foreach (var pair in properties.Dictionary)
            {
                writer.Write(pair.Key);
                writer.Write(pair.Value);
            }
        }
    }
}
