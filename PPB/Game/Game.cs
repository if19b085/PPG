using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PPB.Game
{
    class Game
    {
        public List<string> gameLog = new List<string>();
        public List<User> tournamentContestants;
        private int roundsPlayed = 0;
        //
        private User winner;
        
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
            SortByBattlePoints();
            FindWinner(tournamentContestants);
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
        private Outcome DetermineOutcome(Handtype handtypeOne, Handtype handtypeTwo)
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
        private void FindWinner(List<User> players)
        {
            for (int i = 0; i < tournamentContestants.Count; i++)
            {
            //Check if the first player in sorted list is really the winner or ties
                if(tournamentContestants[i].battlePoints > tournamentContestants[i+1].battlePoints && i == 0)
                {
                    winner = tournamentContestants[i];
                }//Now that we have a predecessor we can check normally for the winner
                else if(tournamentContestants[i].battlePoints > tournamentContestants[i + 1].battlePoints && tournamentContestants[i].battlePoints != tournamentContestants[i - 1].battlePoints)
                {
                    winner = tournamentContestants[i];
                }
            }
        }

        private void SortByBattlePoints()   
        {
            tournamentContestants = tournamentContestants.OrderBy(player => player.battlePoints).ToList();
        }
        
    }
}
