using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class BroadcastCancelledAreaList
    {
        public void InitDefaults()
        {
        }

        public List<CellID_Cancelled_Item> cellID_Cancelled { get; set; }

        public List<EmergencyAreaID_Cancelled_Item> emergencyAreaID_Cancelled { get; set; }

        public List<TAI_Cancelled_Item> tAI_Cancelled { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BroadcastCancelledAreaList Decode(BitArrayInputStream input)
            {
                int num4;
                BroadcastCancelledAreaList list = new BroadcastCancelledAreaList();
                list.InitDefaults();
                input.readBit();
                switch (input.readBits(2))
                {
                    case 0:
                        {
                            input.skipUnreadedBits();
                            list.cellID_Cancelled = new List<CellID_Cancelled_Item>();
                            num4 = 0x10;
                            int num6 = input.readBits(num4) + 1;
                            for (int i = 0; i < num6; i++)
                            {
                                CellID_Cancelled_Item item = CellID_Cancelled_Item.PerDecoder.Instance.Decode(input);
                                list.cellID_Cancelled.Add(item);
                            }
                            return list;
                        }
                    case 1:
                        {
                            input.skipUnreadedBits();
                            list.tAI_Cancelled = new List<TAI_Cancelled_Item>();
                            num4 = 0x10;
                            int num8 = input.readBits(num4) + 1;
                            for (int j = 0; j < num8; j++)
                            {
                                TAI_Cancelled_Item item2 = TAI_Cancelled_Item.PerDecoder.Instance.Decode(input);
                                list.tAI_Cancelled.Add(item2);
                            }
                            return list;
                        }
                    case 2:
                        {
                            input.skipUnreadedBits();
                            list.emergencyAreaID_Cancelled = new List<EmergencyAreaID_Cancelled_Item>();
                            num4 = 0x10;
                            int num10 = input.readBits(num4) + 1;
                            for (int k = 0; k < num10; k++)
                            {
                                EmergencyAreaID_Cancelled_Item item3 
                                    = EmergencyAreaID_Cancelled_Item.PerDecoder.Instance.Decode(input);
                                list.emergencyAreaID_Cancelled.Add(item3);
                            }
                            return list;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class BroadcastCompletedAreaList
    {
        public void InitDefaults()
        {
        }

        public List<CellID_Broadcast_Item> cellID_Broadcast { get; set; }

        public List<EmergencyAreaID_Broadcast_Item> emergencyAreaID_Broadcast { get; set; }

        public List<TAI_Broadcast_Item> tAI_Broadcast { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public BroadcastCompletedAreaList Decode(BitArrayInputStream input)
            {
                int num4;
                BroadcastCompletedAreaList list = new BroadcastCompletedAreaList();
                list.InitDefaults();
                input.readBit();
                switch (input.readBits(2))
                {
                    case 0:
                        {
                            input.skipUnreadedBits();
                            list.cellID_Broadcast = new List<CellID_Broadcast_Item>();
                            num4 = 0x10;
                            int num6 = input.readBits(num4) + 1;
                            for (int i = 0; i < num6; i++)
                            {
                                CellID_Broadcast_Item item = CellID_Broadcast_Item.PerDecoder.Instance.Decode(input);
                                list.cellID_Broadcast.Add(item);
                            }
                            return list;
                        }
                    case 1:
                        {
                            input.skipUnreadedBits();
                            list.tAI_Broadcast = new List<TAI_Broadcast_Item>();
                            num4 = 0x10;
                            int num8 = input.readBits(num4) + 1;
                            for (int j = 0; j < num8; j++)
                            {
                                TAI_Broadcast_Item item2 = TAI_Broadcast_Item.PerDecoder.Instance.Decode(input);
                                list.tAI_Broadcast.Add(item2);
                            }
                            return list;
                        }
                    case 2:
                        {
                            input.skipUnreadedBits();
                            list.emergencyAreaID_Broadcast = new List<EmergencyAreaID_Broadcast_Item>();
                            num4 = 0x10;
                            int num10 = input.readBits(num4) + 1;
                            for (int k = 0; k < num10; k++)
                            {
                                EmergencyAreaID_Broadcast_Item item3 = EmergencyAreaID_Broadcast_Item.PerDecoder.Instance.Decode(input);
                                list.emergencyAreaID_Broadcast.Add(item3);
                            }
                            return list;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class NumberofBroadcastRequest
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = input.readBits(1) + 1;
                input.skipUnreadedBits();
                return input.readBits(num2 * 8);
            }
        }
    }

    [Serializable]
    public class NumberOfBroadcasts
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = input.readBits(1) + 1;
                input.skipUnreadedBits();
                return input.readBits(num2 * 8);
            }
        }
    }

}
