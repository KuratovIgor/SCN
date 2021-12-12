using SCN.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SCN.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        protected SqlConnection _sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        protected string _executedCommand;
        private int _sumPrice;
        private int _countOrders;
        private Order _selectedComponent;
        private string _orderCommand = "";
        private RelayCommand _deleteOrderCommand;
        private RelayCommand _allDeleteOrderCommand;
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order> { };
        public int SumPrice
        {
            get => _sumPrice;
            set
            {
                _sumPrice = value;
                OnPropertyChanged(nameof(SumPrice));
            }
        }

        public int CountOrders
        {
            get => Orders.Count;
            set
            {
                _countOrders = value;
                OnPropertyChanged(nameof(CountOrders));
            }
        }

        public Order SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
                //OnPropertyChanged(nameof(SelectedComponent));
            }
        }

        public OrdersViewModel()
        {
            UpdateOrders();
        }
     
        public void UpdateOrders()
        {
            Orders.Clear();

            _sqlConnection.Open();

            _executedCommand = $"select Номер, Модель, Цена, [Тип комплектующего] from Заказы where [Номер клиента] = 'kuratov'";

            SqlCommand sqlCommand = new SqlCommand(_executedCommand, _sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    string name = reader.GetValue(1) as string;
                    int price = Convert.ToInt32(reader.GetValue(2));
                    int typeComponent = Convert.ToInt32(reader.GetValue(3));
                    Orders.Add(new Order(name, price, typeComponent, id));
                }
            }
            CalculateSumPrice();
            CountOrders = Orders.Count;
            _sqlConnection.Close();

        }

        public void DeleteOrder()
        {
            try
            {
                _sqlConnection.Open();
                int id = SelectedComponent.Id;

                _orderCommand = $"delete from Заказы where [Номер клиента] = 'kuratov' and Номер = {id} ";

                SqlCommand sqlCommand = new SqlCommand(_orderCommand, _sqlConnection);
                sqlCommand.ExecuteNonQuery();

                _sqlConnection.Close();

                UpdateOrders();
            }
            catch (Exception)
            {
                if (_sqlConnection.State == ConnectionState.Open)
                    _sqlConnection.Close();
            }
        }

        public void AllDeleteOrders()
        {
            _sqlConnection.Open();

            _orderCommand = $"delete from Заказы where [Номер клиента] = 'kuratov' ";

            SqlCommand sqlCommand = new SqlCommand(_orderCommand, _sqlConnection);
            sqlCommand.ExecuteNonQuery();

            _sqlConnection.Close();

            UpdateOrders();
        }

        public void CalculateSumPrice()
        {
            SumPrice = 0;
            foreach (var order in Orders)
            {
                SumPrice += order.Price;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public RelayCommand DeleteOrderCommand { get => _deleteOrderCommand ?? (_deleteOrderCommand = new RelayCommand(obj => DeleteOrder())); }
        public RelayCommand AllDeleteOrderCommand { get => _allDeleteOrderCommand ?? (_allDeleteOrderCommand = new RelayCommand(obj => AllDeleteOrders())); }

    }
}
