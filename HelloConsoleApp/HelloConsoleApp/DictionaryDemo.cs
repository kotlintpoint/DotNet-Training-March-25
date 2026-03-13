using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    class DictionaryDemo
    {
        public void DictionaryExample() {

            Dictionary<int, string> data = new Dictionary<int, string>();
            data.Add(1, "One");
            data.Add(2, "Two");
            data.Add(3, "Three");
            Console.WriteLine(data[2]);

            //Console.WriteLine(data[5]);

            var removeBool = data.Remove(1);
            Console.WriteLine($"1 removed => {removeBool}");

            data.TryGetValue(2, out var twoValue);
            Console.WriteLine($"twoValue => {twoValue}");

            foreach (var item in data)
            {
                Console.WriteLine($"{item.Key} => {item.Value}");
            }


            Dictionary<string, List<string>> states = new Dictionary<string, List<string>>();
            states.Add("GJ", new List<string> { "Surat", "Ahmedabad", "Baroda" });
            states.Add("MH", new List<string> { "Mumbai", "Pune", "Nagpur" });

            var GujaratCities = states["GJ"];
            GujaratCities.ForEach(x => Console.WriteLine(x));
        }

    }
}
