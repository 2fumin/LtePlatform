using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class Paging
    {
        public void InitDefaults()
        {
        }

        public etws_Indication_Enum? etws_Indication { get; set; }

        public Paging_v890_IEs nonCriticalExtension { get; set; }

        public List<PagingRecord> pagingRecordList { get; set; }

        public systemInfoModification_Enum? systemInfoModification { get; set; }

        public enum etws_Indication_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Paging Decode(BitArrayInputStream input)
            {
                int num2;
                Paging paging = new Paging();
                paging.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 4);
                if (stream.Read())
                {
                    paging.pagingRecordList = new List<PagingRecord>();
                    num2 = 4;
                    int num3 = input.readBits(num2) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        PagingRecord item = PagingRecord.PerDecoder.Instance.Decode(input);
                        paging.pagingRecordList.Add(item);
                    }
                }
                if (stream.Read())
                {
                    num2 = 1;
                    paging.systemInfoModification = (systemInfoModification_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    num2 = 1;
                    paging.etws_Indication = (etws_Indication_Enum)input.readBits(num2);
                }
                if (stream.Read())
                {
                    paging.nonCriticalExtension = Paging_v890_IEs.PerDecoder.Instance.Decode(input);
                }
                return paging;
            }
        }

        public enum systemInfoModification_Enum
        {
            _true
        }
    }

    [Serializable]
    public class Paging_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public eab_ParamModification_r11_Enum? eab_ParamModification_r11 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public enum eab_ParamModification_r11_Enum
        {
            _true
        }

        [Serializable]
        public class nonCriticalExtension_Type
        {
            public void InitDefaults()
            {
            }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public nonCriticalExtension_Type Decode(BitArrayInputStream input)
                {
                    nonCriticalExtension_Type type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Paging_v1130_IEs Decode(BitArrayInputStream input)
            {
                Paging_v1130_IEs es = new Paging_v1130_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.eab_ParamModification_r11 = (eab_ParamModification_r11_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class Paging_v890_IEs
    {
        public void InitDefaults()
        {
        }

        public string lateNonCriticalExtension { get; set; }

        public Paging_v920_IEs nonCriticalExtension { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Paging_v890_IEs Decode(BitArrayInputStream input)
            {
                Paging_v890_IEs es = new Paging_v890_IEs();
                es.InitDefaults();
                bool flag = false;
                BitMaskStream stream = flag ? new BitMaskStream(input, 2) : new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = input.readBits(8);
                    es.lateNonCriticalExtension = input.readOctetString(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = Paging_v920_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class Paging_v920_IEs
    {
        public void InitDefaults()
        {
        }

        public cmas_Indication_r9_Enum? cmas_Indication_r9 { get; set; }

        public Paging_v1130_IEs nonCriticalExtension { get; set; }

        public enum cmas_Indication_r9_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Paging_v920_IEs Decode(BitArrayInputStream input)
            {
                Paging_v920_IEs es = new Paging_v920_IEs();
                es.InitDefaults();
                BitMaskStream stream = new BitMaskStream(input, 2);
                if (stream.Read())
                {
                    int nBits = 1;
                    es.cmas_Indication_r9 = (cmas_Indication_r9_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = Paging_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }

    [Serializable]
    public class PagingRecord
    {
        public void InitDefaults()
        {
        }

        public cn_Domain_Enum cn_Domain { get; set; }

        public PagingUE_Identity ue_Identity { get; set; }

        public enum cn_Domain_Enum
        {
            ps,
            cs
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PagingRecord Decode(BitArrayInputStream input)
            {
                PagingRecord record = new PagingRecord();
                record.InitDefaults();
                input.readBit();
                record.ue_Identity = PagingUE_Identity.PerDecoder.Instance.Decode(input);
                const int nBits = 1;
                record.cn_Domain = (cn_Domain_Enum)input.readBits(nBits);
                return record;
            }
        }
    }

    [Serializable]
    public class PagingUE_Identity
    {
        public void InitDefaults()
        {
        }

        public List<long> imsi { get; set; }

        public S_TMSI s_TMSI { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public PagingUE_Identity Decode(BitArrayInputStream input)
            {
                PagingUE_Identity identity = new PagingUE_Identity();
                identity.InitDefaults();
                input.readBit();
                switch (input.readBits(1))
                {
                    case 0:
                        identity.s_TMSI = S_TMSI.PerDecoder.Instance.Decode(input);
                        return identity;

                    case 1:
                        {
                            identity.imsi = new List<long>();
                            int nBits = 4;
                            int num4 = input.readBits(nBits) + 6;
                            for (int i = 0; i < num4; i++)
                            {
                                long item = input.readBits(4);
                                identity.imsi.Add(item);
                            }
                            return identity;
                        }
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }

}
