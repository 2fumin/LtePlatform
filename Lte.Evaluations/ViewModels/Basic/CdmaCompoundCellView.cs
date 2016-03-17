using AutoMapper;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;

namespace Lte.Evaluations.ViewModels.Basic
{
    public class CdmaCompoundCellView
    {
        public string BtsName { get; set; }

        public int BtsId { get; set; } = -1;

        public byte SectorId { get; set; } = 31;

        public int CellId { get; set; }

        public string Lac { get; set; }

        public short Pn { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public double Height { get; set; }

        public double DownTilt { get; set; }

        public double Azimuth { get; set; }

        public double AntennaGain { get; set; }

        public string Indoor { get; set; }

        public string OnexFrequencyList { get; set; }

        public string EvdoFrequencyList { get; set; }

        public static CdmaCompoundCellView ConstructView(CdmaCell onexCell, CdmaCell evdoCell, IBtsRepository repository)
        {
            CdmaCompoundCellView view = null;
            if (onexCell != null)
            {
                view = Mapper.Map<CdmaCell, CdmaCompoundCellView>(onexCell);
                view.OnexFrequencyList = onexCell.FrequencyList;
                if (evdoCell != null) view.EvdoFrequencyList = evdoCell.FrequencyList;
            }
            else if (evdoCell != null)
            {
                view = Mapper.Map<CdmaCell, CdmaCompoundCellView>(evdoCell);
                view.EvdoFrequencyList = evdoCell.FrequencyList;
            }

            if (view != null)
            {
                var bts = repository.GetByBtsId(view.BtsId);
                view.BtsName = bts?.Name;
            }

            return view;
        }
    }
}