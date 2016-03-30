using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Evaluations.ViewModels.Basic;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.DataService.Basic
{
    public class CdmaCellService
    {
        private readonly ICdmaCellRepository _repository;
        private readonly IBtsRepository _btsRepository;

        public CdmaCellService(ICdmaCellRepository repository, IBtsRepository btsRepository)
        {
            _repository = repository;
            _btsRepository = btsRepository;
        }

        public CdmaCompoundCellView QueryCell(int btsId, byte sectorId)
        {
            var onexCell = _repository.GetBySectorIdAndCellType(btsId, sectorId, "1X");
            var evdoCell = _repository.GetBySectorIdAndCellType(btsId, sectorId, "DO");
            return CdmaCompoundCellView.ConstructView(onexCell, evdoCell, _btsRepository);
        }

        public CdmaCellView QueryCell(int btsId, byte sectorId, string cellType)
        {
            var item = _repository.GetBySectorIdAndCellType(btsId, sectorId, cellType);
            return item == null ? null : Mapper.Map<CdmaCell, CdmaCellView>(item);
        }

        public List<byte> GetSectorIds(string btsName)
        {
            var bts = _btsRepository.GetByName(btsName);
            return bts == null
                ? null
                : _repository.GetAll().Where(x => x.BtsId == bts.BtsId).Select(x => x.SectorId).Distinct().ToList();
        }

        public List<CdmaCellView> GetCellViews(string name)
        {
            var bts = _btsRepository.GetByName(name);
            var results = bts == null
                ? null
                : Mapper.Map<IEnumerable<CdmaCell>, List<CdmaCellView>>(_repository.GetAll().Where(x => x.BtsId == bts.BtsId));
            results?.ForEach(x => x.BtsName = name);
            return results;
        }

        public IEnumerable<SectorView> QuerySectors(int btsId)
        {
            var cells = _repository.GetAllList(btsId);
            return cells.Any()
                ? Mapper.Map<IEnumerable<CdmaCellView>, IEnumerable<SectorView>>(
                    cells.Select(x => CdmaCellView.ConstructView(x, _btsRepository)))
                : new List<SectorView>();
        }
    }
}
