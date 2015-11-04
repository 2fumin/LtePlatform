using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MeasSubframePattern_r10
    {
        public void InitDefaults()
        {
        }

        public string subframePatternFDD_r10 { get; set; }

        public subframePatternTDD_r10_Type subframePatternTDD_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasSubframePattern_r10 Decode(BitArrayInputStream input)
            {
                MeasSubframePattern_r10 _r = new MeasSubframePattern_r10();
                _r.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        _r.subframePatternFDD_r10 = input.readBitString(40);
                        return _r;

                    case 1:
                        _r.subframePatternTDD_r10 = subframePatternTDD_r10_Type.PerDecoder.Instance.Decode(input);
                        return _r;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }

        [Serializable]
        public class subframePatternTDD_r10_Type
        {
            public void InitDefaults()
            {
            }

            public string subframeConfig0_r10 { get; set; }

            public string subframeConfig1_5_r10 { get; set; }

            public string subframeConfig6_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public subframePatternTDD_r10_Type Decode(BitArrayInputStream input)
                {
                    subframePatternTDD_r10_Type type = new subframePatternTDD_r10_Type();
                    type.InitDefaults();
                    input.readBit();
                    switch (input.readBits(2))
                    {
                        case 0:
                            type.subframeConfig1_5_r10 = input.readBitString(20);
                            return type;

                        case 1:
                            type.subframeConfig0_r10 = input.readBitString(70);
                            return type;

                        case 2:
                            type.subframeConfig6_r10 = input.readBitString(60);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

    [Serializable]
    public class MeasSubframePatternConfigNeigh_r10
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public setup_Type setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasSubframePatternConfigNeigh_r10 Decode(BitArrayInputStream input)
            {
                MeasSubframePatternConfigNeigh_r10 _r = new MeasSubframePatternConfigNeigh_r10();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return _r;

                    case 1:
                        _r.setup = setup_Type.PerDecoder.Instance.Decode(input);
                        return _r;
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

            public List<PhysCellIdRange> measSubframeCellList_r10 { get; set; }

            public MeasSubframePattern_r10 measSubframePatternNeigh_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public setup_Type Decode(BitArrayInputStream input)
                {
                    setup_Type type = new setup_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.measSubframePatternNeigh_r10 = MeasSubframePattern_r10.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.measSubframeCellList_r10 = new List<PhysCellIdRange>();
                        const int nBits = 5;
                        int num3 = input.readBits(nBits) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            PhysCellIdRange item = PhysCellIdRange.PerDecoder.Instance.Decode(input);
                            type.measSubframeCellList_r10.Add(item);
                        }
                    }
                    return type;
                }
            }
        }
    }

    [Serializable]
    public class MeasSubframePatternPCell_r10
    {
        public void InitDefaults()
        {
        }

        public object release { get; set; }

        public MeasSubframePattern_r10 setup { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasSubframePatternPCell_r10 Decode(BitArrayInputStream input)
            {
                MeasSubframePatternPCell_r10 _r = new MeasSubframePatternPCell_r10();
                _r.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        return _r;

                    case 1:
                        _r.setup = MeasSubframePattern_r10.PerDecoder.Instance.Decode(input);
                        return _r;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
