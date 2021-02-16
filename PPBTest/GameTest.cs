using NUnit.Framework;
using System.Collections.Generic;



namespace PPBTest
{
    [TestFixture]
    public class GameTest
    {
        PPB.User primus = new PPB.User("username", "password");
        

        [Test]
        public void ClassExists()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.IsNotNull(primus);
        }

        /*
        [Test]
        public void OutcomeRockvsPaper()
        {
            Assert.Equals(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Paper));
        }

        [Test]
        public void OutcomeLizzardvsVulcanian()
        {
            Assert.Equals(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Vulcanian));
        }

        [Test]
        public void OutcomePapervsLizzard()
        {
            Assert.Equals(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Paper, PPB.Game.Handtype.Lizzard));
        }

        [Test]
        public void OutcomeSciccorsvsLizzard()
        {
            Assert.Equals(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Scissors, PPB.Game.Handtype.Lizzard));
        }

        [Test]
        public void OutcomeDraw()
        {
            Assert.Equals(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Rock));
            Assert.Equals(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Paper, PPB.Game.Handtype.Paper));
            Assert.Equals(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Scissors, PPB.Game.Handtype.Scissors));
            Assert.Equals(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Lizzard));
            Assert.Equals(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Vulcanian, PPB.Game.Handtype.Vulcanian));
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
        }*/
        
    }

}