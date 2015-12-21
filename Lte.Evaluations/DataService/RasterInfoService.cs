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
            return Mapper.Map<IEnumerable<RasterInfo>, IEnumerable<RasterInfoView>>(_repository.GetAllList());
        }

        public IEnumerable<RasterInfoView> QueryWithDataType(string dataType)
        {
            var infos = _repository.GetAllList(dataType);
            return Mapper.Map<IEnumerable<RasterInfo>, IEnumerable<RasterInfoView>>(infos);
        }

        public IEnumerable<FileRasterInfoView> QueryFileNames(string dataType)
        {
            var fileInfos = _repository.GetAllList(dataType).Select(x => new RasterFileInfoView(x, dataType));
            var query = fileInfos.Select(x => x.CsvFilesNames.Select(f => new Tuple<int, string>(x.RasterNum, f)));
            var tuples = query.Aggregate((x, y) => x.Concat(y)).Distinct();

            return from tuple in tuples
                group tuple by tuple.Item2
                into g
                select new FileRasterInfoView
                {
                    CsvFileName = g.Key,
                    RasterNums = g.Select(x => x.Item1)
                };
        }
    }
}
