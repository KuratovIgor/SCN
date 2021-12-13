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
using SCN.Filter;

namespace SCN.ComputerComponents
{
    public class Videocard : ComputerComponent
    {
        private RelayCommand _addOrderCommand;
        private RelayCommand _removeCommand;
        private string _removeSqlCommand = "";

        private string _orderCommand = "";

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

        public Videocard()
        {
            SetImage("../../img/videocard.jpg");
            UpdateInfo("Видеокарты");

            ComponentConnector.Videocard = this;
        }

        private void AddVideocard()
        {
            string maker = (SelectedComponent as DataRowView).Row.ItemArray[1].ToString();
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            string resModel = maker + " " + model;
            int price = Convert.ToInt32((SelectedComponent as DataRowView).Row.ItemArray[6]);

            _orderCommand = $"insert into Заказы values ('kuratov', '4', '{resModel}', {price})";

            AddOrder(_orderCommand);
        }

        private void Remove()
        {
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            _removeSqlCommand = $"delete from Видеокарты where Модель = '{model}'";
            RemoveProduct(_removeSqlCommand);
            UpdateInfo("Видеокарты");
        }

        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddVideocard())); }
        public RelayCommand RemoveCommand { get => _removeCommand ?? (_removeCommand = new RelayCommand(obj => Remove())); }
    }
}
