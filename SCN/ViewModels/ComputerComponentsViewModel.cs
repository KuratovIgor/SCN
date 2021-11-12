using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SCN.ViewModels
{
    public class ComputerComponentsViewModel
    {
        private RelayCommand _exitCommand;

        public RelayCommand ExitCommand
        {
            get => _exitCommand ?? 
                   (_exitCommand = new RelayCommand(obj => Exit()));
        }

        private void Exit()
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Close();
        }
    }
}
