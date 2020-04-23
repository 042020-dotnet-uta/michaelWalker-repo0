using System;
using System.Collections.Generic;

namespace rps
{
    class Player
    {
        // define properties for player
        // player name
        public string playerName { get; set; }
        // player score
        public int score;
        // players hand
        public string hand;

        public Player()
        {
            score = 0;
            hand = "EMPTY";
        }
    }
}