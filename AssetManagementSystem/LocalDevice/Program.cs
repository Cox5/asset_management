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

            LocalDeviceClass localDeviceClass = new LocalDeviceClass();

            localDeviceClass.ListDirectory();

            while (true) {
                var result = localDeviceClass.WriteToXML() ? "Uspesno dodat uredaj" : "Greska pri dodavanju uredjaja";
                Console.WriteLine($"\n*********{result}*********");
                Console.Read();
            }


            Console.Read();


            
        }
    }
}
