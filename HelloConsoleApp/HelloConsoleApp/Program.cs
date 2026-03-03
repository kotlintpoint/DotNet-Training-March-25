using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var value = 100;
            //string msg = "Hello World!!! Good Morning!!!";
            //Console.WriteLine(msg);
            //if (value == 100)
            //{
            //    Console.WriteLine("Value is 100.");
            //    return;
            //}            
            //Console.WriteLine("Main End.");

            //string UserName = Console.ReadLine();
            //Console.WriteLine($"Welcome, {UserName}");

            //BooleanComparison();

            //StringIsNullOrEmptyTesting();

            SortingExample.SortNumbersExample();
        }

        static void BooleanComparison() {
            bool? IsTest = null;            
            int num1 = ReadNumber();            
            int num2 = ReadNumber();
            int sum = num1 + num2;
            Console.WriteLine($"{num1} + {num2} = {sum}");            
        }

        static int ReadNumber() {
            Console.Write("Enter number : ");
            //return int.Parse(Console.ReadLine());
            var input = Console.ReadLine();
            int number;
            bool IsParsed = int.TryParse(input, out number);
            
            if (IsParsed)
            {
                return number;
            }
            // Recursion - Function call itself
            return ReadNumber();              
        }

        static void StringIsNullOrEmptyTesting()
        {
            Console.Write("Enter name : ");            
            var name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                StringIsNullOrEmptyTesting();
                return;
            }
            Console.WriteLine($"Welcome, {name}");
        }
    }
}
