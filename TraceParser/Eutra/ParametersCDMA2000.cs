using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class ParametersCDMA2000_r11
    {
        public void InitDefaults()
        {
        }

        public parameters1XRTT_r11_Type parameters1XRTT_r11 { get; set; }

        public parametersHRPD_r11_Type parametersHRPD_r11 { get; set; }

        public long searchWindowSize_r11 { get; set; }

        public systemTimeInfo_r11_Type systemTimeInfo_r11 { get; set; }

        [Serializable]
        public class parameters1XRTT_r11_Type
        {
            public void InitDefaults()
            {
            }

            public AC_BarringConfig1XRTT_r9 ac_BarringConfig1XRTT_r11 { get; set; }

            public CellReselectionParametersCDMA2000_r11 cellReselectionParameters1XRTT_r11 { get; set; }

            public csfb_DualRxTxSupport_r11_Enum? csfb_DualRxTxSupport_r11 { get; set; }

            public CSFB_RegistrationParam1XRTT_v920 csfb_RegistrationParam1XRTT_Ext_r11 { get; set; }

            public CSFB_RegistrationParam1XRTT csfb_RegistrationParam1XRTT_r11 { get; set; }

            public bool? csfb_SupportForDualRxUEs_r11 { get; set; }

            public string longCodeState1XRTT_r11 { get; set; }

            public enum csfb_DualRxTxSupport_r11_Enum
            {
                _true
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public parameters1XRTT_r11_Type Decode(BitArrayInputStream input)
                {
                    parameters1XRTT_r11_Type type = new parameters1XRTT_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 7);
                    if (stream.Read())
                    {
                        type.csfb_RegistrationParam1XRTT_r11 = CSFB_RegistrationParam1XRTT.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.csfb_RegistrationParam1XRTT_Ext_r11 = CSFB_RegistrationParam1XRTT_v920.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.longCodeState1XRTT_r11 = input.readBitString(0x2a);
                    }
                    if (stream.Read())
                    {
                        type.cellReselectionParameters1XRTT_r11 = CellReselectionParametersCDMA2000_r11.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.ac_BarringConfig1XRTT_r11 = AC_BarringConfig1XRTT_r9.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.csfb_SupportForDualRxUEs_r11 = input.readBit() == 1;
                    }
                    if (stream.Read())
                    {
                        int nBits = 1;
                        type.csfb_DualRxTxSupport_r11 = (csfb_DualRxTxSupport_r11_Enum)input.readBits(nBits);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class parametersHRPD_r11_Type
        {
            public void InitDefaults()
            {
            }

            public CellReselectionParametersCDMA2000_r11 cellReselectionParametersHRPD_r11 { get; set; }

            public PreRegistrationInfoHRPD preRegistrationInfoHRPD_r11 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public parametersHRPD_r11_Type Decode(BitArrayInputStream input)
                {
                    parametersHRPD_r11_Type type = new parametersHRPD_r11_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.preRegistrationInfoHRPD_r11 = PreRegistrationInfoHRPD.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.cellReselectionParametersHRPD_r11 = CellReselectionParametersCDMA2000_r11.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ParametersCDMA2000_r11 Decode(BitArrayInputStream input)
            {
                ParametersCDMA2000_r11 _r = new ParametersCDMA2000_r11();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    _r.systemTimeInfo_r11 = systemTimeInfo_r11_Type.PerDecoder.Instance.Decode(input);
                }
                _r.searchWindowSize_r11 = input.readBits(4);
                if (stream.Read())
                {
                    _r.parametersHRPD_r11 = parametersHRPD_r11_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.parameters1XRTT_r11 = parameters1XRTT_r11_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }

        [Serializable]
        public class systemTimeInfo_r11_Type
        {
            public void InitDefaults()
            {
            }

            public object defaultValue { get; set; }

            public SystemTimeInfoCDMA2000 explicitValue { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public systemTimeInfo_r11_Type Decode(BitArrayInputStream input)
                {
                    systemTimeInfo_r11_Type type = new systemTimeInfo_r11_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.explicitValue = SystemTimeInfoCDMA2000.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }
}
