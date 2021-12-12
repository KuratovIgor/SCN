using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Web.UI.WebControls;

namespace SCN.ComputerComponents
{
    public class CPU : ComputerComponent
    {
        private RelayCommand _filterInfoCommand;
        private RelayCommand _addOrderCommand;
        private RelayCommand _removeCommand;

        private string _filterSqlCommand = "";
        private string _orderSqlCommand = "";
        private string _removeSqlCommand = "";


        private string _maker;
        private string _countCores;
        private string _startFrequency;
        private string _lastFrequency;
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

        public object SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
                //OnPropertyChanged(nameof(SelectedComponent));
            }
        }

        public CPU()
        {
            SetImage("../../img/CPU.jpg");
            UpdateInfo("Процессоры");
        }

        private void AddCPU()
        {
            string maker = (SelectedComponent as DataRowView).Row.ItemArray[1].ToString();
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            string resModel = maker + " " + model;
            int price = Convert.ToInt32((SelectedComponent as DataRowView).Row.ItemArray[7]);

            _orderSqlCommand = $"insert into Заказы values ('kuratov', '2', '{resModel}', {price})";

            AddOrder(_orderSqlCommand);
        }

        private void RemoveCpu()
        {
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            _removeSqlCommand = $"delete from Процессоры where Модель = '{model}'";
            RemoveProduct(_removeSqlCommand);
            UpdateInfo("Процессоры");
        }

        private void FilterInfo()
        {
            FilterMaker();
            FilterCores();
            FilterFrequency();
            FilterPrice();

            if (_filterSqlCommand == "")
                _filterSqlCommand = "select * from Процессоры";

            FilterTheInfo(_filterSqlCommand);

            _filterSqlCommand = "";
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(Maker))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from Процессоры where Производитель like '%{Maker}%'";
                else
                    _filterSqlCommand += $" and Производитель like '%{Maker}%'";
            }
        }

        private void FilterCores()
        {
            if (!string.IsNullOrWhiteSpace(CountCores))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from Процессоры where [Кол-во ядер] = {CountCores}";
                else
                    _filterSqlCommand += $" and [Кол-во ядер] = {CountCores}";
            }
        }

        private void FilterFrequency()
        {
            if (!string.IsNullOrWhiteSpace(StartFrequency) && !string.IsNullOrWhiteSpace(LastFrequency) && Convert.ToInt32(StartFrequency) <= Convert.ToInt32(LastFrequency))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from Процессоры where {StartFrequency} <= Частота and Частота <= {LastFrequency}";
                else
                    _filterSqlCommand += $" and {StartFrequency} <= Частота and Частота <= { LastFrequency}";
            }
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(StartPrice) && !string.IsNullOrWhiteSpace(LastPrice) && Convert.ToInt32(StartPrice) <= Convert.ToInt32(LastPrice))
            {
                if (_filterSqlCommand == "")
                    _filterSqlCommand = $"select * from Процессоры where {StartPrice} <= Цена and Цена <= {LastPrice}";
                else
                    _filterSqlCommand += $" and {StartPrice} <= Цена and Цена <= {LastPrice}";
            }
        }

        public RelayCommand FilterInfoCommand { get => _filterInfoCommand ?? (_filterInfoCommand = new RelayCommand(obj => FilterInfo())); }
        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddCPU())); }
        public RelayCommand RemoveCommand { get => _removeCommand ?? (_removeCommand = new RelayCommand(obj => RemoveCpu())); }
    }
}
