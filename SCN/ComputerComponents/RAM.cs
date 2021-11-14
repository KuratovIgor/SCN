using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCN.ComputerComponents
{
    public class RAM : ComputerComponent
    {
        public RAM()
        {
            SetImage("ram.jpg");
            UpdateInfo("Оперативная память");
        }
    }
}
