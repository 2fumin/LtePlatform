using Lte.Domain.Common;

namespace TraceParser.Eric
{
    public class EricUdpStream : IEric
    {
        public EricUdpStream(uint recordLength, uint recordType, BigEndianBinaryReader contentStream)
        {
            RecordLength = recordLength;
            RecordType = recordType;
            FileFormatVersion = contentStream.ReadChars(5);
            PmRecordingRevision = contentStream.ReadChars(5);
            TraceReference = contentStream.ReadChars(6);
            Scanner = contentStream.ReadUInt16();
            Package = contentStream.ReadUInt32();
        }

        private char[] FileFormatVersion { get; set; }

        public uint Package { get; set; }

        private char[] PmRecordingRevision { get; set; }

        public uint RecordLength { get; set; }

        public uint RecordType { get; set; }

        public uint Scanner { get; set; }

        private char[] TraceReference { get; set; }
    }
}
