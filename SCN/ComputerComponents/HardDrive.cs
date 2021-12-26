using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SCN.AdminVersion.Windows;
using SCN.Filter;

namespace SCN.ComputerComponents
{
    public class HardDrive : ComputerComponent
    {
        protected SqlConnection _sqlConnection =
           new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        protected string _executedCommand;

        private RelayCommand _addOrderCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _topUpCommand;
        private RelayCommand _purchaseCommand;
        private RelayCommand _updateHardCommand;

        private string _orderCommand = "";
        private string _removeSqlCommand = "";
        private string _sqlCommand = "";

        private object _selectedComponent;

        public object SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
            }
        }

        public HardDrive()
        {
            SetImage("../../img/harddrive.jpg");
            UpdateInfo("Жесткие диски");

            ComponentConnector.Hdd = this;
        }

        private bool IsDuplicate()
        {
            string maker = (SelectedComponent as DataRowView).Row.ItemArray[1].ToString();
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            string resModel = maker + " " + model;

            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            _executedCommand = $"select Модель from Заказы where [Номер клиента] = '{User.Login}'";

            SqlCommand sqlCommand = new SqlCommand(_executedCommand, _sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetValue(0) as string;

                    if (name == resModel)
                        return true;

                }
            }

            if (_sqlConnection.State != ConnectionState.Closed)
                _sqlConnection.Close();

            return false;
        }
        private void AddHardDrive()
        {
            try
            {
                string maker = (SelectedComponent as DataRowView).Row.ItemArray[1].ToString();
                string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
                int count_SAS = Convert.ToInt32((SelectedComponent as DataRowView).Row.ItemArray[6]);
                int count = 1;
                string resModel = maker + " " + model;
                int price = Convert.ToInt32((SelectedComponent as DataRowView).Row.ItemArray[5]);

                if (count_SAS == 0)
                {
                    MessageBox.Show("Нет в наличии!");
                }
                else
                {
                    if (IsDuplicate() == true)
                        _orderCommand = $"update Заказы set [Кол-во] = [Кол-во] + 1, Цена = Цена + {price} where Модель = '{resModel}' ";
                    else
                        _orderCommand = $"insert into Заказы values ('{User.Login}', '1', '{resModel}', {price}, {count})";
                    AddOrder(_orderCommand);
                    UpdateHardDrive();
                    UpdateInfo("Жесткие диски");
                }
            }
            catch(Exception)
            {

            }
                     
        }

        private void UpdateHardDrive()
        {
            string model = (SelectedComponent as DataRowView).Row.ItemArray[2].ToString();
            _sqlCommand = $"update [Жесткие диски] set [Кол-во] = [Кол-во] - 1 where Модель = '{model}' ";
            UpdateComponents(_sqlCommand);
        }

        private void Remove()
        {
            RemoveProduct("Жесткие диски", SelectedComponent as DataRowView);
        }

        private void TopUp()
        {
            TopUpProduct("Жесткие диски", SelectedComponent as DataRowView);
        }

        private void OpenPurchaseWindow()
        {
            Window w = new PayHddWindow();
            w.Show();
        }

        public RelayCommand AddOrderCommand { get => _addOrderCommand ?? (_addOrderCommand = new RelayCommand(obj => AddHardDrive())); }
        public RelayCommand RemoveCommand { get => _removeCommand ?? (_removeCommand = new RelayCommand(obj => Remove())); }
        public RelayCommand TopUpCommand { get => _topUpCommand ?? (_topUpCommand = new RelayCommand(obj => TopUp())); }
        public RelayCommand PurchaseCommand { get => _purchaseCommand ?? (_purchaseCommand = new RelayCommand(obj => OpenPurchaseWindow())); }
        public RelayCommand UpdateHardCommand { get => _updateHardCommand ?? (_updateHardCommand = new RelayCommand(obj => UpdateHardDrive())); }
    }
}
