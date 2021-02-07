using NUnit.Framework;
using System.Collections.Generic;

namespace PPBTest
{
    [TestFixture]
    public class GameTest
    {
        //public List<PPB.User> tournamentContestants = new List<PPB.User>();
      
        //Standard Player
        PPB.User primus = new PPB.User("primus", "password", "VVVVV");
        PPB.User secundus = new PPB.User("secundus", "password", "RRRRR");
        PPB.User tertius = new PPB.User("tertius", "password", "SPSPS");
        PPB.User quartus = new PPB.User("quartus", "password", "LPVVV");
        PPB.User quintus = new PPB.User("quintus", "password", "RSVVV");


        //Enemies of primus for Round Winner with one Draw
        PPB.User primusDraw = new PPB.User("primus", "password", "VVVVV");
        PPB.User undeservedButWinning = new PPB.User("primus", "password", "RRRRR");


        PPB.Game.Game game;
        [Test]
        public void ClassExists()
        {
            List<PPB.User> tournamentContestants = new List<PPB.User>();

            tournamentContestants.Add(primus);
            tournamentContestants.Add(secundus);
            tournamentContestants.Add(tertius);
            tournamentContestants.Add(quartus);
            tournamentContestants.Add(quintus);

            tournamentContestants.Add(primusDraw);
            tournamentContestants.Add(undeservedButWinning);

            game = new PPB.Game.Game();

            Assert.IsNotNull(game);
        }

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
        }
    }
}