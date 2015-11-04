using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellGlobalIdCDMA2000
    {
        public void InitDefaults()
        {
        }

        public string cellGlobalId1XRTT { get; set; }

        public string cellGlobalIdHRPD { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellGlobalIdCDMA2000 Decode(BitArrayInputStream input)
            {
                CellGlobalIdCDMA2000 dcdma = new CellGlobalIdCDMA2000();
                dcdma.InitDefaults();
                switch (input.readBits(1))
                {
                    case 0:
                        dcdma.cellGlobalId1XRTT = input.readBitString(0x2f);
                        return dcdma;

                    case 1:
                        dcdma.cellGlobalIdHRPD = input.readBitString(0x80);
                        return dcdma;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

    [Serializable]
    public class CellGlobalIdEUTRA
    {
        public void InitDefaults()
        {
        }

        public string cellIdentity { get; set; }

        public PLMN_Identity plmn_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellGlobalIdEUTRA Decode(BitArrayInputStream input)
            {
                CellGlobalIdEUTRA deutra = new CellGlobalIdEUTRA();
                deutra.InitDefaults();
                deutra.plmn_Identity = PLMN_Identity.PerDecoder.Instance.Decode(input);
                deutra.cellIdentity = input.readBitString(0x1c);
                return deutra;
            }
        }
    }

    [Serializable]
    public class CellGlobalIdGERAN
    {
        public void InitDefaults()
        {
        }

        public string cellIdentity { get; set; }

        public string locationAreaCode { get; set; }

        public PLMN_Identity plmn_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellGlobalIdGERAN Decode(BitArrayInputStream input)
            {
                CellGlobalIdGERAN dgeran = new CellGlobalIdGERAN();
                dgeran.InitDefaults();
                dgeran.plmn_Identity = PLMN_Identity.PerDecoder.Instance.Decode(input);
                dgeran.locationAreaCode = input.readBitString(0x10);
                dgeran.cellIdentity = input.readBitString(0x10);
                return dgeran;
            }
        }
    }

    [Serializable]
    public class CellGlobalIdUTRA
    {
        public void InitDefaults()
        {
        }

        public string cellIdentity { get; set; }

        public PLMN_Identity plmn_Identity { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellGlobalIdUTRA Decode(BitArrayInputStream input)
            {
                CellGlobalIdUTRA dutra = new CellGlobalIdUTRA();
                dutra.InitDefaults();
                dutra.plmn_Identity = PLMN_Identity.PerDecoder.Instance.Decode(input);
                dutra.cellIdentity = input.readBitString(0x1c);
                return dutra;
            }
        }
    }

}
