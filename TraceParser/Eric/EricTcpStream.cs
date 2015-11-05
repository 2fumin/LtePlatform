using Lte.Domain.Common;

namespace TraceParser.Eric
{
    public class EricTcpStream : IEric
    {
        public EricTcpStream(uint recordLength, uint recordType, BigEndianBinaryReader contentStream)
        {
            RecordLength = recordLength;
            RecordType = recordType;
            FileFormatVersion = contentStream.ReadChars(5);
            PmRecordingRevision = contentStream.ReadChars(5);
            Year = contentStream.ReadUInt16();
            Month = contentStream.ReadUInt16();
            Day = contentStream.ReadUInt16();
            Hour = contentStream.ReadUInt16();
            Minute = contentStream.ReadUInt16();
            Second = contentStream.ReadUInt16();
            NeUserLabel = contentStream.ReadChars(0x80);
            NeLogicalName = contentStream.ReadChars(0xff);
            Scanner = contentStream.ReadUInt16();
            CauseOfHeader = contentStream.ReadUInt16();
            NoOfDroppedEvents = contentStream.ReadUInt32();
        }

        public uint CauseOfHeader { get; set; }

        public uint Day { get; set; }

        private char[] FileFormatVersion { get; set; }

        public uint Hour { get; set; }

        public uint Minute { get; set; }

        public uint Month { get; set; }

        private char[] NeLogicalName { get; set; }

        private char[] NeUserLabel { get; set; }

        public uint NoOfDroppedEvents { get; set; }

        private char[] PmRecordingRevision { get; set; }

        public uint RecordLength { get; set; }

        public uint RecordType { get; set; }

        public uint Scanner { get; set; }

        public uint Second { get; set; }

        public uint Year { get; set; }
    }
}
