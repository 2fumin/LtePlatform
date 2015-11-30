using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToExcel;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class BasicImportService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICellRepository _cellRepository;
        private readonly IBtsRepository _btsRepository;
        private readonly ICdmaCellRepository _cdmaCellRepository;

        public BasicImportService(IENodebRepository eNodebRepository, ICellRepository cellRepository,
            IBtsRepository btsRepository, ICdmaCellRepository cdmaCellRepository)
        {
            _eNodebRepository = eNodebRepository;
            _cellRepository = cellRepository;
            _btsRepository = btsRepository;
            _cdmaCellRepository = cdmaCellRepository;
        }

        public static List<ENodebExcel> ENodebExcels { get; set; } = new List<ENodebExcel>();

        public static List<CellExcel> CellExcels { get; set; } = new List<CellExcel>();

        public static List<BtsExcel> BtsExcels { get; set; } = new List<BtsExcel>();

        public static List<CdmaCellExcel> CdmaCellExcels { get; set; } = new List<CdmaCellExcel>();

        public void ImportLteParameters(string path)
        {
            var repo = new ExcelQueryFactory {FileName = path};
            ENodebExcels = (from c in repo.Worksheet<ENodebExcel>("基站级")
                select c).ToList();
            CellExcels = (from c in repo.Worksheet<CellExcel>("小区级")
                select c).ToList();
        }

        public void ImportCdmaParameters(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            BtsExcels = (from c in repo.Worksheet<BtsExcel>("基站级")
                select c).ToList();
            CdmaCellExcels = (from c in repo.Worksheet<CdmaCellExcel>("小区级")
                select c).ToList();
        }

        public IEnumerable<ENodebExcel> GetNewENodebExcels()
        {
            if (!ENodebExcels.Any()) return new List<ENodebExcel>();
            return from info in ENodebExcels
                join eNodeb in _eNodebRepository.GetAllList()
                    on info.ENodebId equals eNodeb.ENodebId into eNodebQuery
                from eq in eNodebQuery.DefaultIfEmpty()
                where eq == null
                select info;
        }

        public IEnumerable<CellExcel> GetNewCellExcels()
        {
            if (!CellExcels.Any()) return new List<CellExcel>();
            return from info in CellExcels
                join cell in _cellRepository.GetAllList()
                    on new {info.ENodebId, info.SectorId} equals new {cell.ENodebId, cell.SectorId} into cellQuery
                from cq in cellQuery.DefaultIfEmpty()
                where cq == null
                select info;
        }

        public IEnumerable<BtsExcel> GetNewBtsExcels()
        {
            if (!BtsExcels.Any()) return new List<BtsExcel>();
            return from info in BtsExcels
                join bts in _btsRepository.GetAllList()
                    on info.BtsId equals bts.BtsId into btsQuery
                from bq in btsQuery.DefaultIfEmpty()
                where bq == null
                select info;
        }

        public IEnumerable<CdmaCellExcel> GetNewCdmaCellExcels()
        {
            if (!CdmaCellExcels.Any()) return new List<CdmaCellExcel>();
            return from info in CdmaCellExcels
                join cell in _cdmaCellRepository.GetAllList()
                    on new {info.BtsId, info.SectorId} equals new {cell.BtsId, cell.SectorId} into cellQuery
                from cq in cellQuery.DefaultIfEmpty()
                where cq == null
                select info;
        } 
    }
}
