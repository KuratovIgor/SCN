using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCN.ComputerComponents
{
    public class HardDrive : ComputerComponent
    {
        public HardDrive()
        {
            SetImage("harddrive.jpg");
            UpdateInfo("Жесткие диски");
        }
    }
}
