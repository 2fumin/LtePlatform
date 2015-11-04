using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class Inter_SystemInformationTransferType
    {
        public void InitDefaults()
        {
        }

        public RIMTransfer rIMTransfer { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Inter_SystemInformationTransferType Decode(BitArrayInputStream input)
            {
                Inter_SystemInformationTransferType type = new Inter_SystemInformationTransferType();
                type.InitDefaults();
                input.readBit();
                if (input.readBits(1) != 0)
                {
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
                type.rIMTransfer = RIMTransfer.PerDecoder.Instance.Decode(input);
                return type;
            }
        }
    }

    [Serializable]
    public class RIMTransfer
    {
        public void InitDefaults()
        {
        }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public string rIMInformation { get; set; }

        public RIMRoutingAddress rIMRoutingAddress { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RIMTransfer Decode(BitArrayInputStream input)
            {
                RIMTransfer transfer = new RIMTransfer();
                transfer.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
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
                transfer.rIMInformation = input.readOctetString(nBits);
                if (stream.Read())
                {
                    transfer.rIMRoutingAddress = RIMRoutingAddress.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    transfer.iE_Extensions = new List<ProtocolExtensionField>();
                    const int num4 = 0x10;
                    int num5 = input.readBits(num4) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        transfer.iE_Extensions.Add(item);
                    }
                }
                return transfer;
            }
        }
    }

    [Serializable]
    public class RIMInformation
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
    public class RIMRoutingAddress
    {
        public void InitDefaults()
        {
        }

        public GERAN_Cell_ID gERAN_Cell_ID { get; set; }

        public TargetRNC_ID targetRNC_ID { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RIMRoutingAddress Decode(BitArrayInputStream input)
            {
                RIMRoutingAddress address = new RIMRoutingAddress();
                address.InitDefaults();
                bool flag = input.readBit() != 0;
                switch (input.readBits(1))
                {
                    case 0:
                        address.gERAN_Cell_ID = GERAN_Cell_ID.PerDecoder.Instance.Decode(input);
                        return address;

                    case 1:
                        if (flag)
                        {
                            address.targetRNC_ID = TargetRNC_ID.PerDecoder.Instance.Decode(input);
                        }
                        return address;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
