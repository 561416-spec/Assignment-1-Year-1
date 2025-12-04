namespace Enum_Practise
{
    internal class Program
    { 
        
        enum Months
        {
            January, 
            February, 
            March, 
            April, 
            May, 
            June, 
            July, 
            August, 
            September, 
            October, 
            November, 
            December
        }

        enum Days
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        enum Seasons
        {
            Spring,
            Summer,
            Autumn,
            Winter
        }

        enum Difficulty
        {
            Low,
            Medium,
            High
        }

        enum Planets
        {
            Venus,
            Mars,
            Mercury,
            Jupiter,
            Saturn,
            Uranus,
            Neptune,
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my enum practise");
            Thread.Sleep(500);
            Console.WriteLine("choose a option below");

            Console.WriteLine("");
            Console.WriteLine("[1] Days, Months, Seasons");
            Console.WriteLine("[2] Difficulty");
            Console.WriteLine("[3] Planets");
            Console.WriteLine("");

            int userchoice1 = Convert.ToInt32(Console.ReadLine());
            if (userchoice1 == 1)
            {
                Thread.Sleep(500);
                Console.Clear();

                Days day = Days.Friday;
                Console.WriteLine(day);

                Months month = Months.January;
                Console.WriteLine(month);

                Seasons season = Seasons.Winter;
                Console.WriteLine(season); 

            }
            else if (userchoice1 == 2)
            {
                Thread.Sleep(500);
                Console.Clear();

                Console.WriteLine("Choose a option for difficulty 0-2");
                string userinput = Console.ReadLine();
                Difficulty myVar = (Difficulty)
                Enum.Parse(typeof(Difficulty), userinput);

                switch (myVar)
                {
                    case Difficulty.Low:
                        Console.WriteLine("Beginner Level");
                        Console.WriteLine(Difficulty.Low);
                        break;
                    case Difficulty.Medium:
                        Console.WriteLine("Standard Level");
                        Console.WriteLine(Difficulty.Medium); break;
                    case Difficulty.High:
                        Console.WriteLine("Expert level");
                        Console.WriteLine(Difficulty.High);
                        break;
                }

            }
            else if (userchoice1 == 3)
            {
                Thread.Sleep(500);
                Console.Clear();

                Console.WriteLine("Welcome to planet info");
                Thread.Sleep(1000);

                Console.WriteLine("");
                Console.WriteLine("[1] Planet's Ordered");
                Console.WriteLine("[2] Planet's Listed");
                Console.WriteLine("");

                int userchoice2 = Convert.ToInt32(Console.ReadLine());
                if (userchoice2 == 1)
                {
                    Thread.Sleep(500);
                    Console.Clear();

                    Console.WriteLine("Choose a number for a planet going closest to furthest away 0-6");
                    string choice2 = Console.ReadLine();
                    Console.WriteLine("");
                    Planets planet;
                    Enum.TryParse<Planets>(choice2, out planet);
                    Console.WriteLine(planet);
                    Thread.Sleep(2000);
                }
                else if (userchoice2 == 2)
                {
                    Thread.Sleep(500);
                    Console.Clear();

                    foreach (string str in Enum.GetNames(typeof(Planets)))
                    {
                        Console.WriteLine(str);
                    }

                }
                else
                {
                    Console.WriteLine("Incorrect Choice");
                }

            }
            else
            {
                Console.WriteLine("Incorrect Choice");
            }
            
        }
    }
}
