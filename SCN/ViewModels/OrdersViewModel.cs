using SCN.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SCN.ViewModels
{
    public class OrdersViewModel
    {
        protected SqlConnection _sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        protected string _executedCommand;

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order> { };

        public OrdersViewModel()
        {
            _sqlConnection.Open();

            _executedCommand = $"select Модель, Цена, [Тип комплектующего] from Заказы where [Номер клиента] = 'kuratov'";
            SqlCommand sqlCommand = new SqlCommand(_executedCommand, _sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while(reader.Read())
                {
                    string name = reader.GetValue(0) as string;
                    int price = Convert.ToInt32(reader.GetValue(1));
                    int typeComponent = Convert.ToInt32(reader.GetValue(2));
                    Orders.Add(new Order(name, price, typeComponent));         
                }
            }

            _sqlConnection.Close();

        }


    }
}
