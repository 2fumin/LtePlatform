using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Evaluations.MapperSerive;
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

        private static Stack<InterferenceMatrixStat> InterferenceMatrixStats { get; set; }

        public static List<PciCell> PciCellList { get; set; }
        
        public InterferenceMatrixService(IInterferenceMatrixRepository repository, ICellRepository cellRepository,
            IInfrastructureRepository infrastructureRepository, IInterferenceMongoRepository mongoRepository)
        {
            _repository = repository;
            _mongoRepository = mongoRepository;
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
        
        public int DumpMongoStats(PciCell cellInfo, DateTime begin, DateTime end)
        {
            if (_mongoRepository.GetOne(cellInfo.ENodebId, cellInfo.Pci) == null) return 0;
            var statTime = begin.Date;
            var count = 0;
            while (statTime < end.Date)
            {
                var time = statTime;
                if (_mongoRepository.GetOne(cellInfo.ENodebId, cellInfo.Pci, time.ToString("yyyyMMdd")) == null)
                {
                    statTime = statTime.AddDays(1);
                    continue;
                }
                for (var i = 0; i < 24; i++)
                {
                    if (_mongoRepository.GetOne(cellInfo.ENodebId, cellInfo.Pci, time.ToString("yyyyMMddHH")) == null)
                    {
                        statTime = statTime.AddHours(1);
                        continue;
                    }
                    for (var j = 0; j < 4; j++)
                    {
                        statTime = statTime.AddMinutes(15);
                        var existedStat =
                            _repository.FirstOrDefault(
                                x =>
                                    x.ENodebId == cellInfo.ENodebId && x.SectorId == cellInfo.SectorId &&
                                    x.RecordTime == time);
                        if (existedStat != null) continue;
                        var mongoStats = QueryStats(cellInfo.ENodebId, cellInfo.Pci, time);
                        foreach (var mongoStat in mongoStats)
                        {
                            mongoStat.SectorId = cellInfo.SectorId;
                            _repository.Insert(mongoStat);
                        }
                    }

                    count += _repository.SaveChanges();
                }
            }
            return count;
        }

        public InterferenceMatrixStat QueryStat(string eNodebInfo, string timeString)
        {
            var mongoStat = _mongoRepository.GetOne(eNodebInfo, timeString);
            return mongoStat == null ? null : Mapper.Map<InterferenceMatrixMongo, InterferenceMatrixStat>(mongoStat);
        }

        public InterferenceMatrixMongo QueryMongo(int eNodebId, short pci)
        {
            return _mongoRepository.GetOne(eNodebId, pci);
        }

        public InterferenceMatrixMongo QueryMongo(int eNodebId, short pci, DateTime date)
        {
            return _mongoRepository.GetOne(eNodebId, pci, date.ToString("yyyyMMdd"));
        }

        public List<InterferenceMatrixStat> QueryStats(int eNodebId, short pci, DateTime time)
        {
            var statList = _mongoRepository.GetList(eNodebId, pci, time);
            if (!statList.Any()) return new List<InterferenceMatrixStat>();
            var results = Mapper.Map<List<InterferenceMatrixMongo>, List<InterferenceMatrixStat>>(statList);
            results.ForEach(x =>
            {
                x.RecordTime = time;
                x.ENodebId = eNodebId;
            });
            return results;
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
