using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LtePlatform.Tests.IdentityUser
{
    [TestFixture]
    public class IdentityUserBasicTest
    {
        [Test]
        public void Test_Contruction()
        {
            var user = new Microsoft.AspNet.Identity.EntityFramework.IdentityUser("aaa");
            Assert.AreEqual(user.UserName, "aaa");
        }
    }
}
