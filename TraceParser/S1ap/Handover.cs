using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class HandoverCancel
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverCancel Decode(BitArrayInputStream input)
            {
                HandoverCancel cancel = new HandoverCancel();
                cancel.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                cancel.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    cancel.protocolIEs.Add(item);
                }
                return cancel;
            }
        }
    }

    [Serializable]
    public class HandoverCancelAcknowledge
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverCancelAcknowledge Decode(BitArrayInputStream input)
            {
                HandoverCancelAcknowledge acknowledge = new HandoverCancelAcknowledge();
                acknowledge.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                acknowledge.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    acknowledge.protocolIEs.Add(item);
                }
                return acknowledge;
            }
        }
    }

    [Serializable]
    public class HandoverCommand
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverCommand Decode(BitArrayInputStream input)
            {
                HandoverCommand command = new HandoverCommand();
                command.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                command.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    command.protocolIEs.Add(item);
                }
                return command;
            }
        }
    }

    [Serializable]
    public class HandoverFailure
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverFailure Decode(BitArrayInputStream input)
            {
                HandoverFailure failure = new HandoverFailure();
                failure.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                failure.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    failure.protocolIEs.Add(item);
                }
                return failure;
            }
        }
    }

    [Serializable]
    public class HandoverNotify
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverNotify Decode(BitArrayInputStream input)
            {
                HandoverNotify notify = new HandoverNotify();
                notify.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                notify.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    notify.protocolIEs.Add(item);
                }
                return notify;
            }
        }
    }

    [Serializable]
    public class HandoverPreparationFailure
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverPreparationFailure Decode(BitArrayInputStream input)
            {
                HandoverPreparationFailure failure = new HandoverPreparationFailure();
                failure.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                failure.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    failure.protocolIEs.Add(item);
                }
                return failure;
            }
        }
    }

    [Serializable]
    public class HandoverRequest
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverRequest Decode(BitArrayInputStream input)
            {
                HandoverRequest request = new HandoverRequest();
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
    public class HandoverRequestAcknowledge
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverRequestAcknowledge Decode(BitArrayInputStream input)
            {
                HandoverRequestAcknowledge acknowledge = new HandoverRequestAcknowledge();
                acknowledge.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                acknowledge.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    acknowledge.protocolIEs.Add(item);
                }
                return acknowledge;
            }
        }
    }

    [Serializable]
    public class HandoverRequired
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolIE_Field> protocolIEs { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverRequired Decode(BitArrayInputStream input)
            {
                HandoverRequired required = new HandoverRequired();
                required.InitDefaults();
                input.readBit();
                input.skipUnreadedBits();
                required.protocolIEs = new List<ProtocolIE_Field>();
                const int nBits = 0x10;
                int num5 = input.readBits(nBits);
                for (int i = 0; i < num5; i++)
                {
                    ProtocolIE_Field item = ProtocolIE_Field.PerDecoder.Instance.Decode(input);
                    required.protocolIEs.Add(item);
                }
                return required;
            }
        }
    }

    [Serializable]
    public class HandoverRestrictionList
    {
        public void InitDefaults()
        {
        }

        public List<string> equivalentPLMNs { get; set; }

        public ForbiddenInterRATs? forbiddenInterRATs { get; set; }

        public List<ForbiddenLAs_Item> forbiddenLAs { get; set; }

        public List<ForbiddenTAs_Item> forbiddenTAs { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string servingPLMN { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public HandoverRestrictionList Decode(BitArrayInputStream input)
            {
                int num4;
                HandoverRestrictionList list = new HandoverRestrictionList();
                list.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 5) : new BitMaskStream(input, 5);
                input.skipUnreadedBits();
                list.servingPLMN = input.readOctetString(3);
                if (stream.Read())
                {
                    list.equivalentPLMNs = new List<string>();
                    num4 = 4;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        input.skipUnreadedBits();
                        string str = input.readOctetString(3);
                        list.equivalentPLMNs.Add(str);
                    }
                }
                if (stream.Read())
                {
                    list.forbiddenTAs = new List<ForbiddenTAs_Item>();
                    num4 = 4;
                    int num7 = input.readBits(num4) + 1;
                    for (int j = 0; j < num7; j++)
                    {
                        ForbiddenTAs_Item item = ForbiddenTAs_Item.PerDecoder.Instance.Decode(input);
                        list.forbiddenTAs.Add(item);
                    }
                }
                if (stream.Read())
                {
                    list.forbiddenLAs = new List<ForbiddenLAs_Item>();
                    num4 = 4;
                    int num9 = input.readBits(num4) + 1;
                    for (int k = 0; k < num9; k++)
                    {
                        ForbiddenLAs_Item item2 = ForbiddenLAs_Item.PerDecoder.Instance.Decode(input);
                        list.forbiddenLAs.Add(item2);
                    }
                }
                if (stream.Read())
                {
                    num4 = (input.readBit() == 0) ? 3 : 3;
                    list.forbiddenInterRATs = (ForbiddenInterRATs)input.readBits(num4);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    list.iE_Extensions = new List<ProtocolExtensionField>();
                    num4 = 0x10;
                    int num11 = input.readBits(num4) + 1;
                    for (int m = 0; m < num11; m++)
                    {
                        ProtocolExtensionField field = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        list.iE_Extensions.Add(field);
                    }
                }
                return list;
            }
        }
    }

}
