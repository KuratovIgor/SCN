using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SCN.Filter;

namespace SCN.ComputerComponents
{
    public class RAM : ComputerComponent
    {
        private RelayCommand _addOrderCommand;
        private RelayCommand _removeCommand;

        private string _orderCommand = "";
        private string _removeSqlCommand = "";

        private object _selectedComponent;

        public object SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
                //OnPropertyChanged(nameof(SelectedComponent));
            }
        }

        public RAM()
        {
            SetImage("../../img/ram.jpg");
            UpdateInfo("Оперативная память");

            ComponentConnector.Ram = this;
        }

        private void AddRAM()
        {
            string maker = (SelectedComponent as DataRowView).Row.ItemArray[1].ToString();
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            string resModel = maker + " " + model;
            int price = Convert.ToInt32((SelectedComponent as DataRowView).Row.ItemArray[5]);

            _orderCommand = $"insert into Заказы values ('kuratov', '5', '{resModel}', {price})";

            AddOrder(_orderCommand);
        }

        private void Remove()
        {
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            _removeSqlCommand = $"delete from [Оперативная память] where Модель = '{model}'";
            RemoveProduct(_removeSqlCommand);
            UpdateInfo("Оперативная память");
        }

        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddRAM())); }
        public RelayCommand RemoveCommand { get => _removeCommand ?? (_removeCommand = new RelayCommand(obj => Remove())); }
    }
}
