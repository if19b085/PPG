using NUnit.Framework;
using System.Collections.Generic;



namespace PPBTest
{
    [TestFixture]
    public class GameTest
    {

        [Test]
        public void ClassExists()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.IsNotNull(game);
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
            game.Battle();
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
            game.Battle();
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
            game.Battle();
            Assert.IsNull(game.winner);
        }
        
    }

}