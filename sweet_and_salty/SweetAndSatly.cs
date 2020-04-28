using System;

namespace sweet_and_salty
{

  /// <summary>
  /// The sweet and salty class will count from 0 to 100.  
  /// Whenever a number is a multiple of 3 it will print out "sweet" to the console.
  /// Whenever there's a multiple of 5, "salty," will be printed.
  /// Finally when conditions for both "sweet" and "salty" are met, the console will print out "sweet'nSalty"
  /// When counting ends, the number of "sweet", "salty" and "sweet'nSalty" had occured will be print to the console
  /// </summary>
  class SweetAndSalty
  {
    // Delcare variables to store the results of how many times each sweetnsalty conditions occur
    /// <summary>
    /// The total number of "sweet"s
    /// </summary>
    private byte numSweet;
    /// <summary>
    /// The total number of "salty"s
    /// </summary>
    private byte numSalty;
    /// <summary>
    /// The total number of "sweet'nSalty"s
    /// </summary>
    private byte numSweetNSalty;

    public SweetAndSalty()
    {
      // Initialize the counters
      numSweet = 0;
      numSalty = 0;
      numSweetNSalty = 0;
    }


    /// <summary>
    /// This will begin the counting checking for each sweetnsalty condition.
    /// At the end the results will be printed out.
    /// </summary>
    public void Run()
    {
      // Begin counting from 1 to 100
      for (var i = 0; i < 100; i++)
      {

        if (i % 3 == 0 && i % 5 == 0) // Check if number is "sweet'nSatly"
        {
          Console.WriteLine("sweet'nSalty");
          numSweetNSalty++;
        }
        else if (i % 3 == 0) // Check if number is "sweet"
        {
          Console.WriteLine("sweet");
          numSweet++;
        }
        else if (i % 5 == 0) // Check if number is "salty"
        {
          Console.WriteLine("salty");
          numSalty++;
        }
      }

      // Once the counting ends, print the results
      Console.WriteLine($"Number of Sweets: {numSweet} | Number of Saltys: {numSalty} | NUmber of Sweet-n-Saltys: {numSweetNSalty}");
    }
  }

}