using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{
    public class Device
    {
        public string Id { get; set; }
        public string TypeDevice { get; set; }
        public string Value { get; set; }
        public string Configuration { get; set; }
        public double WorkTime { get; set; }

        public Device(String Id, String TypeDevice, String Value, String Configuration, String WorkTime)
        {
            this.Id = Id;
            this.TypeDevice = TypeDevice;
            this.Value = Value;
            this.Configuration = Configuration;
            this.WorkTime = Double.Parse(WorkTime);
        }
    }
}
