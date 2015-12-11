using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class SectorView
    {
        public string CellName { get; set; }

        public string Indoor { get; set; }

        public double Azimuth { get; set; }

        public double BaiduLongtitute { get; set; }

        public double BaiduLattitute { get; set; }

        public double Height { get; set; }

        public double DownTilt { get; set; }

        public double AntennaGain { get; set; }

        public int Frequency { get; set; }

        public string OtherInfos { get; set; }
    }
}
