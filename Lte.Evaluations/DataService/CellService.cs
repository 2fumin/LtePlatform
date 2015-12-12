using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
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

        public IEnumerable<Cell> GetCells(double west, double east, double south, double north)
        {
            return _repository.GetAll().Where(x =>
                x.Longtitute >= west && x.Longtitute <= east && x.Lattitute >= south && x.Lattitute <= north);
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

        public Cell QueryCell(int eNodebId, byte sectorId)
        {
            return _repository.FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == sectorId);
        }
    }
}
