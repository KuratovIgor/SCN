﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCN.ComputerComponents
{
    public class HardDrive : ComputerComponent
    {
        private RelayCommand _filterInfoCommand;
        private RelayCommand _addOrderCommand;

        private string _filterCommand = "";
        private string _orderCommand = "";

        private string _maker;
        private string _storage;
        private string _startPrice;
        private string _lastPrice;
        private object _selectedComponent;


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

        public object SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
                //OnPropertyChanged(nameof(SelectedComponent));
            }
        }

        public HardDrive()
        {
            SetImage("../../img/harddrive.jpg");
            UpdateInfo("Жесткие диски");
        }

        private void AddHardDrive()
        {
            string maker = (SelectedComponent as DataRowView).Row.ItemArray[1].ToString();
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            string resModel = maker + " " + model;
            int price = Convert.ToInt32((SelectedComponent as DataRowView).Row.ItemArray[5]);

            _orderCommand = $"insert into Заказы values ('kuratov', '1', '{resModel}', {price})";

            AddOrder(_orderCommand);
        }

        private void FilterInfo()
        {
            FilterMaker();
            FilterStorage();
            FilterPrice();

            if (_filterCommand == "")
                _filterCommand = "select * from [Жесткие диски]";

            FilterTheInfo(_filterCommand);

            _filterCommand = "";
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(Maker))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Жесткие диски] where Производитель like '%{Maker}%'";
                else
                    _filterCommand += $" and Производитель like '%{Maker}%'";
            }
        }

        private void FilterStorage()
        {
            if (!string.IsNullOrWhiteSpace(Storage))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Жесткие диски] where {Storage} = [Объем HDD]";
                else
                    _filterCommand += $" and {Storage} = [Объем HDD]";
            }
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(StartPrice) && !string.IsNullOrWhiteSpace(LastPrice) && Convert.ToInt32(StartPrice) <= Convert.ToInt32(LastPrice))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Жесткие диски] where {StartPrice} <= Цена and Цена <= {LastPrice}";
                else
                    _filterCommand += $" and {StartPrice} <= Цена and Цена <= {LastPrice}";
            }
        }

        public RelayCommand FilterInfoCommand
        {
            get => _filterInfoCommand ?? (_filterInfoCommand = new RelayCommand(obj => FilterInfo()));
        }

        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddHardDrive())); }
    }
}
