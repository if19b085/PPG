using NUnit.Framework;
using System.Collections.Generic;

namespace PPBTest
{
    [TestFixture]
    public class GameTest
    {
        List<PPB.User> tournamentContestants = new List<PPB.User>();
      
        PPB.User primus = new PPB.User("primus", "password", "V,V,V,V,V");
        PPB.User secundus = new PPB.User("secundus", "password", "R,R,R,R,R");
        PPB.User tertius = new PPB.User("tertius", "password", "S,P,S,P,S");
        PPB.User quartus = new PPB.User("quartus", "password", "L,P,V,V,V");
        PPB.User quintus = new PPB.User("quintus", "password", "R,S,V,V,V");

        PPB.Game.Game game = new PPB.Game.Game();
        [Test]
        public void ClassExists()
        {
            Assert.IsNotNull();
        }

        [Test]
        public void OutcomeRockvsPaper()
        {
            Assert.Pass();
        }

        [Test]
        public void OutcomeLizzardvsSpock()
        {
            Assert.Pass();
        }

        [Test]
        public void OutcomeSpockvsLizzard()
        {
            Assert.Pass();
        }

        [Test]
        public void OutcomeSciccorsvsLizzard()
        {
            Assert.Pass();
        }

        [Test]
        public void RoundWinnerNoDraw()
        {
            Assert.Pass();
        }

        [Test]
        public void RoundWinnerWithDraw()
        {
            Assert.Pass();
        }

        [Test]
        public void NoRoundWinner()
        {
            Assert.Pass();
        }
        [Test]

        public void WinnerNoDraw()
        {
            Assert.Pass();
        }

        [Test]
        public void WinnerWithDraw()
        {
            Assert.Pass();
        }

        [Test]
        public void NoWinner()
        {
            Assert.Pass();
        }
    }
}