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
using SCN.Filter;

namespace SCN.ComputerComponents
{
    public class CPU : ComputerComponent
    {
        private RelayCommand _addOrderCommand;
        private RelayCommand _removeCommand;

        private string _orderSqlCommand = "";
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

        public CPU()
        {
            SetImage("../../img/CPU.jpg");
            UpdateInfo("Процессоры");

            ComponentConnector.Cpu = this;
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

        private void Remove()
        {
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            _removeSqlCommand = $"delete from Процессоры where Модель = '{model}'";
            RemoveProduct(_removeSqlCommand);
            UpdateInfo("Процессоры");
        }

        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddCPU())); }
        public RelayCommand RemoveCommand { get => _removeCommand ?? (_removeCommand = new RelayCommand(obj => Remove())); }
    }
}
