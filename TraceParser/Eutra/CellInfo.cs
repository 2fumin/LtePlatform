using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class CellInfoGERAN_r9
    {
        public void InitDefaults()
        {
        }

        public CarrierFreqGERAN carrierFreq_r9 { get; set; }

        public PhysCellIdGERAN physCellId_r9 { get; set; }

        public List<string> systemInformation_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellInfoGERAN_r9 Decode(BitArrayInputStream input)
            {
                CellInfoGERAN_r9 _r = new CellInfoGERAN_r9();
                _r.InitDefaults();
                _r.physCellId_r9 = PhysCellIdGERAN.PerDecoder.Instance.Decode(input);
                _r.carrierFreq_r9 = CarrierFreqGERAN.PerDecoder.Instance.Decode(input);
                _r.systemInformation_r9 = new List<string>();
                const int nBits = 4;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    int num = input.readBits(5);
                    string item = input.readOctetString(num + 1);
                    _r.systemInformation_r9.Add(item);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class CellInfoUTRA_FDD_r9
    {
        public void InitDefaults()
        {
        }

        public long physCellId_r9 { get; set; }

        public string utra_BCCH_Container_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellInfoUTRA_FDD_r9 Decode(BitArrayInputStream input)
            {
                CellInfoUTRA_FDD_r9 _r = new CellInfoUTRA_FDD_r9();
                _r.InitDefaults();
                _r.physCellId_r9 = input.readBits(9);
                int nBits = input.readBits(8);
                _r.utra_BCCH_Container_r9 = input.readOctetString(nBits);
                return _r;
            }
        }
    }

    [Serializable]
    public class CellInfoUTRA_TDD_r10
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq_r10 { get; set; }

        public long physCellId_r10 { get; set; }

        public string utra_BCCH_Container_r10 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellInfoUTRA_TDD_r10 Decode(BitArrayInputStream input)
            {
                CellInfoUTRA_TDD_r10 _r = new CellInfoUTRA_TDD_r10();
                _r.InitDefaults();
                _r.physCellId_r10 = input.readBits(7);
                _r.carrierFreq_r10 = input.readBits(14);
                int nBits = input.readBits(8);
                _r.utra_BCCH_Container_r10 = input.readOctetString(nBits);
                return _r;
            }
        }
    }

    [Serializable]
    public class CellInfoUTRA_TDD_r9
    {
        public void InitDefaults()
        {
        }

        public long physCellId_r9 { get; set; }

        public string utra_BCCH_Container_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public CellInfoUTRA_TDD_r9 Decode(BitArrayInputStream input)
            {
                CellInfoUTRA_TDD_r9 _r = new CellInfoUTRA_TDD_r9();
                _r.InitDefaults();
                _r.physCellId_r9 = input.readBits(7);
                int nBits = input.readBits(8);
                _r.utra_BCCH_Container_r9 = input.readOctetString(nBits);
                return _r;
            }
        }
    }

    [Serializable]
    public class VisitedCellInfo_r12
    {
        public void InitDefaults()
        {
        }

        public long timeSpent_r12 { get; set; }

        public visitedCellId_r12_Type visitedCellId_r12 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public VisitedCellInfo_r12 Decode(BitArrayInputStream input)
            {
                VisitedCellInfo_r12 _r = new VisitedCellInfo_r12();
                _r.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _r.visitedCellId_r12 = visitedCellId_r12_Type.PerDecoder.Instance.Decode(input);
                }
                _r.timeSpent_r12 = input.readBits(12);
                return _r;
            }
        }

        [Serializable]
        public class visitedCellId_r12_Type
        {
            public void InitDefaults()
            {
            }

            public CellGlobalIdEUTRA cellGlobalId_r12 { get; set; }

            public pci_arfcn_r12_Type pci_arfcn_r12 { get; set; }

            [Serializable]
            public class pci_arfcn_r12_Type
            {
                public void InitDefaults()
                {
                }

                public long carrierFreq_r12 { get; set; }

                public long physCellId_r12 { get; set; }

                public class PerDecoder
                {
                    public static readonly PerDecoder Instance = new PerDecoder();

                    public pci_arfcn_r12_Type Decode(BitArrayInputStream input)
                    {
                        pci_arfcn_r12_Type type = new pci_arfcn_r12_Type();
                        type.InitDefaults();
                        type.physCellId_r12 = input.readBits(9);
                        type.carrierFreq_r12 = input.readBits(0x10);
                        return type;
                    }
                }
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public visitedCellId_r12_Type Decode(BitArrayInputStream input)
                {
                    visitedCellId_r12_Type type = new visitedCellId_r12_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.cellGlobalId_r12 = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                            return type;

                        case 1:
                            type.pci_arfcn_r12 = pci_arfcn_r12_Type.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

}
