namespace Quadrant_Practise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input X Coordinate (-5 - 5)");
            double x = Convert.ToDouble(Console.ReadLine());
            Thread.Sleep(100);
            Console.Clear();

            Console.WriteLine("Input Y Coordinate (-5 - 5)");
            double y = Convert.ToDouble(Console.ReadLine());
            Thread.Sleep(100);
            Console.Clear();

            if (x > 0 && y > 0)
            {
                Console.Clear();
                int verticalPadding = 13;
                for (int i = 0; i < verticalPadding; i++)
                    Console.WriteLine();
                PrintCentered("You are in the 1st quadrant", ConsoleColor.Cyan);
                int verticalPadding2 = 13;
                for (int i = 0; i < verticalPadding2; i++)
                    Console.WriteLine();
            }
            else if (x < 0 && y > 0)
            {
                Console.Clear();
                int verticalPadding = 12;
                for (int i = 0; i < verticalPadding; i++)
                    Console.WriteLine();
                PrintCentered("You are in the 2nd quadrant", ConsoleColor.Cyan);
                int verticalPadding2 = 13;
                for (int i = 0; i < verticalPadding2; i++)
                    Console.WriteLine();
            }
            else if (x < 0 && y < 0)
            {
                Console.Clear();
                int verticalPadding = 12;
                for (int i = 0; i < verticalPadding; i++)
                    Console.WriteLine();
                PrintCentered("You are in the 3rd quadrant", ConsoleColor.Cyan);
                int verticalPadding2 = 13;
                for (int i = 0; i < verticalPadding2; i++)
                    Console.WriteLine();
            }
            else if (x > 0 && y < 0)
            {
                Console.Clear();
                int verticalPadding = 12;
                for (int i = 0; i < verticalPadding; i++)
                    Console.WriteLine();
                PrintCentered("You are in the 4th quadrant", ConsoleColor.Cyan);
                int verticalPadding2 = 13;
                for (int i = 0; i < verticalPadding2; i++)
                    Console.WriteLine();
            }
            else if (x==0 && y==0)
            {
                Console.Clear();
                int verticalPadding = 12;
                for (int i = 0; i < verticalPadding; i++)
                    Console.WriteLine();
                PrintCentered("You are in origin", ConsoleColor.Cyan);
                int verticalPadding2 = 13;
                for (int i = 0; i < verticalPadding2; i++)
                    Console.WriteLine();
            }
            else
            {
                Console.Clear();
                int verticalPadding = 12;
                for (int i = 0; i < verticalPadding; i++)
                    Console.WriteLine();
                PrintCentered("Incorrect Number");
            }
        }

        static void PrintCentered(string text, ConsoleColor col = ConsoleColor.Gray, int delay = 0)
        {
            int width = Console.WindowWidth;
            Console.ForegroundColor = col;
            int position = (width - text.Length) / 2;
            Console.SetCursorPosition(Math.Max(0, position), Console.CursorTop);
            Console.WriteLine(text);
            if (delay > 0) Thread.Sleep(delay);
        }
    }
}
