using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Parameters.Concrete.Basic
{
    public class EFCellRepository : EfRepositoryBase<EFParametersContext, Cell>, ICellRepository
    {
        public void AddCells(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                Insert(cell);
            }
        }

        public Cell GetBySectorId(int eNodebId, byte sectorId)
        {
            return FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == sectorId);
        }

        public Cell GetByFrequency(int eNodebId, int frequency)
        {
            return FirstOrDefault(x => x.ENodebId == eNodebId && x.Frequency == frequency);
        }

        public List<Cell> GetAllList(int eNodebId)
        {
            return GetAll().Where(x => x.ENodebId == eNodebId).ToList();
        }

        public List<Cell> GetAllList(double west, double east, double south, double north)
        {
            return GetAllList(x =>
                x.Longtitute >= west 
                && x.Longtitute <= east 
                && x.Lattitute >= south 
                && x.Lattitute <= north);
        }

        public List<Cell> GetAllInUseList()
        {
            return GetAll().Where(x => x.IsInUse).ToList();
        }

        public EFCellRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

    }
}
