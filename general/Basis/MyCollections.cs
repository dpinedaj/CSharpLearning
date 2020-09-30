using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class MyCollections
    {

        public static void Run()
        {
            Console.WriteLine("Testing Dicts");
            testDict();
            Console.WriteLine("Testing Lists");
            testList();
        }

        static void testDict()
        {
            static Dictionary<string, int> fillDict(Dictionary<string, int> dict,string key, int value)
            {
                dict.Add(key, value);
                return dict;
            }

            var myList = new List<string> {"one", "two", "three", "four", "five"};
            var myDict = new Dictionary<string, int>();
            
            for (int i = 1; i < 6; i++)
            {
                myDict = fillDict(myDict, myList[i-1], i);
            }

            foreach (var kvp in myDict)
            {
                Console.WriteLine("Key {0}, Value {1}", kvp.Key, kvp.Value);
            }
            
        }

        static void  testList()
        {
            List<int> list= new List<int>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(i);
            }
            Console.WriteLine("List size {0}", list.Count);
            foreach (var val in list)
            {
                Console.WriteLine("Component {0}", val);
            }
        }
        
    }
}