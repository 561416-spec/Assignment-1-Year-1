using Microsoft.VisualBasic;

namespace String_Functions_Practise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            output("[1] String texting examples");
            output("[2] String texting game");
            output("[3] String awnser checker");
            string menuchoice = inputText();
            if (menuchoice == "1")
            {
                Console.Clear();
                string test = "Testing string number 1, The second testing string number 2.";
                Console.WriteLine("The string is (" + test + ")");

                string check1 = "number 1";
                string check2 = "number 3";
                Console.WriteLine("The text contains " + check1 + " = " + test.Contains(check1));
                Console.WriteLine("The text contains " + check2 + " = " + test.Contains(check2));

                bool endingcheck = test.EndsWith(".");
                Console.WriteLine("");
                Console.WriteLine("({0}) ends with a .: {1}", test, endingcheck);

                string test2 = (string)test.Clone();
                Console.WriteLine("test string = " + test);
                Console.WriteLine("test2 string = " + test2);
            }
            else if (menuchoice == "2")
            {
                Console.Clear();
                Console.WriteLine("Welcome to typing game");
                Console.WriteLine("copy the text below");
                Console.WriteLine();
                Thread.Sleep(500);
                string original = "If you copy this text wrong you will lose";
                Console.WriteLine(original);
                string userinput = inputText();
                int comparison = userinput.CompareTo(original);
                if (comparison == 0)
                {
                    output("You win !");
                }
                else if (comparison == -1)
                {
                    output("You lose !");
                }
            }
        }

        private static string inputText()
        {
            return Console.ReadLine();
        }

        private static void output(string text)
        {
            Console.WriteLine(text);
        }
    }
}
