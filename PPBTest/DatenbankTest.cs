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

    }
}
