namespace PPB.Game
{
    public class Rock:Hand
    {
        public Rock()
        {
            handtype = Handtype.Rock;
            
            outcomes[(int)Handtype.Rock] = Outcome.Draw;
            outcomes[(int)Handtype.Scissors] = Outcome.Lose;
            outcomes[(int)Handtype.Paper] = Outcome.Win;
            outcomes[(int)Handtype.Lizzard] = Outcome.Win;
            outcomes[(int)Handtype.Vulcanian] = Outcome.Lose;
        }
    }
}
