using AMS;
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
        public static List<int> controllerListUI = new List<int>();
        public static List<string> devicesListUI = new List<string>();
        public static List<Tuple<string, ILocalDevice>> tuples = new List<Tuple<string, ILocalDevice>>();
        public static void ProcessingData(Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>> AMSDatabase, BindingList<Device> devices)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"..\..\..\Communication\AMS.xml");

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
                RefreshUserInterface(devices, newDevices);
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
                if (item.Configuration == controllerId && item.Id==deviceId)
                {
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }
    }
}
