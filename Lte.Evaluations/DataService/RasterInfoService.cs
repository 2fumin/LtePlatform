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
    public class RasterInfoService
    {
        private readonly IRasterInfoRepository _repository;

        public RasterInfoService(IRasterInfoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<RasterInfoView> QueryAllList()
        {
            return Mapper.Map<IEnumerable<RasterInfo>, IEnumerable<RasterInfoView>>(_repository.RasterInfos.ToList());
        } 
    }
}
