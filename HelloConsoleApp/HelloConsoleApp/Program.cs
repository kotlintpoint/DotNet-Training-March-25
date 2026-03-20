using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    class Program
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

            //SortingExample.SortNumbersExample();

            //LoopDemo.StringVsStringBuilder();

            //object student = new Student("Ankit");
            //Console.WriteLine(student);

            //Student std = new Engineer("Test");


            //Teacher teacher = new Teacher();
            //teacher.Work();

            //teacher = new GoodTeacher();
            //teacher.Work();

            //teacher = new BadTeacher();
            //teacher.Work();

            // can not instantiate interface
            //IFace1 face1 = new IFace1();

            /*
            IFace1 face1 = new InterfaceDemo();
            face1.HelloInterface();
            //face1.WelcomeToInterface();

            InterfaceDemo demo = new InterfaceDemo();
            demo.HelloInterface();
            demo.WelcomeToInterface(100);
            */
            //ListDemo.ListDemoExample();
            Console.WriteLine("Hello");

            Student std = new Student
            {
                Name = "Hello",
                Description = "Description"
            };

            // Serialization 
            var studentJson = JsonConvert.SerializeObject(std,Formatting.Indented);
            Console.WriteLine(studentJson);


            Student student = JsonConvert.DeserializeObject<Student>(studentJson);
            Console.WriteLine(student);

            List<Student> studentList = new List<Student> { 
                student, student, student
            };

            var studentListJson = JsonConvert.SerializeObject(studentList, Formatting.Indented);
            Console.WriteLine(studentListJson);

            List<Student> studentList1 = JsonConvert.DeserializeObject<List<Student>>(studentJson);
            Console.WriteLine(studentList1);
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
