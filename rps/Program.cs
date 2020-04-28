using System;
using System.Collections.Generic;
using rps.Models;

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

}
