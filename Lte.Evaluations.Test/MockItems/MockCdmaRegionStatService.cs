﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities;
using Moq;

namespace Lte.Evaluations.Test.MockItems
{
    public static class MockCdmaRegionStatService
    {
        public static void MockOperation(this Mock<ICdmaRegionStatRepository> repository)
        {
            repository.Setup(x => x.GetAllList(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) =>
                        repository.Object.GetAll().Where(x => x.StatDate >= begin && x.StatDate < end).ToList());

            repository.Setup(x => x.GetAllListAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime>(
                    (begin, end) =>
                        Task.Run(
                            () =>
                                repository.Object.GetAll().Where(x => x.StatDate >= begin && x.StatDate < end).ToList()));

            repository.Setup(x => x.Import(It.IsAny<IEnumerable<CdmaRegionStatExcel>>()))
                .Returns<IEnumerable<CdmaRegionStatExcel>>(stats => stats.Count());
        }
    }
}
