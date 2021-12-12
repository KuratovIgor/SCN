using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCN.ComputerComponents
{
    public class PSU : ComputerComponent
    {
        private RelayCommand _filterInfoCommand;
        private RelayCommand _addOrderCommand;
        private RelayCommand _removeCommand;

        private string _filterCommand = "";
        private string _orderCommand = "";
        private string _removeSqlCommand = "";

        private string _maker;
        private string _formFactor;
        private string _power;
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

        public string Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
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

        public PSU()
        {
            SetImage("../../img/psu.jpg");
            UpdateInfo("Блоки питания");
        }

        private void AddPSU()
        {
            string maker = (SelectedComponent as DataRowView).Row.ItemArray[1].ToString();
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            string resModel = maker + " " + model;
            int price = Convert.ToInt32((SelectedComponent as DataRowView).Row.ItemArray[5]);

            _orderCommand = $"insert into Заказы values ('kuratov', '3', '{resModel}', {price})";

            AddOrder(_orderCommand);
        }

        private void RemoveCpu()
        {
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            _removeSqlCommand = $"delete from [Блоки питания] where Модель = '{model}'";
            RemoveProduct(_removeSqlCommand);
            UpdateInfo("Блоки питания");
        }

        private void FilterInfo()
        {
            FilterMaker();
            FilterFormFactor();
            FilterPower();
            FilterPrice();

            if (_filterCommand == "")
                _filterCommand = "select * from [Блоки питания]";

            FilterTheInfo(_filterCommand);

            _filterCommand = "";
        }

        private void FilterMaker()
        {
            if (!string.IsNullOrWhiteSpace(Maker))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Блоки питания] where Производитель like '%{Maker}%'";
                else
                    _filterCommand += $" and Производитель like '%{Maker}%'";
            }
        }

        private void FilterFormFactor()
        {
            if (!string.IsNullOrWhiteSpace(FormFactor))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Блоки питания] where [Форм-фактор] = '{FormFactor}'";
                else
                    _filterCommand += $" and [Форм-фактор] = '{FormFactor}'";
            }
        }

        private void FilterPower()
        {
            if (!string.IsNullOrWhiteSpace(Power))
                _filterCommand = $"select * from [Блоки питания] where Мощность = {Power}";
        }

        private void FilterPrice()
        {
            if (!string.IsNullOrWhiteSpace(StartPrice) && !string.IsNullOrWhiteSpace(LastPrice) && Convert.ToInt32(StartPrice) <= Convert.ToInt32(LastPrice))
            {
                if (_filterCommand == "")
                    _filterCommand = $"select * from [Блоки питания] where {StartPrice} <= Цена and Цена <= {LastPrice}";
                else
                    _filterCommand += $" and {StartPrice} <= Цена and Цена <= {LastPrice}";
            }
        }

        public RelayCommand FilterInfoCommand
        {
            get => _filterInfoCommand ?? (_filterInfoCommand = new RelayCommand(obj => FilterInfo()));
        }
        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddPSU())); }
        public RelayCommand RemoveCommand { get => _removeCommand ?? (_removeCommand = new RelayCommand(obj => RemoveCpu())); }
    }
}
