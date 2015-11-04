using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class PRACH_Configuration
    {
        public void InitDefaults()
        {
        }

        public bool highSpeedFlag { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long? prach_ConfigIndex { get; set; }

        public long prach_FreqOffset { get; set; }

        public long rootSequenceIndex { get; set; }

        public long zeroCorrelationIndex { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PRACH_Configuration Decode(BitArrayInputStream input)
            {
                PRACH_Configuration configuration = new PRACH_Configuration();
                configuration.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                input.skipUnreadedBits();
                configuration.rootSequenceIndex = input.readBits(0x10);
                configuration.zeroCorrelationIndex = input.readBits(4);
                configuration.highSpeedFlag = input.readBit() == 1;
                configuration.prach_FreqOffset = input.readBits(7);
                if (stream.Read())
                {
                    configuration.prach_ConfigIndex = input.readBits(6);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    configuration.iE_Extensions = new List<ProtocolExtensionField>();
                    const int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        configuration.iE_Extensions.Add(item);
                    }
                }
                return configuration;
            }
        }
    }

    [Serializable]
    public class HWLoadIndicator
    {
        public void InitDefaults()
        {
        }

        public LoadIndicator dLHWLoadIndicator { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public LoadIndicator uLHWLoadIndicator { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HWLoadIndicator Decode(BitArrayInputStream input)
            {
                HWLoadIndicator indicator = new HWLoadIndicator();
                indicator.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = (input.readBit() == 0) ? 2 : 2;
                indicator.dLHWLoadIndicator = (LoadIndicator)input.readBits(nBits);
                nBits = (input.readBit() == 0) ? 2 : 2;
                indicator.uLHWLoadIndicator = (LoadIndicator)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    indicator.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        indicator.iE_Extensions.Add(item);
                    }
                }
                return indicator;
            }
        }
    }

    [Serializable]
    public class RadioResourceStatus
    {
        public void InitDefaults()
        {
        }

        public long dL_GBR_PRB_usage { get; set; }

        public long dL_non_GBR_PRB_usage { get; set; }

        public long dL_Total_PRB_usage { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long uL_GBR_PRB_usage { get; set; }

        public long uL_non_GBR_PRB_usage { get; set; }

        public long uL_Total_PRB_usage { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RadioResourceStatus Decode(BitArrayInputStream input)
            {
                RadioResourceStatus status = new RadioResourceStatus();
                status.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                status.dL_GBR_PRB_usage = input.readBits(7);
                status.uL_GBR_PRB_usage = input.readBits(7);
                status.dL_non_GBR_PRB_usage = input.readBits(7);
                status.uL_non_GBR_PRB_usage = input.readBits(7);
                status.dL_Total_PRB_usage = input.readBits(7);
                status.uL_Total_PRB_usage = input.readBits(7);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    status.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        status.iE_Extensions.Add(item);
                    }
                }
                return status;
            }
        }
    }

    [Serializable]
    public class S1TNLLoadIndicator
    {
        public void InitDefaults()
        {
        }

        public LoadIndicator dLS1TNLLoadIndicator { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public LoadIndicator uLS1TNLLoadIndicator { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public S1TNLLoadIndicator Decode(BitArrayInputStream input)
            {
                S1TNLLoadIndicator indicator = new S1TNLLoadIndicator();
                indicator.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = (input.readBit() == 0) ? 2 : 2;
                indicator.dLS1TNLLoadIndicator = (LoadIndicator)input.readBits(nBits);
                nBits = (input.readBit() == 0) ? 2 : 2;
                indicator.uLS1TNLLoadIndicator = (LoadIndicator)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    indicator.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        indicator.iE_Extensions.Add(item);
                    }
                }
                return indicator;
            }
        }
    }

    [Serializable]
    public class ErrorIndication
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ErrorIndication Decode(BitArrayInputStream input)
            {
                ErrorIndication indication = new ErrorIndication();
                indication.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                indication.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    indication.protocolIEs.Add(item);
                }
                return indication;
            }
        }
    }

    [Serializable]
    public class LoadInformation
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LoadInformation Decode(BitArrayInputStream input)
            {
                LoadInformation information = new LoadInformation();
                information.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                information.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    information.protocolIEs.Add(item);
                }
                return information;
            }
        }
    }

    [Serializable]
    public class ResetRequest
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ResetRequest Decode(BitArrayInputStream input)
            {
                ResetRequest request = new ResetRequest();
                request.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                request.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    request.protocolIEs.Add(item);
                }
                return request;
            }
        }
    }

    [Serializable]
    public class ResetResponse
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ResetResponse Decode(BitArrayInputStream input)
            {
                ResetResponse response = new ResetResponse();
                response.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                response.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    response.protocolIEs.Add(item);
                }
                return response;
            }
        }
    }

}
