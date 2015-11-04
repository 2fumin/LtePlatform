using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PLMN_Identity
    {
        public void InitDefaults()
        {
        }

        public List<long> mcc { get; set; }

        public List<long> mnc { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PLMN_Identity Decode(BitArrayInputStream input)
            {
                int num2;
                PLMN_Identity identity = new PLMN_Identity();
                identity.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    identity.mcc = new List<long>();
                    num2 = 0;
                    int num3 = input.readBits(num2) + 3;
                    for (int j = 0; j < num3; j++)
                    {
                        long item = input.readBits(4);
                        identity.mcc.Add(item);
                    }
                }
                identity.mnc = new List<long>();
                num2 = 1;
                int num6 = input.readBits(num2) + 2;
                for (int i = 0; i < num6; i++)
                {
                    long num8 = input.readBits(4);
                    identity.mnc.Add(num8);
                }
                return identity;
            }
        }
    }

    [Serializable]
    public class PLMN_IdentityInfo
    {
        public void InitDefaults()
        {
        }

        public cellReservedForOperatorUse_Enum cellReservedForOperatorUse { get; set; }

        public PLMN_Identity plmn_Identity { get; set; }

        public enum cellReservedForOperatorUse_Enum
        {
            reserved,
            notReserved
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PLMN_IdentityInfo Decode(BitArrayInputStream input)
            {
                PLMN_IdentityInfo info = new PLMN_IdentityInfo();
                info.InitDefaults();
                info.plmn_Identity = PLMN_Identity.PerDecoder.Instance.Decode(input);
                const int nBits = 1;
                info.cellReservedForOperatorUse = (cellReservedForOperatorUse_Enum)input.readBits(nBits);
                return info;
            }
        }
    }

}
