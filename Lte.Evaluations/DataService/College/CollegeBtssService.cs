using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeBtssService
    {
        private readonly IInfrastructureRepository _repository;
        private readonly IBtsRepository _btsRepository;

        public CollegeBtssService(IInfrastructureRepository repository, IBtsRepository btsRepository)
        {
            _repository = repository;
            _btsRepository = btsRepository;
        }

        public IEnumerable<CdmaBtsView> QueryCollegeBtss(string collegeName)
        {
            var ids = _repository.GetBtsIds(collegeName);
            var btss = ids.Select(_btsRepository.Get).Where(bts => bts != null).ToList();
            return Mapper.Map<List<CdmaBts>, IEnumerable<CdmaBtsView>>(btss); 
        } 
    }
}
