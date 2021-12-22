using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SCN.ComputerComponents;

namespace SCN.Filter
{
    public class FilterCpu : INotifyPropertyChanged, IFilterComponent
    {
        private string _filterSqlCommand = "";

        private RelayCommand _filterInfoCommand;
        public RelayCommand FilterInfoCommand { get => _filterInfoCommand ?? (_filterInfoCommand = new RelayCommand(obj => FilterInfo())); }

        private string _maker;
        private string _countCores;
        private string _startFrequency;
        private string _lastFrequency;
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

        public string CountCores
        {
            get => _countCores;
            set
            {
                _countCores = value;
                OnPropertyChanged(nameof(CountCores));
            }
        }

        public string StartFrequency
        {
            get => _startFrequency;
            set
            {
                _startFrequency = value;
                OnPropertyChanged(nameof(StartFrequency));
            }
        }

        public string LastFrequency
        {
            get => _lastFrequency;
            set
            {
                _lastFrequency = value;
                OnPropertyChanged(nameof(LastFrequency));
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
            FilterCores();
            FilterFrequency();
            FilterPrice();

            if (_filterSqlCommand == "")
                _filterSqlCommand = "select * from Процессоры";

            ComponentConnector.Cpu.FilterInfoGlobal(_filterSqlCommand);
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(_maker))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from Процессоры where Производитель like '%{_maker}%'";
                else
                    _filterSqlCommand += $" and Производитель like '%{_maker}%'";
            }
        }

        private void FilterCores()
        {
            if (!string.IsNullOrWhiteSpace(_countCores))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from Процессоры where [Кол-во ядер] = {_countCores}";
                else
                    _filterSqlCommand += $" and [Кол-во ядер] = {_countCores}";
            }
        }

        private void FilterFrequency()
        {
            if (!string.IsNullOrWhiteSpace(_startFrequency) && !string.IsNullOrWhiteSpace(_lastFrequency) && Convert.ToInt32(_startFrequency) <= Convert.ToInt32(_lastFrequency))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from Процессоры where {_startFrequency} <= Частота and Частота <= {_lastFrequency}";
                else
                    _filterSqlCommand += $" and {_startFrequency} <= Частота and Частота <= {_lastFrequency}";
            }
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(_startPrice) && !string.IsNullOrWhiteSpace(_lastPrice) && Convert.ToInt32(_startPrice) <= Convert.ToInt32(_lastPrice))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from Процессоры where {_startPrice} <= Цена and Цена <= {_lastPrice}";
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
