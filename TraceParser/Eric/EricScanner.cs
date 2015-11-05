using Lte.Domain.Common;

namespace TraceParser.Eric
{
    public class EricScanner : IEric
    {
        public EricScanner(uint recordLength, uint recordType, BigEndianBinaryReader contentStream)
        {
            RecordLength = recordLength;
            RecordType = recordType;
            Hour = contentStream.ReadByte();
            Minute = contentStream.ReadByte();
            Second = contentStream.ReadUInt16();
            Millisecond = contentStream.ReadUInt16();
            Scannerid = contentStream.ReadUInts(3);
            Status = contentStream.ReadByte();
            PaddingBytes = contentStream.ReadChars(2);
        }

        public uint Hour { get; set; }

        public uint Millisecond { get; set; }

        public uint Minute { get; set; }

        private char[] PaddingBytes { get; set; }

        public uint RecordLength { get; set; }

        public uint RecordType { get; set; }

        public uint Scannerid { get; set; }

        public uint Second { get; set; }

        public uint Status { get; set; }
    }
}
