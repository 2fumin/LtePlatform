using System;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformationBlockType1_v920_IEs
    {
        public void InitDefaults()
        {
        }

        public CellSelectionInfo_v920 cellSelectionInfo_v920 { get; set; }

        public ims_EmergencySupport_r9_Enum? ims_EmergencySupport_r9 { get; set; }

        public SystemInformationBlockType1_v1130_IEs nonCriticalExtension { get; set; }

        public enum ims_EmergencySupport_r9_Enum
        {
            _true
        }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformationBlockType1_v920_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformationBlockType1_v920_IEs();
                es.InitDefaults();
                var stream = new BitMaskStream(input, 3);
                if (stream.Read())
                {
                    const int nBits = 1;
                    es.ims_EmergencySupport_r9 = (ims_EmergencySupport_r9_Enum)input.readBits(nBits);
                }
                if (stream.Read())
                {
                    es.cellSelectionInfo_v920 = CellSelectionInfo_v920.PerDecoder.Instance.Decode(input);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformationBlockType1_v1130_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }
    }
}