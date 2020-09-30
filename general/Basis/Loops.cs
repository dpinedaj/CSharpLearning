

namespace ConsoleApp1
{   
    using System;
    using System.Linq;
    class Loops
    {
        public static void Run(int end)
        {
            Console.WriteLine("For Loop");
            ForLoop(end);
            Console.WriteLine("While Loop");
            WhileLoop(end);
            Console.WriteLine("DoWhile Loop");
            DoWhileLoop(end);
            Console.WriteLine("ForEach Loop");
            ForEachLoop(end);
        }

        private static void ForLoop(int end)
        {
            for (var i = 0; i <= end; i++)
            {
                Console.WriteLine(i);
            }
        }

        private static void ForEachLoop(int end)
        {
            foreach (var i in Enumerable.Range(0, end + 1).ToList())
            {
                Console.WriteLine(i);
            }
        }

        private static void WhileLoop(int end)
        {
            var i = 0;
            while (i <= end)
            {
                Console.WriteLine(i);
                i++;
            }
        }

        private static void DoWhileLoop(int end)
        {
            var i = 0;
            do
            {
                {
                    Console.WriteLine(i);
                    i++;
                }
            } while (i <= end);
            
        }
    }
}