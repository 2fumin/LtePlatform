using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType15_r11
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public List<MBMS_SAI_InterFreq_r11> mbms_SAI_InterFreqList_r11 { get; set; }

        public List<MBMS_SAI_InterFreq_v1140> mbms_SAI_InterFreqList_v1140 { get; set; }

        public List<long> mbms_SAI_IntraFreq_r11 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType15_r11 Decode(BitArrayInputStream input)
            {
                int num2;
                var _r = new SystemInformationBlockType15_r11();
                _r.InitDefaults();
                var flag = false;
                flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 3) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    _r.mbms_SAI_IntraFreq_r11 = new List<long>();
                    num2 = 6;
                    var num3 = input.readBits(num2) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        long item = input.readBits(0x10);
                        _r.mbms_SAI_IntraFreq_r11.Add(item);
                    }
                }
                if (stream.Read())
                {
                    _r.mbms_SAI_InterFreqList_r11 = new List<MBMS_SAI_InterFreq_r11>();
                    num2 = 3;
                    var num6 = input.readBits(num2) + 1;
                    for (var j = 0; j < num6; j++)
                    {
                        var _r2 = MBMS_SAI_InterFreq_r11.PerDecoder.Instance.Decode(input);
                        _r.mbms_SAI_InterFreqList_r11.Add(_r2);
                    }
                }
                if (stream.Read())
                {
                    var nBits = input.readBits(8);
                    _r.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (flag)
                {
                    var stream2 = new BitMaskStream(input, 1);
                    if (!stream2.Read())
                    {
                        return _r;
                    }
                    _r.mbms_SAI_InterFreqList_v1140 = new List<MBMS_SAI_InterFreq_v1140>();
                    num2 = 3;
                    var num8 = input.readBits(num2) + 1;
                    for (var k = 0; k < num8; k++)
                    {
                        var _v = MBMS_SAI_InterFreq_v1140.PerDecoder.Instance.Decode(input);
                        _r.mbms_SAI_InterFreqList_v1140.Add(_v);
                    }
                }
                return _r;
            }
        }
    }
}