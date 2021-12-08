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
        private RelayCommand _addOrderCommand;

        private string _filterCommand = "";
        private string _orderCommand = "";

        private string _maker;
        private string _formFactor;
        private string _storageType;
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

        public object SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
                //OnPropertyChanged(nameof(SelectedComponent));
            }
        }


        public SystemBoard()
        {
            SetImage("../../img/systemboard.jpg");
            UpdateInfo("Материнские платы");
        }

        private void AddSystemBoard()
        {
            string maker = (SelectedComponent as DataRowView).Row.ItemArray[1].ToString();
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            string resModel = maker + " " + model;
            int price = Convert.ToInt32((SelectedComponent as DataRowView).Row.ItemArray[8]);

            _orderCommand = $"insert into Заказы values ('kuratov', '6', '{resModel}', {price})";

            AddOrder(_orderCommand);
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

        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddSystemBoard())); }
    }
}
