using AMS_Common;
using LocalDevice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace LocalControler
{
    public class LocalControlerClass : ILocalControler
    {
        public string Id { get; set; }
        public Dictionary<string, ILocalDevice> Devices { get; set; }

        public LocalControlerClass()
        {
            Console.WriteLine("**************\nLOKALNI Kontroler**************");
            Console.WriteLine("Dodavanje novog kontrolera: \n");
            Console.WriteLine("Unesite ID kontrolera: ");
            Id = Console.ReadLine();

            CreateXMLFile("c", Id);

            
        }

        public void CreateXMLFile(string name, string id)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("controller");
            root.SetAttribute("id", id);

            XmlElement element = doc.CreateElement("prazno");
            root.AppendChild(element);

            doc.AppendChild(root);
            doc.Save(@"..\..\..\Communication\" + name + id + ".xml");
        }

        public void SendData()
        {
            FileInfo[] dllFiles = null;
            do
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(@"../../../Communication/");
                    dllFiles = di.GetFiles("AMS.xml");

                    if (dllFiles.Length < 1)
                    {
                        Console.WriteLine("Greska pri pretrazi fajla: Fajl sa datim nazivom ne postoji...");
                    }
                }
                catch (Exception e)
                {
                    Console.Write($"Greska pri citanju foldera: Comunication direktorijum je prazan...");
                    Thread.Sleep(2500);
                }
            }
            while (dllFiles==null);


            Console.WriteLine("Sledi slanje podataka u fajl...");
        }
    }
}
