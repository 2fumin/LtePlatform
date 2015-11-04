using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class MeasResult2CDMA2000_r9
    {
        public void InitDefaults()
        {
        }

        public CarrierFreqCDMA2000 carrierFreq_r9 { get; set; }

        public MeasResultsCDMA2000 measResultList_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResult2CDMA2000_r9 Decode(BitArrayInputStream input)
            {
                MeasResult2CDMA2000_r9 _r = new MeasResult2CDMA2000_r9();
                _r.InitDefaults();
                _r.carrierFreq_r9 = CarrierFreqCDMA2000.PerDecoder.Instance.Decode(input);
                _r.measResultList_r9 = MeasResultsCDMA2000.PerDecoder.Instance.Decode(input);
                return _r;
            }
        }
    }

    [Serializable]
    public class MeasResult2EUTRA_r9
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq_r9 { get; set; }

        public List<MeasResultEUTRA> measResultList_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResult2EUTRA_r9 Decode(BitArrayInputStream input)
            {
                MeasResult2EUTRA_r9 _r = new MeasResult2EUTRA_r9();
                _r.InitDefaults();
                _r.carrierFreq_r9 = input.readBits(0x10);
                _r.measResultList_r9 = new List<MeasResultEUTRA>();
                int nBits = 3;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    MeasResultEUTRA item = MeasResultEUTRA.PerDecoder.Instance.Decode(input);
                    _r.measResultList_r9.Add(item);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class MeasResult2EUTRA_v9e0
    {
        public void InitDefaults()
        {
        }

        public long? carrierFreq_v9e0 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResult2EUTRA_v9e0 Decode(BitArrayInputStream input)
            {
                MeasResult2EUTRA_v9e0 _ve = new MeasResult2EUTRA_v9e0();
                _ve.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                if (stream.Read())
                {
                    _ve.carrierFreq_v9e0 = input.readBits(0x12) + 0x10000;
                }
                return _ve;
            }
        }
    }

    [Serializable]
    public class MeasResult2UTRA_r9
    {
        public void InitDefaults()
        {
        }

        public long carrierFreq_r9 { get; set; }

        public List<MeasResultUTRA> measResultList_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResult2UTRA_r9 Decode(BitArrayInputStream input)
            {
                MeasResult2UTRA_r9 _r = new MeasResult2UTRA_r9();
                _r.InitDefaults();
                _r.carrierFreq_r9 = input.readBits(14);
                _r.measResultList_r9 = new List<MeasResultUTRA>();
                int nBits = 3;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    MeasResultUTRA item = MeasResultUTRA.PerDecoder.Instance.Decode(input);
                    _r.measResultList_r9.Add(item);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class MeasResultCDMA2000
    {
        public void InitDefaults()
        {
        }

        public CellGlobalIdCDMA2000 cgi_Info { get; set; }

        public measResult_Type measResult { get; set; }

        public long physCellId { get; set; }

        [Serializable]
        public class measResult_Type
        {
            public void InitDefaults()
            {
            }

            public long? pilotPnPhase { get; set; }

            public long pilotStrength { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResult_Type Decode(BitArrayInputStream input)
                {
                    measResult_Type type = new measResult_Type();
                    type.InitDefaults();
                    BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                    if (stream.Read())
                    {
                        type.pilotPnPhase = input.readBits(15);
                    }
                    type.pilotStrength = input.readBits(6);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResultCDMA2000 Decode(BitArrayInputStream input)
            {
                MeasResultCDMA2000 tcdma = new MeasResultCDMA2000();
                tcdma.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                tcdma.physCellId = input.readBits(9);
                if (stream.Read())
                {
                    tcdma.cgi_Info = CellGlobalIdCDMA2000.PerDecoder.Instance.Decode(input);
                }
                tcdma.measResult = measResult_Type.PerDecoder.Instance.Decode(input);
                return tcdma;
            }
        }
    }

    [Serializable]
    public class MeasResultEUTRA
    {
        public void InitDefaults()
        {
        }

        public cgi_Info_Type cgi_Info { get; set; }

        public measResult_Type measResult { get; set; }

        public long physCellId { get; set; }

        [Serializable]
        public class cgi_Info_Type
        {
            public void InitDefaults()
            {
            }

            public CellGlobalIdEUTRA cellGlobalId { get; set; }

            public List<PLMN_Identity> plmn_IdentityList { get; set; }

            public string trackingAreaCode { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cgi_Info_Type Decode(BitArrayInputStream input)
                {
                    cgi_Info_Type type = new cgi_Info_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.cellGlobalId = CellGlobalIdEUTRA.PerDecoder.Instance.Decode(input);
                    type.trackingAreaCode = input.readBitString(0x10);
                    if (stream.Read())
                    {
                        type.plmn_IdentityList = new List<PLMN_Identity>();
                        int nBits = 3;
                        int num3 = input.readBits(nBits) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            PLMN_Identity item = PLMN_Identity.PerDecoder.Instance.Decode(input);
                            type.plmn_IdentityList.Add(item);
                        }
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class measResult_Type
        {
            public void InitDefaults()
            {
            }

            public AdditionalSI_Info_r9 additionalSI_Info_r9 { get; set; }

            public primaryPLMN_Suitable_r12_Enum? primaryPLMN_Suitable_r12 { get; set; }

            public long? rsrpResult { get; set; }

            public long? rsrqResult { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResult_Type Decode(BitArrayInputStream input)
                {
                    BitMaskStream stream2;
                    measResult_Type type = new measResult_Type();
                    type.InitDefaults();
                    bool flag = input.readBit() != 0;
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    if (stream.Read())
                    {
                        type.rsrpResult = input.readBits(7);
                    }
                    if (stream.Read())
                    {
                        type.rsrqResult = input.readBits(6);
                    }
                    if (flag)
                    {
                        stream2 = new BitMaskStream(input, 1);
                        if (stream2.Read())
                        {
                            type.additionalSI_Info_r9 = AdditionalSI_Info_r9.PerDecoder.Instance.Decode(input);
                        }
                    }
                    if (flag)
                    {
                        stream2 = new BitMaskStream(input, 1);
                        if (stream2.Read())
                        {
                            int nBits = 1;
                            type.primaryPLMN_Suitable_r12 = (primaryPLMN_Suitable_r12_Enum)input.readBits(nBits);
                        }
                    }
                    return type;
                }
            }

            public enum primaryPLMN_Suitable_r12_Enum
            {
                _true
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResultEUTRA Decode(BitArrayInputStream input)
            {
                MeasResultEUTRA teutra = new MeasResultEUTRA();
                teutra.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                teutra.physCellId = input.readBits(9);
                if (stream.Read())
                {
                    teutra.cgi_Info = cgi_Info_Type.PerDecoder.Instance.Decode(input);
                }
                teutra.measResult = measResult_Type.PerDecoder.Instance.Decode(input);
                return teutra;
            }
        }
    }

    [Serializable]
    public class MeasResultForECID_r9
    {
        public void InitDefaults()
        {
        }

        public string currentSFN_r9 { get; set; }

        public long ue_RxTxTimeDiffResult_r9 { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResultForECID_r9 Decode(BitArrayInputStream input)
            {
                MeasResultForECID_r9 _r = new MeasResultForECID_r9();
                _r.InitDefaults();
                _r.ue_RxTxTimeDiffResult_r9 = input.readBits(12);
                _r.currentSFN_r9 = input.readBitString(10);
                return _r;
            }
        }
    }

    [Serializable]
    public class MeasResultGERAN
    {
        public void InitDefaults()
        {
        }

        public CarrierFreqGERAN carrierFreq { get; set; }

        public cgi_Info_Type cgi_Info { get; set; }

        public measResult_Type measResult { get; set; }

        public PhysCellIdGERAN physCellId { get; set; }

        [Serializable]
        public class cgi_Info_Type
        {
            public void InitDefaults()
            {
            }

            public CellGlobalIdGERAN cellGlobalId { get; set; }

            public string routingAreaCode { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cgi_Info_Type Decode(BitArrayInputStream input)
                {
                    cgi_Info_Type type = new cgi_Info_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 1);
                    type.cellGlobalId = CellGlobalIdGERAN.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.routingAreaCode = input.readBitString(8);
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class measResult_Type
        {
            public void InitDefaults()
            {
            }

            public long rssi { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResult_Type Decode(BitArrayInputStream input)
                {
                    measResult_Type type = new measResult_Type();
                    type.InitDefaults();
                    input.readBit();
                    type.rssi = input.readBits(6);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResultGERAN Decode(BitArrayInputStream input)
            {
                MeasResultGERAN tgeran = new MeasResultGERAN();
                tgeran.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                tgeran.carrierFreq = CarrierFreqGERAN.PerDecoder.Instance.Decode(input);
                tgeran.physCellId = PhysCellIdGERAN.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    tgeran.cgi_Info = cgi_Info_Type.PerDecoder.Instance.Decode(input);
                }
                tgeran.measResult = measResult_Type.PerDecoder.Instance.Decode(input);
                return tgeran;
            }
        }
    }

    [Serializable]
    public class MeasResults
    {
        public void InitDefaults()
        {
        }

        public LocationInfo_r10 locationInfo_r10 { get; set; }

        public long measId { get; set; }

        public MeasResultForECID_r9 measResultForECID_r9 { get; set; }

        public measResultNeighCells_Type measResultNeighCells { get; set; }

        public measResultPCell_Type measResultPCell { get; set; }

        public List<MeasResultServFreq_r10> measResultServFreqList_r10 { get; set; }

        [Serializable]
        public class measResultNeighCells_Type
        {
            public void InitDefaults()
            {
            }

            public List<MeasResultEUTRA> measResultListEUTRA { get; set; }

            public List<MeasResultGERAN> measResultListGERAN { get; set; }

            public List<MeasResultUTRA> measResultListUTRA { get; set; }

            public MeasResultsCDMA2000 measResultsCDMA2000 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultNeighCells_Type Decode(BitArrayInputStream input)
                {
                    int num2;
                    measResultNeighCells_Type type = new measResultNeighCells_Type();
                    type.InitDefaults();
                    input.readBit();
                    switch (input.readBits(2))
                    {
                        case 0:
                            {
                                type.measResultListEUTRA = new List<MeasResultEUTRA>();
                                num2 = 3;
                                int num4 = input.readBits(num2) + 1;
                                for (int i = 0; i < num4; i++)
                                {
                                    MeasResultEUTRA item = MeasResultEUTRA.PerDecoder.Instance.Decode(input);
                                    type.measResultListEUTRA.Add(item);
                                }
                                return type;
                            }
                        case 1:
                            {
                                type.measResultListUTRA = new List<MeasResultUTRA>();
                                num2 = 3;
                                int num6 = input.readBits(num2) + 1;
                                for (int j = 0; j < num6; j++)
                                {
                                    MeasResultUTRA tutra = MeasResultUTRA.PerDecoder.Instance.Decode(input);
                                    type.measResultListUTRA.Add(tutra);
                                }
                                return type;
                            }
                        case 2:
                            {
                                type.measResultListGERAN = new List<MeasResultGERAN>();
                                num2 = 3;
                                int num8 = input.readBits(num2) + 1;
                                for (int k = 0; k < num8; k++)
                                {
                                    MeasResultGERAN tgeran = MeasResultGERAN.PerDecoder.Instance.Decode(input);
                                    type.measResultListGERAN.Add(tgeran);
                                }
                                return type;
                            }
                        case 3:
                            type.measResultsCDMA2000 = MeasResultsCDMA2000.PerDecoder.Instance.Decode(input);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }

        [Serializable]
        public class measResultPCell_Type
        {
            public void InitDefaults()
            {
            }

            public long rsrpResult { get; set; }

            public long rsrqResult { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultPCell_Type Decode(BitArrayInputStream input)
                {
                    measResultPCell_Type type = new measResultPCell_Type();
                    type.InitDefaults();
                    type.rsrpResult = input.readBits(7);
                    type.rsrqResult = input.readBits(6);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResults Decode(BitArrayInputStream input)
            {
                BitMaskStream stream2;
                MeasResults results = new MeasResults();
                results.InitDefaults();
                bool flag = input.readBit() != 0;
                BitMaskStream stream = new BitMaskStream(input, 1);
                results.measId = input.readBits(5) + 1;
                results.measResultPCell = measResultPCell_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    results.measResultNeighCells = measResultNeighCells_Type.PerDecoder.Instance.Decode(input);
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 1);
                    if (stream2.Read())
                    {
                        results.measResultForECID_r9 = MeasResultForECID_r9.PerDecoder.Instance.Decode(input);
                    }
                }
                if (flag)
                {
                    stream2 = new BitMaskStream(input, 2);
                    if (stream2.Read())
                    {
                        results.locationInfo_r10 = LocationInfo_r10.PerDecoder.Instance.Decode(input);
                    }
                    if (!stream2.Read())
                    {
                        return results;
                    }
                    results.measResultServFreqList_r10 = new List<MeasResultServFreq_r10>();
                    int nBits = 3;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        MeasResultServFreq_r10 item = MeasResultServFreq_r10.PerDecoder.Instance.Decode(input);
                        results.measResultServFreqList_r10.Add(item);
                    }
                }
                return results;
            }
        }
    }

    [Serializable]
    public class MeasResultsCDMA2000
    {
        public void InitDefaults()
        {
        }

        public List<MeasResultCDMA2000> measResultListCDMA2000 { get; set; }

        public bool preRegistrationStatusHRPD { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResultsCDMA2000 Decode(BitArrayInputStream input)
            {
                MeasResultsCDMA2000 scdma = new MeasResultsCDMA2000();
                scdma.InitDefaults();
                scdma.preRegistrationStatusHRPD = input.readBit() == 1;
                scdma.measResultListCDMA2000 = new List<MeasResultCDMA2000>();
                int nBits = 3;
                int num3 = input.readBits(nBits) + 1;
                for (int i = 0; i < num3; i++)
                {
                    MeasResultCDMA2000 item = MeasResultCDMA2000.PerDecoder.Instance.Decode(input);
                    scdma.measResultListCDMA2000.Add(item);
                }
                return scdma;
            }
        }
    }

    [Serializable]
    public class MeasResultServFreq_r10
    {
        public void InitDefaults()
        {
        }

        public measResultBestNeighCell_r10_Type measResultBestNeighCell_r10 { get; set; }

        public measResultSCell_r10_Type measResultSCell_r10 { get; set; }

        public long servFreqId_r10 { get; set; }

        [Serializable]
        public class measResultBestNeighCell_r10_Type
        {
            public void InitDefaults()
            {
            }

            public long physCellId_r10 { get; set; }

            public long rsrpResultNCell_r10 { get; set; }

            public long rsrqResultNCell_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultBestNeighCell_r10_Type Decode(BitArrayInputStream input)
                {
                    measResultBestNeighCell_r10_Type type = new measResultBestNeighCell_r10_Type();
                    type.InitDefaults();
                    type.physCellId_r10 = input.readBits(9);
                    type.rsrpResultNCell_r10 = input.readBits(7);
                    type.rsrqResultNCell_r10 = input.readBits(6);
                    return type;
                }
            }
        }

        [Serializable]
        public class measResultSCell_r10_Type
        {
            public void InitDefaults()
            {
            }

            public long rsrpResultSCell_r10 { get; set; }

            public long rsrqResultSCell_r10 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResultSCell_r10_Type Decode(BitArrayInputStream input)
                {
                    measResultSCell_r10_Type type = new measResultSCell_r10_Type();
                    type.InitDefaults();
                    type.rsrpResultSCell_r10 = input.readBits(7);
                    type.rsrqResultSCell_r10 = input.readBits(6);
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResultServFreq_r10 Decode(BitArrayInputStream input)
            {
                MeasResultServFreq_r10 _r = new MeasResultServFreq_r10();
                _r.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                _r.servFreqId_r10 = input.readBits(3);
                if (stream.Read())
                {
                    _r.measResultSCell_r10 = measResultSCell_r10_Type.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    _r.measResultBestNeighCell_r10 = measResultBestNeighCell_r10_Type.PerDecoder.Instance.Decode(input);
                }
                return _r;
            }
        }
    }

    [Serializable]
    public class MeasResultUTRA
    {
        public void InitDefaults()
        {
        }

        public cgi_Info_Type cgi_Info { get; set; }

        public measResult_Type measResult { get; set; }

        public physCellId_Type physCellId { get; set; }

        [Serializable]
        public class cgi_Info_Type
        {
            public void InitDefaults()
            {
            }

            public CellGlobalIdUTRA cellGlobalId { get; set; }

            public string locationAreaCode { get; set; }

            public List<PLMN_Identity> plmn_IdentityList { get; set; }

            public string routingAreaCode { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public cgi_Info_Type Decode(BitArrayInputStream input)
                {
                    cgi_Info_Type type = new cgi_Info_Type();
                    type.InitDefaults();
                    BitMaskStream stream = new BitMaskStream(input, 3);
                    type.cellGlobalId = CellGlobalIdUTRA.PerDecoder.Instance.Decode(input);
                    if (stream.Read())
                    {
                        type.locationAreaCode = input.readBitString(0x10);
                    }
                    if (stream.Read())
                    {
                        type.routingAreaCode = input.readBitString(8);
                    }
                    if (stream.Read())
                    {
                        type.plmn_IdentityList = new List<PLMN_Identity>();
                        int nBits = 3;
                        int num3 = input.readBits(nBits) + 1;
                        for (int i = 0; i < num3; i++)
                        {
                            PLMN_Identity item = PLMN_Identity.PerDecoder.Instance.Decode(input);
                            type.plmn_IdentityList.Add(item);
                        }
                    }
                    return type;
                }
            }
        }

        [Serializable]
        public class measResult_Type
        {
            public void InitDefaults()
            {
            }

            public AdditionalSI_Info_r9 additionalSI_Info_r9 { get; set; }

            public primaryPLMN_Suitable_r12_Enum? primaryPLMN_Suitable_r12 { get; set; }

            public long? utra_EcN0 { get; set; }

            public long? utra_RSCP { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public measResult_Type Decode(BitArrayInputStream input)
                {
                    BitMaskStream stream2;
                    measResult_Type type = new measResult_Type();
                    type.InitDefaults();
                    bool flag = input.readBit() != 0;
                    BitMaskStream stream = new BitMaskStream(input, 2);
                    if (stream.Read())
                    {
                        type.utra_RSCP = input.readBits(7) + -5;
                    }
                    if (stream.Read())
                    {
                        type.utra_EcN0 = input.readBits(6);
                    }
                    if (flag)
                    {
                        stream2 = new BitMaskStream(input, 1);
                        if (stream2.Read())
                        {
                            type.additionalSI_Info_r9 = AdditionalSI_Info_r9.PerDecoder.Instance.Decode(input);
                        }
                    }
                    if (flag)
                    {
                        stream2 = new BitMaskStream(input, 1);
                        if (stream2.Read())
                        {
                            int nBits = 1;
                            type.primaryPLMN_Suitable_r12 = (primaryPLMN_Suitable_r12_Enum)input.readBits(nBits);
                        }
                    }
                    return type;
                }
            }

            public enum primaryPLMN_Suitable_r12_Enum
            {
                _true
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public MeasResultUTRA Decode(BitArrayInputStream input)
            {
                MeasResultUTRA tutra = new MeasResultUTRA();
                tutra.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 1);
                tutra.physCellId = physCellId_Type.PerDecoder.Instance.Decode(input);
                if (stream.Read())
                {
                    tutra.cgi_Info = cgi_Info_Type.PerDecoder.Instance.Decode(input);
                }
                tutra.measResult = measResult_Type.PerDecoder.Instance.Decode(input);
                return tutra;
            }
        }

        [Serializable]
        public class physCellId_Type
        {
            public void InitDefaults()
            {
            }

            public long fdd { get; set; }

            public long tdd { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public physCellId_Type Decode(BitArrayInputStream input)
                {
                    physCellId_Type type = new physCellId_Type();
                    type.InitDefaults();
                    switch (input.readBits(1))
                    {
                        case 0:
                            type.fdd = input.readBits(9);
                            return type;

                        case 1:
                            type.tdd = input.readBits(7);
                            return type;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }

}
