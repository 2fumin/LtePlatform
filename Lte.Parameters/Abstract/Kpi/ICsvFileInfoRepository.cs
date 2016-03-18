using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface ICsvFileInfoRepository
    {
        IQueryable<CsvFilesInfo> CsvFilesInfos { get; }

        IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName);

        IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName, int rasterNum);

        IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName);

        IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName, int rasterNum);

        IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName);

        IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName, int rasterNum);

        List<CsvFilesInfo> GetAllList(DateTime begin, DateTime end);
    }
}
