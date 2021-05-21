using System;
using System.Collections.Generic;
using System.Text;

namespace BoxeSpil_Console
{
    class Gamecontroller
    {
        public int runderMaxAntal { get; set; }
        public int rundeAktuelle { get; set; }


        public bool CheckWinner(Boxer p1, Boxer p2)
        {
            if (p1.stagging <= 0)
            {
                Console.WriteLine($"--------------------------- {p2.name} has won!! ---------------------------");
                return true;
            }
            else if (p2.stagging <= 0)
            {
                Console.WriteLine($"--------------------------- {p1.name} has won!! ---------------------------");
                return true;
            }
            return false;
        }

        public void GameEnd(Boxer p1, Boxer p2)
        {
            if (p1.stagging < p2.stagging)
            {
                Console.WriteLine($"--------------------------- {p2.name} has won!! ---------------------------");
            }
            else
            {
                Console.WriteLine($"--------------------------- {p1.name} has won!! ---------------------------");
            }
        }
    }

    
}
