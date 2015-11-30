using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
{
    public class College3GTestView
    {
        public DateTime TestTime { get; set; }

        public string CollegeName { get; set; }

        public double DownloadRate { get; set; }

        public int AccessUsers { get; set; }

        public double MinRssi { get; set; }

        public double MaxRssi { get; set; }

        public double Vswr { get; set; }

        public static College3GTestView ConstructView(College3GTestResults results)
        {
            return Mapper.Map<College3GTestResults, College3GTestView>(results);
        }
    }
}
