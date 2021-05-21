using System;
using System.Collections.Generic;
using System.Text;

namespace BoxeSpil_Console
{
    class Boxer
    {
        int fighterNumber = 0;
        public string name { get;  set; }
        public int stagging = 1000;
        public int staggerpunch = 0;
        Random rnd = new Random();
        public int move { get; set; }

        public Boxer()
        {
            fighterNumber++;
        }

        public string actionMove()
        {
            string attack = "";
            switch (move)
            {
                case 1:
                    attack = "jab";
                    staggerpunch = rnd.Next(20, 30);
                    break;
                case 2:
                    attack = "right hook, that must have hurt";
                    staggerpunch = rnd.Next(10, 60);
                    break;
                case 3:
                    attack = "blocked";
                    stagging += rnd.Next(2, 20);
                    staggerpunch = 0;
                    break;
                case 4:
                    attack = "Uppercut, the humiliation!!";
                    staggerpunch = 400;
                    break;
            }
            return attack;
        }

        public void fallroll()
        {
            int fallroll = rnd.Next(0, 1000);
            if(stagging <= fallroll)
            {
                Console.WriteLine($"{name} has fallen, will he get back up?");
                if(stagging <= rnd.Next(50, 950))
                {
                    Console.WriteLine($"{name} can't get up!!");
                    stagging = 0;
                } else
                {
                    Console.WriteLine("AND HE'S BACK UP!!");
                }
            }

        }


       
    }
}
