using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCN.Filter;

namespace SCN.ComputerComponents
{
    public class HardDrive : ComputerComponent
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

        public HardDrive()
        {
            SetImage("../../img/harddrive.jpg");
            UpdateInfo("Жесткие диски");

            ComponentConnector.Hdd = this;
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

        private void Remove()
        {
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            _removeSqlCommand = $"delete from [Жесткие диски] where Модель = '{model}'";
            RemoveProduct(_removeSqlCommand);
            UpdateInfo("Жесткие диски");
        }

        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddHardDrive())); }
        public RelayCommand RemoveCommand { get => _removeCommand ?? (_removeCommand = new RelayCommand(obj => Remove())); }
    }
}
