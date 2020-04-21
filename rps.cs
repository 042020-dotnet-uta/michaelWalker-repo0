/*
    Rock Paper Shotgun 
    
    A small game that will let 2 players play a game of RPS. Choices will be chosen randomly for each player.
    Best 2 out 3 wins!
    
    @Authors - Kingsley Ononachi (chijekwu), David Sawyer (d-sawyer), Michael Walker (Mohcka)
*/
            
using System;  

namespace rps
{
    class Game
    {
        // Instantiate players
        Player player1;
        Player player2;
        
        //create variable to count number of rounds
        int rounds;
        static void Main(string[] args)
        {
            //assign players with scores of 0
            
            // ask for input of player1 and player 2's  name and store it their respective property

            // create an array of strings that will hold the values of "Rock", "Paper", "Scissors" to compare
            // and decide winner

            // start while loop that will keep running until either player1 or player2's score value is => 2
            
            // create temp variables for player1 and player2 that will store the hand they drew (Rock, Paper, Scissor)
            
            // Randomly choose the hand each player drew and store into temp their temp variable

            // compare the player's value, check if there's a tie, if so give no score, increment round, restart the loop
            // else give point to winner and store point to Player's score property
            
            // print out result of the players, which hand they drew, which player won or state that there's a tie and what the
            // current round is
            
            // end of loop logic, at this point a player should have 2 points


            // finally, print the winner, their W-L ratio and their total amount of ties
            
            // endgame
        }
    }
    
    class Player
    {
        // define properties for player
        // player name
        // player score
    }
}