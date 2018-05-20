using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS_Common
{
    public interface ILocalDevice
    {
        string Id { get; set; }
        string TypeDevice { get; set; }
        string Value { get; set; }
        //U stringu se nalazi naziv fajla u koji uredjaj upisuje promene (u zavisnosti kako je konfigurisan)
        string Configuration { get; set; }
    }
}
