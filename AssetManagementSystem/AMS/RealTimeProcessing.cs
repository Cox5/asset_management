﻿using AMS;
using AMS_Common;
using LocalDevice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AMS
{
    public static class RealTimeProcessing
    {
        public static bool uslov = true;
        public static bool uslov1 = true;
        public static List<int> controllerListUI = new List<int>();
        public static List<string> devicesListUI = new List<string>();
        public static List<Tuple<string, ILocalDevice>> tuples = new List<Tuple<string, ILocalDevice>>();
        public static Dictionary<int, Dictionary<string, List<Tuple<string, Device>>>> changesListOdDevice = new Dictionary<int, Dictionary<string, List<Tuple<string, Device>>>>(); 
        public static void ProcessingData(Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>> AMSDatabase, BindingList<Device> devices)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            do {
                try
                {
                    xmlDoc.Load(@"..\..\..\Communication\AMS.xml");
                    uslov = false;
                }
                catch
                {
                    uslov = true;
                }
            }
            while (uslov);

            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/AMS/LocalControllerCode");
                
            List<Device> newDevices = new List<Device>();
            
            List<Tuple<string, List<Tuple<string, ILocalDevice>>>> listOfTuples = null;

            foreach (XmlNode node in nodeList)
            {
                int controllerId = Convert.ToInt32(node.Attributes["id"].Value);
                string controllerTime = node.SelectSingleNode("Time").InnerText;

                XmlNodeList nodeList2 = node.ChildNodes[1].ChildNodes;

                foreach (XmlNode node2 in nodeList2)
                {
                    string deviceId = node2.Attributes["id"].Value;
                    string deviceType = node2.SelectSingleNode("Type").InnerText;
                    string deviceTime = node2.SelectSingleNode("Time").InnerText;
                    string deviceValue = node2.SelectSingleNode("Value").InnerText;
                    string deviceWorkTime = node2.SelectSingleNode("WorkTime").InnerText;

                    Device deviceExample = new Device(deviceId, deviceType, deviceValue, Convert.ToString(controllerId), deviceWorkTime);
                    if (!DeviceContains(Convert.ToString(controllerId), deviceId, newDevices)) {
                        newDevices.Add(deviceExample);
                    }
                    Tuple<string, ILocalDevice> deviceTuple = new Tuple<string, ILocalDevice>(deviceTime, new LocalDeviceClass(deviceId, deviceType, deviceValue, Double.Parse(deviceWorkTime)));
                    

                    tuples.Add(deviceTuple);

                    //Dodavanje svih promena uredjaja pod njihovim id-om....
                    if (changesListOdDevice.ContainsKey(controllerId))
                    {
                        if (changesListOdDevice[controllerId].ContainsKey(deviceId)) {
                            Device newDevice = new Device(deviceId, deviceType, deviceValue, Convert.ToString(controllerId), deviceWorkTime);
                            changesListOdDevice[controllerId][deviceId].Add(new Tuple<string, Device>(deviceTime, newDevice));
                        }
                        else
                        {
                            Device newDevice = new Device(deviceId, deviceType, deviceValue, Convert.ToString(controllerId), deviceWorkTime);
                            List<Tuple<string, Device>> list = new List<Tuple<string, Device>>() { new Tuple<string, Device>(deviceTime, newDevice) };
                            
                            changesListOdDevice[controllerId].Add(deviceId, list);
                        }
                    }
                    else
                    {
                        Device newDevice = new Device(deviceId, deviceType, deviceValue, Convert.ToString(controllerId), deviceWorkTime);
                        List<Tuple<string, Device>> list = new List<Tuple<string, Device>>() { new Tuple<string, Device>(deviceTime, newDevice) };

                        Dictionary<string, List<Tuple<string, Device>>> dict = new Dictionary<string, List<Tuple<string, Device>>>();
                        dict.Add(deviceId, list);
                        changesListOdDevice.Add(controllerId, dict);
                    }

                }

                if (!AMSDatabase.ContainsKey(controllerId))
                {
                    listOfTuples = new List<Tuple<string, List<Tuple<string, ILocalDevice>>>>() { new Tuple<string, List<Tuple<string, ILocalDevice>>>(controllerTime, tuples) };
                    AMSDatabase.Add(controllerId, listOfTuples);
                }
                else
                {
                    AMSDatabase[controllerId].Add(new Tuple<string, List<Tuple<string, ILocalDevice>>>(controllerTime, tuples));
                }

                // Logika za dodavanje kontrolera u combo box, tj. da ne postoje duplikati u drop down listi
                if (!controllerListUI.Contains(controllerId))
                {
                    controllerListUI.Add(controllerId);
                }

                for (int i = 0; i < tuples.Count; i++)
                {
                    if (!devicesListUI.Contains(tuples[i].Item2.Id))
                    {
                        devicesListUI.Add(tuples[i].Item2.Id);
                    }
                }

            }
            CreateRecieveXmlFile(); //Posle iscitavanja podataka, kreira se novi fajl (stare vrednosti se pregaze odnosno izbrisu)
            RefreshUserInterface(devices, newDevices);
        }

        public static void CreateRecieveXmlFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("AMS");

            doc.AppendChild(root);
            do
            {
                try
                {
                    doc.Save(@"..\..\..\Communication\AMS.xml");
                    uslov1 = false;
                }
                catch
                {
                    uslov1 = true;
                }
            }
            while (uslov1);
        }

        public static void RefreshUserInterface(BindingList<Device> devices, List<Device> newValues)
        {

            devices.Clear();
            foreach (var item in newValues)
            {
                devices.Add(item);
            }
            
        }


        public static bool DeviceContains(string controllerId, string deviceId, List<Device> devices)
        {
            bool retValue = false;

            foreach (var item in devices)
            {
                if (String.Equals( item.Configuration, controllerId) && String.Equals(item.Id, deviceId))
                {
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }
    }
}
