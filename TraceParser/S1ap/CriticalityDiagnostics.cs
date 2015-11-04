using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class CriticalityDiagnostics
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public List<CriticalityDiagnostics_IE_Item> iEsCriticalityDiagnostics { get; set; }

        public long? procedureCode { get; set; }

        public Criticality? procedureCriticality { get; set; }

        public TriggeringMessage? triggeringMessage { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CriticalityDiagnostics Decode(BitArrayInputStream input)
            {
                int num4;
                CriticalityDiagnostics diagnostics = new CriticalityDiagnostics();
                diagnostics.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 5) : new BitMaskStream(input, 5);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    diagnostics.procedureCode = input.readBits(8);
                }
                if (stream.Read())
                {
                    num4 = 2;
                    diagnostics.triggeringMessage = (TriggeringMessage)input.readBits(num4);
                }
                if (stream.Read())
                {
                    num4 = 2;
                    diagnostics.procedureCriticality = (Criticality)input.readBits(num4);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    diagnostics.iEsCriticalityDiagnostics = new List<CriticalityDiagnostics_IE_Item>();
                    num4 = 8;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        CriticalityDiagnostics_IE_Item item = CriticalityDiagnostics_IE_Item.PerDecoder.Instance.Decode(input);
                        diagnostics.iEsCriticalityDiagnostics.Add(item);
                    }
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    diagnostics.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num7 = input.readBits(num4) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        diagnostics.iE_Extensions.Add(field);
                    }
                }
                return diagnostics;
            }
        }
    }

    [Serializable]
    public class CriticalityDiagnostics_IE_Item
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long iE_ID { get; set; }

        public Criticality iECriticality { get; set; }

        public TypeOfError typeOfError { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CriticalityDiagnostics_IE_Item Decode(BitArrayInputStream input)
            {
                CriticalityDiagnostics_IE_Item item = new CriticalityDiagnostics_IE_Item();
                item.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = 2;
                item.iECriticality = (Criticality)input.readBits(nBits);
                nBits = input.readBits(1) + 1;
                input.skipUnreadedBits();
                item.iE_ID = input.readBits(nBits * 8);
                nBits = 1;
                item.typeOfError = (TypeOfError)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    item.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        item.iE_Extensions.Add(field);
                    }
                }
                return item;
            }
        }
    }

    [Serializable]
    public class CriticalityDiagnostics_IE_List
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CriticalityDiagnostics_IE_Item> Decode(BitArrayInputStream input)
            {
                return new List<CriticalityDiagnostics_IE_Item>();
            }
        }
    }

}
