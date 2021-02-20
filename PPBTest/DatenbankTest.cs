using NUnit.Framework;
using System.Collections.Generic;


namespace PPBTest
{
    [TestFixture]
    class DatenbankTest
    {
        PPB.Database db = new PPB.Database();
        [Test]
        public void ScoreboardTest()
        {
            string scoreboard = "";
            scoreboard = db.Scoreboard();
            Assert.IsNotNull(scoreboard);
        }
        [Test]
        public void GetStatsTest()
        {
           //einmalig verwendet, bleibt als Testuser in der Datenbank
           //db.AddUser("testi", "mctestface");
            int stat = db.GetStats("testi");
            Assert.AreEqual(stat, 100);
        }

        [Test]
        public void LoginNameFails()
        {
            bool fail = db.Login("fail", "mctestface");
            Assert.IsFalse(fail);
        }
        [Test]
        public void LoginPasswordFails()
        {
            bool fail = db.Login("testi", "nope");
            Assert.IsFalse(fail);
        }
        [Test]
        public void LoginBothFails()
        {
            bool fail = db.Login("fail", "nope");
            Assert.IsFalse(fail);
        }

    }
}
