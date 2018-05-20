using AMS_Common;
using LocalDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
