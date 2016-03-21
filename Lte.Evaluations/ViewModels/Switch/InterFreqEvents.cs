using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels.Switch
{
    public class InterFreqEventA1 : IHoEventView
    {
        public int Hysteresis { get; set; }

        public int TimeToTrigger { get; set; }

        public int ThresholdOfRsrp { get; set; }

        public int ThresholdOfRsrq { get; set; }
    }

    public class InterFreqEventA2 : IHoEventView
    {
        public int Hysteresis { get; set; }

        public int TimeToTrigger { get; set; }

        public int ThresholdOfRsrp { get; set; }

        public int ThresholdOfRsrq { get; set; }
    }

    public class InterFreqEventA3 : IHoEventView
    {
        public int Hysteresis { get; set; }

        public int TimeToTrigger { get; set; }

        public int ThresholdOfRsrp { get; set; }

        public int ThresholdOfRsrq { get; set; }

        public int A3Offset { get; set; }
    }

    public class InterFreqEventA4 : IHoEventView
    {
        public int Hysteresis { get; set; }

        public int TimeToTrigger { get; set; }

        public int ThresholdOfRsrp { get; set; }

        public int ThresholdOfRsrq { get; set; }
    }

    public class InterFreqEventA5 : IHoEventView
    {
        public int Hysteresis { get; set; }

        public int TimeToTrigger { get; set; }

        public int ThresholdOfRsrp { get; set; }

        public int ThresholdOfRsrq { get; set; }

        public int Threshold2OfRsrp { get; set; }

        public int Threshold2OfRsrq { get; set; }
    }
}
