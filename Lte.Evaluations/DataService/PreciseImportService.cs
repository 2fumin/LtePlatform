using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;

namespace Lte.Evaluations.DataService
{
    public class PreciseImportService
    {
        private readonly IPreciseCoverage4GRepository _repository;
        private readonly ITownPreciseCoverage4GStatRepository _regionRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;

        private static Stack<PreciseCoverage4G> PreciseCoverage4Gs { get; set; } 

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
        }

        public IEnumerable<TownPreciseView> UploadItems(StreamReader reader)
        {
            try
            {
                var items = CsvContext.Read<PreciseCoverage4GCsv>(reader, CsvFileDescription.CommaDescription).ToList();
                var stats = Mapper.Map<List<PreciseCoverage4GCsv>, List<PreciseCoverage4G>>(items);
                var query = from stat in stats
                    join eNodeb in _eNodebRepository.GetAllList() on stat.CellId equals eNodeb.ENodebId
                    select
                        new
                        {
                            Stat = stat,
                            eNodeb.TownId
                        };
                foreach (var stat in stats)
                {
                    PreciseCoverage4Gs.Push(stat);
                }
                var townStats = query.Select(x =>
                {
                    var townStat = Mapper.Map<PreciseCoverage4G, TownPreciseCoverage4GStat>(x.Stat);
                    townStat.TownId = x.TownId;
                    return townStat;
                });
                return townStats.Select(x=>TownPreciseView.ConstructView(x,_townRepository));
            }
            catch
            {
                // ignored
                return new List<TownPreciseView>();
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
    }
}
