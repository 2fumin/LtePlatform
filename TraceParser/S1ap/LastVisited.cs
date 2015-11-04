using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class LastVisitedCell_Item
    {
        public void InitDefaults()
        {
        }

        public LastVisitedEUTRANCellInformation e_UTRAN_Cell { get; set; }

        public LastVisitedGERANCellInformation gERAN_Cell { get; set; }

        public string uTRAN_Cell { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LastVisitedCell_Item Decode(BitArrayInputStream input)
            {
                int nBits;
                LastVisitedCell_Item item = new LastVisitedCell_Item();
                item.InitDefaults();
                input.readBit();
                switch (input.readBits(2))
                {
                    case 0:
                        item.e_UTRAN_Cell = LastVisitedEUTRANCellInformation.PerDecoder.Instance.Decode(input);
                        return item;

                    case 1:
                        input.skipUnreadedBits();
                        nBits = 0;
                        break;

                    case 2:
                        item.gERAN_Cell = LastVisitedGERANCellInformation.PerDecoder.Instance.Decode(input);
                        return item;

                    default:
                        throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
                while (true)
                {
                    switch (input.readBit())
                    {
                        case 0:
                            nBits += input.readBits(7);
                            goto Label_00F2;

                        case 1:
                            switch (input.readBit())
                            {
                                case 0:
                                    nBits += input.readBits(14);
                                    goto Label_00F2;

                                case 1:
                                    input.readBits(2);
                                    nBits += input.readBits(4) * 0x400;
                                    break;
                            }
                            break;
                    }
                }
            Label_00F2:
                item.uTRAN_Cell = input.readOctetString(nBits);
                return item;
            }
        }
    }

    [Serializable]
    public class LastVisitedEUTRANCellInformation
    {
        public void InitDefaults()
        {
        }

        public CellType cellType { get; set; }

        public EUTRAN_CGI global_Cell_ID { get; set; }

        public List<ProtocolExtensionField> iE_Extensions { get; set; }

        public long time_UE_StayedInCell { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LastVisitedEUTRANCellInformation Decode(BitArrayInputStream input)
            {
                LastVisitedEUTRANCellInformation information = new LastVisitedEUTRANCellInformation();
                information.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                information.global_Cell_ID = EUTRAN_CGI.PerDecoder.Instance.Decode(input);
                information.cellType = CellType.PerDecoder.Instance.Decode(input);
                input.skipUnreadedBits();
                information.time_UE_StayedInCell = input.readBits(0x10);
                if (stream.Read())
                {
                    input.skipUnreadedBits();
                    information.iE_Extensions = new List<ProtocolExtensionField>();
                    int nBits = 0x10;
                    int num5 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        ProtocolExtensionField item = ProtocolExtensionField.PerDecoder.Instance.Decode(input);
                        information.iE_Extensions.Add(item);
                    }
                }
                return information;
            }
        }
    }

    [Serializable]
    public class LastVisitedGERANCellInformation
    {
        public void InitDefaults()
        {
        }

        public object undefined { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public LastVisitedGERANCellInformation Decode(BitArrayInputStream input)
            {
                LastVisitedGERANCellInformation information = new LastVisitedGERANCellInformation();
                information.InitDefaults();
                input.readBit();
                if (input.readBits(1) != 0)
                {
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
                return information;
            }
        }
    }

    [Serializable]
    public class LastVisitedUTRANCellInformation
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
