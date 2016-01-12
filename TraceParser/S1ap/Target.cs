using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class Target_ToSource_TransparentContainer
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class TargetBSS_ToSourceBSS_TransparentContainer
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

    [Serializable]
    public class TargeteNB_ID
    {
        public void InitDefaults()
        {
        }

        public Global_ENB_ID global_ENB_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public TAI selected_TAI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TargeteNB_ID Decode(BitArrayInputStream input)
            {
                TargeteNB_ID enb_id = new TargeteNB_ID();
                enb_id.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                enb_id.global_ENB_ID = Global_ENB_ID.PerDecoder.Instance.Decode(input);
                enb_id.selected_TAI = TAI.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    enb_id.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        enb_id.iE_Extensions.Add(item);
                    }
                }
                return enb_id;
            }
        }
    }

    [Serializable]
    public class TargeteNB_ToSourceeNB_TransparentContainer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string rRC_Container { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TargeteNB_ToSourceeNB_TransparentContainer Decode(BitArrayInputStream input)
            {
                TargeteNB_ToSourceeNB_TransparentContainer container = new TargeteNB_ToSourceeNB_TransparentContainer();
                container.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00C9;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00C9;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00C9:
                container.rRC_Container = input.readOctetString(nBits);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    container.iE_Extensions = new List<ProtocolExtensionField>();
                    int num4 = 0x10;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        container.iE_Extensions.Add(item);
                    }
                }
                return container;
            }
        }
    }

    [Serializable]
    public class TargetID
    {
        public void InitDefaults()
        {
        }

        public CGI cGI { get; set; }

        public TargeteNB_ID targeteNB_ID { get; set; }

        public TargetRNC_ID targetRNC_ID { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TargetID Decode(BitArrayInputStream input)
            {
                TargetID tid = new TargetID();
                tid.InitDefaults();
                input.readBit();
                switch (input.readBits(2))
                {
                    case 0:
                        tid.targeteNB_ID = TargeteNB_ID.PerDecoder.Instance.Decode(input);
                        return tid;

                    case 1:
                        tid.targetRNC_ID = TargetRNC_ID.PerDecoder.Instance.Decode(input);
                        return tid;

                    case 2:
                        tid.cGI = CGI.PerDecoder.Instance.Decode(input);
                        return tid;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class TargetRNC_ID
    {
        public void InitDefaults()
        {
        }

        public long? extendedRNC_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public LAI lAI { get; set; }

        public string rAC { get; set; }

        public long rNC_ID { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TargetRNC_ID Decode(BitArrayInputStream input)
            {
                int num4;
                TargetRNC_ID trnc_id = new TargetRNC_ID();
                trnc_id.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0)
                    ? new BitMaskStream(input, 3)
                    : new BitMaskStream(input, 3);
                trnc_id.lAI = LAI.PerDecoder.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    trnc_id.rAC = input.readOctetString(1);
                }
                input.skipUnreadedBits();
                trnc_id.rNC_ID = input.readBits(0x10);
                if (stream.Read())
                {
                    num4 = input.readBits(1) + 1;
                    input.skipUnreadedBits();
                    trnc_id.extendedRNC_ID = input.readBits(num4*8) + 0x1000;
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    trnc_id.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        trnc_id.iE_Extensions.Add(item);
                    }
                }
                return trnc_id;
            }
        }
    }

    [Serializable]
    public class TargetRNC_ToSourceRNC_TransparentContainer
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                input.skipUnreadedBits();
                int nBits = 0;
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_0096;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_0096;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_0096:
                return input.readOctetString(nBits);
            }
        }
    }

}
