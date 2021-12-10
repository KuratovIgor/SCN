using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SCN.Models;
using RadioButton = System.Web.UI.WebControls.RadioButton;

namespace SCN.ViewModels
{
    public class ConfiguratorViewModel : INotifyPropertyChanged
    {
        private bool _isForOffice;
        private bool _isForHome;
        private bool _isForGames;
        private int _price;

        private Assembly _newAssembly;

        private Assembly NewAssembly
        {
            get => _newAssembly;
            set
            {
                _newAssembly = value;
                OnPropertyChanged(nameof(NewAssembly));
            }
        }

        public bool IsForOffice
        {
            get => _isForOffice;
            set
            {
                _isForOffice = value;
                OnPropertyChanged(nameof(IsForOffice));
            }
        }

        public bool IsForDevelopment
        {
            get => _isForHome;
            set
            {
                _isForHome = value;
                OnPropertyChanged(nameof(IsForDevelopment));
            }
        }

        public bool IsForGames
        {
            get => _isForGames;
            set
            {
                _isForGames = value;
                OnPropertyChanged(nameof(IsForGames));
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private RelayCommand _createCommand;

        public RelayCommand CreateCommand
        {
            get => _createCommand ??
                   (_createCommand = new RelayCommand(obj => CreateConf()));
        }

        private void CreateConf()
        {
            if (IsForOffice)
                NewAssembly = Configurator.ConfigurateForOffice();
            if (IsForDevelopment)
                NewAssembly = Configurator.ConfigurateForDevelopment();
            if (IsForGames)
                NewAssembly = Configurator.ConfigurateForGames();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
