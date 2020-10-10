namespace ConsoleApp1
{
    public class Conditionals
    {
        public static void Run()
        {
            IfCondition(15);
            IfCondition(20);
            IfCondition(50);
            mySwitch("Hello");

        }

        private static void IfCondition(int age)
        {
            if (age >= 18 && age < 40)
            {
                System.Console.WriteLine("Is an adult");
            }
            else if (age >= 40)
            {
                System.Console.WriteLine("Is an old man");
            }
            else
            {
                System.Console.WriteLine("Is just a child");
            }
        }

        private void terciaryCheck(int age)
        {

         bool checkAge = (age < 18 || age >= 65) ? true: false;
        }

        private static void mySwitch(string option)
        {
            switch (option)
            {
                case "hello":
                    System.Console.WriteLine("Good Morning");
                    break;
                default:
                    System.Console.WriteLine("Good Afternoon");
                    break;
            }
        }
    }

}