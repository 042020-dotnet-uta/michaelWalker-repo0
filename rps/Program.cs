using System;
using System.Collections.Generic;

namespace rps
{
    class Program
    {
        Player boo = new Player();
        static void Main(string[] args)
        {
            Game rps = new Game();
            rps.StartGame();
        }
    }

    class Game
    {
        //Instantiate players
        Player player1 = new Player();
        Player player2 = new Player();

        //create variable to count number of rounds
        int rounds;
        int ties;

        // Starts the game.  Sets rounds and scores to 0.  Asks for players name.
        public void StartGame()
        {
            //assign players with scores of 0 and start at round 0
            rounds = 1;
            ties = 0;
            player1.score = 0;
            player2.score = 0;

            // ask for input of player1 and player 2's  name and store it their respective property
            Console.WriteLine("Enter first player name:");
            player1.playerName = Console.ReadLine();

            Console.WriteLine("Enter second player name:");
            player2.playerName = Console.ReadLine();


            Console.WriteLine($"Player1's name is:  {player1.playerName}");
            Console.WriteLine($"Player2's name is:  {player2.playerName}");


            // start while loop that will keep running until either player1 or player2's score value is => 2
            while (player1.score < 2 && player2.score < 2)
            {
                // Randomly choose the hand each player drew and store into temp their temp variable
                player1.hand = ChooseHand();
                player2.hand = ChooseHand();

                // print out result of the players, which hand they drew, which player won or state that there's a tie and what the
                // current round is
                Console.WriteLine($"Starting Round {rounds}");
                Console.WriteLine($"Player {player1.playerName} choose  {player1.hand}");
                Console.WriteLine($"Player {player2.playerName} choose  {player2.hand}");

                // compare
                compare();

                rounds++;
            }

            // finally, print the winner, their W-L ratio and their total amount of ties
            if (player1.score > player2.score)
            {
                Console.WriteLine($"{player1.playerName} wins {player1.score}-{player2.score} with {ties} ties");
            }
            else
            {
                Console.WriteLine($"{player2.playerName} wins {player2.score}-{player1.score} with {ties} ties");
            }


        }

        // Returns a random hand
        public string ChooseHand()
        {
            // create an array of strings that will hold the values of "Rock", "Paper", "Scissors" to compare
            // and decide winner

            // code modified from https://www.tutorialspoint.com/how-to-select-a-random-element-from-a-chash-list
            var random = new Random();
            var list = new List<string> { "rock", "paper", "scissors" };
            int index = random.Next(list.Count);
            return list[index];
        }

        // compare player hands and deterine winner or tie
        public void compare()
        {
            if (player1.hand == player2.hand)
            {
                ties++;
            }
            else if ((player1.hand == "rock" && player2.hand == "scissors") || (player1.hand == "scissors" && player2.hand == "paper") || (player1.hand == "paper" && player2.hand == "rock"))
            {
                player1.score++;
                Console.WriteLine($"Player {player1.playerName} wins.");
            }
            else
            {
                player2.score++;
                Console.WriteLine($"Player {player2.playerName} wins.");
            }
        }
    }

    class Player
    {
        // define properties for player
        // player name
        public string playerName;
        // player score
        public int score;
        // players hand
        public string hand = "EMPTY";
    }
}
