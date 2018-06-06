using AMS;
using AMS_Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSTest
{
    [TestFixture]
    public class RealTimeProcessingTest
    {
        [Test]
        public void ProcessingData(Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>> AMSDatabase, BindingList<Device> devices)
        {

        }

        [Test]
        public void DeviceContains(string controllerId, string deviceId, List<Device> devices )
        {

        }

    }
}
