using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using NUnit.Framework;

namespace Lte.Evaluations.Test.DataService
{
    public static class College3GTestQueries
    {
        public static void AssertUsers(this College3GTestView view, int collegeId, int users)
        {
            Assert.AreEqual(view.CollegeName, "college-" + collegeId);
            Assert.AreEqual(view.AccessUsers, users);
        }

        public static void AssertUsers(this College3GTestResults result, int users)
        {
            Assert.AreEqual(result.AccessUsers, users);
        }
    }
}
