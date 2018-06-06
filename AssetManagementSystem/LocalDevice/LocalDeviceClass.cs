using AMS_Common;
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


    public class LocalDeviceClass : ILocalDevice
    {
        #region Properties
        public string Id { get; set; }
        public string TypeDevice { get; set; }
        public string Value { get; set; }
        public string Configuration { get; set; }
        public double WorkTime { get; set; }
        #endregion

        public bool uslov = true;
        public LocalDeviceClass()
        {

            Console.WriteLine("**************LOKALNI UREDJAJ**************");
            Console.WriteLine("Dodavanje novog uredjaja: \n");

            Console.Write("Unesite ID uredjaja: ");
            Id = Console.ReadLine();
            do
            {
                Console.Write("Unesite tip uredjaja <A ili D>: ");
                TypeDevice = Console.ReadLine();
                if (TypeDevice.Equals("A") || TypeDevice.Equals("a"))
                {
                    Console.Write("Unesite inicijalnu vrednost za analogni uredjaj (broj): ");
                    Value = Console.ReadLine();

                    Console.Write("Unesite broj nominalnih radnih sati predvidjenih za uredjaj (broj) :");
                    WorkTime = Double.Parse(Console.ReadLine());
                    break;
                }
                else if (TypeDevice.Equals("D") || TypeDevice.Equals("d"))
                {
                    Console.Write("Unesite vrednost za digitalni uredjaj (1/0): ");
                    Value = Console.ReadLine();

                    Console.Write("Unesite broj nominalnih promena predvidjenih za uredjaj (broj) :");
                    WorkTime = Double.Parse(Console.ReadLine());
                    break;
                }
                else
                {
                    Console.WriteLine("Pogresno unet tip uredjaja... Unesite \'A\' ili \'D\' za tip uredjaja.");
                    //throw new ArgumentException("Pogresno unet tip uredjaja (unesite A ili D)");
                }
            } while (!TypeDevice.Equals("A") || !TypeDevice.Equals("a") || !TypeDevice.Equals("D") || !TypeDevice.Equals("d"));

            Console.Write("Unesite ime kontrolera kome saljete: ");
            Configuration = Console.ReadLine();
        }

        public LocalDeviceClass(string id, string typeDevice, string value, double workTime)
        {
            //if(!typeDevice.Equals("A") || !typeDevice.Equals("a") || !typeDevice.Equals("D") || !typeDevice.Equals("d"))
            //{
            //    throw new ArgumentException("Pogresno unet tip uredjaja (unesite A ili D)");
            //}
            this.Id = id;
            this.TypeDevice = typeDevice;
            this.Value = value;
            this.WorkTime = workTime;
        }


        // Citanje direktorijuma Communication u kome se nalaze .xml fajlovi u koje upisujemo informacije o lokalnom uredjaju/ima
        public static bool ListDirectory()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(@"..\..\..\Communication\");
                FileInfo[] xmlFiles = di.GetFiles("*.xml");

                Console.WriteLine("Sadrzaj Communication foldera: ");
                int i = 0;
                foreach (var item in xmlFiles)
                {
                    Console.WriteLine($"{++i}: {item}");
                }

                return true;      // kada uspesno iscita sve fajlove, izadji iz provere

            }
            catch (ArgumentException)
            {
                Console.Write($"Greska pri citanju foldera: Communication direktorijum je prazan...");
                return false;
            }

        }

        // Metoda za upis informacija o lokalnom uredjaju u XML fajl kontrolera koji ga obradjuje
        public bool WriteToXML()
        {
            XDocument xmlDoc = null;
            try
            {
                String xmlFileName = @"..\..\..\Communication\" + Configuration + ".xml";
                do {
                    try
                    {
                        xmlDoc = XDocument.Load(xmlFileName);
                        uslov = false;
                    }
                    catch
                    {
                        uslov = true;
                    }
                }
                while (uslov);

                xmlDoc.Root.Add(new XElement("Device",
                        new XAttribute("id", Id),
                        new XElement("Type", TypeDevice),
                        new XElement("Time", DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                        new XElement("Value", Value),
                        new XElement("WorkTime", WorkTime)

                    ));

                xmlDoc.Save(xmlFileName);
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri ucitavanju ili upisu u {Configuration}.xml : {e.Message}");
                return false;
            }
        }


    }
}
