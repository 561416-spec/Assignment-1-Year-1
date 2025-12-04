namespace Class_Practise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car Ford = new Car("Mustang", "Red", 1969);
            Car Opel = new Car("Astra", "White", 2005);
            Car Mazda = new Car("MX-5 Miata", "Red", 2021);

            Ford.showInfo();
            Opel.showInfo();
            Mazda.showInfo();

        }
    }
}
