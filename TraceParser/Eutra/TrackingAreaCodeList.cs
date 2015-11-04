using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class TrackingAreaCodeList_v1130
    {
        public void InitDefaults()
        {
        }

        public List<PLMN_Identity> plmn_Identity_perTAC_List_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TrackingAreaCodeList_v1130 Decode(BitArrayInputStream input)
            {
                TrackingAreaCodeList_v1130 _v = new TrackingAreaCodeList_v1130();
                _v.InitDefaults();
                _v.plmn_Identity_perTAC_List_r11 = new List<PLMN_Identity>();
                int nBits = 3;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    PLMN_Identity item = PLMN_Identity.PerDecoder.Instance.Decode(input);
                    _v.plmn_Identity_perTAC_List_r11.Add(item);
                }
                return _v;
            }
        }
    }
}
