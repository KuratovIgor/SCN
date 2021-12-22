using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SCN.AdminVersion.Windows;

namespace SCN.ViewModels
{
    public class ComputerComponentsViewModel : INotifyPropertyChanged
    {
        private string _visible;

        private RelayCommand _exitCommand;
        private RelayCommand _addProductCommand;

        public RelayCommand ExitCommand
        {
            get => _exitCommand ?? 
                   (_exitCommand = new RelayCommand(obj => Exit()));
        }

        public string Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }

        public ComputerComponentsViewModel()
        {
            if (User.IsAdmin == 1)
                Visible = "Visible";
            else
                Visible = "Hidden";
        }

        private void Exit()
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
