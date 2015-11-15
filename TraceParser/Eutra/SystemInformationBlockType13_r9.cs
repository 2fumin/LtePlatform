using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType13_r9
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public List<MBSFN_AreaInfo_r9> mbsfn_AreaInfoList_r9 { get; set; }

        public MBMS_NotificationConfig_r9 notificationConfig_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType13_r9 Decode(BitArrayInputStream input)
            {
                var _r = new SystemInformationBlockType13_r9();
                _r.InitDefaults();
                var stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                _r.mbsfn_AreaInfoList_r9 = new List<MBSFN_AreaInfo_r9>();
                const int num2 = 3;
                var num3 = input.readBits(num2) + 1;
                for (var i = 0; i < num3; i++)
                {
                    var item = MBSFN_AreaInfo_r9.PerDecoder.Instance.Decode(input);
                    _r.mbsfn_AreaInfoList_r9.Add(item);
                }
                _r.notificationConfig_r9 = MBMS_NotificationConfig_r9.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    var nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                return _r;
            }
        }
    }
}