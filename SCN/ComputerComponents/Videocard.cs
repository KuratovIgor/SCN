using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace SCN.ComputerComponents
{
    public class Videocard : ComputerComponent
    {
        private RelayCommand _filterInfoCommand;

        private string _filterCommand = "";

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

        public Videocard()
        {
            SetImage("videocard.jpg");
            UpdateInfo("Видеокарты");
        }

        private void FilterInfo()
        {
            FilterMaker();
            FilterStorageType();
            FilterStorage();
            FilterPrice();

            if (_filterCommand == "")
                _filterCommand = "select * from [Видеокарты]";

            FilterTheInfo(_filterCommand);

            _filterCommand = "";
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(Maker))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Видеокарты] where Производитель like '%{Maker}%'";
                else
                    _filterCommand += $" and Производитель like '%{Maker}%'";
            }
        }

        private void FilterStorageType()
        {
            if (!string.IsNullOrWhiteSpace(StorageType))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Видеокарты] where [Тип памяти] = '{StorageType}'";
                else
                    _filterCommand += $" and [Тип памяти] = '{StorageType}'";
            }
        }

        private void FilterStorage()
        {
            if (!string.IsNullOrWhiteSpace(Storage))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Видеокарты] where '{Storage}' = [Объем видеопамяти]";
                else
                    _filterCommand += $" and '{Storage}' = [Объем видеопамяти]";
            }
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(StartPrice) && !string.IsNullOrWhiteSpace(LastPrice) && Convert.ToInt32(StartPrice) <= Convert.ToInt32(LastPrice))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Видеокарты] where {StartPrice} <= Цена and Цена <= {LastPrice}";
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
