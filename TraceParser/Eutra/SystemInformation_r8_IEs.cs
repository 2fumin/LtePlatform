using System;
using System.Collections.Generic;
using Lte.Domain.Common;

namespace TraceParser.Eutra
{
    [Serializable]
    public class SystemInformation_r8_IEs
    {
        public static void InitDefaults()
        {
        }

        public SystemInformation_v8a0_IEs nonCriticalExtension { get; set; }

        public List<sib_TypeAndInfo_Element> sib_TypeAndInfo { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public SystemInformation_r8_IEs Decode(BitArrayInputStream input)
            {
                var es = new SystemInformation_r8_IEs();
                InitDefaults();
                var stream = new BitMaskStream(input, 1);
                es.sib_TypeAndInfo = new List<sib_TypeAndInfo_Element>();
                const int nBits = 5;
                var num3 = input.readBits(nBits) + 1;
                for (var i = 0; i < num3; i++)
                {
                    var item = sib_TypeAndInfo_Element.PerDecoder.Instance.Decode(input);
                    es.sib_TypeAndInfo.Add(item);
                }
                if (stream.Read())
                {
                    es.nonCriticalExtension = SystemInformation_v8a0_IEs.PerDecoder.Instance.Decode(input);
                }
                return es;
            }
        }

        [Serializable]
        public class sib_TypeAndInfo_Element
        {
            public void InitDefaults()
            {
            }

            public SystemInformationBlockType10 sib10 { get; set; }

            public SystemInformationBlockType11 sib11 { get; set; }

            public SystemInformationBlockType12_r9 sib12_v920 { get; set; }

            public SystemInformationBlockType13_r9 sib13_v920 { get; set; }

            public SystemInformationBlockType14_r11 sib14_v1130 { get; set; }

            public SystemInformationBlockType15_r11 sib15_v1130 { get; set; }

            public SystemInformationBlockType16_r11 sib16_v1130 { get; set; }

            public SystemInformationBlockType2 sib2 { get; set; }

            public SystemInformationBlockType3 sib3 { get; set; }

            public SystemInformationBlockType4 sib4 { get; set; }

            public SystemInformationBlockType5 sib5 { get; set; }

            public SystemInformationBlockType6 sib6 { get; set; }

            public SystemInformationBlockType7 sib7 { get; set; }

            public SystemInformationBlockType8 sib8 { get; set; }

            public SystemInformationBlockType9 sib9 { get; set; }

            public class PerDecoder
            {
                public static readonly PerDecoder Instance = new PerDecoder();

                public sib_TypeAndInfo_Element Decode(BitArrayInputStream input)
                {
                    var element = new sib_TypeAndInfo_Element();
                    element.InitDefaults();
                    var flag = input.readBit() != 0;
                    switch (input.readBits(4))
                    {
                        case 0:
                            element.sib2 = SystemInformationBlockType2.PerDecoder.Instance.Decode(input);
                            return element;

                        case 1:
                            element.sib3 = SystemInformationBlockType3.PerDecoder.Instance.Decode(input);
                            return element;

                        case 2:
                            element.sib4 = SystemInformationBlockType4.PerDecoder.Instance.Decode(input);
                            return element;

                        case 3:
                            element.sib5 = SystemInformationBlockType5.PerDecoder.Instance.Decode(input);
                            return element;

                        case 4:
                            element.sib6 = SystemInformationBlockType6.PerDecoder.Instance.Decode(input);
                            return element;

                        case 5:
                            element.sib7 = SystemInformationBlockType7.PerDecoder.Instance.Decode(input);
                            return element;

                        case 6:
                            element.sib8 = SystemInformationBlockType8.PerDecoder.Instance.Decode(input);
                            return element;

                        case 7:
                            element.sib9 = SystemInformationBlockType9.PerDecoder.Instance.Decode(input);
                            return element;

                        case 8:
                            element.sib10 = SystemInformationBlockType10.PerDecoder.Instance.Decode(input);
                            return element;

                        case 9:
                            element.sib11 = SystemInformationBlockType11.PerDecoder.Instance.Decode(input);
                            return element;

                        case 10:
                            if (flag)
                            {
                                element.sib12_v920 = SystemInformationBlockType12_r9.PerDecoder.Instance.Decode(input);
                            }
                            return element;

                        case 11:
                            if (flag)
                            {
                                element.sib13_v920 = SystemInformationBlockType13_r9.PerDecoder.Instance.Decode(input);
                            }
                            return element;

                        case 12:
                            if (flag)
                            {
                                element.sib14_v1130 = SystemInformationBlockType14_r11.PerDecoder.Instance.Decode(input);
                            }
                            return element;

                        case 13:
                            if (flag)
                            {
                                element.sib15_v1130 = SystemInformationBlockType15_r11.PerDecoder.Instance.Decode(input);
                            }
                            return element;

                        case 14:
                            if (flag)
                            {
                                element.sib16_v1130 = SystemInformationBlockType16_r11.PerDecoder.Instance.Decode(input);
                            }
                            return element;
                    }
                    throw new Exception(GetType().Name + ":NoChoice had been choose");
                }
            }
        }
    }
}