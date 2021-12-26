using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SCN.AdminVersion.ViewModels
{
    public class AllOrdersVM : INotifyPropertyChanged
    {
        private SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        private DataTable _orders;

        public DataTable Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public AllOrdersVM()
        {
            UpdateOrders();
        }

        private void UpdateOrders()
        {
            _sqlConnection.Open();

            if (Orders == null)
                Orders = new DataTable();

            string command = "select FIO, Phone, [Название комплетующего], Модель, Цена, [Кол-во] from Client inner join Заказы on Client.Login = Заказы.[Номер клиента] inner join Комплектующие on Комплектующие.id = Заказы.[Тип комплектующего]";
            SqlDataAdapter adapter = new SqlDataAdapter(command, _sqlConnection);
            adapter.Fill(Orders);

            _sqlConnection.Close();
        }




        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
