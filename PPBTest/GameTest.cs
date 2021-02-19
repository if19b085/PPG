using NUnit.Framework;
using System.Collections.Generic;



namespace PPBTest
{
    [TestFixture]
    public class GameTest
    {
        
        PPB.User quartus = new PPB.User("tertius", "SSSSS");
        PPB.User quintus = new PPB.User("quintus", "SSSSS");


        [Test]
        public void ClassExists()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.IsNotNull(game);
        }

        
        [Test]
        public void OutcomeRockvsPaper()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Paper));
        }

        [Test]
        public void OutcomeLizzardvsVulcanian()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Vulcanian));
        }

        [Test]
        public void OutcomePapervsLizzard()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Paper, PPB.Game.Handtype.Lizzard));
        }

        [Test]
        public void OutcomeSciccorsvsLizzard()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Scissors, PPB.Game.Handtype.Lizzard));
        }

        [Test]
        public void OutcomeDraw()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Rock));
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Paper, PPB.Game.Handtype.Paper));
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Scissors, PPB.Game.Handtype.Scissors));
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Lizzard));
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Vulcanian, PPB.Game.Handtype.Vulcanian));
        }

        
        [Test]

        public void WinnerNoDraw()
        {
            /*Testing a "normal" game where one participants has a hand which beats all other hands*/
            List<PPB.User> users = new List<PPB.User>();
            PPB.User primus = new PPB.User("primus", "SSSSS");
            users.Add(primus);
            PPB.User secundus = new PPB.User("secundus", "SSSSS");
            users.Add(secundus);
            PPB.User tertius = new PPB.User("tertius", "RRRRR");
            users.Add(tertius);
            PPB.Game.Game game = new PPB.Game.Game();
            game.tournamentContestants = users;

            Assert.AreEqual(tertius, game.winner);
        }

        [Test]
        public void WinnerWithDraw()
        {
            /*Testing a game where a participant wins because the two better hands draw*/
            List<PPB.User> users = new List<PPB.User>();
            PPB.User primus = new PPB.User("primus", "SSSSS");
            users.Add(primus);
            PPB.User secundus = new PPB.User("secundus", "SSSSS");
            users.Add(secundus);
            PPB.User tertius = new PPB.User("tertius", "PPPPP");
            users.Add(tertius);
            PPB.Game.Game game = new PPB.Game.Game();
            game.tournamentContestants = users;

            Assert.AreEqual(tertius, game.winner);
        }

        [Test]
        public void NoWinner()
        {
            /* Testing a game where every participant plays a draw*/
            List<PPB.User> users = new List<PPB.User>();
            PPB.User primus = new PPB.User("primus", "SSSSS");
            users.Add(primus);
            PPB.User secundus = new PPB.User("secundus", "SSSSS");
            users.Add(secundus);
            PPB.User tertius = new PPB.User("tertius", "SSSSS");
            users.Add(tertius);
            PPB.Game.Game game = new PPB.Game.Game();
            game.tournamentContestants = users;

            Assert.IsNull(game.winner);
        }
        
    }

}