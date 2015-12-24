using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class NearestPciCellService
    {
        private readonly INearestPciCellRepository _repository;

        public NearestPciCellService(INearestPciCellRepository repository)
        {
            _repository = repository;
        }

        public List<NearestPciCell> QueryCells(int cellId, byte sectorId)
        {
            return _repository.GetAllList(cellId, sectorId);
        }
    }
}
