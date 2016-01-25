using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Dump
{
    public class CellDumpService
    {
        private readonly IBtsRepository _btsRepository;
        private readonly ICellRepository _cellRepository;

        public CellDumpService(IBtsRepository btsRepository, ICellRepository cellRepository)
        {
            _btsRepository = btsRepository;
            _cellRepository = cellRepository;
        }

        public int DumpNewCellExcels(IEnumerable<CellExcel> infos)
        {
            var cellList = Mapper.Map<IEnumerable<CellExcel>, List<Cell>>(infos);
            if (!cellList.Any()) return 0;
            var count = 0;
            foreach (var cell in cellList)
            {
                var item = _cellRepository.GetBySectorId(cell.ENodebId, cell.SectorId);
                if (item == null)
                {
                    if (_cellRepository.Insert(cell) != null) count++;
                }
                else
                {
                    item.Pci = cell.Pci;
                    item.IsInUse = true;
                    _cellRepository.Update(item);
                    count++;
                }
            }
            _cellRepository.SaveChanges();
            return count;
        }

        public void UpdateENodebBtsIds(IEnumerable<CellExcel> infos)
        {
            var idPairs =
                Mapper.Map<IEnumerable<CellExcel>, IEnumerable<ENodebBtsIdPair>>(infos)
                    .Where(x => x.BtsId != -1)
                    .Distinct()
                    .ToList();
            if (!idPairs.Any()) return;
            idPairs.ForEach(x =>
            {
                var bts = _btsRepository.GetByBtsId(x.BtsId);
                if (bts == null) return;
                bts.ENodebId = x.ENodebId;
                _btsRepository.Update(bts);
            });
        }

        public bool DumpSingleCellExcel(CellExcel info)
        {
            var cell = _cellRepository.GetBySectorId(info.ENodebId, info.SectorId);
            if (cell == null)
            {
                cell = Cell.ConstructItem(info);
                var fields = info.ShareCdmaInfo.GetSplittedFields('_');
                var btsId = (fields.Length > 2) ? fields[1].ConvertToInt(-1) : -1;
                if (btsId > 0)
                {
                    var bts = _btsRepository.GetByBtsId(btsId);
                    if (bts != null)
                    {
                        bts.ENodebId = info.ENodebId;
                        _btsRepository.Update(bts);
                    }
                }
                var result = _cellRepository.Insert(cell);
                if (result == null) return false;
                var item =
                    BasicImportService.CellExcels.FirstOrDefault(
                        x => x.ENodebId == info.ENodebId && x.SectorId == info.SectorId);
                if (item != null)
                {
                    BasicImportService.CellExcels.Remove(item);
                }
                _cellRepository.SaveChanges();
                return true;
            }
            cell.Pci = info.Pci;
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
