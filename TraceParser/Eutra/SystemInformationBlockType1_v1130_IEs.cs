using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType1_v1130_IEs
    {
        public void InitDefaults()
        {
        }

        public CellSelectionInfo_v1130 cellSelectionInfo_v1130 { get; set; }

        public nonCriticalExtension_Type nonCriticalExtension { get; set; }

        public TDD_Config_v1130 tdd_Config_v1130 { get; set; }

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
                    var type = new nonCriticalExtension_Type();
                    type.InitDefaults();
                    return type;
                }
            }
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v1130_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformationBlockType1_v1130_IEs();
                es.InitDefaults();
                var stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    es.tdd_Config_v1130 = TDD_Config_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.cellSelectionInfo_v1130 = CellSelectionInfo_v1130.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = nonCriticalExtension_Type.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }
}