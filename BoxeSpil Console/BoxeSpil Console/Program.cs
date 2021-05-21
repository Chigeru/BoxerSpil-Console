using System;

namespace BoxeSpil_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Gamecontroller gamecontroller = new Gamecontroller();
            Boxer player1 = new Boxer();
            Boxer player2 = new Boxer();
            Random rnd = new Random();
            string players;
            bool earlyEnding = false;

            Console.WriteLine("Will you be playing with a friend tonight, or shall i find a volunteer? (y/n)");
            players = Console.ReadLine();
            Console.WriteLine("What is your name?");
            player1.name = StringMinLengthChecker(Console.ReadLine());

            // Player 2 or NPC
            if(players == "y")
            {
                Console.WriteLine("What is the name of your opponent?");
                player2.name = StringMinLengthChecker(Console.ReadLine());
            }
            else
            {
                player2.name = "Volenteer";
            }

            // Moves pr. round
            Console.WriteLine("how many rounds will you be doing tonight?");
            gamecontroller.runderMaxAntal = StringToIntChecker(Console.ReadLine());


            // Random storytext & info
            Console.WriteLine($"\nWelcome to the boxing match of the year, here tonight we present to you the most brutal fight you'll ever see\n" +
                $"in the right corner we have {player1.name} who is undefeated for the last 3 seasons. \n" +
                $"and in the left corner we have our challenger who's *ahem*........ {player2.name} who thinks he \ncan take down our champion");
            Console.WriteLine("(there's 2 kind of attacks og 1 defend.) \n1 = jab \n2 = uppercut \n3 = defend\n");

            // Fighting
            int i = 1;
            while(i <= 3 && earlyEnding == false)
            {
                Console.WriteLine($"--------------------------- round {i} start! ---------------------------");
                gamecontroller.rundeAktuelle = 1;
                while (gamecontroller.rundeAktuelle <= gamecontroller.runderMaxAntal && earlyEnding == false)
                {
                    Console.WriteLine($"\nit's {player1.name}'s turn to pull the punch.. what will his next move be?");
                    
                    player1.move = StringToIntChecker(Console.ReadLine());
                    if(players == "y")
                    {
                        Console.WriteLine($"\nit's {player2.name}'s turn to pull the punch.. what will his next move be?");
                        player2.move = StringToIntChecker(Console.ReadLine());
                    } else
                    {
                        player2.move = rnd.Next(1, 4);
                    }

                    if(player1.move == 3 || player2.move == 3)
                    {
                        moveBlocker();
                    } else
                    {
                        moveAttack(player1, player2);
                        moveAttack(player2, player1);
                    }

                    Console.WriteLine($"{player1.name}: {player1.stagging} - {player1.staggerpunch}");
                    Console.WriteLine($"{player2.name}: {player2.stagging} - {player2.staggerpunch}");

                    player1.fallroll();
                    earlyEnding = gamecontroller.CheckWinner(player1, player2);
                    if(earlyEnding == false)
                    {
                        player2.fallroll();
                        earlyEnding = gamecontroller.CheckWinner(player1, player2);
                    }
                    gamecontroller.rundeAktuelle++;
                }
                if(earlyEnding == false)
                {
                    Console.WriteLine($"\n--------------------------- end of round {i} ---------------------------");
                    if (i == 3 && gamecontroller.rundeAktuelle == gamecontroller.runderMaxAntal+1)
                    {
                        gamecontroller.GameEnd(player1, player2);
                        break;
                    }

                }
                i++;
            }

            Console.ReadLine();         // Stopper before closing window

            // Metoder
            string StringMinLengthChecker(String inputString)
            {
                if(inputString.Length < 3)
                {
                    Console.WriteLine("the input name is too short");
                    StringMinLengthChecker(Console.ReadLine());
                }
                return inputString;
            }

            int StringToIntChecker(String inputString)
            {
                int output = 0;
                try
                {
                    output = Int32.Parse(inputString);
                }
                catch
                {
                    Console.WriteLine("please insert a number");
                    StringToIntChecker(Console.ReadLine());
                }
                return output;
            }

            void moveBlocker()
            {
                if(player1.move == 3)
                {
                    Console.WriteLine($"{player1.name} {player1.actionMove()} {player2.name}'s attack");
                    player1.stagging -= player2.staggerpunch;
                }else
                {
                    Console.WriteLine($"{player2.name} {player2.actionMove()} {player1.name}'s attack");
                    player2.stagging -= player1.staggerpunch;
                }
            }

            void moveAttack(Boxer PMover, Boxer PReciever)
            {
                Console.WriteLine($"{PMover.name} landed a {PMover.actionMove()}");
                PReciever.stagging -= PMover.staggerpunch;
            }




        }
    }
}
