using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SCN.AdminVersion.Windows;
using SCN.ViewModels;

namespace SCN.AdminVersion.ViewModels
{
    public class MainAdminViewModel : MainMenuViewModel
    {
        private RelayCommand _openClientWindowCommand;
        private RelayCommand _showAllOrdersCommand;

        private void OpenClientWindow()
        {
            Window w = new ClientWindow();
            w.ShowDialog();
        }

        private void OpenAllOrders()
        {
            Window w = new AllOrdersWindow();
            w.ShowDialog();
        }

        public RelayCommand OpenClientWindowCommand
        {
            get => _openClientWindowCommand ?? 
                   (_openClientWindowCommand = new RelayCommand(obj => OpenClientWindow()));
        }

        public RelayCommand ShowAllOrdersCommand
        {
            get => _showAllOrdersCommand ?? 
                   (_showAllOrdersCommand = new RelayCommand(obj => OpenAllOrders()));
        }
    }
}
