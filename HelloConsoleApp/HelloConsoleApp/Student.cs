using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    public class Student
    {
        private int _id;
        public void SetId(int id) { 
            _id = id;
        }
        public int GetId()
        {
            return _id;
        }



        public string Name { get; set; }
        public string Description { get; set; }

        public Student() { }

        public Student(string name) {
            Console.WriteLine("Custom Constructor");
            Name = name;
        }

        public void Talk() {
            Console.WriteLine("Student can talk.");
        }
    }

    public class Engineer : Student {
        public Engineer(string name): base(name) { 
        
        }

        public void GreetEngineer() {
            Console.WriteLine("Welcome, Engineer");
        }
    }
}
