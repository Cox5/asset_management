using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalControler
{
    class Program
    {
        static void Main(string[] args)
        {
            LocalControlerClass controller = new LocalControlerClass();

            //Console.ReadKey();
            while (true) {
                controller.RecieveData();
                controller.SendData();
                Thread.Sleep(3000);
            }
            Console.ReadKey();
        }
    }
}
