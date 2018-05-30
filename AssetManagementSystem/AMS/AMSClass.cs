using AMS_Common;
using LocalDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AMS
{
    public class AMSClass
    {
        public Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>> AMSDatabase { get; set; }

        public AMSClass()
        {
            
        }

    
    //// Parse XML from Controller and add data to dictionary
    //public void ParseXML()
    //    {
    //        XmlDocument xmlDoc = new XmlDocument();
    //        xmlDoc.Load(@"..\..\..\Communication\AMS.xml");

    //        XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/AMS/LocalControllerCode");
    //        XmlNodeList nodeList2 = xmlDoc.DocumentElement.SelectNodes("/AMS/LocalControllerCode/List/Device");


    //        foreach (XmlNode node in nodeList)
    //        {
    //            int controllerId = Convert.ToInt32(node.Attributes["id"].Value);
    //            string controllerTime = node.SelectSingleNode("Time").InnerText;

    //            foreach (XmlNode node2 in nodeList2)
    //            {
    //                string deviceId = node2.Attributes["id"].Value;
    //                string deviceType = node2.SelectSingleNode("Type").InnerText;
    //                string deviceTime = node2.SelectSingleNode("Time").InnerText;
    //                string deviceValue = node2.SelectSingleNode("Value").InnerText;
    //                string deviceWorkTime = node2.SelectSingleNode("WorkTime").InnerText;

    //                //if (AMSDatabase.ContainsKey(deviceId))
    //                //{
    //                //    Tuple<string, ILocalDevice> deviceTuple = new Tuple<string, ILocalDevice>(deviceTime, new LocalDeviceClass(deviceId, deviceType, deviceValue, Double.Parse(deviceWorkTime)));
    //                //    AMSDatabase[deviceId].Add()
    //                //}
    //                //else
    //                //{
    //                //    //Tuple<string, ILocalDevice> temp = new Tuple<string, ILocalDevice>(time, new LocalDeviceClass(id, type, value, Double.Parse(workTime)));
    //                //    List<Tuple<string, List<Tuple<string, ILocalDevice>>>> lista = new List<Tuple<string, List<Tuple<string, ILocalDevice>>>>();
    //                //    AMSDatabase.Add(deviceId, lista);
    //                //}

    //                //Tuple<string, ILocalDevice> temp = new Tuple<string, ILocalDevice>(time, new LocalDeviceClass(id, type, value, Double.Parse(workTime)));
    //                //Dictionary<string, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>> AMSDatabase;

    //                Tuple<string, ILocalDevice> deviceTuple = new Tuple<string, ILocalDevice>(deviceTime, new LocalDeviceClass(deviceId, deviceType, deviceValue, Double.Parse(deviceWorkTime)));
    //                List<Tuple<string, ILocalDevice>> tuples = new List<Tuple<string, ILocalDevice>>
    //                {
    //                    deviceTuple
    //                };
    //                Tuple<string, List<Tuple<string, ILocalDevice>>> controllerTuple = new Tuple<string, List<Tuple<string, ILocalDevice>>>(controllerTime, tuples);
    //                List<Tuple<string, List<Tuple<string, ILocalDevice>>>> listOfTuples = new List<Tuple<string, List<Tuple<string, ILocalDevice>>>>
    //                {
    //                    controllerTuple
    //                };

    //                AMSDatabase = new Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>>(50);
    //                AMSDatabase.Add(controllerId, listOfTuples);
    //            }
    //            //string type = node.SelectSingleNode("Device")
    //        }



        //}
    }


}
