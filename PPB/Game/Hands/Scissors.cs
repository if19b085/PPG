
namespace PPB.Game
{
    class Scissors:Hand
    {
        public Scissors()
        {
            handtype = Handtype.Scissors;

            outcomes[(int)Handtype.Rock] = Outcome.Lose;
            outcomes[(int)Handtype.Scissors] = Outcome.Draw;
            outcomes[(int)Handtype.Paper] = Outcome.Win;
            outcomes[(int)Handtype.Lizzard] = Outcome.Win;
            outcomes[(int)Handtype.Vulcanian] = Outcome.Lose;
        }
    }
}
