using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.ExcelCsv;

namespace Lte.Evaluations.DataService
{
    public class NearestPciCellService
    {
        private readonly INearestPciCellRepository _repository;
        private readonly ICellRepository _cellRepository;
        private readonly IENodebRepository _eNodebRepository;

        private static Stack<NearestPciCell> NearestCells { get; set; } 

        public NearestPciCellService(INearestPciCellRepository repository, ICellRepository cellRepository,
            IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            _eNodebRepository = eNodebRepository;
            if (NearestCells == null)
                NearestCells = new Stack<NearestPciCell>();
        }

        public List<NearestPciCellView> QueryCells(int cellId, byte sectorId)
        {
            return _repository.GetAllList(cellId, sectorId).Select(x=>NearestPciCellView.ConstructView(x, _eNodebRepository)).ToList();
        }

        public int UpdateNeighborPcis(int cellId, byte sectorId)
        {
            var neighborList = _repository.GetAllList(cellId, sectorId);
            foreach (var pciCell in neighborList)
            {
                var cell = _cellRepository.GetBySectorId(pciCell.NearestCellId, pciCell.NearestSectorId);
                if (cell == null || pciCell.Pci == cell.Pci) continue;
                pciCell.Pci = cell.Pci;
                _repository.Update(pciCell);
            }
            return _repository.SaveChanges();
        }

        public void UploadZteNeighbors(StreamReader reader)
        {
            var groupInfos = NeighborCellZteCsv.ReadNeighborCellZteCsvs(reader);
            foreach (var info in groupInfos)
            {
                var cell = NearestPciCell.ConstructCell(info, _cellRepository);
                if (cell.Pci >= 0) NearestCells.Push(cell);
            }
        }

        public void UploadHwNeighbors(StreamReader reader)
        {
            var groupInfos = NeighborCellHwCsv.ReadNeighborCellHwCsvs(reader);
            foreach (var info in groupInfos)
            {
                var cell = NearestPciCell.ConstructCell(info, _cellRepository);
                if (cell.Pci >= 0) NearestCells.Push(cell);
            }
        }

        public async Task<bool> DumpOneStat()
        {
            var stat = NearestCells.Pop();
            if (stat == null) return false;
            var item =
                _repository.FirstOrDefault(
                    x =>
                        x.CellId == stat.CellId && x.SectorId == stat.SectorId && x.Pci == stat.Pci);
            if (item == null)
            {
                await _repository.InsertAsync(stat);
            }
            else if (stat.TotalTimes > 0)
            {
                item.NearestSectorId = stat.NearestSectorId;
                item.NearestCellId = stat.NearestCellId;
                item.TotalTimes = stat.TotalTimes;
                await _repository.UpdateAsync(item);
            }
            _repository.SaveChanges();
            return true;
        }

        public int GetNeighborsToBeDump()
        {
            return NearestCells.Count;
        }

        public void ClearNeighbors()
        {
            NearestCells.Clear();
        }
    }
}
