using System;

namespace BoxeSpil_Console
{
    class Program
    {
        static int runderMaxAntal = 0;
        static int rundeAktuelle = 0;
        static Boxer player1 = new Boxer();
        static Boxer player2 = new Boxer();
        static Random rnd = new Random();
        static string players;
        static void Main(string[] args)
        {
            
            bool earlyEnding = false;

            Console.WriteLine("Will you be playing with a friend tonight, or shall i find a volunteer? (y/n - n is default)");
            players = Console.ReadLine();
            Console.WriteLine("What is your name? (Atleast 3 characters)");  
            player1.name = StringMinLengthChecker(Console.ReadLine());

            // Player 2 or NPC
            if(players == "y")
            {
                Console.WriteLine("What is the name of your opponent? (Atleast 3 characters)");
                player2.name = StringMinLengthChecker(Console.ReadLine());
            }
            else
            {
                player2.name = "Volenteer";
            }

            // Moves pr. round
            Console.WriteLine("how many rounds will you be doing tonight? (1-13)");
            runderMaxAntal = StringToIntChecker(Console.ReadLine());


            // Random storytext & info
            Console.WriteLine($"\nWelcome to the boxing match of the year, here tonight we present to you the most brutal fight you'll ever see\n" +
                $"in the right corner we have {player1.name} who is undefeated for the last 3 seasons. \n" +
                $"and in the left corner we have our challenger who's *ahem*........ {player2.name} who thinks he can take down our champion");
            Console.WriteLine("(there's 2 kind of attacks og 1 defend, also player 1 has a power move.) \n1 = Jab \n2 = Hook \n3 = Defend \n4 = Uppercut (unlimited power) \n");

            // Fighting
            int i = 1;
            while(i <= 3 && earlyEnding == false)
            {
                Console.WriteLine($"--------------------------- round {i} start! ---------------------------");
                rundeAktuelle = 1;
                while (rundeAktuelle <= runderMaxAntal && earlyEnding == false)
                {
                    Console.WriteLine($"\nit's {player1.name}'s turn to pull the punch.. what will his next move be?");
                    player1.move = StringToIntChecker(Console.ReadLine(),1);


                    if(players == "y")
                    {
                        Console.WriteLine($"\nit's {player2.name}'s turn to pull the punch.. what will his next move be?");
                        player2.move = StringToIntChecker(Console.ReadLine(),1);
                    } else
                    {
                        player2.move = rnd.Next(1, 4);
                    }

                    // Both players selects moves before executing them, so defending will have an effect 
                    if(player1.move == 3 || player2.move == 3)
                    {
                        moveBlocker();
                    } else
                    {
                        moveAttack(player1, player2);
                        moveAttack(player2, player1);
                    }

                    // Shows stagging informations
                    Console.WriteLine($"{player1.name}: {player1.stagging} - {player1.staggerpunch}");
                    Console.WriteLine($"{player2.name}: {player2.stagging} - {player2.staggerpunch}");

                    // Fall check & get-back-up check on both 
                    player1.fallroll();
                    earlyEnding = CheckWinner(player1, player2);
                    if(earlyEnding == false)
                    {
                        player2.fallroll();
                        earlyEnding = CheckWinner(player1, player2);
                    }

                    rundeAktuelle++;
                }
                if(earlyEnding == false)
                {
                    Console.WriteLine($"\n--------------------------- end of round {i} ---------------------------");
                    
                    // checks for early ending and if all rounds were played out
                    if (i == 3 && rundeAktuelle == runderMaxAntal+1)
                    {
                        GameEnd(player1, player2);
                        break;
                    }

                }
                i++;
            }

            Console.ReadLine();         // Stopper before closing window

        }

        // String checker & feedback
        static string StringMinLengthChecker(String inputString)
        {
            if (inputString.Length < 3)
            {
                Console.WriteLine("the input name is too short");
                StringMinLengthChecker(Console.ReadLine());
            }
            return inputString;
        }

        // Conversion check & feedback + input type (default - 0 & move - 1)
        static int StringToIntChecker(String inputString, int inputType = 0)
        {
            int output = 0;
            try
            {
                Int32.TryParse(inputString, out output);
            }
            catch
            {
                Console.WriteLine("please insert a valid number");
                output = StringToIntChecker(Console.ReadLine());
            }
            if (output <= 0 || output > 13)
            {
                Console.WriteLine("We won't stay here all night, please insert an other number");
                output = StringToIntChecker(Console.ReadLine());
            } else if(inputType == 1 && output >= 5)
            {
                Console.WriteLine("please insert a valid number");
                output = StringToIntChecker(Console.ReadLine(), inputType);
            }
            return output;
        }

        // Blocking a move
        static void moveBlocker()
        {
            if (player1.move == 3)
            {
                Console.WriteLine($"{player1.name} {player1.actionMove()} {player2.name}'s attack");
                player1.stagging -= player2.staggerpunch;
            }
            else
            {
                Console.WriteLine($"{player2.name} {player2.actionMove()} {player1.name}'s attack");
                player2.stagging -= player1.staggerpunch;
            }
        }

        // Attack move
        static void moveAttack(Boxer PMover, Boxer PReciever)
        {
            Console.WriteLine($"{PMover.name} landed a {PMover.actionMove()}");
            PReciever.stagging -= PMover.staggerpunch;
        }

        static bool CheckWinner(Boxer p1, Boxer p2)
        {
            if (p1.stagging <= 0)
            {
                Winner(p2);
                return true;
            }
            else if (p2.stagging <= 0)
            {
                Winner(p1);
                return true;
            }
            return false;
        }

        static void GameEnd(Boxer p1, Boxer p2)
        {
            if (p1.stagging < p2.stagging)
            {
                Winner(p2);
            }
            else
            {
                Winner(p1);
            }
        }

        static void Winner(Boxer winPlayer)
        {
            Console.WriteLine($"--------------------------- {winPlayer.name} has won!! ---------------------------");
        }
    }
}
