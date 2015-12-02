using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    public class ENodebDumpService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;

        public ENodebDumpService(IENodebRepository eNodebRepository, ITownRepository townRepository)
        {
            _eNodebRepository = eNodebRepository;
            _townRepository = townRepository;
        }

        public void DumpNewEnodebExcels(IEnumerable<ENodebExcel> infos)
        {
            foreach (var info in infos)
            {
                _eNodebRepository.InsertAsync(Mapper.Map<ENodebExcel, ENodeb>(info));
            }
        }
    }
}
