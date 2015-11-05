using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceParser.Eutra;

namespace TraceParser.Outputs
{
    public static class MeasurementReportOutputs
    {
        public static string GetOutputs(this MeasResults.measResultPCell_Type type)
        {
            string result = "RSRP:" + type.rsrpResult;
            result += ", RSRQ:" + type.rsrqResult;
            return result;
        }

        public static string GetOutputs(this MeasResultEUTRA item)
        {
            string result = "PCI:" + item.physCellId;
            result += ", Measure results:" + item.measResult.GetOutputs();
            return result;
        }

        public static string GetOutputs(this MeasResultEUTRA.measResult_Type type)
        {
            string result = "RSRP:" + type.rsrpResult;
            result += ", RSRQ:" + type.rsrqResult;
            return result;
        }
    }
}
