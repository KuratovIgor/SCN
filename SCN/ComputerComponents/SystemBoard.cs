using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;

namespace SCN.ComputerComponents
{
    public class SystemBoard : ComputerComponent
    {
        private RelayCommand _filterInfoCommand;

        private string _filterCommand = "";

        private string _maker;
        private string _formFactor;
        private string _storageType;
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

        public string FormFactor
        {
            get => _formFactor;
            set
            {
                _formFactor = value;
                OnPropertyChanged(nameof(FormFactor));
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

        public SystemBoard()
        {
            SetImage("systemboard.jpg");
            UpdateInfo("Материнские платы");
        }

        private void FilterInfo()
        {
            FilterMaker();
            FilterFormFactor();
            FilterStorageType();
            FilterPrice();

            if (_filterCommand == "")
                _filterCommand = "select * from [Материнские платы]";

            FilterTheInfo(_filterCommand);

            _filterCommand = "";
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(Maker))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Материнские платы] where Производитель like '%{Maker}%'";
                else
                    _filterCommand += $" and Производитель like '%{Maker}%'";
            }
        }

        private void FilterFormFactor()
        {
            if (!string.IsNullOrWhiteSpace(FormFactor))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Материнские платы] where [Форм-фактор] = '{FormFactor}'";
                else
                    _filterCommand += $" and [Форм-фактор] = '{FormFactor}'";
            }
        }

        private void FilterStorageType()
        {
            if (!string.IsNullOrWhiteSpace(StorageType))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Материнские платы] where [Тип памяти] = '{StorageType}'";
                else
                    _filterCommand += $" and [Тип памяти] = '{StorageType}'";
            }
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(StartPrice) && !string.IsNullOrWhiteSpace(LastPrice) && Convert.ToInt32(StartPrice) <= Convert.ToInt32(LastPrice))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Материнские платы] where {StartPrice} <= Цена and Цена <= {LastPrice}";
                else
                    _filterCommand += $" and {StartPrice} <= Цена and Цена <= {LastPrice}";
            }
        }

        public RelayCommand FilterInfoCommand
        {
            get => _filterInfoCommand ?? (_filterInfoCommand = new RelayCommand(obj => FilterInfo()));
        }
    }
}
