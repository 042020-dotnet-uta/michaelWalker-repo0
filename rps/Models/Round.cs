using System;

namespace rps
{
    /// <summary>Round class - records the result of the round</summary>
    class Round
    {
        // The rounds current winner
        public Player winner { get; set; }
        public Player loser { get; set; }
        public Player tieBreaker1 { get; set; }
        public Player tieBreaker2 { get; set; }
        // The number of the current round
        public int roundIndex { get; set; }

        public bool isTie;


        public Round()
        {
            // Assume there is no tie unless otherwise specified
            isTie = false;
            tieBreaker1 = null;
            tieBreaker2 = null;

            winner = null;
            loser = null;
            // Round hasn't been recorded yet
            roundIndex = -1;
        }

        public void printResults()
        {
            // TODO: Determine scenario where printing wouldn't work and throw error explaining why
            // if((winner == null || loser == null) && !isTie)
            // {
            //     throw new System.ApplicationException("Something went wrong.  One or more of the players are missing");
            // }

            Console.WriteLine($"Is tie? {isTie}");
            // String to declare the winner or if there was a tie
            string winOrTieResult = !isTie ? $"Winner - {winner.name}" : "Tie!";
            // String to determine what hands were delt and by who
            string handsDelt;

            if (!isTie)
            {
                winOrTieResult = $"Winner - {winner.name}";
                handsDelt = $"{winner.name} played {winner.hand}, {loser.name} played {loser.hand}";
            }
            else
            {
                winOrTieResult = "Tie!";
                handsDelt = $"{tieBreaker1.name} played {tieBreaker1.hand}, {tieBreaker2.name} played {tieBreaker2.hand}";
            }

            Console.WriteLine($"Round {roundIndex}: {winOrTieResult} | {handsDelt}");
        }
    }
}