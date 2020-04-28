using System;
using System.Collections.Generic;

namespace rps
{
    class Player
    {
        // define properties for player
        // player name
        public string name { get; set; }
        // Player's score
        public int score;
        // Player's wins and losses
        public int wins { get; set; }
        public int losses { get; set; }
        // players hand
        public string hand;

        public Player()
        {
            // Initalize default values
            score = 0;
            hand = "EMPTY";
            wins = 0;
        }
    }
}