
namespace PPB.Game
{
    class Lizzard:Hand
    {
        public Lizzard()
        {
            handtype = Handtype.Lizzard;

            outcomes[(int)Handtype.Rock] = Outcome.Lose;
            outcomes[(int)Handtype.Scissors] = Outcome.Lose;
            outcomes[(int)Handtype.Paper] = Outcome.Win;
            outcomes[(int)Handtype.Lizzard] = Outcome.Draw;
            outcomes[(int)Handtype.Vulcanian] = Outcome.Win;
        }
       
    }
}
