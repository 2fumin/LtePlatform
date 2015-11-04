using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class PMCH_Config_r9
    {
        public void InitDefaults()
        {
        }

        public long dataMCS_r9 { get; set; }

        public mch_SchedulingPeriod_r9_Enum mch_SchedulingPeriod_r9 { get; set; }

        public long sf_AllocEnd_r9 { get; set; }

        public enum mch_SchedulingPeriod_r9_Enum
        {
            rf8,
            rf16,
            rf32,
            rf64,
            rf128,
            rf256,
            rf512,
            rf1024
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PMCH_Config_r9 Decode(BitArrayInputStream input)
            {
                PMCH_Config_r9 _r = new PMCH_Config_r9();
                _r.InitDefaults();
                input.readBit();
                _r.sf_AllocEnd_r9 = input.readBits(11);
                _r.dataMCS_r9 = input.readBits(5);
                const int nBits = 3;
                _r.mch_SchedulingPeriod_r9 = (mch_SchedulingPeriod_r9_Enum)input.readBits(nBits);
                return _r;
            }
        }
    }

    [Serializable]
    public class PMCH_Info_r9
    {
        public void InitDefaults()
        {
        }

        public List<MBMS_SessionInfo_r9> mbms_SessionInfoList_r9 { get; set; }

        public PMCH_Config_r9 pmch_Config_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PMCH_Info_r9 Decode(BitArrayInputStream input)
            {
                PMCH_Info_r9 _r = new PMCH_Info_r9();
                _r.InitDefaults();
                input.readBit();
                _r.pmch_Config_r9 = PMCH_Config_r9.PerDecoder.Instance.Decode(input);
                _r.mbms_SessionInfoList_r9 = new List<MBMS_SessionInfo_r9>();
                const int nBits = 5;
                int num3 = input.readBits(nBits);
                for (int i = 0; i < num3; i++)
                {
                    MBMS_SessionInfo_r9 item = MBMS_SessionInfo_r9.PerDecoder.Instance.Decode(input);
                    _r.mbms_SessionInfoList_r9.Add(item);
                }
                return _r;
            }
        }
    }

}
