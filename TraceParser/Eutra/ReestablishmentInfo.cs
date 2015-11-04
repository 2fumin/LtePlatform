using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class ReestablishmentInfo
    {
        public void InitDefaults()
        {
        }

        public List<AdditionalReestabInfo> additionalReestabInfoList { get; set; }

        public long sourcePhysCellId { get; set; }

        public string targetCellShortMAC_I { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ReestablishmentInfo Decode(BitArrayInputStream input)
            {
                ReestablishmentInfo info = new ReestablishmentInfo();
                info.InitDefaults();
                BitMaskStream stream = (input.readBit() != 0) ? new BitMaskStream(input, 1) : new BitMaskStream(input, 1);
                info.sourcePhysCellId = input.readBits(9);
                info.targetCellShortMAC_I = input.readBitString(0x10);
                if (stream.Read())
                {
                    info.additionalReestabInfoList = new List<AdditionalReestabInfo>();
                    int nBits = 5;
                    int num3 = input.readBits(nBits) + 1;
                    for (int i = 0; i < num3; i++)
                    {
                        AdditionalReestabInfo item = AdditionalReestabInfo.PerDecoder.Instance.Decode(input);
                        info.additionalReestabInfoList.Add(item);
                    }
                }
                return info;
            }
        }
    }

    [Serializable]
    public class AdditionalReestabInfo
    {
        public void InitDefaults()
        {
        }

        public string cellIdentity { get; set; }

        public string key_eNodeB_Star { get; set; }

        public string shortMAC_I { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public AdditionalReestabInfo Decode(BitArrayInputStream input)
            {
                AdditionalReestabInfo info = new AdditionalReestabInfo();
                info.InitDefaults();
                info.cellIdentity = input.readBitString(0x1c);
                info.key_eNodeB_Star = input.readBitString(0x100);
                info.shortMAC_I = input.readBitString(0x10);
                return info;
            }
        }
    }

    [Serializable]
    public class ReestabUE_Identity
    {
        public void InitDefaults()
        {
        }

        public string c_RNTI { get; set; }

        public long physCellId { get; set; }

        public string shortMAC_I { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public ReestabUE_Identity Decode(BitArrayInputStream input)
            {
                ReestabUE_Identity identity = new ReestabUE_Identity();
                identity.InitDefaults();
                identity.c_RNTI = input.readBitString(0x10);
                identity.physCellId = input.readBits(9);
                identity.shortMAC_I = input.readBitString(0x10);
                return identity;
            }
        }
    }

}
