using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    public class NewCellListContainer
    {
        public IEnumerable<CellExcel> Infos { get; set; } 
    }
}
