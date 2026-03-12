using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloConsoleApp
{
    // Difference between class and interface 
    interface IFace1 {
        void HelloInterface();  // bydefault abstract method
    }

    interface IFace2 {
        void WelcomeToInterface(int num);
    }

    interface IFace3: IFace1, IFace2 
    { 
    
    }

    public class InterfaceDemo : Student, IFace1, IFace2
    {
        public void HelloInterface() {
         
        }

        public void WelcomeToInterface(int num)
        {
           
        }
    }
}
