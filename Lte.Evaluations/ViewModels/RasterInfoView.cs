using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
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

    [TypeDoc("测试数据文件网格视图，一个测试数据文件包含的若干个网格")]
    public class FileRasterInfoView
    {
        [MemberDoc("测试数据文件名")]
        [Required]
        public string CsvFileName { get; set; }

        [MemberDoc("包含的网格编号列表")]
        public IEnumerable<int> RasterNums { get; set; } 
    }
}
