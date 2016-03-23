using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Lte.Parameters.Entities.Basic;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Neighbor
{
    public class EutranIntraFreqNCell : IEntity<ObjectId>, IHuaweiMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int CtrlMode { get; set; }

        public int CellMeasPriority { get; set; }

        public string NeighbourCellName { get; set; }

        public int AttachCellSwitch { get; set; }

        public int NoHoFlag { get; set; }

        public int CellId { get; set; }

        public int LocalCellName { get; set; }

        public int CellRangeExpansion { get; set; }

        public int Mnc { get; set; }

        public int Mcc { get; set; }

        public int NCellClassLabel { get; set; }

        public int CellQoffset { get; set; }

        public int NoRmvFlag { get; set; }

        public int eNodeBId { get; set; }

        public int CellIndividualOffset { get; set; }

        public int AnrFlag { get; set; }
    }
}
