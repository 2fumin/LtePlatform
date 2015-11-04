using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AntennaInfoCommon
    {
        public void InitDefaults()
        {
        }

        public antennaPortsCount_Enum antennaPortsCount { get; set; }

        public enum antennaPortsCount_Enum
        {
            an1,
            an2,
            an4,
            spare1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AntennaInfoCommon Decode(BitArrayInputStream input)
            {
                AntennaInfoCommon common = new AntennaInfoCommon();
                common.InitDefaults();
                int nBits = 2;
                common.antennaPortsCount = (antennaPortsCount_Enum)input.readBits(nBits);
                return common;
            }
        }
    }

    [Serializable]
    public class AntennaInfoDedicated
    {
        public void InitDefaults()
        {
        }

        public codebookSubsetRestriction_Type codebookSubsetRestriction { get; set; }

        public transmissionMode_Enum transmissionMode { get; set; }

        public ue_TransmitAntennaSelection_Type ue_TransmitAntennaSelection { get; set; }

        [Serializable]
        public class codebookSubsetRestriction_Type
        {
            public void InitDefaults()
            {
            }

            public string n2TxAntenna_tm3 { get; set; }

            public string n2TxAntenna_tm4 { get; set; }

            public string n2TxAntenna_tm5 { get; set; }

            public string n2TxAntenna_tm6 { get; set; }

            public string n4TxAntenna_tm3 { get; set; }

            public string n4TxAntenna_tm4 { get; set; }

            public string n4TxAntenna_tm5 { get; set; }

            public string n4TxAntenna_tm6 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public codebookSubsetRestriction_Type Decode(BitArrayInputStream input)
                {
                    codebookSubsetRestriction_Type type = new codebookSubsetRestriction_Type();
                    type.InitDefaults();
                    switch (input.readBits(3))
                    {
                        case 0:
                            type.n2TxAntenna_tm3 = input.readBitString(2);
                            return type;

                        case 1:
                            type.n4TxAntenna_tm3 = input.readBitString(4);
                            return type;

                        case 2:
                            type.n2TxAntenna_tm4 = input.readBitString(6);
                            return type;

                        case 3:
                            type.n4TxAntenna_tm4 = input.readBitString(0x40);
                            return type;

                        case 4:
                            type.n2TxAntenna_tm5 = input.readBitString(4);
                            return type;

                        case 5:
                            type.n4TxAntenna_tm5 = input.readBitString(0x10);
                            return type;

                        case 6:
                            type.n2TxAntenna_tm6 = input.readBitString(4);
                            return type;

                        case 7:
                            type.n4TxAntenna_tm6 = input.readBitString(0x10);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AntennaInfoDedicated Decode(BitArrayInputStream input)
            {
                AntennaInfoDedicated dedicated = new AntennaInfoDedicated();
                dedicated.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                const int nBits = 3;
                dedicated.transmissionMode = (transmissionMode_Enum)input.readBits(nBits);
                if (stream.Read())
                {
                    dedicated.codebookSubsetRestriction = codebookSubsetRestriction_Type.PerDecoder.Instance.Decode(input);
                }
                dedicated.ue_TransmitAntennaSelection = ue_TransmitAntennaSelection_Type.PerDecoder.Instance.Decode(input);
                return dedicated;
            }
        }

        public enum transmissionMode_Enum
        {
            tm1,
            tm2,
            tm3,
            tm4,
            tm5,
            tm6,
            tm7,
            tm8_v920
        }

        [Serializable]
        public class ue_TransmitAntennaSelection_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Enum setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ue_TransmitAntennaSelection_Type Decode(BitArrayInputStream input)
                {
                    ue_TransmitAntennaSelection_Type type = new ue_TransmitAntennaSelection_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.release = new object();
                            return type;

                        case 1:
                            {
                                const int nBits = 1;
                                type.setup = (setup_Enum)input.readBits(nBits);
                                return type;
                            }
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            public enum setup_Enum
            {
                closedLoop,
                openLoop
            }
        }
    }

    [Serializable]
    public class AntennaInfoDedicated_r10
    {
        public void InitDefaults()
        {
        }

        public string codebookSubsetRestriction_r10 { get; set; }

        public transmissionMode_r10_Enum transmissionMode_r10 { get; set; }

        public ue_TransmitAntennaSelection_Type ue_TransmitAntennaSelection { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AntennaInfoDedicated_r10 Decode(BitArrayInputStream input)
            {
                AntennaInfoDedicated_r10 _r = new AntennaInfoDedicated_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                int num2 = 4;
                _r.transmissionMode_r10 = (transmissionMode_r10_Enum)input.readBits(num2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    _r.codebookSubsetRestriction_r10 = input.readBitString(nBits);
                }
                _r.ue_TransmitAntennaSelection = ue_TransmitAntennaSelection_Type.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }

        public enum transmissionMode_r10_Enum
        {
            tm1,
            tm2,
            tm3,
            tm4,
            tm5,
            tm6,
            tm7,
            tm8_v920,
            tm9_v1020,
            tm10_v1130,
            spare6,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }

        [Serializable]
        public class ue_TransmitAntennaSelection_Type
        {
            public void InitDefaults()
            {
            }

            public object release { get; set; }

            public setup_Enum setup { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public ue_TransmitAntennaSelection_Type Decode(BitArrayInputStream input)
                {
                    ue_TransmitAntennaSelection_Type type = new ue_TransmitAntennaSelection_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            return type;

                        case 1:
                            {
                                int nBits = 1;
                                type.setup = (setup_Enum)input.readBits(nBits);
                                return type;
                            }
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }

            public enum setup_Enum
            {
                closedLoop,
                openLoop
            }
        }
    }

    [Serializable]
    public class AntennaInfoDedicated_v12xx
    {
        public void InitDefaults()
        {
        }

        public alternativeCodebookEnabledFor4TX_r12_Enum? alternativeCodebookEnabledFor4TX_r12 { get; set; }

        public enum alternativeCodebookEnabledFor4TX_r12_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AntennaInfoDedicated_v12xx Decode(BitArrayInputStream input)
            {
                AntennaInfoDedicated_v12xx _vxx = new AntennaInfoDedicated_v12xx();
                _vxx.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    int nBits = 1;
                    _vxx.alternativeCodebookEnabledFor4TX_r12 = (alternativeCodebookEnabledFor4TX_r12_Enum)input.readBits(nBits);
                }
                return _vxx;
            }
        }
    }

    [Serializable]
    public class AntennaInfoDedicated_v920
    {
        public void InitDefaults()
        {
        }

        public codebookSubsetRestriction_v920_Type codebookSubsetRestriction_v920 { get; set; }

        [Serializable]
        public class codebookSubsetRestriction_v920_Type
        {
            public void InitDefaults()
            {
            }

            public string n2TxAntenna_tm8_r9 { get; set; }

            public string n4TxAntenna_tm8_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public codebookSubsetRestriction_v920_Type Decode(BitArrayInputStream input)
                {
                    codebookSubsetRestriction_v920_Type type = new codebookSubsetRestriction_v920_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.n2TxAntenna_tm8_r9 = input.readBitString(6);
                            return type;

                        case 1:
                            type.n4TxAntenna_tm8_r9 = input.readBitString(0x20);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AntennaInfoDedicated_v920 Decode(BitArrayInputStream input)
            {
                AntennaInfoDedicated_v920 _v = new AntennaInfoDedicated_v920();
                _v.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _v.codebookSubsetRestriction_v920 = codebookSubsetRestriction_v920_Type.PerDecoder.Instance.Decode(input);
                }
                return _v;
            }
        }
    }

    [Serializable]
    public class AntennaInfoUL_r10
    {
        public void InitDefaults()
        {
        }

        public fourAntennaPortActivated_r10_Enum? fourAntennaPortActivated_r10 { get; set; }

        public transmissionModeUL_r10_Enum? transmissionModeUL_r10 { get; set; }

        public enum fourAntennaPortActivated_r10_Enum
        {
            setup
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AntennaInfoUL_r10 Decode(BitArrayInputStream input)
            {
                int num2;
                AntennaInfoUL_r10 _r = new AntennaInfoUL_r10();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    num2 = 3;
                    _r.transmissionModeUL_r10 = (transmissionModeUL_r10_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.fourAntennaPortActivated_r10 = (fourAntennaPortActivated_r10_Enum)input.readBits(num2);
                }
                return _r;
            }
        }

        public enum transmissionModeUL_r10_Enum
        {
            tm1,
            tm2,
            spare6,
            spare5,
            spare4,
            spare3,
            spare2,
            spare1
        }
    }

}
