﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Evaluations.MapperSerive.Kpi;
using Lte.Evaluations.ViewModels.Precise;
using Lte.Evaluations.ViewModels.RegionKpi;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.DataService.Kpi
{
    public class PreciseImportService
    {
        private readonly IPreciseCoverage4GRepository _repository;
        private readonly ITownPreciseCoverage4GStatRepository _regionRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;

        public static Stack<PreciseCoverage4G> PreciseCoverage4Gs { get; set; } 

        public static List<TownPreciseView> TownPreciseViews { get; set; }

        public PreciseImportService(IPreciseCoverage4GRepository repository,
            ITownPreciseCoverage4GStatRepository regionRepository,
            IENodebRepository eNodebRepository, ITownRepository townRepository)
        {
            _repository = repository;
            _regionRepository = regionRepository;
            _eNodebRepository = eNodebRepository;
            _townRepository = townRepository;
            if (PreciseCoverage4Gs == null)
                PreciseCoverage4Gs = new Stack<PreciseCoverage4G>();
            if (TownPreciseViews == null)
                TownPreciseViews = new List<TownPreciseView>();
        }

        public void UploadItems(StreamReader reader)
        {
            try
            {
                var items = CsvContext.Read<PreciseCoverage4GCsv>(reader, CsvFileDescription.CommaDescription).ToList();
                var statTime = items[0].StatTime;
                var stats = Mapper.Map<List<PreciseCoverage4GCsv>, List<PreciseCoverage4G>>(items);
                PreciseCoverage4Gs = new Stack<PreciseCoverage4G>();
                foreach (var stat in stats)
                {
                    PreciseCoverage4Gs.Push(stat);
                }

                var townStats = GetTownStats(stats);
                var mergeStats = GetMergeStats(townStats, statTime);
                TownPreciseViews = mergeStats.Select(x => TownPreciseView.ConstructView(x, _townRepository)).ToList();
            }
            catch
            {
                // ignored
                TownPreciseViews = new List<TownPreciseView>();
            }
        }

        public IEnumerable<TownPreciseCoverage4GStat> GetMergeStats(IEnumerable<TownPreciseCoverage4GStat> townStats, DateTime statTime)
        {
            var mergeStats = from stat in townStats
                group stat by stat.TownId
                into g
                select new TownPreciseCoverage4GStat
                {
                    TownId = g.Key,
                    FirstNeighbors = g.Sum(x => x.FirstNeighbors),
                    SecondNeighbors = g.Sum(x => x.SecondNeighbors),
                    ThirdNeighbors = g.Sum(x => x.ThirdNeighbors),
                    TotalMrs = g.Sum(x => x.TotalMrs),
                    StatTime = statTime
                };
            return mergeStats;
        }

        public IEnumerable<TownPreciseCoverage4GStat> GetTownStats(List<PreciseCoverage4G> stats)
        {
            var query = from stat in stats
                join eNodeb in _eNodebRepository.GetAllList() on stat.CellId equals eNodeb.ENodebId
                select
                    new
                    {
                        Stat = stat,
                        eNodeb.TownId
                    };
            var townStats = query.Select(x =>
            {
                var townStat = Mapper.Map<PreciseCoverage4G, TownPreciseCoverage4GStat>(x.Stat);
                townStat.TownId = x.TownId;
                return townStat;
            });
            return townStats;
        }

        public void DumpTownStats(TownPreciseViewContainer container)
        {
            var stats = Mapper.Map<IEnumerable<TownPreciseView>, IEnumerable<TownPreciseCoverage4GStat>>(container.Views);
            foreach (var stat in from stat in stats
                                 let item = _regionRepository.GetByTown(stat.TownId, stat.StatTime)
                                 where item == null select stat)
            {
                _regionRepository.Insert(stat);
            }
        }

        public bool DumpOneStat()
        {
            var stat = PreciseCoverage4Gs.Pop();
            if (stat == null) return false;
            var item =
                _repository.FirstOrDefault(
                    x =>
                        x.StatTime == stat.StatTime && x.CellId == stat.CellId && x.SectorId == stat.SectorId );
            if (item == null)
            {
                _repository.Insert(stat);
            }
            return true;
        }

        public int GetStatsToBeDump()
        {
            return PreciseCoverage4Gs.Count;
        }

        public void ClearStats()
        {
            PreciseCoverage4Gs.Clear();
        }

        public IEnumerable<PreciseHistory> GetPreciseHistories(DateTime begin, DateTime end)
        {
            var results = new List<PreciseHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin.Date;
                var endDate = beginDate.AddDays(1);
                var items = _repository.GetAllList(beginDate, endDate);
                var townItems = _regionRepository.GetAllList(beginDate, endDate);
                results.Add(new PreciseHistory
                {
                    DateString = begin.ToShortDateString(),
                    PreciseStats = items.Count,
                    TownPreciseStats = townItems.Count
                });
                begin = begin.AddDays(1);
            }
            return results;
        }
    }
}
