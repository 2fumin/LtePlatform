using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PreRegistrationInfoHRPD
    {
        public void InitDefaults()
        {
        }

        public bool preRegistrationAllowed { get; set; }

        public long? preRegistrationZoneId { get; set; }

        public List<long> secondaryPreRegistrationZoneIdList { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PreRegistrationInfoHRPD Decode(BitArrayInputStream input)
            {
                PreRegistrationInfoHRPD ohrpd = new PreRegistrationInfoHRPD();
                ohrpd.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                ohrpd.preRegistrationAllowed = input.readBit() == 1;
                if (stream.Read())
                {
                    ohrpd.preRegistrationZoneId = input.readBits(8);
                }
                if (stream.Read())
                {
                    ohrpd.secondaryPreRegistrationZoneIdList = new List<long>();
                    const int nBits = 1;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        long item = input.readBits(8);
                        ohrpd.secondaryPreRegistrationZoneIdList.Add(item);
                    }
                }
                return ohrpd;
            }
        }
    }
}
