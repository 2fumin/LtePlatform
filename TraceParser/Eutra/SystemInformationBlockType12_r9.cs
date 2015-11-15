using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType12_r9
    {
        public void InitDefaults()
        {
        }

        public string dataCodingScheme_r9 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public string messageIdentifier_r9 { get; set; }

        public string serialNumber_r9 { get; set; }

        public string warningMessageSegment_r9 { get; set; }

        public long warningMessageSegmentNumber_r9 { get; set; }

        public warningMessageSegmentType_r9_Enum warningMessageSegmentType_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType12_r9 Decode(BitArrayInputStream input)
            {
                var _r = new SystemInformationBlockType12_r9();
                _r.InitDefaults();
                var stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                _r.messageIdentifier_r9 = input.readBitString(0x10);
                _r.serialNumber_r9 = input.readBitString(0x10);
                const int num2 = 1;
                _r.warningMessageSegmentType_r9 = (warningMessageSegmentType_r9_Enum)input.readBits(num2);
                _r.warningMessageSegmentNumber_r9 = input.readBits(6);
                var nBits = input.readBits(8);
                _r.warningMessageSegment_r9 = input.readOctetString(nBits);
                if (stream.Read())
                {
                    _r.dataCodingScheme_r9 = input.readOctetString(1);
                }
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return _r;
            }
        }

        public enum warningMessageSegmentType_r9_Enum
        {
            notLastSegment,
            lastSegment
        }
    }
}