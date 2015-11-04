using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SoundingRS_UL_ConfigCommon
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SoundingRS_UL_ConfigCommon Decode(BitArrayInputStream input)
            {
                SoundingRS_UL_ConfigCommon common = new SoundingRS_UL_ConfigCommon();
                common.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        common.release=new object();
                        return common;

                    case 1:
                        common.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return common;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type
        {
            public void InitDefaults()
            {
            }

            public bool ackNackSRS_SimultaneousTransmission { get; set; }

            public srs_BandwidthConfig_Enum srs_BandwidthConfig { get; set; }

            public srs_MaxUpPts_Enum? srs_MaxUpPts { get; set; }

            public srs_SubframeConfig_Enum srs_SubframeConfig { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    int nBits = 3;
                    type.srs_BandwidthConfig = (srs_BandwidthConfig_Enum)input.readBits(nBits);
                    nBits = 4;
                    type.srs_SubframeConfig = (srs_SubframeConfig_Enum)input.readBits(nBits);
                    type.ackNackSRS_SimultaneousTransmission = input.readBit() == 1;
                    if (stream.Read())
                    {
                        nBits = 1;
                        type.srs_MaxUpPts = (srs_MaxUpPts_Enum)input.readBits(nBits);
                    }
                    return type;
                }
            }

            public enum srs_BandwidthConfig_Enum
            {
                bw0,
                bw1,
                bw2,
                bw3,
                bw4,
                bw5,
                bw6,
                bw7
            }

            public enum srs_MaxUpPts_Enum
            {
                _true
            }

            public enum srs_SubframeConfig_Enum
            {
                sc0,
                sc1,
                sc2,
                sc3,
                sc4,
                sc5,
                sc6,
                sc7,
                sc8,
                sc9,
                sc10,
                sc11,
                sc12,
                sc13,
                sc14,
                sc15
            }
        }
    }

    [Serializable]
    public class SoundingRS_UL_ConfigDedicated
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SoundingRS_UL_ConfigDedicated Decode(BitArrayInputStream input)
            {
                SoundingRS_UL_ConfigDedicated dedicated = new SoundingRS_UL_ConfigDedicated();
                dedicated.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        dedicated.release=new object();
                        return dedicated;

                    case 1:
                        dedicated.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return dedicated;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type
        {
            public void InitDefaults()
            {
            }

            public cyclicShift_Enum cyclicShift { get; set; }

            public bool duration { get; set; }

            public long freqDomainPosition { get; set; }

            public srs_Bandwidth_Enum srs_Bandwidth { get; set; }

            public long srs_ConfigIndex { get; set; }

            public srs_HoppingBandwidth_Enum srs_HoppingBandwidth { get; set; }

            public long transmissionComb { get; set; }

            public enum cyclicShift_Enum
            {
                cs0,
                cs1,
                cs2,
                cs3,
                cs4,
                cs5,
                cs6,
                cs7
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    int nBits = 2;
                    type.srs_Bandwidth = (srs_Bandwidth_Enum)input.readBits(nBits);
                    nBits = 2;
                    type.srs_HoppingBandwidth = (srs_HoppingBandwidth_Enum)input.readBits(nBits);
                    type.freqDomainPosition = input.readBits(5);
                    type.duration = input.readBit() == 1;
                    type.srs_ConfigIndex = input.readBits(10);
                    type.transmissionComb = input.readBits(1);
                    nBits = 3;
                    type.cyclicShift = (cyclicShift_Enum)input.readBits(nBits);
                    return type;
                }
            }

            public enum srs_Bandwidth_Enum
            {
                bw0,
                bw1,
                bw2,
                bw3
            }

            public enum srs_HoppingBandwidth_Enum
            {
                hbw0,
                hbw1,
                hbw2,
                hbw3
            }
        }
    }

    [Serializable]
    public class SoundingRS_UL_ConfigDedicated_v1020
    {
        public void InitDefaults()
        {
        }

        public SRS_AntennaPort srs_AntennaPort_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SoundingRS_UL_ConfigDedicated_v1020 Decode(BitArrayInputStream input)
            {
                SoundingRS_UL_ConfigDedicated_v1020 _v = new SoundingRS_UL_ConfigDedicated_v1020();
                _v.InitDefaults();
                int nBits = 2;
                _v.srs_AntennaPort_r10 = (SRS_AntennaPort)input.readBits(nBits);
                return _v;
            }
        }
    }

    [Serializable]
    public class SoundingRS_UL_ConfigDedicatedAperiodic_r10
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SoundingRS_UL_ConfigDedicatedAperiodic_r10 Decode(BitArrayInputStream input)
            {
                SoundingRS_UL_ConfigDedicatedAperiodic_r10 _r = new SoundingRS_UL_ConfigDedicatedAperiodic_r10();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return _r;

                    case 1:
                        _r.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return _r;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type
        {
            public void InitDefaults()
            {
            }

            public srs_ActivateAp_r10_Type srs_ActivateAp_r10 { get; set; }

            public List<SRS_ConfigAp_r10> srs_ConfigApDCI_Format4_r10 { get; set; }

            public long srs_ConfigIndexAp_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    type.srs_ConfigIndexAp_r10 = input.readBits(5);
                    if (stream.Read())
                    {
                        type.srs_ConfigApDCI_Format4_r10 = new List<SRS_ConfigAp_r10>();
                        const int nBits = 2;
                        int num3 = input.readBits(nBits) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            SRS_ConfigAp_r10 item = SRS_ConfigAp_r10.PerDecoder.Instance.Decode(input);
                            type.srs_ConfigApDCI_Format4_r10.Add(item);
                        }
                    }
                    if (stream.Read())
                    {
                        type.srs_ActivateAp_r10 = srs_ActivateAp_r10_Type.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }

            [Serializable]
            public class srs_ActivateAp_r10_Type
            {
                public void InitDefaults()
                {
                }

                public object release { get; set; }

                public setup_Type setup { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public srs_ActivateAp_r10_Type Decode(BitArrayInputStream input)
                    {
                        srs_ActivateAp_r10_Type type = new srs_ActivateAp_r10_Type();
                        type.InitDefaults();
                        switch (input.readBits(1))
                        {
                            case 0:
                                return type;

                            case 1:
                                type.setup = setup_Type.PerDecoder.Instance.Decode(input);
                                return type;
                        }
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                    }
                }

                [Serializable]
                public class setup_Type
                {
                    public void InitDefaults()
                    {
                    }

                    public SRS_ConfigAp_r10 srs_ConfigApDCI_Format0_r10 { get; set; }

                    public SRS_ConfigAp_r10 srs_ConfigApDCI_Format1a2b2c_r10 { get; set; }

                    public class PerDecoder
                    {
                        public static readonly PerDecoder Instance = new PerDecoder();

                        public setup_Type Decode(BitArrayInputStream input)
                        {
                            setup_Type type = new setup_Type();
                            type.InitDefaults();
                            input.readBit();
                            type.srs_ConfigApDCI_Format0_r10 = SRS_ConfigAp_r10.PerDecoder.Instance.Decode(input);
                            type.srs_ConfigApDCI_Format1a2b2c_r10 = SRS_ConfigAp_r10.PerDecoder.Instance.Decode(input);
                            return type;
                        }
                    }
                }
            }
        }
    }

}
