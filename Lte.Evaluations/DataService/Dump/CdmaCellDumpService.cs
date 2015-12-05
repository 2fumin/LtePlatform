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
    public class CdmaCellDumpService
    {
        private readonly ICdmaCellRepository _cellRepository;

        public CdmaCellDumpService(ICdmaCellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public void DumpNewCellExcels(IEnumerable<CdmaCellExcel> infos)
        {
            var cellList = Mapper.Map<IEnumerable<CdmaCellExcel>, List<CdmaCell>>(infos);
            cellList.ForEach(cell => _cellRepository.InsertAsync(cell));
        }
    }
}
