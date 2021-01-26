
namespace PPB.Game
{
    class Paper:Hand
    {
        public Paper()
        {
            handtype = Handtype.Paper;

            outcomes[(int)Handtype.Rock] = Outcome.Win;
            outcomes[(int)Handtype.Scissors] = Outcome.Lose;
            outcomes[(int)Handtype.Paper] = Outcome.Draw;
            outcomes[(int)Handtype.Lizzard] = Outcome.Lose;
            outcomes[(int)Handtype.Vulcanian] = Outcome.Win;
        }
    }
}
