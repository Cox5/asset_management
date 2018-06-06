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
        /* LocalDeviceClass : 4 metode
         * LocalDeviceClass()
         * LocalDeviceClass(string id, string typeDevice, string value, double workTime)
         * bool ListDirectory()
         * bool WriteToXML()
         * */

            // testiranje konstruktora sa dobrim parametrima LocalDeviceClass(str,str,str,double)
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


        //[Test]
        //[TestCase("01", "A", "1200", 500)]
        //[TestCase("02", "D", "1", 5000)]
        //public void LocalDeviceClassLimitParameters(string id, string typeDevice, string value, double workTime)
        //{
        //    LocalDeviceClass localDevice = new LocalDeviceClass(id, typeDevice, value, workTime);

        //    Assert.AreEqual(localDevice.Id, id);
        //    Assert.AreEqual(localDevice.TypeDevice, typeDevice);
        //    Assert.AreEqual(localDevice.Value, value);
        //    Assert.AreEqual(localDevice.WorkTime, workTime);

        //}


    }
}
