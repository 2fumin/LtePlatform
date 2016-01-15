using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.ExcelCsv;

namespace Lte.Evaluations.DataService
{
    public class InterferenceMatrixService
    {
        private readonly IInterferenceMatrixRepository _repository;
        private readonly ICellRepository _cellRepository;

        private static Stack<InterferenceMatrixStat> InterferenceMatrixStats { get; set; }

        public InterferenceMatrixService(IInterferenceMatrixRepository repository, ICellRepository cellRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
            if (InterferenceMatrixStats == null)
                InterferenceMatrixStats = new Stack<InterferenceMatrixStat>();
        }

        public void UploadInterferenceStats(string path)
        {
            var container = InterferenceMatrixCsv.ReadInterferenceMatrixCsvs(path);
            if (container == null) return;
            var time = container.RecordTime;
            var pcis =
                Mapper.Map<List<InterferenceMatrixCsv>, List<InterferenceMatrixPci>>(container.InterferenceMatrixCsvs);
            foreach (var matrixPci in pcis)
            {
                var cell = _cellRepository.GetByFrequency(matrixPci.ENodebId, matrixPci.Frequency);
                if (cell != null)
                {
                    var stat = Mapper.Map<InterferenceMatrixPci, InterferenceMatrixStat>(matrixPci);
                    stat.RecordTime = time;
                    stat.SectorId = cell.SectorId;
                    InterferenceMatrixStats.Push(stat);
                }
            }
        }
    }
}
