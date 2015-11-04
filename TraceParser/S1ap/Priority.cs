using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class PriorityLevel
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                return input.readBits(4);
            }
        }
    }

    [Serializable]
    public class AllocationAndRetentionPriority
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public Pre_emptionCapability pre_emptionCapability { get; set; }

        public Pre_emptionVulnerability pre_emptionVulnerability { get; set; }

        public long priorityLevel { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AllocationAndRetentionPriority Decode(BitArrayInputStream input)
            {
                AllocationAndRetentionPriority priority = new AllocationAndRetentionPriority();
                priority.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                priority.priorityLevel = input.readBits(4);
                int nBits = 1;
                priority.pre_emptionCapability = (Pre_emptionCapability)input.readBits(nBits);
                nBits = 1;
                priority.pre_emptionVulnerability = (Pre_emptionVulnerability)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    priority.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        priority.iE_Extensions.Add(item);
                    }
                }
                return priority;
            }
        }
    }

}
