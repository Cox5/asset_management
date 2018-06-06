using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LocalDevice
{
    class Program
    {
        static void Main(string[] args)
        {

            LocalDeviceClass.ListDirectory();
            Random rnd = new Random();
            LocalDeviceClass localDeviceClass = new LocalDeviceClass();


            while (true) {
                var result = localDeviceClass.WriteToXML() ? "Uspesno dodat uredaj" : "Greska pri dodavanju uredjaja";
                if (String.Equals(localDeviceClass.TypeDevice, "a") || String.Equals(localDeviceClass.TypeDevice, "A"))
                {
                    localDeviceClass.Value = rnd.Next(1, 1000).ToString();
                }
                else
                {
                    localDeviceClass.Value = (rnd.Next(1, 1000) % 2).ToString();
                }
                Console.WriteLine($"\n*********{result}*********");
                Thread.Sleep(3000);
            }


            //Console.Read();


            
        }
    }
}
