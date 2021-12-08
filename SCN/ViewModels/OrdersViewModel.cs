using SCN.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace SCN.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        protected SqlConnection _sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        protected string _executedCommand;
        private int _sumPrice;

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

        public OrdersViewModel()
        {
            _sqlConnection.Open();

            _executedCommand = $"select Модель, Цена, [Тип комплектующего] from Заказы where [Номер клиента] = 'kuratov'";

            SqlCommand sqlCommand = new SqlCommand(_executedCommand, _sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetValue(0) as string;
                    int price = Convert.ToInt32(reader.GetValue(1));
                    int typeComponent = Convert.ToInt32(reader.GetValue(2));
                    Orders.Add(new Order(name, price, typeComponent));
                }
            }

            _sqlConnection.Close();

            CalculateSumPrice();
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

    }
}
