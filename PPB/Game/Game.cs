using System;
using System.Collections.Generic;
using System.Text;

namespace PPB.Game
{
    class Game
    {
        public List<string> gameLog = new List<string>();
        public List<User> tournamentContestants;
        private int roundsPlayed = 0;
        //Intialize the diffrent Hands
        Rock rock = new Rock();
        Paper paper = new Paper();
        Scissors scissors = new Scissors();
        Lizzard lizzard = new Lizzard();
        Vulcanian vulcanian = new Vulcanian();
        public Game(List<User> _tournamentContestants)
        {
            tournamentContestants = _tournamentContestants;
            Battle();
        }
        private void Battle()
        {
            //tournament
            do
            {
                //rounds
                Outcome battleOutcome;
                for (int i = 0; i < tournamentContestants.Count; i++)
                {
                    for(int j = i+1; j == tournamentContestants.Count; j++)
                    {
                        battleOutcome = DetermineOutcome(tournamentContestants[i].set[roundsPlayed], tournamentContestants[j].set[roundsPlayed]);
                        if(battleOutcome == Outcome.Win)
                        {
                            tournamentContestants[i].BattleWon();
                        }
                        else if (battleOutcome == Outcome.Lose)
                        {
                            tournamentContestants[j].BattleWon();
                        }
                    }
                }
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
