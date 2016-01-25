using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    [TypeDoc("CDMA小区EXCEL信息容器，用于打包向服务器POST")]
    public class NewCdmaCellListContainer
    {
        public IEnumerable<CdmaCellExcel> Infos { get; set; } 
    }
}
