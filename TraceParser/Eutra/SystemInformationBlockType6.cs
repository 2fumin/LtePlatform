using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType6
    {
        public void InitDefaults()
        {
        }

        public List<CarrierFreqUTRA_FDD> carrierFreqListUTRA_FDD { get; set; }

        public List<CarrierFreqUTRA_TDD> carrierFreqListUTRA_TDD { get; set; }

        public string lateNonCriticalExtension { get; set; }

        public long t_ReselectionUTRA { get; set; }

        public SpeedStateScaleFactors t_ReselectionUTRA_SF { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType6 Decode(BitArrayInputStream input)
            {
                int num2;
                var type = new SystemInformationBlockType6();
                type.InitDefaults();
                var flag = false;
                flag = input.readBit() != 0;
                var stream = flag ? new BitMaskStream(input, 4) : new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    type.carrierFreqListUTRA_FDD = new List<CarrierFreqUTRA_FDD>();
                    num2 = 4;
                    var num3 = input.readBits(num2) + 1;
                    for (var i = 0; i < num3; i++)
                    {
                        var item = CarrierFreqUTRA_FDD.PerDecoder.Instance.Decode(input);
                        type.carrierFreqListUTRA_FDD.Add(item);
                    }
                }
                if (stream.Read())
                {
                    type.carrierFreqListUTRA_TDD = new List<CarrierFreqUTRA_TDD>();
                    num2 = 4;
                    var num5 = input.readBits(num2) + 1;
                    for (var j = 0; j < num5; j++)
                    {
                        var qutra_tdd = CarrierFreqUTRA_TDD.PerDecoder.Instance.Decode(input);
                        type.carrierFreqListUTRA_TDD.Add(qutra_tdd);
                    }
                }
                type.t_ReselectionUTRA = input.readBits(3);
                if (stream.Read())
                {
                    type.t_ReselectionUTRA_SF = SpeedStateScaleFactors.PerDecoder.Instance.Decode(input);
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