using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly ICellRepository _cellRepository;

        private static Stack<NearestPciCell> NearestCells { get; set; } 

        public NearestPciCellService(INearestPciCellRepository repository, ICellRepository cellRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            if (NearestCells == null)
                NearestCells = new Stack<NearestPciCell>();
        }

        public List<NearestPciCell> QueryCells(int cellId, byte sectorId)
        {
            return _repository.GetAllList(cellId, sectorId);
        }

        public void UploadZteNeighbors(StreamReader reader)
        {
            
        }

        public void UploadHwNeighbors(StreamReader reader)
        {
            
        }
    }
}
