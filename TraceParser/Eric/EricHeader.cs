using Lte.Domain.Common;

namespace TraceParser.Eric
{
    public class EricHeader : IEric
    {
        public EricHeader(uint recordLength, uint recordType, BigEndianBinaryReader contentStream)
        {
            RecordLength = recordLength;
            RecordType = recordType;
            FileFormatVersion = contentStream.ReadChars(5);
            PmRecordingRevision = contentStream.ReadChars(((int)RecordLength) - 0x18f);
            Year = contentStream.ReadUInt16();
            Month = contentStream.ReadByte();
            Day = contentStream.ReadByte();
            Hour = contentStream.ReadByte();
            Minute = contentStream.ReadByte();
            Second = contentStream.ReadByte();
            NeUserLabel = contentStream.ReadChars(0x80);
            NeLogicalName = contentStream.ReadChars(0xff);
        }

        public uint Day { get; set; }

        private char[] FileFormatVersion { get; set; }

        public uint Hour { get; set; }

        public uint Minute { get; set; }

        public uint Month { get; set; }

        private char[] NeLogicalName { get; set; }

        private char[] NeUserLabel { get; set; }

        private char[] PmRecordingRevision { get; set; }

        public uint RecordLength { get; set; }

        public uint RecordType { get; set; }

        public uint Second { get; set; }

        public uint Year { get; set; }
    }

    public enum EricHeadEnum
    {
        EricHeader,
        EricTcpStream,
        EricUdpStream,
        EricScanner,
        EricEvent,
        EricFooter
    }

}
