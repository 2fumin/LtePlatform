using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;

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

    public class RasterFileInfoView
    {
        public int RasterNum { get; set; }

        public IEnumerable<string> CsvFilesNames { get; set; } = new List<string>();

        public RasterFileInfoView(RasterInfo info, string dataType)
        {
            RasterNum = info.RasterNum ?? 0;
            if (dataType == "2G" && !string.IsNullOrEmpty(info.CsvFilesName2G))
                CsvFilesNames = info.CsvFilesName2G.Split(';');
            if (dataType == "3G" && !string.IsNullOrEmpty(info.CsvFilesName3G))
                CsvFilesNames = info.CsvFilesName3G.Split(';');
            if (dataType == "4G" && !string.IsNullOrEmpty(info.CsvFilesName4G))
                CsvFilesNames = info.CsvFilesName4G.Split(';');
        }
    }

    public class FileRasterInfoView
    {
        public string CsvFileName { get; set; }

        public IEnumerable<int> RasterNums { get; set; } 
    }
}
