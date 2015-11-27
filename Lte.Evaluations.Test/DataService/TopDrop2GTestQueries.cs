using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    public static class TopDrop2GTestQueries
    {
        public static void AssertEqual(this TopDrop2GCellView view, byte sectorId, int drops, int assignmentSuccess,
            string lteName, string cdmaName)
        {
            Assert.AreEqual(view.SectorId, sectorId);
            Assert.AreEqual(view.Drops, drops);
            Assert.AreEqual(view.TrafficAssignmentSuccess, assignmentSuccess);
            Assert.AreEqual(view.LteName, lteName);
            Assert.AreEqual(view.CdmaName, cdmaName);
        }
    }
}
