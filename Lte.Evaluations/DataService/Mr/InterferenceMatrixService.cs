using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Evaluations.MapperSerive;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.ExcelCsv;
using Lte.Parameters.Entities.Mr;

namespace Lte.Evaluations.DataService.Mr
{
    public class InterferenceMatrixService
    {
        private readonly IInterferenceMatrixRepository _repository;
        private readonly IInterferenceMongoRepository _mongoRepository;
        private readonly ICellStatMysqlRepository _statRepository;
        private readonly ICellStasticRepository _mongoStatRepository;

        private static Stack<InterferenceMatrixStat> InterferenceMatrixStats { get; set; }

        public static List<PciCell> PciCellList { get; private set; }
        
        public InterferenceMatrixService(IInterferenceMatrixRepository repository, ICellRepository cellRepository,
            IInfrastructureRepository infrastructureRepository, IInterferenceMongoRepository mongoRepository,
            ICellStatMysqlRepository statRepository, ICellStasticRepository mongoStatRepository)
        {
            _repository = repository;
            _mongoRepository = mongoRepository;
            _statRepository = statRepository;
            _mongoStatRepository = mongoStatRepository;
            if (InterferenceMatrixStats == null)
                InterferenceMatrixStats = new Stack<InterferenceMatrixStat>();
            if (PciCellList == null)
            {
                var cells = from cell in cellRepository.GetAllList()
                    join moinitor in infrastructureRepository.GetAllPreciseMonitor() on cell.Id equals
                        moinitor.InfrastructureId
                    select cell;
                PciCellList = cells.MapTo<List<PciCell>>();
            }
        }

        public Tuple<int, bool> QueryExistedStatsCount(int eNodebId, byte sectorId, DateTime date)
        {
            var beginDay = date.Date;
            var nextDay = date.AddDays(1).Date;
            return new Tuple<int, bool>(_repository.Count(
                x =>
                    x.ENodebId == eNodebId && x.SectorId == sectorId && x.RecordTime >= beginDay &&
                    x.RecordTime < nextDay), _statRepository.Get(eNodebId, sectorId, beginDay) != null);
        }
        
        public int DumpMongoStats(InterferenceMatrixStat stat)
        {
            stat.RecordTime = stat.RecordTime.Date;
            var existedStat =
                _repository.FirstOrDefault(
                    x =>
                        x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId
                        && x.DestPci == stat.DestPci && x.RecordTime == stat.RecordTime);
            if (existedStat == null)
                _repository.Insert(stat);

            return _repository.SaveChanges();
        }

        public int DumpCellStat(CellStatMysql cellStat)
        {
            var existedStat =
                _statRepository.FirstOrDefault(x => x.ENodebId == cellStat.ENodebId && x.SectorId == cellStat.SectorId
                                                    && x.CurrentDate == cellStat.CurrentDate);
            if (existedStat == null)
                _statRepository.Insert(cellStat);
            return _statRepository.SaveChanges();
        }

        public void TestDumpOneStat(int eNodebId, byte sectorId, DateTime date, double interference)
        {
            _repository.Insert(new InterferenceMatrixStat
            {
                ENodebId = eNodebId,
                SectorId = sectorId,
                RecordTime = date,
                InterferenceLevel = interference
            });
            _repository.SaveChanges();
        }

        public InterferenceMatrixMongo QueryMongo(int eNodebId, short pci)
        {
            return _mongoRepository.GetOne(eNodebId, pci);
        }
        
        public async Task<List<InterferenceMatrixMongo>> QueryMongoList(int eNodebId, short pci, DateTime date)
        {
            var cellList = await _mongoRepository.GetListAsync(eNodebId, pci, date);
            return cellList;
        }

        public CellStatMysql QueryCellStat(int eNodebId, short pci, DateTime date)
        {
            var cellStatList = _mongoStatRepository.GetList(eNodebId, pci, date);
            return !cellStatList.Any()
                ? null
                : new CellStatMysql
                {
                    ENodebId = eNodebId,
                    SectorId = PciCellList.FirstOrDefault(x => x.ENodebId == eNodebId && x.Pci == pci)?.SectorId ?? 0,
                    Pci = pci,
                    CurrentDate = date,
                    Mod3Count = cellStatList.Sum(x => x.Mod3Count),
                    Mod6Count = cellStatList.Sum(x => x.Mod6Count),
                    MrCount = cellStatList.Sum(x => x.MrCount),
                    OverCoverCount = cellStatList.Sum(x => x.OverCoverCount),
                    PreciseCount = cellStatList.Sum(x => x.PreciseCount),
                    WeakCoverCount = cellStatList.Sum(x => x.WeakCoverCount)
                };
        }

        public async Task<List<InterferenceMatrixStat>> QueryStats(int eNodebId, short pci, DateTime time)
        {
            var statList = await _mongoRepository.GetListAsync(eNodebId, pci, time);
            return !statList.Any() ? new List<InterferenceMatrixStat>() : GenereateStatList(time, statList);
        }

        public async Task<InterferenceMatrixStat> QueryStat(int eNodebId, short pci, short neighborPci, DateTime time)
        {
            var statList = await _mongoRepository.GetListAsync(eNodebId, pci, neighborPci, time);
            if (!statList.Any()) return null;
            return new InterferenceMatrixStat
            {
                ENodebId = eNodebId,
                DestPci = neighborPci,
                InterferenceLevel = statList.Sum(x => x.InterfLevel ?? 0),
                Mod3Interferences = statList.Sum(x => x.Mod3Count ?? 0),
                Mod6Interferences = statList.Sum(x => x.Mod6Count ?? 0),
                OverInterferences10Db = statList.Sum(x => x.Over10db ?? 0),
                OverInterferences6Db = statList.Sum(x => x.Over6db ?? 0),
                RecordTime = time
            };
        }

        public async Task<List<InterferenceMatrixStat>> QueryStats(int eNodebId, short pci)
        {
            var stats = await _mongoRepository.GetListAsync(eNodebId, pci);
            return !stats.Any() ? new List<InterferenceMatrixStat>() : GenereateStatList(stats);
        }

        private static List<InterferenceMatrixStat> GenereateStatList(DateTime time, IEnumerable<InterferenceMatrixMongo> statList)
        {
            var results = Mapper.Map<IEnumerable<InterferenceMatrixMongo>, IEnumerable<InterferenceMatrixStat>>(statList);
            return (from s in results
                group s by new {s.DestPci, s.ENodebId}
                into g
                select new InterferenceMatrixStat
                {
                    ENodebId = g.Key.ENodebId,
                    DestPci = g.Key.DestPci,
                    InterferenceLevel = g.Sum(x => x.InterferenceLevel),
                    Mod3Interferences = g.Sum(x => x.Mod3Interferences),
                    Mod6Interferences = g.Sum(x => x.Mod6Interferences),
                    OverInterferences10Db = g.Sum(x => x.OverInterferences10Db),
                    OverInterferences6Db = g.Sum(x => x.OverInterferences6Db),
                    RecordTime = time
                }).ToList();
        }

        private static List<InterferenceMatrixStat> GenereateStatList(List<InterferenceMatrixMongo> statList)
        {
            var results = Mapper.Map<List<InterferenceMatrixMongo>, IEnumerable<InterferenceMatrixStat>>(statList);
            return (from s in results
                    group s by new { s.DestPci, s.ENodebId, RecordDate = s.RecordTime.Date }
                into g
                    select new InterferenceMatrixStat
                    {
                        ENodebId = g.Key.ENodebId,
                        DestPci = g.Key.DestPci,
                        InterferenceLevel = g.Sum(x => x.InterferenceLevel),
                        Mod3Interferences = g.Sum(x => x.Mod3Interferences),
                        Mod6Interferences = g.Sum(x => x.Mod6Interferences),
                        OverInterferences10Db = g.Sum(x => x.OverInterferences10Db),
                        OverInterferences6Db = g.Sum(x => x.OverInterferences6Db),
                        RecordTime = g.Key.RecordDate
                    }).ToList();
        }

        public void UploadInterferenceStats(StreamReader reader, string path)
        {
            var container = InterferenceMatrixCsv.ReadInterferenceMatrixCsvs(reader, path);
            if (container == null) return;
            var time = container.RecordTime;
            var pcis = from info in
                Mapper.Map<List<InterferenceMatrixCsv>, List<InterferenceMatrixPci>>(container.InterferenceMatrixCsvs)
                join cell in PciCellList on new
                {
                    info.ENodebId,
                    Pci = info.SourcePci,
                    info.Frequency
                }
                    equals new
                    {
                        cell.ENodebId,
                        cell.Pci,
                        cell.Frequency
                    }
                select new
                {
                    Info = info,
                    cell.SectorId
                };
            foreach (var matrixPci in pcis)
            {
                var stat = matrixPci.Info.MapTo<InterferenceMatrixStat>();
                stat.RecordTime = time;
                stat.SectorId = matrixPci.SectorId;
                InterferenceMatrixStats.Push(stat);
            }
        }

        public async Task<bool> DumpOneStat()
        {
            var stat = InterferenceMatrixStats.Pop();
            if (stat == null) return false;
            var item =
                _repository.FirstOrDefault(
                    x =>
                        x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId && x.DestPci == stat.DestPci && x.RecordTime == stat.RecordTime);
            if (item == null)
            {
                await _repository.InsertAsync(stat);
            }
            _repository.SaveChanges();
            return true;
        }
        
        public int GetStatsToBeDump()
        {
            return InterferenceMatrixStats.Count;
        }

        public void ClearStats()
        {
            InterferenceMatrixStats.Clear();
        }
    }
    
}
