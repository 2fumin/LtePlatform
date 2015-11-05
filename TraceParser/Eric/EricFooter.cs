using Lte.Domain.Common;

namespace TraceParser.Eric
{
    public class EricFooter : IEric
    {
        public EricFooter(uint recordLength, uint recordType, BigEndianBinaryReader contentStream)
        {
            RecordLength = recordLength;
            RecordType = recordType;
            Year = contentStream.ReadUInt16();
            Month = contentStream.ReadByte();
            Day = contentStream.ReadByte();
            Hour = contentStream.ReadByte();
            Minute = contentStream.ReadByte();
            Second = contentStream.ReadByte();
            PaddingBytes = contentStream.ReadByte();
        }

        public uint Day { get; set; }

        public uint Hour { get; set; }

        public uint Minute { get; set; }

        public uint Month { get; set; }

        public uint PaddingBytes { get; set; }

        public uint RecordLength { get; set; }

        public uint RecordType { get; set; }

        public uint Second { get; set; }

        public uint Year { get; set; }
    }
}
