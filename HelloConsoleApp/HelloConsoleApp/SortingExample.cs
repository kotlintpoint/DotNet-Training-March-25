using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    public class SortingExample
    {
        public static void SortNumbersExample()
        {
            int[] numbers = { 4, 12, 3, 6, 1 };
            PrintData(numbers, "Before Sorting");
            SortNumbers(numbers);
            PrintData(numbers, "After Sorting");

            int[] Marks;    // NullReferenceException
            Marks = new int[10];
            // Marks[10] -> IndexOutOfRangeException

            int value = 100;
            Console.WriteLine($"\n\nBefore Change - {value}");
            PassByValue(value);
            Console.WriteLine($"After Change - {value}");

            Console.WriteLine($"\n\nReference Before Change - {value}");
            PassByReferenceWithPrimite(ref value);
            Console.WriteLine($"Reference After Change - {value}");

            PrintData(numbers, "Before Chaning Array");
            PassByReference(numbers);
            PrintData(numbers, "After Changing Array");
        }

        private static void SortNumbers(int[] numbers) {
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[i] > numbers[j])
                    {
                        var temp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
        }

        private static void PrintData(int [] numbers, string msg) {
            var StringMessage = string.Format("\n\n{0}", msg);
            Console.WriteLine(StringMessage);
            foreach(var num in numbers) {
                Console.Write($"{num}, ");
            }
        }

        private static void PassByValue(int value) {
            value += 100;
        }

        private static void PassByReferenceWithPrimite(ref int value)
        {
            value += 100;
        }

        private static void PassByReference(int[] values)
        {
            values[0] += 100;
        }
    }
}
