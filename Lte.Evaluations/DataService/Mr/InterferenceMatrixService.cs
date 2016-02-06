using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Evaluations.MapperSerive;
using Lte.Parameters.Abstract;
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
            var statTime = begin;
            while (statTime < end)
            {
                var time = statTime;
                statTime = statTime.AddMinutes(15);
                var existedStats =
                    _repository.GetAllList(
                        x =>
                            x.ENodebId == cellInfo.ENodebId && x.SectorId == cellInfo.SectorId &&
                            x.RecordTime == time);
                if (existedStats.Any()) continue;
                var mongoStats = QueryStats(cellInfo.ENodebId, cellInfo.Pci, statTime);
                foreach (var mongoStat in mongoStats)
                {
                    mongoStat.SectorId = cellInfo.SectorId;
                    _repository.Insert(mongoStat);
                }
            }
            return _repository.SaveChanges();
        }

        public InterferenceMatrixStat QueryStat(string eNodebInfo, string timeString)
        {
            var mongoStat = _mongoRepository.GetOne(eNodebInfo, timeString);
            return mongoStat == null ? null : Mapper.Map<InterferenceMatrixMongo, InterferenceMatrixStat>(mongoStat);
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
