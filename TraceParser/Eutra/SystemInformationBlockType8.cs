using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType8
    {
        public void InitDefaults()
        {
        }

        public AC_BarringConfig1XRTT_r9 ac_BarringConfig1XRTT_r9 { get; set; }

        public CellReselectionParametersCDMA2000_v920 cellReselectionParameters1XRTT_v920 { get; set; }

        public CellReselectionParametersCDMA2000_v920 cellReselectionParametersHRPD_v920 { get; set; }

        public csfb_DualRxTxSupport_r10_Enum? csfb_DualRxTxSupport_r10 { get; set; }

        public CSFB_RegistrationParam1XRTT_v920 csfb_RegistrationParam1XRTT_v920 { get; set; }

        public bool? csfb_SupportForDualRxUEs_r9 { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public parameters1XRTT_Type parameters1XRTT { get; set; }

        public parametersHRPD_Type parametersHRPD { get; set; }

        public long? searchWindowSize { get; set; }

        public List<SIB8_PerPLMN_r11> sib8_PerPLMN_List_r11 { get; set; }

        public SystemTimeInfoCDMA2000 systemTimeInfo { get; set; }

        public enum csfb_DualRxTxSupport_r10_Enum
        {
            _true
        }

        [Serializable]
        public class parameters1XRTT_Type
        {
            public void InitDefaults()
            {
            }

            public CellReselectionParametersCDMA2000 cellReselectionParameters1XRTT { get; set; }

            public CSFB_RegistrationParam1XRTT csfb_RegistrationParam1XRTT { get; set; }

            public string longCodeState1XRTT { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public parameters1XRTT_Type Decode(BitArrayInputStream input)
                {
                    var type = new parameters1XRTT_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 3);
                    if (stream.Read())
                    {
                        type.csfb_RegistrationParam1XRTT = CSFB_RegistrationParam1XRTT.PerDecoder.Instance.Decode(input);
                    }
                    if (stream.Read())
                    {
                        type.longCodeState1XRTT = input.readBitString(0x2a);
                    }
                    if (stream.Read())
                    {
                        type.cellReselectionParameters1XRTT = CellReselectionParametersCDMA2000.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class parametersHRPD_Type
        {
            public void InitDefaults()
            {
            }

            public CellReselectionParametersCDMA2000 cellReselectionParametersHRPD { get; set; }

            public PreRegistrationInfoHRPD preRegistrationInfoHRPD { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public parametersHRPD_Type Decode(BitArrayInputStream input)
                {
                    var type = new parametersHRPD_Type();
                    type.InitDefaults();
                    var stream = new BitMaskStream(input, 1);
                    type.preRegistrationInfoHRPD = PreRegistrationInfoHRPD.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.cellReselectionParametersHRPD = CellReselectionParametersCDMA2000.PerDecoder.Instance.Decode(input);
                    }
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType8 Decode(BitArrayInputStream input)
            {
                int num2;
                BitMaskStream stream2;
                var type = new SystemInformationBlockType8();
                type.InitDefaults();
                var flag = false;
                flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 5) : new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    type.systemTimeInfo = SystemTimeInfoCDMA2000.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    type.searchWindowSize = (long)input.readBits(4);
                }
                if (stream.Read())
                {
                    type.parametersHRPD = parametersHRPD_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    type.parameters1XRTT = parameters1XRTT_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag && stream.Read())
                {
                    var nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 5);
                    if (stream2.Read())
                    {
                        type.csfb_SupportForDualRxUEs_r9 = input.readBit() == 1;
                    }
                    if (stream2.Read())
                    {
                        type.cellReselectionParametersHRPD_v920 = CellReselectionParametersCDMA2000_v920.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.cellReselectionParameters1XRTT_v920 = CellReselectionParametersCDMA2000_v920.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.csfb_RegistrationParam1XRTT_v920 = CSFB_RegistrationParam1XRTT_v920.PerDecoder.Instance.Decode(input);
                    }
                    if (stream2.Read())
                    {
                        type.ac_BarringConfig1XRTT_r9 = AC_BarringConfig1XRTT_r9.PerDecoder.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        num2 = 1;
                        type.csfb_DualRxTxSupport_r10 = (csfb_DualRxTxSupport_r10_Enum)input.readBits(num2);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (!stream2.Read())
                    {
                        return type;
                    }
                    type.sib8_PerPLMN_List_r11 = new List<SIB8_PerPLMN_r11>();
                    num2 = 3;
                    var num3 = input.readBits(num2) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = SIB8_PerPLMN_r11.PerDecoder.Decode(input);
                        type.sib8_PerPLMN_List_r11.Add(item);
                    }
                }
                return type;
            }
        }
    }
}