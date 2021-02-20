using NUnit.Framework;

namespace PPBTest
{
    [TestFixture]
    class OutcomeTest
    {
        PPB.Game.Game game = new PPB.Game.Game();
        [Test]
        public void OutcomeRockvsSciccors()
        {
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Scissors));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Scissors, PPB.Game.Handtype.Rock));
        }

        [Test]
        public void OutcomeRockvsLizzard()
        {
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Lizzard));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Rock));
        }

        [Test]
        public void OutcomePapervsRock()
        {
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Paper, PPB.Game.Handtype.Rock));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Paper));
        }

        [Test]
        public void OutcomePapervsSpock()
        {
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Paper, PPB.Game.Handtype.Vulcanian));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Vulcanian, PPB.Game.Handtype.Paper));
        }

        [Test]
        public void OutcomeSciccorsvsLizzard()
        {
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Scissors, PPB.Game.Handtype.Lizzard));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Scissors));
        }

        [Test]
        public void OutcomeLizzardvsPaper()
        {
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Paper));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Paper, PPB.Game.Handtype.Lizzard));
        }
        [Test]
        public void OutcomeLizzardvsVulcanian()
        {
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Vulcanian));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Vulcanian, PPB.Game.Handtype.Lizzard));
        }

        [Test]
        public void OutcomeVulcanianvsRock()
        {
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Vulcanian, PPB.Game.Handtype.Rock));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Vulcanian));
        }

        [Test]
        public void OutcomeVulcanianvsScissors()
        {
            PPB.Game.Game game = new PPB.Game.Game();
            Assert.AreEqual(PPB.Game.Outcome.Win, game.DetermineOutcome(PPB.Game.Handtype.Vulcanian, PPB.Game.Handtype.Scissors));
            Assert.AreEqual(PPB.Game.Outcome.Lose, game.DetermineOutcome(PPB.Game.Handtype.Scissors, PPB.Game.Handtype.Vulcanian));
        }

        [Test]
        public void OutcomeDraw()
        {
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Rock, PPB.Game.Handtype.Rock));
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Paper, PPB.Game.Handtype.Paper));
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Scissors, PPB.Game.Handtype.Scissors));
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Lizzard, PPB.Game.Handtype.Lizzard));
            Assert.AreEqual(PPB.Game.Outcome.Draw, game.DetermineOutcome(PPB.Game.Handtype.Vulcanian, PPB.Game.Handtype.Vulcanian));
        }
    }
}
