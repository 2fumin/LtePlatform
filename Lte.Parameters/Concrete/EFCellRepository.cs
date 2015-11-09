using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    public class EFCellRepository : LightWeightRepositroyBase<Cell>, ICellRepository
    {
        protected override DbSet<Cell> Entities
        {
            get { return context.Cells; }
        }

        public void AddCells(IEnumerable<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                Insert(cell);
            }
        }
    }
}
