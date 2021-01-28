using System;
using System.Collections.Generic;
using System.Text;

namespace PPB
{
    public class User
    {
        public string username;
        public string password;
        //private int score;
        //private bool admin;
        //Used for playing
        public List<Game.Handtype> set;
        int battlePoints;
        int roundPoints;
        //Used for playlist
        //private List<Media.MMC> library = new List<Media.MMC>();
        
        public User(string _username, string _password,  string _handtypes)
        {
            username = _username;
            password = _password;
            SetCreator(_handtypes);
        }
        //User for Game
        //gets an string like "V,V,R,R,S"
        private void SetCreator(string handtypes)
        {
             set = new List<Game.Handtype>();

            if (!string.IsNullOrEmpty(handtypes))
            {
                string[] hand = handtypes.Split(",");
                
                if(hand.Length == 5)
                {
                    for (int i = 0; i < hand.Length; i++)
                    {
                        switch (hand[i])
                        {
                            case "R":
                                set[i] = Game.Handtype.Rock;
                                break;
                            case "P":
                                set[i] = Game.Handtype.Paper;
                                break;
                            case "S":
                                set[i] = Game.Handtype.Scissors;
                                break;
                            case "L":
                                set[i] = Game.Handtype.Lizzard;
                                break;
                            case "V":
                                set[i] = Game.Handtype.Vulcanian;
                                break;
                            default:
                                //Give some server feedback that a wrong handtype was entered
                                Console.WriteLine("Wrong handtype entered at position: " + i);
                                break;
                        }
                    }

                }
                else
                {
                    //Give some server feedback that a wrong amount of handtypes was entered if != 5
                    Console.WriteLine("Wrong amount of handtype entered.");
                }

            }
        }
       
        public void BattleWon()
        {
            battlePoints++;
        }
    }
}
