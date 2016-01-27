using System.Collections.Generic;
using System.Linq;
using Lte.Domain.LinqToExcel;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.ExcelCsv;

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
                join eNodeb in _eNodebRepository.GetAllInUseList()
                    on info.ENodebId equals eNodeb.ENodebId into eNodebQuery
                from eq in eNodebQuery.DefaultIfEmpty()
                where eq == null
                select info;
        }

        public IEnumerable<int> GetVanishedENodebIds()
        {
            if (!ENodebExcels.Any()) return new List<int>();
            return from eNodeb in _eNodebRepository.GetAllInUseList()
                join info in ENodebExcels
                    on eNodeb.ENodebId equals info.ENodebId into eNodebQuery
                from eq in eNodebQuery.DefaultIfEmpty()
                where eq == null
                select eNodeb.ENodebId;
        } 

        public IEnumerable<CellExcel> GetNewCellExcels()
        {
            if (!CellExcels.Any()) return new List<CellExcel>();
            return from info in CellExcels
                join cell in _cellRepository.GetAllInUseList()
                    on new {info.ENodebId, info.SectorId, info.Pci} equals 
                    new {cell.ENodebId, cell.SectorId, cell.Pci} into cellQuery
                from cq in cellQuery.DefaultIfEmpty()
                where cq == null
                select info;
        }

        public IEnumerable<CellIdPair> GetVanishedCellIds()
        {
            if (!CellExcels.Any()) return new List<CellIdPair>();
            return from cell in _cellRepository.GetAllInUseList()
                join info in CellExcels
                    on new {cell.ENodebId, cell.SectorId} equals new {info.ENodebId, info.SectorId}
                    into cellQuery
                from cq in cellQuery.DefaultIfEmpty()
                where cq == null
                select new CellIdPair {CellId = cell.ENodebId, SectorId = cell.SectorId};
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
