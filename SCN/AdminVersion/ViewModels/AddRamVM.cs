using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SCN.AdminVersion.ViewModels
{
    public class AddRamVM : BaseAddVM
    {
        private string _maker;
        private string _model;
        private string _storageType;
        private int _storage;
        private int _price;
        private int _count;

        public string Maker
        {
            get => _maker;
            set
            {
                _maker = value;
                OnPropertyChanged(nameof(Maker));
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string StorageType
        {
            get => _storageType;
            set
            {
                _storageType = value;
                OnPropertyChanged(nameof(StorageType));
            }
        }

        public int Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                OnPropertyChanged(nameof(Storage));
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        protected override void PurchaseProduct()
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            try
            {
                string command = $"insert into [Оперативная память] values ('{Maker}', '{Model}', '{StorageType}', {Storage}, {Price}, {Count})";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Оперативная память закуплена и добавлена на склад!");

                ComponentConnector.Ram.UpdateInfo("Оперативная память");
            }
            catch (Exception) { }

            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
