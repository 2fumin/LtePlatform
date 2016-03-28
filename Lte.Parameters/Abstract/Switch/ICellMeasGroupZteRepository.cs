using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Switch
{
    public interface ICellMeasGroupZteRepository : IRepository<CellMeasGroupZte, ObjectId>
    {
        CellMeasGroupZte GetRecent(int eNodebId);
    }

    public interface IEUtranCellMeasurementZteRepository : IRepository<EUtranCellMeasurementZte, ObjectId>
    {
        EUtranCellMeasurementZte GetRecent(int eNodebId, byte sectorId);
    }
}
