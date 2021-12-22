using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using SCN.Windows;

namespace SCN.ViewModels
{
    public class MainMenuViewModel
    {
        private RelayCommand _exitToStartWindowCommand;
        private RelayCommand _exitCommand;
        private RelayCommand _openComputerComponentsCommand;
        private RelayCommand _openOrdersWindowCommand;
        private RelayCommand _openConfiguratorCommand;


        public string SourceUri
        {
            get => Path.GetFullPath("../../img/mainimage.jpg");
        }

        public RelayCommand ExitToStartWindowCommand
        {
            get => _exitToStartWindowCommand ?? 
                   (_exitToStartWindowCommand = new RelayCommand(obj => ExitToStartWindow()));
        }

        public RelayCommand ExitCommand
        {
            get => _exitCommand ?? 
                   (_exitCommand = new RelayCommand(obj => Exit()));
        }

        public RelayCommand OpenComputerComponentsCommand
        {
            get => _openComputerComponentsCommand ?? 
                   (_openComputerComponentsCommand = new RelayCommand(obj => OpenComputerComponents()));
        }

        public RelayCommand OpenOrdersWindowCommand
        {
            get => _openOrdersWindowCommand ??
                   (_openOrdersWindowCommand = new RelayCommand(obj => OpenOrdersWindow()));
        }

        public RelayCommand OpenConfiguratorCommand
        {
            get => _openConfiguratorCommand ??
                   (_openConfiguratorCommand = new RelayCommand(obj => OpenConfigurator()));
        }

        private void OpenComputerComponents()
        {
            Window w = new ComputerComponentsWindow();
            w.ShowDialog();
        }

        private void OpenOrdersWindow()
        {
            Window w = new OrdersWindow();
            w.ShowDialog();
        }

        private void ExitToStartWindow()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Close();

            AuthorizationWindow aw = new AuthorizationWindow();
            aw.ShowDialog();
        }

        private void Exit()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Close();
        }

        private void OpenConfigurator()
        {
            ChoosePcWindow w = new ChoosePcWindow();
            w.ShowDialog();
        }
    }
}