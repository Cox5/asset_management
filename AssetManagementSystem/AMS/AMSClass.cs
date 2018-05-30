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
            AMSDatabase = new Dictionary<int, List<Tuple<string, List<Tuple<string, ILocalDevice>>>>>();
        }
    }


}
