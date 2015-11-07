using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.ViewModels
{
    public class College4GTestView
    {
        public string CollegeName { get; set; }

        public string CellName { get; set; }

        public short Pci { get; set; }

        public double DownloadRate { get; set; }

        public double UploadRate { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int AccessUsers { get; set; }

        public double Rsrp { get; set; }

        public double Sinr { get; set; }

        public College4GTestView() { }

        public College4GTestView(College4GTestResults results)
        {
            results.CloneProperties(this);
        }
    }
}
