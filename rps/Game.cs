using System;
using System.Collections.Generic;

namespace rps
{
    class Game
    {
        //Instantiate players
        private Player player1;
        private Player player2;

        //create variable to count number of rounds
        private int rounds;
        private List<Round> Rounds { get; set; }
        private int ties;

        // Dictionary keys for winner and loser
        enum PlayerResults
        {
            Winner,
            Loser
        }
        // Variable to store the results of who and and who lost for the current round
        Dictionary<PlayerResults, Player> currentResults;


        public Game()
        {
            // Initialize palyers
            player1 = new Player();
            player2 = new Player();

            // initialize Rounds records
            Rounds = new List<Round>();

            // Register dictionary with keys for winner and loser
            // will add Player values to pair with keys once a round has ended
            currentResults = new Dictionary<PlayerResults, Player>();
            currentResults.Add(PlayerResults.Winner, null);
            currentResults.Add(PlayerResults.Loser, null);

            //assign players with scores of 0 and start at round 1
            rounds = 1;
            ties = 0;
        }
        // Starts the game.  Sets rounds and scores to 0.  Asks for players name.
        public void StartGame()
        {
            // ask for input of player1 and player 2's  name and store it their respective property
            Console.WriteLine("Enter first player name:");
            player1.name = Console.ReadLine();

            Console.WriteLine("Enter second player name:");
            player2.name = Console.ReadLine();


            Console.WriteLine($"Player1's name is:  {player1.name}");
            Console.WriteLine($"Player2's name is:  {player2.name}");


            // start while loop that will keep running until either player1 or player2's score value is => 2
            while (player1.score < 2 && player2.score < 2)
            {
                // Randomly choose the hand each player drew and store into temp their temp variable
                player1.hand = ChooseHand();
                player2.hand = ChooseHand();

                // compare
                compare();

                // Record the data of the current round
                Round currentRound = new Round();
                currentRound.roundIndex = rounds;
                // If there is no winner, mark the round as a tie
                if (currentResults[0] == null)
                {
                    currentRound.isTie = true;
                    currentRound.tieBreaker1 = player1;
                    currentRound.tieBreaker2 = player2;
                }
                else
                {
                    // Record the outcome to the currentRound
                    currentRound.winner = currentResults[PlayerResults.Winner];
                    currentRound.loser = currentResults[PlayerResults.Loser];
                }

                Rounds.Add(currentRound);

                rounds++;
            }

            // Print out the results of each round
            foreach (Round round in Rounds)
            {
                round.printResults();
            }

            // finally, print the winner, their W-L ratio and their total amount of ties
            if (player1.score > player2.score)
            {
                Console.WriteLine($"{player1.name} wins {player1.score}-{player2.score} with {ties} ties");
            }
            else
            {
                Console.WriteLine($"{player2.name} wins {player2.score}-{player1.score} with {ties} ties");
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
            // If both hands are the same, results is a tie
            if (player1.hand == player2.hand)
            {
                ties++;

                // THere are no winners
                currentResults[PlayerResults.Winner] = null;
                currentResults[PlayerResults.Loser] = null;

            }
            // Catch every case where player 1 would win
            // if player 1 does not win, safe to assume player2 is the winner and is given the point
            else if ((player1.hand == "rock" && player2.hand == "scissors") || (player1.hand == "scissors" && player2.hand == "paper") || (player1.hand == "paper" && player2.hand == "rock"))
            {
                // Score for 1
                player1.score++;

                // Record who and and lost
                currentResults[PlayerResults.Winner] = player1;
                currentResults[PlayerResults.Loser] = player2;
            }
            else
            {
                // Score for 2
                player2.score++;

                // Record who and and lost
                currentResults[PlayerResults.Winner] = player2;
                currentResults[PlayerResults.Loser] = player1;
            }
        }
    }

}