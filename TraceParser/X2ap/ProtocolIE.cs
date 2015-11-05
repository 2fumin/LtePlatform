using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.X2ap
{
    [Serializable]
    public class ProtocolIE_Container
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_Field>();
            }
        }
    }

    [Serializable]
    public class ProtocolIE_ContainerList
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<List<ProtocolIE_Field>> Decode(BitArrayInputStream input)
            {
                return new List<List<ProtocolIE_Field>>();
            }
        }
    }

    [Serializable]
    public class ProtocolIE_ContainerPair
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolIE_FieldPair> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolIE_FieldPair>();
            }
        }
    }

    [Serializable]
    public class ProtocolIE_ContainerPairList
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<List<ProtocolIE_FieldPair>> Decode(BitArrayInputStream input)
            {
                return new List<List<ProtocolIE_FieldPair>>();
            }
        }
    }

    [Serializable]
    public class ProtocolIE_Field
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public long id { get; set; }

        public object value { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ProtocolIE_Field Decode(BitArrayInputStream input)
            {
                ProtocolIE_Field field = new ProtocolIE_Field();
                field.InitDefaults();
                int num4 = input.readBits(1) + 1;
                input.skipUnreadedBits();
                field.id = input.readBits(num4 * 8);
                num4 = 2;
                field.criticality = (Criticality)input.readBits(num4);
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00DD;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00DD;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00DD:
                long num3 = input.Position;
                try
                {
                    field.value = X2AP_PROTOCOL_IES.Switcher(field.id, "Value", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    field.value = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return field;
            }
        }
    }

    [Serializable]
    public class ProtocolIE_FieldPair
    {
        public void InitDefaults()
        {
        }

        public Criticality firstCriticality { get; set; }

        public object firstValue { get; set; }

        public long id { get; set; }

        public Criticality secondCriticality { get; set; }

        public object secondValue { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ProtocolIE_FieldPair Decode(BitArrayInputStream input)
            {
                int nBits = 0;
                long num3 = 0L;
                ProtocolIE_FieldPair pair = new ProtocolIE_FieldPair();
                pair.InitDefaults();
                int num4 = input.readBits(1) + 1;
                input.skipUnreadedBits();
                pair.id = input.readBits(num4 * 8);
                num4 = 2;
                pair.firstCriticality = (Criticality)input.readBits(num4);
                input.skipUnreadedBits();
                nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00DD;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00DD;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00DD:
                num3 = input.Position;
                try
                {
                    pair.firstValue = X2AP_PROTOCOL_IES_PAIR.Switcher(pair.id, "FirstValue", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    pair.firstValue = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                num4 = 2;
                pair.secondCriticality = (Criticality)input.readBits(num4);
                input.skipUnreadedBits();
                nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_01ED;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_01ED;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_01ED:
                num3 = input.Position;
                try
                {
                    pair.secondValue = X2AP_PROTOCOL_IES_PAIR.Switcher(pair.id, "SecondValue", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    pair.secondValue = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return pair;
            }
        }
    }

    [Serializable]
    public class ProtocolIE_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = 0;
                num2 = input.readBits(1) + 1;
                input.skipUnreadedBits();
                return (long)input.readBits(num2 * 8);
            }
        }
    }

    [Serializable]
    public class ProtocolExtensionContainer
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<ProtocolExtensionField> Decode(BitArrayInputStream input)
            {
                return new List<ProtocolExtensionField>();
            }
        }
    }

    [Serializable]
    public class ProtocolExtensionField
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public object extensionValue { get; set; }

        public long id { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ProtocolExtensionField Decode(BitArrayInputStream input)
            {
                int nBits = 0;
                long num3 = 0L;
                ProtocolExtensionField field = new ProtocolExtensionField();
                field.InitDefaults();
                int num4 = input.readBits(1) + 1;
                input.skipUnreadedBits();
                field.id = input.readBits(num4 * 8);
                num4 = 2;
                field.criticality = (Criticality)input.readBits(num4);
                input.skipUnreadedBits();
                nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00DD;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00DD;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00DD:
                num3 = input.Position;
                try
                {
                    field.extensionValue = X2AP_PROTOCOL_EXTENSION.Switcher(field.id, "Extension", input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    field.extensionValue = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                return field;
            }
        }
    }

}
