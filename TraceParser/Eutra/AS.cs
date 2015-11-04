using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AS_Config
    {
        public void InitDefaults()
        {
        }

        public AntennaInfoCommon antennaInfoCommon { get; set; }

        public long sourceDl_CarrierFreq { get; set; }

        public MasterInformationBlock sourceMasterInformationBlock { get; set; }

        public MeasConfig sourceMeasConfig { get; set; }

        public OtherConfig_r9 sourceOtherConfig_r9 { get; set; }

        public RadioResourceConfigDedicated sourceRadioResourceConfig { get; set; }

        public List<SCellToAddMod_r10> sourceSCellConfigList_r10 { get; set; }

        public SecurityAlgorithmConfig sourceSecurityAlgorithmConfig { get; set; }

        public SystemInformationBlockType1 sourceSystemInformationBlockType1 { get; set; }

        public string sourceSystemInformationBlockType1Ext { get; set; }

        public SystemInformationBlockType2 sourceSystemInformationBlockType2 { get; set; }

        public string sourceUE_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AS_Config Decode(BitArrayInputStream input)
            {
                BitMaskStream stream;
                AS_Config config = new AS_Config();
                config.InitDefaults();
                bool flag = input.readBit() != 0;
                config.sourceMeasConfig = MeasConfig.PerDecoder.Instance.Decode(input);
                config.sourceRadioResourceConfig = RadioResourceConfigDedicated.PerDecoder.Instance.Decode(input);
                config.sourceSecurityAlgorithmConfig = SecurityAlgorithmConfig.PerDecoder.Instance.Decode(input);
                config.sourceUE_Identity = input.readBitString(0x10);
                config.sourceMasterInformationBlock = MasterInformationBlock.PerDecoder.Instance.Decode(input);
                config.sourceSystemInformationBlockType1 = SystemInformationBlockType1.PerDecoder.Instance.Decode(input);
                config.sourceSystemInformationBlockType2 = SystemInformationBlockType2.PerDecoder.Instance.Decode(input);
                config.antennaInfoCommon = AntennaInfoCommon.PerDecoder.Instance.Decode(input);
                config.sourceDl_CarrierFreq = input.readBits(0x10);
                if (flag)
                {
                    stream = new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        int nBits = input.readBits(8);
                        config.sourceSystemInformationBlockType1Ext = input.readOctetString(nBits);
                    }
                    config.sourceOtherConfig_r9 = OtherConfig_r9.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream = new BitMaskStream(input, 1);
                    if (!stream.Read())
                    {
                        return config;
                    }
                    config.sourceSCellConfigList_r10 = new List<SCellToAddMod_r10>();
                    int num2 = 2;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        SCellToAddMod_r10 item = SCellToAddMod_r10.PerDecoder.Instance.Decode(input);
                        config.sourceSCellConfigList_r10.Add(item);
                    }
                }
                return config;
            }
        }
    }

    [Serializable]
    public class AS_Config_v9e0
    {
        public void InitDefaults()
        {
        }

        public long sourceDl_CarrierFreq_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AS_Config_v9e0 Decode(BitArrayInputStream input)
            {
                AS_Config_v9e0 _ve = new AS_Config_v9e0();
                _ve.InitDefaults();
                _ve.sourceDl_CarrierFreq_v9e0 = input.readBits(0x12) + 0x10000;
                return _ve;
            }
        }
    }

    [Serializable]
    public class AS_Context
    {
        public void InitDefaults()
        {
        }

        public ReestablishmentInfo reestablishmentInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AS_Context Decode(BitArrayInputStream input)
            {
                AS_Context context = new AS_Context();
                context.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    context.reestablishmentInfo = ReestablishmentInfo.PerDecoder.Instance.Decode(input);
                }
                return context;
            }
        }
    }

    [Serializable]
    public class AS_Context_v1130
    {
        public void InitDefaults()
        {
        }

        public string idc_Indication_r11 { get; set; }

        public string mbmsInterestIndication_r11 { get; set; }

        public string powerPrefIndication_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AS_Context_v1130 Decode(BitArrayInputStream input)
            {
                int nBits;
                AS_Context_v1130 _v = new AS_Context_v1130();
                _v.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _v.idc_Indication_r11 = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _v.mbmsInterestIndication_r11 = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    nBits = input.readBits(8);
                    _v.powerPrefIndication_r11 = input.readOctetString(nBits);
                }
                return _v;
            }
        }
    }

}
