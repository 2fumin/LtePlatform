using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;

namespace Lte.Evaluations.ViewModels
{
    public class WorkItemView
    {
        public string SerialNumber { get; set; }

        public string WorkItemType { get; set; }

        public string WorkItemSubType { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime Deadline { get; set; }

        public short RepeatTimes { get; set; }

        public short RejectTimes { get; set; }

        public string StaffName { get; set; }

        public DateTime? FeedbackTime { get; set; }

        public DateTime? FinishTime { get; set; }

        public string WorkItemCause { get; set; }

        public string WorkItemState { get; set; }

        public string Comments { get; set; }

        public string FeedbackContents { get; set; }

        public void UpdateTown(IENodebRepository eNodebRepository, IBtsRepository btsRepository,
            ITownRepository townRepository)
        {
            if (ENodebId > 10000)
            {
                var eNodeb = eNodebRepository.GetByENodebId(ENodebId);
                if (eNodeb != null)
                {
                    var town = townRepository.Get(eNodeb.TownId);
                    if (town != null)
                    {
                        City = town.CityName;
                        District = town.DistrictName;
                        Town = town.TownName;
                        return;
                    }
                }
            }
            var bts = btsRepository.GetByBtsId(ENodebId);
            if (bts != null)
            {
                var town = townRepository.Get(bts.TownId);
                if (town != null)
                {
                    City = town.CityName;
                    District = town.DistrictName;
                    Town = town.TownName;
                }
            }
        }
    }
}
