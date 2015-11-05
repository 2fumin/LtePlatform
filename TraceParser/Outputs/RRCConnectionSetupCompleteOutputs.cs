using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceParser.Eutra;

namespace TraceParser.Outputs
{
    public static class RRCConnectionSetupCompleteOutputs
    {
        public static string GetOutputs(this RRCConnectionSetupComplete_r8_IEs item)
        {
            string result = "Selected PLMN ID:" + item.selectedPLMN_Identity;
            result += ", Dedicated info NAS:" + item.dedicatedInfoNAS;
            if (item.registeredMME != null)
            {
                result += ", Registered MME:" + item.registeredMME.GetOutputs();
            }
            return result;
        }

        public static string GetOutputs(this RegisteredMME mme)
        {
            string result = "MMEGI:" + mme.mmegi;
            result += ", MME:" + mme.mmec;
            return result;
        }
    }
}
