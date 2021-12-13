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
    public class FilterRam : IFilterComponent, INotifyPropertyChanged
    {
        private RelayCommand _filterInfoCommand;
        public RelayCommand FilterInfoCommand
        {
            get => _filterInfoCommand ?? (_filterInfoCommand = new RelayCommand(obj => FilterInfo()));
        }

        private string _filterSqlCommand;

        private string _maker;
        private string _storageType;
        private string _storage;
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

        public string StorageType
        {
            get => _storageType;
            set
            {
                _storageType = value;
                OnPropertyChanged(nameof(StorageType));
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
            FilterStorageType();
            FilterStorage();
            FilterPrice();

            if (_filterSqlCommand == "")
                _filterSqlCommand = "select * from [Оперативная память]";

            ComponentConnector.Ram.FilterInfoGlobal(_filterSqlCommand);
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(_maker))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from [Оперативная память] where Производитель like '%{_maker}%'";
                else
                    _filterSqlCommand += $" and Производитель like '%{_maker}%'";
            }
        }

        private void FilterStorageType()
        {
            if (!string.IsNullOrWhiteSpace(_storageType))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from [Оперативная память] where [Тип памяти] = '{_storageType}'";
                else
                    _filterSqlCommand += $" and [Тип памяти] = '{_storageType}'";
            }
        }

        private void FilterStorage()
        {
            if (!string.IsNullOrWhiteSpace(_storage))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from [Оперативная память] where {_storage} = [Объем]";
                else
                    _filterSqlCommand += $" and {_storage} = [Объем]";
            }
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(_startPrice) && !string.IsNullOrWhiteSpace(_lastPrice) && Convert.ToInt32(_startPrice) <= Convert.ToInt32(_lastPrice))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from [Оперативная память] where {_startPrice} <= Цена and Цена <= {_lastPrice}";
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
