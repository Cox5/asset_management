using LocalDevice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeviceTest
{
    [TestFixture]
    public class LocalDeviceClassTest
    {
        [Test]
        [TestCase("01", "A", "1200", 500)]
        [TestCase("02", "D", "1", 5000)]
        public void LocalDeviceClassGoodParameters(string id, string typeDevice, string value, double workTime)
        {
            LocalDeviceClass localDevice = new LocalDeviceClass(id, typeDevice, value, workTime);

            Assert.AreEqual(localDevice.Id, id);
            Assert.AreEqual(localDevice.TypeDevice, typeDevice);
            Assert.AreEqual(localDevice.Value, value);
            Assert.AreEqual(localDevice.WorkTime, workTime);

        }


        [Test]
        [TestCase("111111111", "a", "0", 1000000)]
        [TestCase("000000000", "d", "0", 1000000)]
        public void LocalDeviceClassLimitParameters(string id, string typeDevice, string value, double workTime)
        {
            LocalDeviceClass localDevice = new LocalDeviceClass(id, typeDevice, value, workTime);

            Assert.AreEqual(localDevice.Id, id);
            Assert.AreEqual(localDevice.TypeDevice, typeDevice);
            Assert.AreEqual(localDevice.Value, value);
            Assert.AreEqual(localDevice.WorkTime, workTime);

        }


    }
}
