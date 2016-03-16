using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.MapperSerive;
using Lte.Evaluations.MapperSerive.Kpi;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    public class CellService
    {
        private readonly ICellRepository _repository;
        private readonly IENodebRepository _eNodebRepository;

        public CellService(ICellRepository repository, IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
        }

        public CellView GetCell(int eNodebId, byte sectorId)
        {
            var cell = _repository.GetBySectorId(eNodebId, sectorId);
            return cell == null ? null : CellView.ConstructView(cell, _eNodebRepository);
        }

        public IEnumerable<Cell> GetCells(double west, double east, double south, double north)
        {
            return _repository.GetAllList(west, east, south, north);
        }

        public IEnumerable<CellView> GetCellViews(int eNodebId)
        {
            var cells = _repository.GetAllList(eNodebId);
            return cells.Any()
                ? cells.Select(x => CellView.ConstructView(x, _eNodebRepository))
                : new List<CellView>();
        }

        public IEnumerable<CellView> GetNearbyCellsWithPci(int eNodebId, byte sectorId, short pci)
        {
            var cell = _repository.GetBySectorId(eNodebId, sectorId);
            if (cell==null) return new List<CellView>();
            return
                GetCells(cell.Longtitute - 0.2, cell.Longtitute + 0.2, cell.Lattitute - 0.2, cell.Lattitute + 0.2)
                    .Where(x => x.Pci == pci && sectorId < x.SectorId + 16 && x.SectorId < sectorId + 16)
                    .Select(x => CellView.ConstructView(x, _eNodebRepository));
        }

        public List<byte> GetSectorIds(string eNodebName)
        {
            var eNodeb = _eNodebRepository.GetByName(eNodebName);
            return eNodeb == null
                ? null
                : _repository.GetAll().Where(x => x.ENodebId == eNodeb.ENodebId).Select(x => x.SectorId).ToList();
        }

        public IEnumerable<SectorView> QuerySectors(int eNodebId)
        {
            var cells = _repository.GetAllList(eNodebId);
            return cells.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : new List<SectorView>();
        }

        public IEnumerable<SectorView> QuerySectors(double west, double east, double south, double north)
        {
            var cells = _repository.GetAllList(west, east, south, north);
            return cells.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : new List<SectorView>();
        }

        public IEnumerable<SectorView> QuerySectors(SectorRangeContainer container)
        {
            var cells =
                _repository.GetAllList(container.West, container.East, container.South, container.North)
                    .Where(x => x.IsInUse).ToList();
            var excludeCells = from cell in cells join sector in container.ExcludedCells on new
                {
                    CellId = cell.ENodebId,
                    cell.SectorId
                } equals new
                {
                    sector.CellId,
                    sector.SectorId
                } select cell;
            cells = cells.Except(excludeCells).ToList();
            return cells.Any()
                ? Mapper.Map<IEnumerable<CellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CellView.ConstructView(x, _eNodebRepository)))
                : new List<SectorView>();
        }

        public IEnumerable<Precise4GSector> QuerySectors(TopPreciseViewContainer container)
        {
            return container.Views.Select(x => Precise4GSector.ConstructSector(x, _repository));
        } 
    }
}
