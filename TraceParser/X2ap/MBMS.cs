using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class MBMS_Service_Area_Identity
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                return input.readOctetString(2);
            }
        }
    }

    [Serializable]
    public class MBMS_Service_Area_Identity_List
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<string> Decode(BitArrayInputStream input)
            {
                return new List<string>();
            }
        }
    }

    [Serializable]
    public class MBSFN_Subframe_Info
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long radioframeAllocationOffset { get; set; }

        public RadioframeAllocationPeriod radioframeAllocationPeriod { get; set; }

        public SubframeAllocation subframeAllocation { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MBSFN_Subframe_Info Decode(BitArrayInputStream input)
            {
                MBSFN_Subframe_Info info = new MBSFN_Subframe_Info();
                info.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = (input.readBit() == 0) ? 3 : 3;
                info.radioframeAllocationPeriod = (RadioframeAllocationPeriod)input.readBits(nBits);
                input.readBit();
                info.radioframeAllocationOffset = input.readBits(3);
                info.subframeAllocation = SubframeAllocation.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    info.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        info.iE_Extensions.Add(item);
                    }
                }
                return info;
            }
        }
    }

    [Serializable]
    public class MBSFN_Subframe_Infolist
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<MBSFN_Subframe_Info> Decode(BitArrayInputStream input)
            {
                return new List<MBSFN_Subframe_Info>();
            }
        }
    }

    [Serializable]
    public class SubframeAllocation
    {
        public void InitDefaults()
        {
        }

        public string fourframes { get; set; }

        public string oneframe { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SubframeAllocation Decode(BitArrayInputStream input)
            {
                SubframeAllocation allocation = new SubframeAllocation();
                allocation.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        allocation.oneframe = input.readBitString(6);
                        return allocation;

                    case 1:
                        input.skipUnreadedBits();
                        allocation.fourframes = input.readBitString(0x18);
                        return allocation;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
