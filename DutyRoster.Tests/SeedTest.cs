using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DutyRoster.Tests
{
    [TestClass]
    public class SeedTest
    {
        [TestMethod]
        public void SeedClubs()
        {
            using (Data.RosterContext context = new Data.RosterContext())
            {
                Data.Utils.DBSeeder.SeedClubs(context);
            }
        }

        [TestMethod]
        public void SeedIdentity()
        {
            using (Data.RosterContext context = new Data.RosterContext())
            {
                Data.Utils.DBSeeder.SeedIdentities(context);
            }
        }
    }
}
