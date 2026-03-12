using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    public class ListDemo
    {
        public static void ListDemoExample() { 
            //List<int> list = new List<int>();
            
            List<int> list = new List<int> { 10, 20, 30 };

            Console.WriteLine($"Count = {list.Count}");
            Console.WriteLine($"Capacity = {list.Capacity}");

            PrintList(list);
            list.Add(40);
            Console.WriteLine($"Count = {list.Count}");
            PrintList(list);

            list.Remove(20);
            PrintList(list);

            var allListMoreThan20 = list.All(x => x > 20);
            Console.WriteLine($"allListMoreThan20 => {allListMoreThan20}");

            var AnyValuMoreThan20 = list.Any(x => x > 20);
            Console.WriteLine($"AnyValuMoreThan20 => {AnyValuMoreThan20}");

            int value = list.Find(x => x > 20);
            List<int> values = list.FindAll(x => x > 20);
            
        }

        public static void PrintList(List<int> list) {
            /*foreach (var item in list)
            {
                Console.Write($"{item}, ");
            }*/

            list.ForEach(x => Console.Write($"{x}, "));
            Console.WriteLine();
        }
    }
}
