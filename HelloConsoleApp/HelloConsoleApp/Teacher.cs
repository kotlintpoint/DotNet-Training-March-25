using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    public abstract class Teacher
    {
        //public virtual void Work()
        //{
        //    Console.WriteLine("Teaching");
        //}

        public abstract void Work();
    }

    public class GoodTeacher : Teacher
    {
        public override void Work()
        {
            Console.WriteLine("Learn together.");
        }
    }

    public class BadTeacher: Teacher
    {
        public override void Work()
        {
            Console.WriteLine("I know more than you. Teaching");
        }
    }
}
