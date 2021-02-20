
using System.Collections.Generic;
using System.Linq;

namespace PPB.Game
{
    public class Game
    {
        public List<string> gameLog = new List<string>();
        public List<User> tournamentContestants = new List<User>();
        private int roundsPlayed = 1;

        //Intialize the different Hands
        public Rock rock = new Rock();
        public Paper paper = new Paper();
        public Scissors scissors = new Scissors();
        public Lizzard lizzard = new Lizzard();
        public Vulcanian vulcanian = new Vulcanian();


        //Used to handle access to game lobby
        public bool tournamentOver = false;


        //
        private Database db = new Database();
        //
        static object locki = new object();
        //For Testing
        public User winner;
        public Game()
        {
            
        }

        public List<string> Battle()
        {
            PointBet();
            lock (locki)
            {
                do
                {
                    //Log that the round has started
                       gameLog.Add("Round " + roundsPlayed + " has started.\n");
                    ClearBattlePoints();
                    Outcome battleOutcome;
                    for (int i = 0; i < tournamentContestants.Count; i++)
                    {
                        for (int j = i + 1; j < tournamentContestants.Count; j++)
                        {
                            battleOutcome = DetermineOutcome(tournamentContestants[i].set[roundsPlayed - 1], tournamentContestants[j].set[roundsPlayed - 1]);
                            if (battleOutcome == Outcome.Win)
                            {
                                tournamentContestants[i].BattleWon();
                                //Log who won
                                gameLog.Add(tournamentContestants[i].username + " with " + tournamentContestants[i].set[roundsPlayed - 1] + " won against " + tournamentContestants[j].username + " with " + tournamentContestants[j].set[roundsPlayed - 1] + "\n");
                            }
                            else if (battleOutcome == Outcome.Lose)
                            {
                                tournamentContestants[j].BattleWon();
                                //Log enemy won
                                gameLog.Add(tournamentContestants[j].username + " with " + tournamentContestants[j].set[roundsPlayed - 1] + " won against " + tournamentContestants[i].username + " with " + tournamentContestants[i].set[roundsPlayed - 1] + "\n");

                            }
                            else
                            {
                                gameLog.Add(tournamentContestants[j].username + " with " + tournamentContestants[j].set[roundsPlayed - 1] + " played a draw  against " + tournamentContestants[i].username + " with " + tournamentContestants[i].set[roundsPlayed - 1] + "\n");

                            }
                        }
                    }
                    SortByBattlePoints();
                    FindRoundWinner();
                    roundsPlayed++;

                } while (roundsPlayed <= 5);
            }

            SortByRoundPoints();
            FindWinner();
            tournamentOver = true;


            return gameLog;
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
        public void FindRoundWinner()
        {
            /* Find Winner gets an ordered List:
             * Criteria for being winner:
             * -I have to have the most point without draw
             * We search the ordered list from top to bottom an check if the following item is lower in points than me
             * an the previos item doesnt draw with me.
             * The upmost and downmost item need seperate cases because the do not have previos or following items.
            */
            int i = 0;
            for (; i + 1 < tournamentContestants.Count; i++)
            {
                if (i == 0 && tournamentContestants[i].battlePoints > tournamentContestants[i + 1].battlePoints)
                {
                    //Check if the first player in sorted list is really the winner or ties
                    tournamentContestants[i].RoundWon();
                    gameLog.Add(tournamentContestants[i].username + " won Round " + roundsPlayed + "\n");
                    return;
                }
                //Now that we have a predecessor we can check normally for the winner
                else if (tournamentContestants[i].battlePoints > tournamentContestants[i + 1].battlePoints && tournamentContestants[i].battlePoints != tournamentContestants[i - 1].battlePoints)
                {
                    tournamentContestants[i].RoundWon();
                    gameLog.Add(tournamentContestants[i].username + " won Round " + roundsPlayed + "\n");
                    return;
                }

            }
            //The last item is checked seperatedly after the for loop
            if (tournamentContestants[i].battlePoints != tournamentContestants[i-1].battlePoints)
            {
                tournamentContestants[i].RoundWon();
                gameLog.Add(tournamentContestants[i].username + " won Round " + roundsPlayed + "\n");
                return;

            }
            else
            {
                gameLog.Add("No Winner in Round " + roundsPlayed + "\n");
                return;
            }


        }

        public void FindWinner()
        {
            /* Find Winner gets an ordered List:
             * Criteria for being winner:
             * -I have to have the most point without draw
             * We earch the ordered list from top to bottom an check if the following item is lower in points than me
             * an the previos item doesnt draw with me.
             * The upmost and downmost item need seperate cases because they do not have previos or following items.
            */
            int i = 0;
            for (; i+1 < tournamentContestants.Count; i++)
            {
                //Check if the first player in sorted list is really the winner or ties
                if (i == 0 && tournamentContestants[i].roundPoints > tournamentContestants[i + 1].roundPoints)
                {
                    //If a new administrator is decided potential other administartors loose their status
                    db.TakeAdministrator();
                    tournamentContestants[i].GiveAdministrator();
                    gameLog.Add(tournamentContestants[i].username + " won the game \n");
                    winner = tournamentContestants[i];
                    return;
                }
                //Now that we have a predecessor we can check normally for the winner until the penultimate item
                else if (tournamentContestants[i].roundPoints > tournamentContestants[i + 1].roundPoints && tournamentContestants[i].roundPoints != tournamentContestants[i - 1].roundPoints)
                {
                    //If a new administrator is decided potential other administartors loose their status
                    db.TakeAdministrator();
                    tournamentContestants[i].GiveAdministrator();
                    gameLog.Add(tournamentContestants[i].username + " won the game \n");
                    winner = tournamentContestants[i];
                    return;
                }
            }
            //The last item is checked seperatedly after the for loop
            if (tournamentContestants[i].battlePoints != tournamentContestants[i-1].battlePoints)
            {
                //If a new administrator is decided potential other administartors loose their status
                db.TakeAdministrator();
                tournamentContestants[i].GiveAdministrator();
                gameLog.Add(tournamentContestants[i].username + " won the game \n");
                winner = tournamentContestants[i];
                return;

            }
            else
            {
                UndoBet();
                gameLog.Add("No winner in this  game. \n");
                return;
            }

        }

        private void SortByBattlePoints()
        {

            tournamentContestants = tournamentContestants.OrderBy(player => player.battlePoints).ToList();
            tournamentContestants.Reverse();
        }

        private void SortByRoundPoints()
        {
            tournamentContestants = tournamentContestants.OrderBy(player => player.roundPoints).ToList();
            tournamentContestants.Reverse();
        }

        private void ClearBattlePoints()
        {
            foreach (User player in tournamentContestants)
            {
                player.battlePoints = 0;
            }
        }
        private void PointBet()
        {
            /* At the beginning of every game every participants "bets" one point, the winner gains two points.
             * So in the end everyone lost one point and the winner gained one point.
             In case of a draw this needs to be undone. -> UndoBet()*/

            foreach(User player in tournamentContestants)
            {
                player.BetPoint();
            }
        }

        private void UndoBet()
        {
            /* Look at PointBet() defintion*/
            foreach(User player in tournamentContestants)
            {
                player.UndoBet();
            }

        }
    }
}
