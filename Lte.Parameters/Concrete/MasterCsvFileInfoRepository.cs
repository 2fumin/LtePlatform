﻿using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class MasterCsvFileInfoRepository : ICsvFileInfoRepository
    {
        private readonly MasterTestContext _context = new MasterTestContext();

        public IQueryable<CsvFilesInfo> CsvFilesInfos => _context.CsvFilesInfos;

        public IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName)
        {
            return _context.Get4GFileContents(fileName);
        }

        public IEnumerable<FileRecord4G> GetFileRecord4Gs(string fileName, int rasterNum)
        {
            return _context.Get4GFileContents(fileName, rasterNum);
        }

        public IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName)
        {
            return _context.Get3GFileContents(fileName);
        }

        public IEnumerable<FileRecord3G> GetFileRecord3Gs(string fileName, int rasterNum)
        {
            return _context.Get3GFileContents(fileName, rasterNum);
        }

        public IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName)
        {
            return _context.Get2GFileContents(fileName);
        }

        public IEnumerable<FileRecord2G> GetFileRecord2Gs(string fileName, int rasterNum)
        {
            return _context.Get2GFileContents(fileName, rasterNum);
        }

        public List<CsvFilesInfo> GetAllList(DateTime begin, DateTime end)
        {
            return CsvFilesInfos.Where(x => x.TestDate != null && x.TestDate >= begin && x.TestDate < end).ToList();
        }
    }
}
