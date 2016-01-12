using System;
using System.Collections.Generic;
using Lte.Domain.Common;
using TraceParser.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class Source_ToTarget_TransparentContainer
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
    public class SourceBSS_ToTargetBSS_TransparentContainer
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
    public class SourceeNB_ID
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

            public SourceeNB_ID Decode(BitArrayInputStream input)
            {
                SourceeNB_ID enb_id = new SourceeNB_ID();
                enb_id.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
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
    public class SourceeNB_ToTargeteNB_TransparentContainer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> e_RABInformationList { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string rRC_Container { get; set; }

        public long? subscriberProfileIDforRFP { get; set; }

        public EUTRAN_CGI targetCell_ID { get; set; }

        public List<LastVisitedCell_Item> uE_HistoryInformation { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SourceeNB_ToTargeteNB_TransparentContainer Decode(BitArrayInputStream input)
            {
                int num4;
                SourceeNB_ToTargeteNB_TransparentContainer container = new SourceeNB_ToTargeteNB_TransparentContainer();
                container.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
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
                    container.e_RABInformationList = new List<ProtocolIE_Field>();
                    num4 = 8;
                    int num5 = input.readBits(num4) + 1;
                    for (int j = 0; j < num5; j++)
                    {
                        ProtocolIE_Field field = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                        container.e_RABInformationList.Add(field);
                    }
                }
                container.targetCell_ID = EUTRAN_CGI.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    container.subscriberProfileIDforRFP = input.readBits(8) + 1;
                }
                container.uE_HistoryInformation = new List<LastVisitedCell_Item>();
                num4 = 4;
                int num7 = input.readBits(num4) + 1;
                for (int i = 0; i < num7; i++)
                {
                    LastVisitedCell_Item item = LastVisitedCell_Item.PerDecoder.Instance.Decode(input);
                    container.uE_HistoryInformation.Add(item);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    container.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num9 = input.readBits(num4) + 1;
                    for (int k = 0; k < num9; k++)
                    {
                        ProtocolExtensionField field2 = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        container.iE_Extensions.Add(field2);
                    }
                }
                return container;
            }
        }
    }

    [Serializable]
    public class SourceRNC_ToTargetRNC_TransparentContainer
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
