using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SCN.ComputerComponents
{
    public class CPU : ComputerComponent
    {
        public CPU()
        {
            SetImage("CPU.jpg");
            UpdateInfo("Процессоры");
        }
    }
}
