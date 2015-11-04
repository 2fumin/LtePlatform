using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CrossCarrierSchedulingConfig_r10
    {
        public void InitDefaults()
        {
        }

        public schedulingCellInfo_r10_Type schedulingCellInfo_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CrossCarrierSchedulingConfig_r10 Decode(BitArrayInputStream input)
            {
                CrossCarrierSchedulingConfig_r10 _r = new CrossCarrierSchedulingConfig_r10();
                _r.InitDefaults();
                _r.schedulingCellInfo_r10 = schedulingCellInfo_r10_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }

        [Serializable]
        public class schedulingCellInfo_r10_Type
        {
            public void InitDefaults()
            {
            }

            public other_r10_Type other_r10 { get; set; }

            public own_r10_Type own_r10 { get; set; }

            [Serializable]
            public class other_r10_Type
            {
                public void InitDefaults()
                {
                }

                public long pdsch_Start_r10 { get; set; }

                public long schedulingCellId_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public other_r10_Type Decode(BitArrayInputStream input)
                    {
                        other_r10_Type type = new other_r10_Type();
                        type.InitDefaults();
                        type.schedulingCellId_r10 = input.readBits(3);
                        type.pdsch_Start_r10 = input.readBits(2) + 1;
                        return type;
                    }
                }
            }

            [Serializable]
            public class own_r10_Type
            {
                public void InitDefaults()
                {
                }

                public bool cif_Presence_r10 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public own_r10_Type Decode(BitArrayInputStream input)
                    {
                        own_r10_Type type = new own_r10_Type();
                        type.InitDefaults();
                        type.cif_Presence_r10 = input.readBit() == 1;
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public schedulingCellInfo_r10_Type Decode(BitArrayInputStream input)
                {
                    schedulingCellInfo_r10_Type type = new schedulingCellInfo_r10_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.own_r10 = own_r10_Type.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.other_r10 = other_r10_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }
}
