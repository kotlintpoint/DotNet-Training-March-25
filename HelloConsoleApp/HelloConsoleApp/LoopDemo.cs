using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    internal class LoopDemo
    {
        public static void ContinueExample() {
            for (int i = 0; i < 10; i++)
            {
                if (i == 5) continue;
                Console.WriteLine(i);
            }
        }

        public static void BreakExample()
        {
            for (int i = 0; i < 10; i++)
            {                
                Console.WriteLine(i);
                if (i == 5) break;
            }
        }

        public static void StringExample(string name)
        {
            //name[0] = 'K';
            Console.WriteLine(name[0]);
        }

        public static void StringVsStringBuilder() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var str = "";
            for (var i = 1; i <= 100000; i++) {
                str += "Testing";
            }
            sw.Stop();
            long elapsed1 = sw.ElapsedMilliseconds;

            sw.Reset();
            sw.Start();
            var sb = new StringBuilder();
            for (var i = 1; i <= 100000; i++)
            {
                sb.Append("Testing");
            }
            sw.Stop();
            long elapsed2 = sw.ElapsedMilliseconds;

            Console.WriteLine($"String took {elapsed1}ms, StringBuilder took {elapsed2}ms.");
        }

        public static void StringExamples() {
            var word = "Hello World!!!";
            string result;
            //result = word[..5];
        }
    }
}
