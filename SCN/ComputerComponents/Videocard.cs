using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace SCN.ComputerComponents
{
    public class Videocard : ComputerComponent
    {
        public Videocard()
        {
            SetImage("videocard.jpg");
            UpdateInfo("Видеокарты");
        }
    }
}
