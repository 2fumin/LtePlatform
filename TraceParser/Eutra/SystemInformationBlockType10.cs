using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType10
    {
        public void InitDefaults()
        {
        }

        public string dummy { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public string messageIdentifier { get; set; }

        public string serialNumber { get; set; }

        public string warningType { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType10 Decode(BitArrayInputStream input)
            {
                var type = new SystemInformationBlockType10();
                type.InitDefaults();
                var flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 1);
                type.messageIdentifier = input.readBitString(0x10);
                type.serialNumber = input.readBitString(0x10);
                type.warningType = input.readOctetString(2);
                if (stream.Read())
                {
                    type.dummy = input.readOctetString(50);
                }
                if (flag && stream.Read())
                {
                    var nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }
}