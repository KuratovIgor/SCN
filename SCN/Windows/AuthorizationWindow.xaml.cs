using System.Windows;
using SCN.ViewModels;

namespace SCN.Windows
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            DataContext = new AuthorizationViewModel();
        }
    }
}