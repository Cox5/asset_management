﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS_Common
{
    public interface ILocalControler
    {
        string Id { get; set; }
        Dictionary<string, List<Tuple<string, ILocalDevice>>> Devices { get; set; }
    }
}
