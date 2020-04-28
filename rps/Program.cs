using System;
using System.Collections.Generic;
using rps.Models;

namespace rps
{
  class Program
  {
    static void Main(string[] args)
    {
      Game rps = new Game();
      rps.StartGame();
    }
  }

}
