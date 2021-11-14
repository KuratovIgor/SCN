using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCN.ComputerComponents
{
    public class PSU : ComputerComponent
    {
        public PSU()
        {
            SetImage("psu.jpg");
            UpdateInfo("Блоки питания");
        }
    }
}
