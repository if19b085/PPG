using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PPB.Game
{
    public class Game
    {
        public List<string> gameLog = new List<string>();
        public List<User> tournamentContestants = new List<User>();
        private int roundsPlayed = 0;

        //Intialize the different Hands
        Rock rock = new Rock();
        Paper paper = new Paper();
        Scissors scissors = new Scissors();
        Lizzard lizzard = new Lizzard();
        Vulcanian vulcanian = new Vulcanian();
       

        //Used to handle access to game lobby
        bool tournamentRunning = false;
        bool tournamentOver = false;
        bool hostPlayer = false;

        //Used to handle access thorugh multiple threads (=mutexes)
        static object singleHost = new object();
        static object singleAdd = new object();

        public Game()
        { 
        }

        public List<string> Battle(User user)
        {
            //game lobby
            lock(singleHost)
            {
                if (tournamentContestants.Count == 0)
                {
                    //We check if there are any contestants if not we start the procedure for the game host}
                    hostPlayer = true;
                }
                else if (tournamentContestants.Count != 0 && tournamentRunning == false)
                {
                    //If the contestant is not the host we just add him to the list of contestants and let him wait
                    hostPlayer = false;
                }
                else
                {
                    //If we are not the one to start the torunament (=host) and the tournament has already started we have nothing to do here -> kick him

                }
            }
            
            if (hostPlayer == true)
            {
                //Log who hosted the game.
                gameLog.Add(user.username + " has started a new game.\n");
                tournamentContestants.Add(user);
                Thread.Sleep(15000);
                tournamentRunning = true;

                //tournament
                do
                {
                    //rounds
                    //Log that the round has started
                    gameLog.Add("Round " + roundsPlayed+1 + "has started.\n");
                    Outcome battleOutcome;
                    for (int i = 0; i < tournamentContestants.Count; i++)
                    {
                        for (int j = i + 1; j == tournamentContestants.Count; j++)
                        {
                            battleOutcome = DetermineOutcome(tournamentContestants[i].set[roundsPlayed], tournamentContestants[j].set[roundsPlayed]);
                            if (battleOutcome == Outcome.Win)
                            {
                                tournamentContestants[i].BattleWon();
                                //Log who won
                                gameLog.Add(tournamentContestants[i].username + " with " + tournamentContestants[i].set[roundsPlayed]  + " won against " + tournamentContestants[j].username + " with " + tournamentContestants[j].set[roundsPlayed] + "\n");
                            }
                            else if (battleOutcome == Outcome.Lose)
                            {
                                 tournamentContestants[j].BattleWon();
                                //Log enemy won
                                gameLog.Add(tournamentContestants[j].username + " with " + tournamentContestants[j].set[roundsPlayed] + " won against " + tournamentContestants[i].username + " with " + tournamentContestants[i].set[roundsPlayed] + "\n");

                            }
                        }
                    }
                    roundsPlayed++;
                    SortByBattlePoints();
                    FindRoundWinner(tournamentContestants, roundsPlayed);
                } while (roundsPlayed < 5);

                SortByRoundPoints();
                FindWinner(tournamentContestants);
                tournamentOver = true;
            }
            else
            {
                lock (singleAdd)
                {
                    tournamentContestants.Add(user);
                    //Log who joined the game
                    gameLog.Add(user.username + " joined the game.\n");
                }
                //Player who are not the host go to sleep and wait till its over
                while (tournamentOver == false)
                {
                    Thread.Sleep(2000);
                }
            }
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
        public void FindRoundWinner(List<User> players , int roundsPlayed)
        {
            for (int i = 0; i < tournamentContestants.Count; i++)
            {
                //Check if the first player in sorted list is really the winner or ties
                if (tournamentContestants[i].battlePoints > tournamentContestants[i + 1].battlePoints && i == 0)
                {
                    tournamentContestants[i].RoundWon();
                    //Log who won th round
                    gameLog.Add(tournamentContestants[i].username + "won Round " + roundsPlayed+1 + "\n");

                }//Now that we have a predecessor we can check normally for the winner
                else if (tournamentContestants[i].battlePoints > tournamentContestants[i + 1].battlePoints && tournamentContestants[i].battlePoints != tournamentContestants[i - 1].battlePoints)
                {
                    tournamentContestants[i].RoundWon();
                    //Log who won the round
                    gameLog.Add(tournamentContestants[i].username + "won Round " + roundsPlayed + 1 + "\n");
                }
            }
        }

        public void FindWinner(List<User> players)
        {
            for (int i = 0; i < tournamentContestants.Count; i++)
            {
                //Check if the first player in sorted list is really the winner or ties
                if (tournamentContestants[i].roundPoints > tournamentContestants[i + 1].roundPoints && i == 0)
                {
                    tournamentContestants[i].GiveAdministrator();
                    //Log who won the game
                    gameLog.Add(tournamentContestants[i].username + "won the game \n");

                }//Now that we have a predecessor we can check normally for the winner
                else if (tournamentContestants[i].roundPoints > tournamentContestants[i + 1].roundPoints && tournamentContestants[i].roundPoints != tournamentContestants[i - 1].roundPoints)
                {
                    tournamentContestants[i].GiveAdministrator();
                    //Log who won the game
                    gameLog.Add(tournamentContestants[i].username + "won the game \n");
                }
            }
        }

        public void SortByBattlePoints()
        {
            tournamentContestants = tournamentContestants.OrderBy(player => player.battlePoints).ToList();
        }

        public void SortByRoundPoints()
        {
            tournamentContestants = tournamentContestants.OrderBy(player => player.roundPoints).ToList();
        }


    }
}
