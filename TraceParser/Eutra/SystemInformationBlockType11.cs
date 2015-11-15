using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType11
    {
        public void InitDefaults()
        {
        }

        public string dataCodingScheme { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public string messageIdentifier { get; set; }

        public string serialNumber { get; set; }

        public string warningMessageSegment { get; set; }

        public long warningMessageSegmentNumber { get; set; }

        public warningMessageSegmentType_Enum warningMessageSegmentType { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType11 Decode(BitArrayInputStream input)
            {
                var type = new SystemInformationBlockType11();
                type.InitDefaults();
                var flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 1);
                type.messageIdentifier = input.readBitString(0x10);
                type.serialNumber = input.readBitString(0x10);
                const int num2 = 1;
                type.warningMessageSegmentType = (warningMessageSegmentType_Enum)input.readBits(num2);
                type.warningMessageSegmentNumber = input.readBits(6);
                var nBits = input.readBits(8);
                type.warningMessageSegment = input.readOctetString(nBits);
                if (stream.Read())
                {
                    type.dataCodingScheme = input.readOctetString(1);
                }
                if (flag && stream.Read())
                {
                    nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }

        public enum warningMessageSegmentType_Enum
        {
            notLastSegment,
            lastSegment
        }
    }
}