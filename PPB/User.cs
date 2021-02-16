using System;
using System.Collections.Generic;
using System.Text;

namespace PPB
{
    public class User
    {
        public string username;
        //private int score;
        public bool admin = false;
        //Used for playing
        public Game.Handtype[] set = new Game.Handtype[5];
        public int battlePoints;
        public int roundPoints;
        //Used for playlist
        //private List<Media.MMC> library = new List<Media.MMC>();
        
        public User(string _username, string handtypes)
        {
            username = _username;
            SetCreator(handtypes);
        }
        //User for Game
        //gets an string like "VVRRS"
        public void SetCreator(string handtypes)
        {
            if (!string.IsNullOrEmpty(handtypes))
            {  
                if(handtypes.Length == 5)
                {
                    for (int i = 0; i < handtypes.Length; i++)
                    {
                        switch (handtypes[i])
                        {
                            case 'R':
                                set[i] = Game.Handtype.Rock;
                                break;
                            case 'P':
                                set[i] = Game.Handtype.Paper;
                                break;
                            case 'S':
                                set[i] = Game.Handtype.Scissors;
                                break;
                            case 'L':
                                set[i] = Game.Handtype.Lizzard;
                                break;
                            case 'V':
                                set[i] = Game.Handtype.Vulcanian;
                                break;
                            default:
                                //Give some feedback that a wrong handtype was entered
                                throw new InvalidOperationException("Wrong handtype entered at position: " + i);
                        }
                    }

                }
                else
                {
                    //Give some feedback that a wrong amount of handtypes was entered if != 5
                    throw new InvalidOperationException("Wrong amount of handtype entered.");
                }

            }
        }
       
        public void BattleWon()
        {
            battlePoints++;
        }

        public void RoundWon()
        {
            roundPoints++;
        }
        public void GiveAdministrator()
        {
            admin = true;
        }
    }
}
