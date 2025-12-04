namespace RPS_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool GameEnd = false;
            int PlayerScore = 0;
            int CpuScore = 0;
            Console.WriteLine("Welcome to Rock, Paper, Scizzors");
            Thread.Sleep(2000);
            Console.Clear();


            while (GameEnd == false)
            {
                Console.WriteLine("You have " + PlayerScore + " Point(s)");
                Console.WriteLine("Computer has " + CpuScore + " Point(s)");
                Console.WriteLine("");
                Console.WriteLine("Choose a option below");
                Console.WriteLine("[1] Rock");
                Console.WriteLine("[2] Paper");
                Console.WriteLine("[3] Scizzors");
                Console.WriteLine("");
                Console.WriteLine("[0] Quit Game");


                int userchoice = Convert.ToInt32(Console.ReadLine());
                if (userchoice == 1)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("You have choosen Rock");
                    Console.WriteLine("");
                }
                else if (userchoice == 2)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("You have choosen Paper");
                    Console.WriteLine("");
                }
                else if (userchoice == 3)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("You have choosen Scizzors");
                    Console.WriteLine("");
                }
                else if (userchoice == 0)
                {
                    GameEnd = true;
                    Console.Clear();
                    Console.WriteLine("Goodbye");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }
                else
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("Incorrect Choice :(");
                    Console.WriteLine("");
                }


                Console.WriteLine("Computer is choosing");
                Thread.Sleep(2000);


                Random randomchoice = new Random();
                int cpuchoice = randomchoice.Next(1, 3);
                if (userchoice == 1 && cpuchoice == 2)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("Computer has choosen Paper");
                    Console.WriteLine(@"
    _______
---'   ____)____
          ______)
          _______)
         _______)
---.__________)
");
                    Thread.Sleep(500);
                    Console.WriteLine("You Lose");
                    CpuScore++;
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else if (userchoice == 1 && cpuchoice == 3)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("Computer has choosen Scizzors");
                    Console.WriteLine(@"
    _______
---'   ____)____
          ______)
       __________)
      (____)
---.__(___)
");
                    Console.WriteLine("");
                    Thread.Sleep(500);
                    Console.WriteLine("You Win");
                    PlayerScore++;
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else if (userchoice == 2 && cpuchoice == 1)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("Computer has choosen Rock");
                    Console.WriteLine(@"
    _______
---'   ____)
      (_____)
      (_____)
      (____)
---.__(___)
");
                    Thread.Sleep(500);
                    Console.WriteLine("You Win");
                    PlayerScore++;
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else if (userchoice == 2 && cpuchoice == 3)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("Computer has choosen Scizzors");
                    Console.WriteLine(@"
    _______
---'   ____)____
          ______)
       __________)
      (____)
---.__(___)
");
                    Console.WriteLine("");
                    Thread.Sleep(500);
                    Console.WriteLine("You Lose");
                    CpuScore++;
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else if (userchoice == 3 && cpuchoice == 1)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("Computer has choosen Rock");
                    Console.WriteLine(@"
    _______
---'   ____)
      (_____)
      (_____)
      (____)
---.__(___)
");
                    Thread.Sleep(500);
                    Console.WriteLine("You Lose");
                    CpuScore++;
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else if (userchoice == 3 && cpuchoice == 2)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("Computer has choosen Paper");
                    Console.WriteLine(@"
    _______
---'   ____)____
          ______)
          _______)
         _______)
---.__________)
");
                    Console.WriteLine("");
                    Thread.Sleep(500);
                    Console.WriteLine("You Win");
                    PlayerScore++;
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else
                {
                    if (userchoice == cpuchoice)
                    {
                        Thread.Sleep(500);
                        Console.Clear();
                        Console.WriteLine("Same Choice");
                        Console.WriteLine("");
                        Thread.Sleep(500);
                        Console.WriteLine("Draw");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("I have no idea how you got this message");
                    }
                }


            }

        }
    }
}
