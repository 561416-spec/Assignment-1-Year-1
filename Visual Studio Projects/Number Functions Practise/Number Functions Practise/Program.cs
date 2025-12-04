namespace Number_Functions_Practise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Please input a double number");
            double Input = Convert.ToDouble(Console.ReadLine());

            Console.Clear();

            Console.WriteLine("Please input a second double number");
            double Input2 = Convert.ToDouble(Console.ReadLine());

            Console.Clear();

            Console.WriteLine("Please input a third interger number");
            int Input3 = Convert.ToInt32(Console.ReadLine());

            Thread.Sleep(1000);
            Console.Clear();

            Console.WriteLine("Number 1 = " + Input);
            Console.WriteLine("Number 2 = " + Input2);
            Console.WriteLine("Number 3 = " + Input3);
            Console.WriteLine();

            Console.WriteLine("PI = " + Math.PI);
            Console.WriteLine("Eular = " + Math.E);
            Console.WriteLine();

            Console.WriteLine("Your input with Abs = " + Math.Abs(Input));
            Console.WriteLine("Your input with Acosh = " + Math.Acosh(Input));
            Console.WriteLine("Your input with Asinh = " + Math.Asinh(Input));

            Console.WriteLine();

            Console.WriteLine("Your input and input2 with Min = " + Math.Min(Input, Input2));
            Console.WriteLine("Your input and input2 with Max = " + Math.Max(Input, Input2));
            Console.WriteLine("Your input and input2 with Pow = " + Math.Pow(Input, Input2));

            Console.WriteLine();

            Console.WriteLine("Your input with Round = " + Math.Round(Input));

            Console.WriteLine("Your input 3 with Even Check = " + EvenCheck(Input3));
            Console.WriteLine("Your input 3 with Odd Check = " + OddCheck(Input3));


        }

        public static bool EvenCheck(int value)
        {
            return value % 2 == 0;
        }

        public static bool OddCheck(int value)
        {
            return value % 2 != 0;
        }
    }
}
