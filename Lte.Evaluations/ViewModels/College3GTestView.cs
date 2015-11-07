using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
{
    public class College3GTestView
    {
        public string CollegeName { get; set; }

        public double DownloadRate { get; set; }

        public int AccessUsers { get; set; }

        public double MinRssi { get; set; }

        public double MaxRssi { get; set; }

        public double Vswr { get; set; }

        public College3GTestView() { }

        public College3GTestView(College3GTestResults results)
        {
            results.CloneProperties(this);
        }
    }
}
