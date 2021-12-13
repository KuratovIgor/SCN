using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SCN.ComputerComponents;

namespace SCN.Filter
{
    public class FilterSsd : IFilterComponent, INotifyPropertyChanged
    {
        private RelayCommand _filterInfoCommand;
        public RelayCommand FilterInfoCommand
        {
            get => _filterInfoCommand ?? (_filterInfoCommand = new RelayCommand(obj => FilterInfo()));
        }

        private string _filterSqlCommand;

        private string _maker;
        private string _storage;
        private string _connectionInterface;
        private string _startPrice;
        private string _lastPrice;

        public string Maker
        {
            get => _maker;
            set
            {
                _maker = value;
                OnPropertyChanged(nameof(Maker));
            }
        }

        public string Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                OnPropertyChanged(nameof(Storage));
            }
        }

        public string ConnectionInterface
        {
            get => _connectionInterface;
            set
            {
                _connectionInterface = value;
                OnPropertyChanged(nameof(ConnectionInterface));
            }
        }

        public string StartPrice
        {
            get => _startPrice;
            set
            {
                _startPrice = value;
                OnPropertyChanged(nameof(StartPrice));
            }
        }

        public string LastPrice
        {
            get => _lastPrice;
            set
            {
                _lastPrice = value;
                OnPropertyChanged(nameof(LastPrice));
            }
        }

        public void FilterInfo()
        {
            _filterSqlCommand = "";

            FilterMaker();
            FilterStorage();
            FilterConnectionInterface();
            FilterPrice();

            if (_filterSqlCommand == "")
                _filterSqlCommand = "select * from [SSD Накопители]";

            ComponentConnector.Ssd.FilterInfoGlobal(_filterSqlCommand);
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(_maker))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from [SSD Накопители] where Производитель like '%{_maker}%'";
                else
                    _filterSqlCommand += $" and Производитель like '%{_maker}%'";
            }
        }

        private void FilterStorage()
        {
            if (!string.IsNullOrWhiteSpace(_storage))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from [SSD Накопители] where '{_storage}' = [Объем]";
                else
                    _filterSqlCommand += $" and '{_storage}' = [Объем]";
            }
        }

        private void FilterConnectionInterface()
        {
            if (!string.IsNullOrWhiteSpace(_connectionInterface))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from [SSD Накопители] where [Физический интерфейс] = '{_connectionInterface}'";
                else
                    _filterSqlCommand += $" and [Физический интерфейс] = '{_connectionInterface}'";
            }
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(_startPrice) && !string.IsNullOrWhiteSpace(_lastPrice) && Convert.ToInt32(_startPrice) <= Convert.ToInt32(_lastPrice))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from [SSD Накопители] where {_startPrice} <= Цена and Цена <= {_lastPrice}";
                else
                    _filterSqlCommand += $" and {_startPrice} <= Цена and Цена <= {_lastPrice}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
