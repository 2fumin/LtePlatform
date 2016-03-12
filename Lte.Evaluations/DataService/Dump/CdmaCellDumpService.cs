using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

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
            var cellList = Mapper.Map<IEnumerable<CdmaCellExcel>, List<CdmaCell>>(infos);
            if (!cellList.Any()) return 0;
            var count = 0;
            foreach (var cell in cellList)
            {
                var item = _cellRepository.GetBySectorId(cell.BtsId, cell.SectorId);
                if (item == null)
                {
                    if (_cellRepository.Insert(cell) != null) count++;
                }
                else
                {
                    item.Pn = cell.Pn;
                    item.IsInUse = true;
                    _cellRepository.Update(item);
                    count++;
                }
            }
            _cellRepository.SaveChanges();
            return count;
        }

        public bool DumpSingleCellExcel(CdmaCellExcel info)
        {
            var cell = _cellRepository.GetBySectorIdAndCellType(info.BtsId, info.SectorId, info.CellType);
            if (cell == null)
            {
                cell = Mapper.Map<CdmaCellExcel, CdmaCell>(info);
                cell.Import(info);
                _cellRepository.SaveChanges();
                return _cellRepository.Insert(cell) != null;
            }
            cell.Import(info);
            cell.IsInUse = true;
            _cellRepository.Update(cell);
            _cellRepository.SaveChanges();
            return true;
        }

        public void VanishCells(CellIdsContainer container)
        {
            foreach (
                var cell in
                    container.CellIdPairs.Select(
                        cellIdPair => _cellRepository.GetBySectorId(cellIdPair.CellId, cellIdPair.SectorId))
                        .Where(cell => cell != null))
            {
                cell.IsInUse = false;
                _cellRepository.Update(cell);
            }
            _cellRepository.SaveChanges();
        }
    }
}
