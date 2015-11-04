using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class TimeSynchronizationInfo
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long stratumLevel { get; set; }

        public SynchronizationStatus synchronizationStatus { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TimeSynchronizationInfo Decode(BitArrayInputStream input)
            {
                TimeSynchronizationInfo info = new TimeSynchronizationInfo();
                info.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.readBit();
                info.stratumLevel = input.readBits(2);
                int nBits = 1;
                info.synchronizationStatus = (SynchronizationStatus)input.readBits(nBits);
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
}
