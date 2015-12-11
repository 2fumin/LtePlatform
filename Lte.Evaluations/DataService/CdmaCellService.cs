using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.DataService
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
