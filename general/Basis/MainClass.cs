namespace ConsoleApp1
{
    using System;
    public class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data Types: ");
            Types.Run();
            Console.WriteLine("Loops: ");
            Loops.Run(2);
            Console.WriteLine("Collections");
            MyCollections.Run();
            
            MyStrings.Run();
        }
    }
}