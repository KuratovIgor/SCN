using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SCN.AdminVersion.ViewModels;

namespace SCN.AdminVersion.Windows
{
    /// <summary>
    /// Логика взаимодействия для PayHddWindow.xaml
    /// </summary>
    public partial class PayHddWindow : Window
    {
        public PayHddWindow(AddHddVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
