using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CRS_AssistanceInfo_r11
    {
        public void InitDefaults()
        {
        }

        public antennaPortsCount_r11_Enum antennaPortsCount_r11 { get; set; }

        public List<MBSFN_SubframeConfig> mbsfn_SubframeConfigList_r11 { get; set; }

        public long physCellId_r11 { get; set; }

        public enum antennaPortsCount_r11_Enum
        {
            an1,
            an2,
            an4,
            spare1
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CRS_AssistanceInfo_r11 Decode(BitArrayInputStream input)
            {
                CRS_AssistanceInfo_r11 _r = new CRS_AssistanceInfo_r11();
                _r.InitDefaults();
                input.readBit();
                _r.physCellId_r11 = input.readBits(9);
                int nBits = 2;
                _r.antennaPortsCount_r11 = (antennaPortsCount_r11_Enum)input.readBits(nBits);
                _r.mbsfn_SubframeConfigList_r11 = new List<MBSFN_SubframeConfig>();
                nBits = 3;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    MBSFN_SubframeConfig item = MBSFN_SubframeConfig.PerDecoder.Instance.Decode(input);
                    _r.mbsfn_SubframeConfigList_r11.Add(item);
                }
                return _r;
            }
        }
    }
}
