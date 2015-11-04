using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class TPC_Index
    {
        public void InitDefaults()
        {
        }

        public long indexOfFormat3 { get; set; }

        public long indexOfFormat3A { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TPC_Index Decode(BitArrayInputStream input)
            {
                TPC_Index index = new TPC_Index();
                index.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        index.indexOfFormat3 = input.readBits(4) + 1;
                        return index;

                    case 1:
                        index.indexOfFormat3A = input.readBits(5) + 1;
                        return index;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class TPC_PDCCH_Config
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public TPC_PDCCH_Config Decode(BitArrayInputStream input)
            {
                TPC_PDCCH_Config config = new TPC_PDCCH_Config();
                config.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        config.release=new object();
                        return config;

                    case 1:
                        config.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return config;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class setup_Type
        {
            public void InitDefaults()
            {
            }

            public TPC_Index tpc_Index { get; set; }

            public string tpc_RNTI { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    type.tpc_RNTI = input.readBitString(0x10);
                    type.tpc_Index = TPC_Index.PerDecoder.Instance.Decode(input);
                    return type;
                }
            }
        }
    }

}

