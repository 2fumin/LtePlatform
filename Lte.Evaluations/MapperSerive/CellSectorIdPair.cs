using Abp.EntityFramework.AutoMapper;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Neighbor;

namespace Lte.Evaluations.MapperSerive
{
    [AutoMapFrom(typeof(Precise4GView), typeof(NearestPciCell), typeof(LteNeighborCell))]
    public class CellSectorIdPair
    {
        public int CellId { get; set; }

        public byte SectorId { get; set; }
    }
}
