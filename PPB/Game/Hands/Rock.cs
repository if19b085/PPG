namespace PPB.Game
{
    public class Rock:Hand
    {
        public Rock()
        {
            handtype = Handtype.Rock;
            
            outcomes[(int)Handtype.Rock] = Outcome.Draw;
            outcomes[(int)Handtype.Scissors] = Outcome.Win;
            outcomes[(int)Handtype.Paper] = Outcome.Lose;
            outcomes[(int)Handtype.Lizzard] = Outcome.Win;
            outcomes[(int)Handtype.Vulcanian] = Outcome.Lose;
        }
    }
}
