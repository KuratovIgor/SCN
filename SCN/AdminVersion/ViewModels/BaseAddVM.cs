using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SCN.AdminVersion.ViewModels
{
    public class BaseAddVM : INotifyPropertyChanged
    {
        private RelayCommand _addComponentCommand;
        private RelayCommand _purchaseCommand;

        private int _selectedNumber;

        protected SqlConnection sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        protected string component = "";

        public ObservableCollection<int> NumberOfComponent { get; set; } = new ObservableCollection<int> { };

        public int SelectedNumber
        {
            get => _selectedNumber;
            set
            {
                _selectedNumber = value;
                OnPropertyChanged(nameof(SelectedNumber));
            }
        }


        protected void UpdateNumberCollection(string component)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            NumberOfComponent.Clear();

            string command = $"select Код from [{component}]";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    NumberOfComponent.Add(Convert.ToInt32(reader.GetValue(0)));
                }
            }

            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        protected virtual void AddComponent()
        {
            sqlConnection.Open();

            string command = $"update [{component}] set [Кол-во] = [Кол-во] + 1 where Код = {SelectedNumber}";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            MessageBox.Show("Товар добавлен на склад!");

            sqlConnection.Close();
        }

        protected virtual void PurchaseProduct() { }

        public RelayCommand AddComponentCommand { get => _addComponentCommand ?? (_addComponentCommand = new RelayCommand(obj => AddComponent())); }
        public RelayCommand PurchaseCommand { get => _purchaseCommand ?? (_purchaseCommand = new RelayCommand(obj => PurchaseProduct())); }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
