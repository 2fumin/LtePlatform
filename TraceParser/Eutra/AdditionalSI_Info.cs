using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class AdditionalSI_Info_r9
    {
        public void InitDefaults()
        {
        }

        public string csg_Identity_r9 { get; set; }

        public csg_MemberStatus_r9_Enum? csg_MemberStatus_r9 { get; set; }

        public enum csg_MemberStatus_r9_Enum
        {
            member
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AdditionalSI_Info_r9 Decode(BitArrayInputStream input)
            {
                AdditionalSI_Info_r9 _r = new AdditionalSI_Info_r9();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    _r.csg_MemberStatus_r9 = (csg_MemberStatus_r9_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    _r.csg_Identity_r9 = input.readBitString(0x1b);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class SI_OrPSI_GERAN
    {
        public void InitDefaults()
        {
        }

        public List<string> psi { get; set; }

        public List<string> si { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SI_OrPSI_GERAN Decode(BitArrayInputStream input)
            {
                int num2;
                int num4;
                int num5;
                string str;
                int num;
                SI_OrPSI_GERAN rpsi_geran = new SI_OrPSI_GERAN();
                rpsi_geran.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        rpsi_geran.si = new List<string>();
                        num2 = 4;
                        num4 = input.readBits(num2) + 1;
                        for (num5 = 0; num5 < num4; num5++)
                        {
                            num = input.readBits(5);
                            str = input.readOctetString(num + 1);
                            rpsi_geran.si.Add(str);
                        }
                        return rpsi_geran;

                    case 1:
                        rpsi_geran.psi = new List<string>();
                        num2 = 4;
                        num4 = input.readBits(num2) + 1;
                        for (num5 = 0; num5 < num4; num5++)
                        {
                            num = input.readBits(5);
                            str = input.readOctetString(num + 1);
                            rpsi_geran.psi.Add(str);
                        }
                        return rpsi_geran;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
