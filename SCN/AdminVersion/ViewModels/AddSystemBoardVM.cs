using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents.DocumentStructures;

namespace SCN.AdminVersion.ViewModels
{
    public class AddSystemBoardVM : BaseAddVM
    {
        private string _maker;
        private string _model;
        private string _formFactor;
        private string _socket;
        private string _chipset;
        private string _storageType;
        private int _countSata;
        private int _count;
        private int _price;

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

        public string FormFactor
        {
            get => _formFactor;
            set
            {
                _formFactor = value;
                OnPropertyChanged(nameof(FormFactor));
            }
        }

        public string Socket
        {
            get => _socket;
            set
            {
                _socket = value;
                OnPropertyChanged(nameof(Socket));
            }
        }

        public string Chipset
        {
            get => _chipset;
            set
            {
                _chipset = value;
                OnPropertyChanged(nameof(Chipset));
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

        public int CountSata
        {
            get => _countSata;
            set
            {
                _countSata = value;
                OnPropertyChanged(nameof(CountSata));
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

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        protected override void PurchaseProduct()
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            try
            {
                string command = $"insert into [Материнские платы] values ('{Maker}', '{Model}', '{FormFactor}', '{Socket}', '{Chipset}', '{StorageType}', {CountSata}, {Count}, {Price})";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Материнские платы закуплены и добавлены на склад!");

                ComponentConnector.SystemBoard.UpdateInfo("Материнские платы");
            }
            catch (Exception) { }

            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
