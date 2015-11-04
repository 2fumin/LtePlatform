using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class RLC_Config
    {
        public void InitDefaults()
        {
        }

        public am_Type am { get; set; }

        public um_Bi_Directional_Type um_Bi_Directional { get; set; }

        public um_Uni_Directional_DL_Type um_Uni_Directional_DL { get; set; }

        public um_Uni_Directional_UL_Type um_Uni_Directional_UL { get; set; }

        [Serializable]
        public class am_Type
        {
            public void InitDefaults()
            {
            }

            public DL_AM_RLC dl_AM_RLC { get; set; }

            public UL_AM_RLC ul_AM_RLC { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public am_Type Decode(BitArrayInputStream input)
                {
                    am_Type type = new am_Type();
                    type.InitDefaults();
                    type.ul_AM_RLC = UL_AM_RLC.PerDecoder.Instance.Decode(input);
                    type.dl_AM_RLC = DL_AM_RLC.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public RLC_Config Decode(BitArrayInputStream input)
            {
                RLC_Config config = new RLC_Config();
                config.InitDefaults();
                input.readBit();
                switch (input.readBits(2))
                {
                    case 0:
                        config.am = am_Type.PerDecoder.Instance.Decode(input);
                        return config;

                    case 1:
                        config.um_Bi_Directional = um_Bi_Directional_Type.PerDecoder.Instance.Decode(input);
                        return config;

                    case 2:
                        config.um_Uni_Directional_UL = um_Uni_Directional_UL_Type.PerDecoder.Instance.Decode(input);
                        return config;

                    case 3:
                        config.um_Uni_Directional_DL = um_Uni_Directional_DL_Type.PerDecoder.Instance.Decode(input);
                        return config;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class um_Bi_Directional_Type
        {
            public void InitDefaults()
            {
            }

            public DL_UM_RLC dl_UM_RLC { get; set; }

            public UL_UM_RLC ul_UM_RLC { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public um_Bi_Directional_Type Decode(BitArrayInputStream input)
                {
                    um_Bi_Directional_Type type = new um_Bi_Directional_Type();
                    type.InitDefaults();
                    type.ul_UM_RLC = UL_UM_RLC.PerDecoder.Instance.Decode(input);
                    type.dl_UM_RLC = DL_UM_RLC.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }

        [Serializable]
        public class um_Uni_Directional_DL_Type
        {
            public void InitDefaults()
            {
            }

            public DL_UM_RLC dl_UM_RLC { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public um_Uni_Directional_DL_Type Decode(BitArrayInputStream input)
                {
                    um_Uni_Directional_DL_Type type = new um_Uni_Directional_DL_Type();
                    type.InitDefaults();
                    type.dl_UM_RLC = DL_UM_RLC.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }

        [Serializable]
        public class um_Uni_Directional_UL_Type
        {
            public void InitDefaults()
            {
            }

            public UL_UM_RLC ul_UM_RLC { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public um_Uni_Directional_UL_Type Decode(BitArrayInputStream input)
                {
                    um_Uni_Directional_UL_Type type = new um_Uni_Directional_UL_Type();
                    type.InitDefaults();
                    type.ul_UM_RLC = UL_UM_RLC.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }
    }
}
