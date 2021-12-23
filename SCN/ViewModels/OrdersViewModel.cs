using SCN.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
        private int _sumCount;
        private Order _selectedComponent;
        private string _orderCommand = "";
        private RelayCommand _deleteOrderCommand;
        private RelayCommand _allDeleteOrderCommand;
        private RelayCommand _closeWindowCommand;
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order> { };
        public string SourceBack { get; set; }
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
            get => _sumCount;
            set
            {
                _sumCount = value;
                OnPropertyChanged(nameof(CountOrders));
            }
        }

        public Order SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = value;
            }
        }

        public OrdersViewModel()
        {
            UpdateOrders();
            SetImage("../../img/basket.png");
        }

        public void SetImage(string path)
        {
            SourceBack = Path.GetFullPath(path);
        }

        public void UpdateOrders()
        {
            Orders.Clear();

            _sqlConnection.Open();

            _executedCommand = $"select Номер, Модель, Цена, [Тип комплектующего], [Кол-во] from Заказы where [Номер клиента] = 'kuratov'";

            SqlCommand sqlCommand = new SqlCommand(_executedCommand, _sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    string name = reader.GetValue(1) as string;
                    int price = Convert.ToInt32(reader.GetValue(2));
                    int typeComponent = Convert.ToInt32(reader.GetValue(3));
                    int count = Convert.ToInt32(reader.GetValue(4));
                    Orders.Add(new Order(name, price, typeComponent, id, count));
                    _sumCount += count;
                }
            }
            CalculateSumPrice();
            CountOrders = _sumCount;
            _sqlConnection.Close();

        }
        private void UpdateComponents()
        {
            int count = SelectedComponent.CountOrder;
            int type = SelectedComponent._typeComponent;
            string model = SelectedComponent.Name;
            model = model.Substring(model.IndexOf(" ") + 1);

            if (type == 1) { _orderCommand = $"update from [Жесткие диски] set [Кол-во] = [Кол-во] + {count} where Модель = {model} "; }
            if (type == 2) { _orderCommand = $"update from Процессоры set [Кол-во] = [Кол-во] + {count} where Модель = {model} "; }
            if (type == 3) { _orderCommand = $"update from [Блоки питания] set [Кол-во] = [Кол-во] + {count} where Модель = {model} "; }
            if (type == 4) { _orderCommand = $"update from Видеокарты where set [Кол-во] = [Кол-во] + {count} where Модель = {model} "; }
            if (type == 5) { _orderCommand = $"update from [Оперативная память] set [Кол-во] = [Кол-во] + {count} where Модель = {model} "; }
            if (type == 6) { _orderCommand = $"update from [Материнские платы] set [Кол-во] = [Кол-во] + {count} where Модель = {model} "; }
            if (type == 7) { _orderCommand = $"update from [SSD накопители] set [Кол-во] = [Кол-во] + {count} where Модель = {model} "; }

            SqlCommand sqlCommand2 = new SqlCommand(_orderCommand, _sqlConnection);
            sqlCommand2.ExecuteNonQuery();
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

                UpdateComponents();

                foreach (var order in Orders)
                {
                    _sumCount -= order.CountOrder;
                }         
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
            _sumCount = 0;

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

        public void CloseThisWindow()
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public RelayCommand DeleteOrderCommand { get => _deleteOrderCommand ?? (_deleteOrderCommand = new RelayCommand(obj => DeleteOrder())); }
        public RelayCommand AllDeleteOrderCommand { get => _allDeleteOrderCommand ?? (_allDeleteOrderCommand = new RelayCommand(obj => AllDeleteOrders())); }
        public RelayCommand CloseWindowCommand { get => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(obj => CloseThisWindow())); }

    }
}
