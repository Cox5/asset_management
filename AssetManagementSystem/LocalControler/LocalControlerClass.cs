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
using System.Xml.Linq;

namespace LocalControler
{
    public class LocalControlerClass : ILocalControler
    {
        public bool uslov = true;
        public string Id { get; set; }
        public Dictionary<string, List<Tuple<string, ILocalDevice>>> Devices { get; set; }

        public LocalControlerClass()
        {
            Devices = new Dictionary<string, List<Tuple<string, ILocalDevice>>>();
            Console.WriteLine("**************LOKALNI Kontroler**************");
            Console.WriteLine("Dodavanje novog kontrolera: \n");
            Console.WriteLine("Unesite ID kontrolera: ");
            Id = Console.ReadLine();

            CreateRecieveXMLFile();

            
        }

        public void CreateRecieveXMLFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("controller");
            root.SetAttribute("id", Id);

            doc.AppendChild(root);
            doc.Save(@"..\..\..\Communication\" + "c" + Id + ".xml");

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
                catch (Exception)
                {
                    Console.Write($"Greska pri citanju foldera: Comunication direktorijum je prazan...");
                    Thread.Sleep(2500);
                }
            }
            while (dllFiles==null);

            WriteToSendXML();
        }

        public bool WriteToSendXML()
        {
            try
            {
                String xmlFileName = @"..\..\..\Communication\AMS.xml";
                XDocument xmlDoc = XDocument.Load(xmlFileName);

                xmlDoc.Root.Add(new XElement("LocalControllerCode", new XAttribute("id", Id),
                                new XElement("Time", DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss")),
                                new XElement("List")));

                var children = xmlDoc.Elements();
                
                XElement child1 = xmlDoc.Element("List");

                XElement child2 = (XElement)xmlDoc.Element("AMS").DescendantNodes().Last();
                foreach (var item in Devices)
                {
                    foreach (var item1 in item.Value)
                    {
                        child2.AddFirst(
                                            new XElement("Device",
                                            new XAttribute("id", item.Key),
                                            new XElement("Type", item1.Item2.TypeDevice),
                                            new XElement("Time", item1.Item1),
                                            new XElement("Value", item1.Item2.Value),
                                            new XElement("WorkTime", item1.Item2.WorkTime)));

                    }

                }



                xmlDoc.Save(xmlFileName);
                Devices.Clear();
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri ucitavanju ili upisu u AMS.xml : {e.Message}");
                return false;
            }
        }

        public void RecieveData()
        {

            XmlDocument xmlDoc = new XmlDocument();
            do {
                try
                {
                    xmlDoc.Load(@"../../../Communication/c" + Id + ".xml");
                    uslov = false;
                }
                catch
                {
                    uslov = true;
                }
            }
            while (uslov);
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/controller/Device");

            foreach (XmlNode node in nodeList)
            {
                string id = node.Attributes["id"].Value;
                string type = node.SelectSingleNode("Type").InnerText;
                string time = node.SelectSingleNode("Time").InnerText;
                string value = node.SelectSingleNode("Value").InnerText;
                string workTime = node.SelectSingleNode("WorkTime").InnerText;
                Tuple<string, ILocalDevice> temp = new Tuple<string, ILocalDevice>(time, new LocalDeviceClass(id, type, value, Double.Parse(workTime)));

                if (Devices.ContainsKey(id)) {
                    Devices[id].Add(temp);
                }
                else
                {
                    List<Tuple<string, ILocalDevice>> temp1 = new List<Tuple<string, ILocalDevice>>();
                    temp1.Add(temp);
                    Devices.Add(id, temp1);
                }

            }

            CreateRecieveXMLFile();
        }
    }
}
