using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemTimeInfoCDMA2000
    {
        public void InitDefaults()
        {
        }

        public bool cdma_EUTRA_Synchronisation { get; set; }

        public cdma_SystemTime_Type cdma_SystemTime { get; set; }

        [Serializable]
        public class cdma_SystemTime_Type
        {
            public void InitDefaults()
            {
            }

            public string asynchronousSystemTime { get; set; }

            public string synchronousSystemTime { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cdma_SystemTime_Type Decode(BitArrayInputStream input)
                {
                    var type = new cdma_SystemTime_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.synchronousSystemTime = input.readBitString(0x27);
                            return type;

                        case 1:
                            type.asynchronousSystemTime = input.readBitString(0x31);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemTimeInfoCDMA2000 Decode(BitArrayInputStream input)
            {
                var ocdma = new SystemTimeInfoCDMA2000();
                ocdma.InitDefaults();
                ocdma.cdma_EUTRA_Synchronisation = input.readBit() == 1;
                ocdma.cdma_SystemTime = cdma_SystemTime_Type.PerDecoder.Instance.Decode(input);
                return ocdma;
            }
        }
    }
}