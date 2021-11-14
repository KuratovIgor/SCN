using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;

namespace SCN.ComputerComponents
{
    public class SystemBoard : ComputerComponent
    {
        public SystemBoard()
        {
            SetImage("systemboard.jpg");
            UpdateInfo("Материнские платы");
        }
    }
}
