using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCN.ComputerComponents;

namespace SCN
{
    public static class ComponentConnector
    {
        public static ComputerComponent Cpu { get; set; }
        public static ComputerComponent Hdd { get; set; }
        public static ComputerComponent Psu { get; set; }
        public static ComputerComponent Ram { get; set; }
        public static ComputerComponent Ssd { get; set; }
        public static ComputerComponent SystemBoard { get; set; }
        public static ComputerComponent Videocard { get; set; }
    }
}
