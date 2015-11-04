using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class TMGI_r9
    {
        public void InitDefaults()
        {
        }

        public plmn_Id_r9_Type plmn_Id_r9 { get; set; }

        public string serviceId_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TMGI_r9 Decode(BitArrayInputStream input)
            {
                TMGI_r9 _r = new TMGI_r9();
                _r.InitDefaults();
                _r.plmn_Id_r9 = plmn_Id_r9_Type.PerDecoder.Instance.Decode(input);
                _r.serviceId_r9 = input.readOctetString(3);
                return _r;
            }
        }

        [Serializable]
        public class plmn_Id_r9_Type
        {
            public void InitDefaults()
            {
            }

            public PLMN_Identity explicitValue_r9 { get; set; }

            public long plmn_Index_r9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public plmn_Id_r9_Type Decode(BitArrayInputStream input)
                {
                    plmn_Id_r9_Type type = new plmn_Id_r9_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.plmn_Index_r9 = input.readBits(3) + 1;
                            return type;

                        case 1:
                            type.explicitValue_r9 = PLMN_Identity.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }
}
