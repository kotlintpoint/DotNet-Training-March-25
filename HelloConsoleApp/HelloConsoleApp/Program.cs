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
            var value = 100;
            string msg = "Hello World!!! Good Morning!!!";
            Console.WriteLine(msg);
            //if (value == 100)
            //{
            //    Console.WriteLine("Value is 100.");
            //    return;
            //}            
            //Console.WriteLine("Main End.");

            string UserName = Console.ReadLine();
            Console.WriteLine($"Welcome, {UserName}");
        }
    }
}
