using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class PrivateIE_Container
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public List<PrivateIE_Field> Decode(BitArrayInputStream input)
            {
                return new List<PrivateIE_Field>();
            }
        }
    }

    [Serializable]
    public class PrivateIE_Field
    {
        public void InitDefaults()
        {
        }

        public Criticality criticality { get; set; }

        public PrivateIE_ID id { get; set; }

        public object value { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PrivateIE_Field Decode(BitArrayInputStream input)
            {
                PrivateIE_Field field = new PrivateIE_Field();
                field.InitDefaults();
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00A5;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00A5;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00A5:
                long num3 = input.Position;
                try
                {
                    field.id = PrivateIE_ID.PerDecoder.Instance.Decode(input);
                    input.skipUnreadedBits();
                }
                catch (Exception)
                {
                    input.skipUnreadedBits();
                    input.Position = num3;
                    field.id.global = input.readOctetString(nBits);
                }
                if (input.Position != (num3 + nBits))
                {
                    input.Position = num3 + nBits;
                }
                int num4 = 2;
                field.criticality = (Criticality)input.readBits(num4);
                input.skipUnreadedBits();
                nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_01AE;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_01AE;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_01AE:
                num3 = input.Position;
                try
                {
                    field.value = S1AP_PRIVATE_IES.Switcher(field.id, "Value", input);
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
    public class PrivateIE_ID
    {
        public void InitDefaults()
        {
        }

        public string global { get; set; }

        public long local { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PrivateIE_ID Decode(BitArrayInputStream input)
            {
                PrivateIE_ID eie_id = new PrivateIE_ID();
                eie_id.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        {
                            int num4 = input.readBits(1) + 1;
                            input.skipUnreadedBits();
                            eie_id.local = input.readBits(num4 * 8);
                            return eie_id;
                        }
                    case 1:
                        return eie_id;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class PrivateMessage
    {
        public void InitDefaults()
        {
        }

        public List<PrivateIE_Field> privateIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PrivateMessage Decode(BitArrayInputStream input)
            {
                PrivateMessage message = new PrivateMessage();
                message.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                message.privateIEs = new List<PrivateIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits) + 1;
                for (int i = 0; i < num5; i++)
                {
                    PrivateIE_Field item = PrivateIE_Field.PerDecoder.Instance.Decode(input);
                    message.privateIEs.Add(item);
                }
                return message;
            }
        }
    }

}
