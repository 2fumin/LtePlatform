using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    public class CdmaCellDumpService
    {
        private readonly ICdmaCellRepository _cellRepository;

        public CdmaCellDumpService(ICdmaCellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public int DumpNewCellExcels(IEnumerable<CdmaCellExcel> infos)
        {
            var infoList = infos.ToArray();
            if (!infoList.Any()) return 0;
            var cellList = new List<CdmaCell>();
            foreach (var info in infoList)
            {
                var cell =
                    cellList.FirstOrDefault(
                        x => x.BtsId == info.BtsId && x.SectorId == info.SectorId && x.CellType == info.CellType);
                if (cell == null)
                {
                    cell = Mapper.Map<CdmaCellExcel, CdmaCell>(info);
                    cellList.Add(cell);
                }
                cell.Import(info);
            }
            
            var count = 0;
            foreach (var cell in cellList)
            {
                if (_cellRepository.Insert(cell) != null)
                    count++;
            }
            return count;
        }

        public bool DumpSingleCellExcel(CdmaCellExcel info)
        {
            var cell = _cellRepository.GetBySectorIdAndCellType(info.BtsId, info.SectorId, info.CellType);
            if (cell == null)
            {
                cell = Mapper.Map<CdmaCellExcel, CdmaCell>(info);
                cell.Import(info);
                return _cellRepository.Insert(cell) != null;
            }
            cell.Import(info);
            _cellRepository.Update(cell);
            return true;
        }
    }
}
