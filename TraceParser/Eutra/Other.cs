using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class Other_Parameters_r11
    {
        public void InitDefaults()
        {
        }

        public inDeviceCoexInd_r11_Enum? inDeviceCoexInd_r11 { get; set; }

        public powerPrefInd_r11_Enum? powerPrefInd_r11 { get; set; }

        public ue_Rx_TxTimeDiffMeasurements_r11_Enum? ue_Rx_TxTimeDiffMeasurements_r11 { get; set; }

        public enum inDeviceCoexInd_r11_Enum
        {
            supported
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Other_Parameters_r11 Decode(BitArrayInputStream input)
            {
                int num2;
                Other_Parameters_r11 _r = new Other_Parameters_r11();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    num2 = 1;
                    _r.inDeviceCoexInd_r11 = (inDeviceCoexInd_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.powerPrefInd_r11 = (powerPrefInd_r11_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    _r.ue_Rx_TxTimeDiffMeasurements_r11 = (ue_Rx_TxTimeDiffMeasurements_r11_Enum)input.readBits(num2);
                }
                return _r;
            }
        }

        public enum powerPrefInd_r11_Enum
        {
            supported
        }

        public enum ue_Rx_TxTimeDiffMeasurements_r11_Enum
        {
            supported
        }
    }

    [Serializable]
    public class OtherConfig_r9
    {
        public void InitDefaults()
        {
        }

        public IDC_Config_r11 idc_Config_r11 { get; set; }

        public ObtainLocationConfig_r11 obtainLocationConfig_r11 { get; set; }

        public PowerPrefIndicationConfig_r11 powerPrefIndicationConfig_r11 { get; set; }

        public ReportProximityConfig_r9 reportProximityConfig_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public OtherConfig_r9 Decode(BitArrayInputStream input)
            {
                OtherConfig_r9 _r = new OtherConfig_r9();
                _r.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _r.reportProximityConfig_r9 = ReportProximityConfig_r9.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    BitMaskStream stream2 = new BitMaskStream(input, 3);
                    if (stream2.Read())
                    {
                        _r.idc_Config_r11 = IDC_Config_r11.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.powerPrefIndicationConfig_r11 
                            = PowerPrefIndicationConfig_r11.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        _r.obtainLocationConfig_r11 = ObtainLocationConfig_r11.PerDecoder.Instance.Decode(input);
                    }
                }
                return _r;
            }
        }
    }

}
