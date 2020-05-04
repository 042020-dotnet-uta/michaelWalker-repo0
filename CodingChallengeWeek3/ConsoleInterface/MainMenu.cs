using System;
using System.Collections.Generic;

using CodingChallengeWeek3.Utils;

namespace CodingChallengeWeek3.ConsoleInterface
{
  /// <summary>
  /// The entry point for the interface of this application.  The menu will provide
  /// a starting point for users by providing them with choices to interact with
  /// the application.
  /// </summary>
  public static class MainMenu
  {
    /// <summary>
    /// Determines whether the ConsoleApplication shall continue to run
    /// </summary>
    private static bool isRunning = true;

    /// <summary>
    /// Displays the main menu to the user and invokes them for input.
    /// </summary>
    public static void Show()
    {

      string choice;

      while (isRunning)
      {
        Console.WriteLine(
@"Please choose a selection:
1. IsEven test
2. Get multiplication table
3. Shuffle a list
0. Exit" + "\n"
        );

        choice = Console.ReadLine();
        Console.WriteLine("\n");

        switch (choice)
        {
          case "0":
            isRunning = false;
            break;
          case "1":
            AskIsEven();
            Console.WriteLine("\n");
            break;
          case "2":
            AskForMultiTable();
            Console.WriteLine("\n");
            break;
          case "3":
            AskForListsToShuffle();
            Console.WriteLine("\n");
            break;
        }
      }
    }

    /// <summary>
    /// Asks the user to input a number and will
    /// output to the user whether that number was true or false
    /// </summary>
    private static void AskIsEven()
    {
      string numToTest;
      int num;

      Console.WriteLine("Please enter a number to test:");

      numToTest = Console.ReadLine();

      bool canParse = Int32.TryParse(numToTest, out num);

      if (!canParse) // Validate input
      {
        Console.WriteLine("Your input was invalid.  Please try again...");
        return;
      }

      bool isEven = Utils.Logic.IsEven(num); // Test number
      Console.WriteLine($"{num} " + (isEven ? "is even" : "is not even")); // Print results
    }

    /// <summary>
    /// Asks the user for number n which will be used to print out a nxn
    /// multiplication table in the form of a list;
    /// </summary>
    private static void AskForMultiTable()
    {
      string numToMultiply;
      int num;

      Console.WriteLine("Please enter a number to test:");
      numToMultiply = Console.ReadLine();

      bool canParse = Int32.TryParse(numToMultiply, out num);

      if (!canParse) // Validate input
      {
        Console.WriteLine("Your input was invalid.  Please try again...");
        return;
      }

      List<string> multiTableList = Utils.Logic.MultiTable(num);

      Console.WriteLine(String.Join(" ", multiTableList));
    }

    /// <summary>
    /// This will ask the user to insert a set of two list that have
    /// 5 values each.  The console will then shuffle both of the lists into one
    /// and return the result to the user.
    /// </summary>
    private static void AskForListsToShuffle()
    {
      string listStr1;
      string listStr2;
      List<string> list1 = new List<string>();
      List<string> list2 = new List<string>();

      string resultStr;
      List<string> resultList = new List<string>();
      // Ask user for first and second comma seprated list
      // Validate each list
      Console.WriteLine("Enter first comma seperated list");
      listStr1 = Console.ReadLine();
      list1.AddRange(listStr1.Split(","));
      if (list1.Count != 5)
      {
        Console.WriteLine("Please enter 5 entries for list 1.  Returning ot main menu...");
        return;
      }

      Console.WriteLine("Enter second comma seperated list");
      listStr2 = Console.ReadLine();
      list2.AddRange(listStr2.Split(","));
      if (list2.Count != 5)
      {
        Console.WriteLine("Please enter 5 entries for list 2.  Returning ot main menu...");
        return;
      }

      // Format and print results
      resultList.AddRange((List<string>)Utils.Logic.Shuffle2(list1, list2));
      resultStr = "[" + String.Join(",", resultList) + "]";

      Console.WriteLine($"Here are both lists in randomized order: \n{resultStr}");
    }
  }
}