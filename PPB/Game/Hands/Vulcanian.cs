
namespace PPB.Game
{
    class Vulcanian:Hand
    {
        public Vulcanian()
        {
            handtype = Handtype.Vulcanian;

            outcomes[(int)Handtype.Rock] = Outcome.Win;
            outcomes[(int)Handtype.Scissors] = Outcome.Win;
            outcomes[(int)Handtype.Paper] = Outcome.Lose;
            outcomes[(int)Handtype.Lizzard] = Outcome.Lose;
            outcomes[(int)Handtype.Vulcanian] = Outcome.Draw;
        }
        
    }
}
