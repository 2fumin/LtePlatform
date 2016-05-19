using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.ViewModels.Mr;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Mr;

namespace Lte.Evaluations.DataService.Mr
{
    public class CellStasticService
    {
        private readonly ICellStasticRepository _repository;
        private readonly ICellStatMysqlRepository _statRepository;
        private readonly ICellRepository _cellRepository;

        public CellStasticService(ICellStasticRepository repository, ICellStatMysqlRepository statRepository,
            ICellRepository cellRepository)
        {
            _repository = repository;
            _statRepository = statRepository;
            _cellRepository = cellRepository;
        }

        public CellStasticView QueryDateSpanAverageStat(int eNodebId, short pci, DateTime begin, DateTime end)
        {
            var dateSpanStats = QueryDateSpanStatList(eNodebId, pci, begin, end);
            return dateSpanStats.Any() ? new CellStasticView(dateSpanStats) : null;
        }

        public List<CellStastic> QueryOneDayStats(int eNodebId, byte sectorId, DateTime date)
        {
            var cell = _cellRepository.GetBySectorId(eNodebId, sectorId);
            var pci = cell?.Pci ?? 0;
            return _repository.GetList(eNodebId, pci, date);
        }

        private List<ICellStastic> QueryDateSpanStatList(int eNodebId, short pci, DateTime begin, DateTime end)
        {
            var dateSpanStats = new List<ICellStastic>();
            while (begin < end)
            {
                var oneDayMysqlStat = _statRepository.Get(eNodebId, pci, begin);
                if (oneDayMysqlStat != null)
                {
                    dateSpanStats.Add(oneDayMysqlStat);
                }
                else
                {
                    var oneDayStats = _repository.GetList(eNodebId, pci, begin);
                    
                    if (oneDayStats.Any())
                    {
                        var stat = new CellStatMysql
                        {
                            Mod3Count = oneDayStats.Sum(x => x.Mod3Count),
                            Mod6Count = oneDayStats.Sum(x => x.Mod6Count),
                            MrCount = oneDayStats.Sum(x => x.MrCount),
                            OverCoverCount = oneDayStats.Sum(x => x.OverCoverCount),
                            PreciseCount = oneDayStats.Sum(x => x.PreciseCount),
                            WeakCoverCount = oneDayStats.Sum(x => x.WeakCoverCount),
                            CurrentDate = begin,
                            ENodebId = eNodebId,
                            Pci = pci,
                            SectorId =
                                _cellRepository.FirstOrDefault(x => x.ENodebId == eNodebId && x.Pci == pci)?.SectorId ??
                                0
                        };
                        dateSpanStats.Add(stat);
                        _statRepository.Insert(stat);
                        _statRepository.SaveChanges();
                    }
                }
                
                begin = begin.AddDays(1);
            }
            return dateSpanStats;
        }

        public List<CellStatMysql> QueryDateSpanStatList(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var dateSpanStats = new List<CellStatMysql>();
            var cell = _cellRepository.GetBySectorId(eNodebId, sectorId);
            var pci = cell?.Pci ?? 0; 
            while (begin < end)
            {
                var oneDayMysqlStat = _statRepository.Get(eNodebId, sectorId, begin);
                if (oneDayMysqlStat != null)
                {
                    dateSpanStats.Add(oneDayMysqlStat);
                }
                else
                {
                    var oneDayStats = _repository.GetList(eNodebId, pci, begin);

                    if (oneDayStats.Any())
                    {
                        var stat = new CellStatMysql
                        {
                            Mod3Count = oneDayStats.Sum(x => x.Mod3Count),
                            Mod6Count = oneDayStats.Sum(x => x.Mod6Count),
                            MrCount = oneDayStats.Sum(x => x.MrCount),
                            OverCoverCount = oneDayStats.Sum(x => x.OverCoverCount),
                            PreciseCount = oneDayStats.Sum(x => x.PreciseCount),
                            WeakCoverCount = oneDayStats.Sum(x => x.WeakCoverCount),
                            CurrentDate = begin,
                            ENodebId = eNodebId,
                            Pci = pci,
                            SectorId = sectorId
                        };
                        dateSpanStats.Add(stat);
                        _statRepository.Insert(stat);
                        _statRepository.SaveChanges();
                    }
                }

                begin = begin.AddDays(1);
            }
            return dateSpanStats;
        }
    }
}
