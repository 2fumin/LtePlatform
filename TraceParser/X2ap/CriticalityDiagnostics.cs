using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class CriticalityDiagnostics
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public List<CriticalityDiagnostics_IE_List_Element> iEsCriticalityDiagnostics { get; set; }

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
                    diagnostics.iEsCriticalityDiagnostics = new List<CriticalityDiagnostics_IE_List_Element>();
                    num4 = 8;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        CriticalityDiagnostics_IE_List_Element item = CriticalityDiagnostics_IE_List_Element.PerDecoder.Instance.Decode(input);
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
    public class CriticalityDiagnostics_IE_List
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<CriticalityDiagnostics_IE_List_Element> Decode(BitArrayInputStream input)
            {
                return new List<CriticalityDiagnostics_IE_List_Element>();
            }
        }
    }

    [Serializable]
    public class CriticalityDiagnostics_IE_List_Element
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

            public CriticalityDiagnostics_IE_List_Element Decode(BitArrayInputStream input)
            {
                CriticalityDiagnostics_IE_List_Element element = new CriticalityDiagnostics_IE_List_Element();
                element.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                int nBits = 2;
                element.iECriticality = (Criticality)input.readBits(nBits);
                nBits = input.readBits(1) + 1;
                input.skipUnreadedBits();
                element.iE_ID = input.readBits(nBits * 8);
                nBits = 1;
                element.typeOfError = (TypeOfError)input.readBits(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    element.iE_Extensions = new List<ProtocolExtensionField>();
                    nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        element.iE_Extensions.Add(item);
                    }
                }
                return element;
            }
        }
    }

}
