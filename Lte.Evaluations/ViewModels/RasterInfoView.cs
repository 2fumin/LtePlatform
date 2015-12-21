using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels
{
    public class RasterInfoView
    {
        public int RasterNum { get; set; }

        public IEnumerable<string> CsvFilesName4Gs { get; set; }

        public IEnumerable<string> CsvFilesName3Gs { get; set; }

        public IEnumerable<string> CsvFilesName2Gs { get; set; }

        public string Area { get; set; }

        public double WestLongtitute { get; set; }

        public double EastLongtitute { get; set; }

        public double SouthLattitute { get; set; }

        public double NorthLattitute { get; set; }
    }
}
