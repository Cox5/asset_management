using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace LocalDevice
{
    class Program
    {
        static void Main(string[] args)
        {

            String deviceType = String.Empty;           // tip uredjaja (analogni ili digitalni)
            String controllerName = String.Empty;       // kontroler u koji saljemo informacije o uredjaju (ujedno i ime xml fajla (c1.xml, c2.xml... ))
            FileInfo[] dllFiles = new FileInfo[0];      // niz u koji ce biti smesteni .xml fajlovi u koje cemo da upisujemo


            Console.WriteLine("**************LOKALNI UREDJAJ**************");
            Console.WriteLine("Dodavanje novog uredjaja: \n");

            Console.Write("Unesite ID uredjaja: ");
            String deviceId = Console.ReadLine();
            do
            {
                Console.Write("Unesite tip uredjaja <A ili D>: ");
                deviceType = Console.ReadLine();
                if (deviceType.Equals("A") || deviceType.Equals("a"))
                {
                    Console.Write("Unesite inicijalnu vrednost za analogni uredjaj (broj): ");
                    String analogValue = Console.ReadLine();
                    break;
                }
                else if (deviceType.Equals("D") || deviceType.Equals("d"))
                {
                    Console.Write("Unesite vrednost za digitalni uredjaj (ON/OFF/Open/Close): ");
                    string digitalValue = Console.ReadLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Pogresno unet tip uredjaja... Unesite \'A\' ili \'D\' za tip uredjaja.");
                }
            } while (!deviceType.Equals("A") || !deviceType.Equals("a") || !deviceType.Equals("D") || !deviceType.Equals("d"));
            Console.Write("Unesite ime kontrolera kome saljete: ");
            controllerName = Console.ReadLine();

            // Citanje direktorijuma Comunication u kome se nalaze .xml fajlovi u koje upisujemo informacije o lokalnom uredjaju/ima
            do
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(@"../Comunicaton/");
                    dllFiles = di.GetFiles("*.xml");

                    Console.WriteLine("Sadrzaj Comunication foldera: ");
                    int i = 0;
                    foreach (var item in dllFiles)
                    {
                        Console.WriteLine($"{++i}: {item}");
                    }

                    break;      // kada uspesno iscita sve fajlove, izadji iz provere

                }
                catch (Exception)
                {
                    Console.Write($"Greska pri citanju foldera: Comunication direktorijum je prazan...");
                    Thread.Sleep(2500);
                }


            } while (dllFiles.Length == 0);

            // controller1,2,3 ... 
            while (true)
            {
                // pisanje u xml fajl
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load($"{controllerName}.xml");

                }
                catch (Exception)
                {
                    Console.WriteLine($"Greska pri ucitavanju {controllerName}.xml");
                }                


                Console.WriteLine("WHILE");
                Thread.Sleep(2000);

            }
            // proveravaj da li postoji fajl xml
        }
    }
}
