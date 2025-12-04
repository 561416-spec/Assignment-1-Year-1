namespace Triangle_Practise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Side 1  Side 2  Side 3");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Please input side 1");
            int a = Convert.ToInt32(Console.ReadLine());

            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("Side 1  Side 2  Side 3");
            Console.WriteLine("  " + a + "              ");
            Console.WriteLine();
            Console.WriteLine("Please input side 2");
            int b = Convert.ToInt32(Console.ReadLine());

            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("Side 1  Side 2  Side 3");
            Console.WriteLine("  " + a + "       " + b);
            Console.WriteLine();
            Console.WriteLine("Please input side 3");
            int c = Convert.ToInt32(Console.ReadLine());

            Thread.Sleep(1000);
            Console.Clear();

            Console.WriteLine("Side 1  Side 2  Side 3");
            Console.WriteLine("  " + a + "       " + b + "     " + c);
            Console.WriteLine();

            Thread.Sleep(1000);
            Console.Clear();

            if (a==c)
            {
                Console.WriteLine("Isosceles Triangle");
                Thread.Sleep(500);
            }
            else if (a>b && b>c)
            {
                Console.WriteLine("Scalene Triangle");
                Thread.Sleep(500);
            }
            else if (a==b && a==c)
            {
                Console.WriteLine("Equilateral Triangle");
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("Incorrect Input");
            }
        }
    }
}
