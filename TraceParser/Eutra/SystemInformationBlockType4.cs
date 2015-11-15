using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType4
    {
        public void InitDefaults()
        {
        }

        public PhysCellIdRange csg_PhysCellIdRange { get; set; }

        public List<PhysCellIdRange> intraFreqBlackCellList { get; set; }

        public List<IntraFreqNeighCellInfo> intraFreqNeighCellList { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType4 Decode(BitArrayInputStream input)
            {
                int num2;
                var type = new SystemInformationBlockType4();
                type.InitDefaults();
                var flag = false;
                flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 4) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    type.intraFreqNeighCellList = new List<IntraFreqNeighCellInfo>();
                    num2 = 4;
                    var num3 = input.readBits(num2) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = IntraFreqNeighCellInfo.PerDecoder.Instance.Decode(input);
                        type.intraFreqNeighCellList.Add(item);
                    }
                }
                if (stream.Read())
                {
                    type.intraFreqBlackCellList = new List<PhysCellIdRange>();
                    num2 = 4;
                    var num5 = input.readBits(num2) + 1;
                    for (var j = 0; j < num5; j++)
                    {
                        var range = PhysCellIdRange.PerDecoder.Instance.Decode(input);
                        type.intraFreqBlackCellList.Add(range);
                    }
                }
                if (stream.Read())
                {
                    type.csg_PhysCellIdRange = PhysCellIdRange.PerDecoder.Instance.Decode(input);
                }
                if (flag && stream.Read())
                {
                    var nBits = input.readBits(8);
                    type.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return type;
            }
        }
    }
}