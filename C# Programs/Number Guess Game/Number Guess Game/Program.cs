using System;
using System.Drawing;

namespace Number_Guess_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //pick random number
            Random randomchoice = new Random();
            int cpuchoice = randomchoice.Next(1, 100);

            //player variables
            int attempts = 6;

            //game guess loop
            while (attempts >= 1)
            {
                //player pick number
                Console.WriteLine("You have " + attempts + " attempt(s) left");
                Console.WriteLine("Choose a number");
                int playerchoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                Thread.Sleep(1000);

                //checks for player choice vs cpu choice
                if (playerchoice > cpuchoice)
                {
                    Console.WriteLine("To High");
                    Console.WriteLine("");
                    Thread.Sleep(1000);
                    attempts--;
                    Console.Clear();
                }
                else if (playerchoice < cpuchoice)
                {
                    Console.WriteLine("To Low");
                    Console.WriteLine("");
                    Thread.Sleep(1000);
                    attempts--;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("You Win");
                    Console.WriteLine("");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }
            }

            //lose message
            Console.WriteLine("You Lose");
            Thread.Sleep(1000);
            Console.WriteLine("Starting Nuke Countdown");
            Thread.Sleep(1000);
            Console.Clear();

            //nuke countdown
            int counter = 10;
            while (counter > 0)
            {
                Console.WriteLine(counter);
                counter--;
                Thread.Sleep(300);
                Console.Clear();
            }

            //nuke effect
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"


                                                   ________________
                                              ____/ (  (    )   )  \___
                                             /( (  (  )   _    ))  )   )\
                                           ((     (   )(    )  )   (   )  )
                                         ((/  ( _(   )   (   _) ) (  () )  )
                                        ( (  ( (_)   ((    (   )  .((_ ) .  )_
                                       ( (  )    (      (  )    )   ) . ) (   )
                                      (  (   (  (   ) (  _  ( _) ).  ) . ) ) ( )
                                      ( (  (   ) (  )   (  ))     ) _)(   )  )  )
                                     ( (  ( \ ) (    (_  ( ) ( )  )   ) )  )) ( )
                                      (  (   (  (   (_ ( ) ( _    )  ) (  )  )   )
                                     ( (  ( (  (  )     (_  )  ) )  _)   ) _( ( )
                                      ((  (   )(    (     _    )   _) _(_ (  (_ )
                                       (_((__(_(__(( ( ( |  ) ) ) )_))__))_)___)
                                       ((__)        \\||lll|l||///          \_))
                                                (   /(/ (  )  ) )\   )
                                              (    ( ( ( | | ) ) )\   )
                                               (   /(| / ( )) ) ) )) )
                                             (     ( ((((_(|)_)))))     )
                                              (      ||\(|(|)|/||     )
                                            (        |(||(||)||||        )
                                              (     //|/l|||)|\\ \     )
                                            (/ / //  /|//||||\\  \ \  \ _)
                    -------------------------------------------------------------------------------


");
            Thread.Sleep(2000);

        }
    }
}
