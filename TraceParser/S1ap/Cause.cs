using System;
using Lte.Domain.Common;

namespace TraceParser.S1ap
{
    [Serializable]
    public class Cause
    {
        public void InitDefaults()
        {
        }

        public CauseMisc misc { get; set; }

        public CauseNas nas { get; set; }

        public CauseProtocol protocol { get; set; }

        public CauseRadioNetwork radioNetwork { get; set; }

        public CauseTransport transport { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public Cause Decode(BitArrayInputStream input)
            {
                int num4;
                Cause cause = new Cause();
                cause.InitDefaults();
                input.readBit();
                switch (input.readBits(3))
                {
                    case 0:
                        num4 = (input.readBit() == 0) ? 6 : 6;
                        cause.radioNetwork = (CauseRadioNetwork)input.readBits(num4);
                        return cause;

                    case 1:
                        num4 = 1;
                        cause.transport = (CauseTransport)input.readBits(num4);
                        return cause;

                    case 2:
                        num4 = (input.readBit() == 0) ? 3 : 3;
                        cause.nas = (CauseNas)input.readBits(num4);
                        return cause;

                    case 3:
                        num4 = (input.readBit() == 0) ? 3 : 3;
                        cause.protocol = (CauseProtocol)input.readBits(num4);
                        return cause;

                    case 4:
                        num4 = (input.readBit() == 0) ? 3 : 3;
                        cause.misc = (CauseMisc)input.readBits(num4);
                        return cause;
                }
                throw new Exception(GetType().Name + ":NoChoice had been choose");
            }
        }
    }
}
