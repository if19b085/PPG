using System;
using System.Collections.Generic;
using System.Text;

namespace PPB.Game
{
    class Game
    {
        public List<string> gameLog = new List<string>();
        public List<User> tournamentContestant;
        private int roundsPlayed = 0;
        //Intialize the diffrent Hands
        Rock rock = new Rock();
        Paper paper = new Paper();
        Scissors scissors = new Scissors();
        Lizzard lizzard = new Lizzard();
        Vulcanian vulcanian = new Vulcanian();
        public Game(List<User> _tournamentContestant)
        {
            tournamentContestant = _tournamentContestant;
            Battle();
        }
        private void Battle()
        {
            //tournament
            do
            {
                //rounds

                roundsPlayed++;
            } while (roundsPlayed < 5);
        }

        public Outcome DetermineOutcome(Handtype handtypeOne, Handtype handtypeTwo)
        {
            switch (handtypeOne)
            {
                case Handtype.Rock:
                    return rock.outcomes[(int)handtypeTwo];
                case Handtype.Paper:
                    return paper.outcomes[(int)handtypeTwo];
                case Handtype.Scissors:
                    return scissors.outcomes[(int)handtypeTwo];
                case Handtype.Lizzard:
                    return lizzard.outcomes[(int)handtypeTwo];
                case Handtype.Vulcanian:
                    return vulcanian.outcomes[(int)handtypeTwo];
                default:
                    return Outcome.Draw;
            }
        }
    }
}
