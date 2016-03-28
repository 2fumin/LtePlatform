using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities.Switch
{
    public interface IZteMeasGroup
    {
        int rsrpPeriodMeasCfgIdDl { get; set; }

        string intraFHOMeasCfg { get; set; }

        string closedInterFMeasCfg { get; set; }

        int tdCSFBMeasCfg { get; set; }

        int interRatUTRANPeriodMeasCfg { get; set; }

        int tdsLBMeasCfg { get; set; }

        string openRatFMeasCfg { get; set; }

        int rsrpEventMeasCfgIdDl { get; set; }

        string cdma2KHRPDMeasCfg { get; set; }

        int cdmaANRMeasCfg { get; set; }

        int cdma2K1xCSFBMeasCfg { get; set; }

        int geranANRMeasCfg { get; set; }

        string cdma2K1xMeasCfg { get; set; }

        string tdMeasCfg { get; set; }

        string openRedMeasCfg { get; set; }

        string interFHOMeasCfg { get; set; }

        int interRatGSMPeriodMeasCfg { get; set; }

        int utranANRMeasCfg { get; set; }

        int gsmLBMeasCfg { get; set; }

        int intraFPeriodMeasCfg { get; set; }

        string geranMeasCfg { get; set; }

        int intraLBMeasCfg { get; set; }

        int gsmCSFBMeasCfg { get; set; }

        int interFPeriodMeasCfg { get; set; }

        int anrMeasCfg { get; set; }

        string openInterFMeasCfg { get; set; }

        int icicMeasCfg { get; set; }

        int eICICMeasCfg { get; set; }

        int wcdmaLBMeasCfg { get; set; }

        string wcdmaMeasCfg { get; set; }

        int rptCGIMeasCfg { get; set; }

        int wcdmaCSFBMeasCfg { get; set; }
    }
}
