using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using Lte.Evaluations.ViewModels;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    public static class AlarmsServiceTestQueries
    {
        public static void AssertBasicParameters(this AlarmView view, int eNodebId, string details)
        {
            Assert.AreEqual(view.ENodebId, eNodebId);
            Assert.AreEqual(view.Details, details);
        }

        public static void AssertPosition(this AlarmView view, string position, double duration)
        {
            Assert.AreEqual(view.Position, position);
            Assert.AreEqual(view.Duration, duration);
        }

        public static void AssertTypes(this AlarmView view, AlarmLevel level, AlarmCategory category, AlarmType type,
            string typeDescription)
        {
            Assert.AreEqual(view.AlarmTypeDescription, typeDescription);
            Assert.AreEqual(view.AlarmCategoryDescription, category.GetAlarmCategoryDescription());
            Assert.AreEqual(view.AlarmLevelDescription, level.GetAlarmLevelDescription());
        }
    }
}
