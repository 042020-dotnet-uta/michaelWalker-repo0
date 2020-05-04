using System;
using System.Collections.Generic;

namespace CodingChallengeWeek3.Utils
{
  /// <summary>
  /// The logic class provides several methods for the ConsoleInterface
  /// that will handle reading, manipulating and returning data based on the
  /// User's input.
  /// </summary>
  public static class Logic
  {

    /// <summary>
    /// Takes an integer and determines whether the number
    /// is or is not even.
    /// </summary>
    /// <param name="num">The number to test for it's eveness</param>
    /// <returns>A boolean value that will return true if num is even</returns>
    public static bool IsEven(int num)
    {
      return num % 2 == 0;
    }

    /// <summary>
    /// Returns an array of strings that will hold mathmatical statements of an entire
    /// multiplication table ranging from 1 to the provided num (A multiplication table 
    /// that is numxnum)
    /// </summary>
    /// <param name="num">The number to base the multiplication table off of</param>
    /// <returns>A List of strings with each operation of the multiplication table</returns>
    public static List<string> MultiTable(int num)
    {
      List<string> tempTable = new List<string>();
      for (var i = 1; i <= num; i++)
      {
        for (var j = 1; j <= num; j++)
        {
          tempTable.Add($"{i}x{j}={i * j}");
        }
      }

      return tempTable;
    }

    /// <summary>
    /// Takes two lists and combines them into one shuffled list
    /// </summary>
    /// <param name="list1">The first list to combine and shuffle</param>
    /// <param name="list2">The second list to combine and shuffle</param>
    /// <returns>A shuffled List of the two given lists.</returns>
    public static List<string> Shuffle2(IEnumerable<string> list1, IEnumerable<string> list2)
    {
      // Add both lists to temp
      List<string> tempList = new List<string>();
      tempList.AddRange(list1);
      tempList.AddRange(list2);

      // Shuffle list
      // Algorithm taken from https://stackoverflow.com/a/1262619
      // Usage of Fisher-Yates shuffle
      int n = tempList.Count;
      Random rng = new Random();
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        string value = tempList[k];
        tempList[k] = tempList[n];
        tempList[n] = value;
      }

      return tempList;
    }
  }
}