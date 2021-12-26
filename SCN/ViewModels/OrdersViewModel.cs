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

        private string _orderCommand = "";
        private int _sumPrice;
        private int _sumCount;      
       
        private Order _selectedComponent;
        private RelayCommand _deleteOrderCommand;
        private RelayCommand _allDeleteOrderCommand;
        private RelayCommand _closeWindowCommand;
        private RelayCommand _addCountOrderCommand;
        private RelayCommand _placeOrderCommand;
        private RelayCommand _substructCountOrderCommand;
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

            _executedCommand = $"select Номер, Модель, Цена, [Тип комплектующего], [Кол-во] from Заказы where [Номер клиента] = '{User.Login}'";

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

        private bool IsСomponentNotExist(string model)
        {
            _executedCommand = $"select [Кол-во], Модель from (select [Кол-во], Модель from Процессоры" +
                $" union select [Кол-во], Модель from Видеокарты" +
                $" union select [Кол-во], Модель from[Оперативная память] " +
                $"union select [Кол-во], Модель from[Жесткие диски] " +
                $"union select [Кол-во], Модель from[Материнские платы] " +
                $"union select [Кол-во], Модель from[SSD Накопители] " +
                $"union select [Кол-во], Модель from[Блоки питания]) as Comp where Модель = '{model}'";

            SqlCommand sqlCommand = new SqlCommand(_executedCommand, _sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int count = Convert.ToInt32(reader.GetValue(0));
                    if (count <= 0)
                        return true;
                }
            }
            return false;
        }
        private void ReturnComponentsFromBasket()
        {
            _executedCommand = $"select [Кол-во], [Тип комплектующего], Модель from Заказы where [Номер клиента] = '{User.Login}'";

            SqlCommand sqlCommand3 = new SqlCommand(_executedCommand, _sqlConnection);

            using (SqlDataReader reader = sqlCommand3.ExecuteReader())
            {
                _orderCommand = "";
                while (reader.Read())
                {               
                    int count = Convert.ToInt32(reader.GetValue(0));
                    int type = Convert.ToInt32(reader.GetValue(1));
                    string model = reader.GetValue(2) as string;
                    string nameTable = "";
                    model = model.Substring(model.IndexOf(" ") + 1);

                    if (type == 1) { nameTable = "[Жесткие диски]"; }
                    if (type == 2) { nameTable = "Процессоры"; }
                    if (type == 3) { nameTable = "[Блоки питания]"; }
                    if (type == 4) { nameTable = "Видеокарты"; }
                    if (type == 5) { nameTable = "[Оперативная память]"; }
                    if (type == 6) { nameTable = "[Материнские платы]"; }
                    if (type == 7) { nameTable = "[SSD накопители]"; }

                    _orderCommand += $"update {nameTable} set [Кол-во] = [Кол-во] + {count} where Модель = '{model}'; ";
                }
            }
            SqlCommand sqlCommand2 = new SqlCommand(_orderCommand, _sqlConnection);
            sqlCommand2.ExecuteNonQuery();
        }
        private void ReturnComponentFromBasket(char znak, int count)
        {
            
            int type = SelectedComponent._typeComponent;
            string model = SelectedComponent.Name;
            string nameTable = "";
            model = model.Substring(model.IndexOf(" ") + 1);
            

            if (type == 1) { nameTable = "[Жесткие диски]"; }
            if (type == 2) { nameTable = "Процессоры"; }
            if (type == 3) { nameTable = "[Блоки питания]"; }
            if (type == 4) { nameTable = "Видеокарты"; }
            if (type == 5) { nameTable = "[Оперативная память]"; }
            if (type == 6) { nameTable = "[Материнские платы]"; }
            if (type == 7) { nameTable = "[SSD накопители]"; }

            _orderCommand = $"update {nameTable} set [Кол-во] = [Кол-во] {znak} {count} where Модель = '{model}' ";
            SqlCommand sqlCommand = new SqlCommand(_orderCommand, _sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public void DeleteOrder()
        {
            try
            {
                if (_sqlConnection.State != ConnectionState.Open)
                    _sqlConnection.Open();
          
                int id = SelectedComponent.Id;

                _orderCommand = $"delete from Заказы where [Номер клиента] = '{User.Login}' and Номер = {id} ";
                int count = SelectedComponent.CountOrder;
                _orderCommand = $"delete from Заказы where [Номер клиента] = '{User.Login}' and Номер = {id} ";

                SqlCommand sqlCommand = new SqlCommand(_orderCommand, _sqlConnection);
                sqlCommand.ExecuteNonQuery();

                ReturnComponentFromBasket('+', count);

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
            try
            {
                _sqlConnection.Open();
                ReturnComponentsFromBasket();
                _orderCommand = $"delete from Заказы where [Номер клиента] = '{User.Login}' ";
                SqlCommand sqlCommand = new SqlCommand(_orderCommand, _sqlConnection);
                sqlCommand.ExecuteNonQuery();

                _sumCount = 0;

                _sqlConnection.Close();
                UpdateOrders();
            }
            catch (Exception)
            {
                if (_sqlConnection.State == ConnectionState.Open)
                    _sqlConnection.Close();
            }
        }

        public void PlaceOrder()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            _orderCommand = $"delete from Заказы where [Номер клиента] = '{User.Login}' ";

            SqlCommand sqlCommand = new SqlCommand(_orderCommand, _sqlConnection);
            sqlCommand.ExecuteNonQuery();
            _sumCount = 0;

            _sqlConnection.Close();

            UpdateOrders();

            MessageBox.Show("Заказ успешно оформлен!");
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

        public void AddCountOrder()
        {
            try
            {
                _sqlConnection.Open();
                string model = SelectedComponent.Name;
                string modelBuf = model.Substring(model.IndexOf(" ") + 1);

                _orderCommand = $"update Заказы set [Кол-во] = [Кол-во] + 1, Цена = Цена + (Цена / [Кол-во]) where Модель = '{model}'; ";

                if (IsСomponentNotExist(modelBuf) == false)
                {
                    SqlCommand sqlCommand = new SqlCommand(_orderCommand, _sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    ReturnComponentFromBasket('-', 1);
                    _sumCount = 0;

                    if (_sqlConnection.State != ConnectionState.Closed)
                        _sqlConnection.Close();

                    UpdateOrders();
                }
                else
                {
                    MessageBox.Show("Товара больше нет в наличии!");
                }
            }
            catch (Exception)
            {
                if (_sqlConnection.State == ConnectionState.Open)
                    _sqlConnection.Close();
            }
        }

        public void SubstructCountOrder()
        {  
           try
           {
                _sqlConnection.Open();
                string model = SelectedComponent.Name;
                int count = SelectedComponent.CountOrder;
                _orderCommand = $"update Заказы set [Кол-во] = [Кол-во] - 1, Цена = Цена - (Цена / [Кол-во]) where Модель = '{model}' ";
                
                if (count > 1)
                {
                    SqlCommand sqlCommand = new SqlCommand(_orderCommand, _sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    ReturnComponentFromBasket('+', 1);
                }
                else
                {
                    DeleteOrder();
                }
                _sumCount = 0;
                _sqlConnection.Close();

                UpdateOrders();
            } 
            catch(Exception) 
            {
                if (_sqlConnection.State == ConnectionState.Open)
                    _sqlConnection.Close();
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
        public RelayCommand PlaceOrderCommand { get => _placeOrderCommand ?? (_placeOrderCommand = new RelayCommand(obj => PlaceOrder())); }
        public RelayCommand CloseWindowCommand { get => _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(obj => CloseThisWindow())); }
        public RelayCommand AddCountOrderCommand { get => _addCountOrderCommand ?? (_addCountOrderCommand = new RelayCommand(obj => AddCountOrder())); }
        public RelayCommand SubstructCountOrderCommand { get => _substructCountOrderCommand ?? (_substructCountOrderCommand = new RelayCommand(obj => SubstructCountOrder())); }
    }
}
