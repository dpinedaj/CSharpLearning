using System;

namespace ConsoleApp1
{
    class Loops
    {
        static void Main(string[] args)
        {
            Console.WriteLine("For Loop");
            ForLoop(20);
            Console.WriteLine("While Loop");
            WhileLoop(20);
        }

        static void ForLoop(int end)
        {
            for (int i = 0; i <= end; i++)
            {
                Console.WriteLine(i);
            }
        }
        static void WhileLoop(int end)
        {
            var i = 0;
            while (i <= end)
            {
                Console.WriteLine(i);
                i++;
            }
        }
    }
}