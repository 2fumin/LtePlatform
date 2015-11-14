using System.IO;
using System.Runtime.Serialization;

namespace Lte.Domain.Lz4Net.ExtraZip
{
    [DataContract]
    public class Lz4PackageEntry
    {
        public ExtraDecompressStream Open()
        {
            FileStream targetStream = new FileStream(Package.Filename, FileMode.Open, 
                FileAccess.Read, FileShare.Read, 0x100000);
            targetStream.Seek(Entry, SeekOrigin.Begin);
            return new ExtraDecompressStream(targetStream, true);
        }

        public override string ToString()
        {
            return Filename;
        }

        [DataMember(Name = "compressedSize", Order = 3)]
        public long CompressedSize { get; internal set; }

        [DataMember(Name = "create", Order = 5)]
        public string CreateTime { get; internal set; }

        [DataMember(Name = "entry", Order = 7)]
        public long Entry { get; internal set; }

        [DataMember(Name = "add", Order = 4)]
        public string EntryCreateTime { get; internal set; }

        [DataMember(Name = "name", Order = 1)]
        public string Filename { get; internal set; }

        [DataMember(Name = "modify", Order = 6)]
        public string LastModifyTime { get; internal set; }

        [DataMember(Name = "origionSize", Order = 2)]
        public long OriginSize { get; internal set; }

        public Lz4Package Package { get; internal set; }
    }
}

