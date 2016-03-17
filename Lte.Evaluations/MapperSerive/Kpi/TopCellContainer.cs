using Lte.Parameters.Abstract.Basic;

namespace Lte.Evaluations.MapperSerive.Kpi
{
    public class TopCellContainer<TTopCell>
        where TTopCell : IBtsIdQuery
    {
        public TTopCell TopCell { get; set; }

        public string CdmaName { get; set; }

        public string LteName { get; set; }
    }
}
