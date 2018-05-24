using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalControler
{
    class Program
    {
        static void Main(string[] args)
        {
            LocalControlerClass controller = new LocalControlerClass();

            Console.ReadKey();
            controller.RecieveData();
            controller.SendData();

            Console.ReadKey();
        }
    }
}
