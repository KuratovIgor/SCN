using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCN.ComputerComponents;

namespace SCN.Models
{
    public class Assembly
    {
        public CPU Cpu { get; set; }
        public HardDrive Hdd { get; set; }
        public PSU Psu { get; set; }
        public  RAM Ram { get; set; }
        public SSD Ssd { get; set; }
        public SystemBoard SysBoard { get; set; }
        public Videocard VidCard { get; set; }
    }
}
