using System;


namespace Class_Practise
{
    public class Car
    {
       //fields
        public string model;
        public string color;
        public int year;

        //construtor

        public Car(string modelName, string modelColor, int modelYear)
        {
            model = modelName;
            color = modelColor;
            year = modelYear;
        }

        //methods
        public void showInfo()
        {

            Console.WriteLine(model);
            Console.WriteLine(color);
        }
    }
}
