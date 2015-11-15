using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType16_r11
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public timeInfo_r11_Type timeInfo_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType16_r11 Decode(BitArrayInputStream input)
            {
                var _r = new SystemInformationBlockType16_r11();
                _r.InitDefaults();
                var stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    _r.timeInfo_r11 = timeInfo_r11_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    var nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return _r;
            }
        }

        [Serializable]
        public class timeInfo_r11_Type
        {
            public void InitDefaults()
            {
            }

            public string dayLightSavingTime_r11 { get; set; }

            public long? leapSeconds_r11 { get; set; }

            public long? localTimeOffset_r11 { get; set; }

            public long timeInfoUTC_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public timeInfo_r11_Type Decode(BitArrayInputStream input)
                {
                    var type = new timeInfo_r11_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 3);
                    type.timeInfoUTC_r11 = input.readBits(40);
                    if (stream.Read())
                    {
                        type.dayLightSavingTime_r11 = input.readBitString(2);
                    }
                    if (stream.Read())
                    {
                        type.leapSeconds_r11 = input.readBits(8) + -127;
                    }
                    if (stream.Read())
                    {
                        type.localTimeOffset_r11 = input.readBits(7) + -63;
                    }
                    return type;
                }
            }
        }
    }
}