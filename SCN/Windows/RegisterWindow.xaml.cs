using System.Windows;
using SCN.ViewModels;

namespace SCN.Windows
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }
    }
}