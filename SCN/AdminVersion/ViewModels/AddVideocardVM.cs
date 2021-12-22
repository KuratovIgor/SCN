using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SCN.AdminVersion.ViewModels
{
    public class AddVideocardVM : BaseAddVM
    {
        private string _maker;
        private string _model;
        private string _storageType;
        private int _videoStorage;
        private string _interface;
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

        public string StorageType
        {
            get => _storageType;
            set
            {
                _storageType = value;
                OnPropertyChanged(nameof(StorageType));
            }
        }

        public int VideoStorage
        {
            get => _videoStorage;
            set
            {
                _videoStorage = value;
                OnPropertyChanged(nameof(VideoStorage));
            }
        }

        public string Interface
        {
            get => _interface;
            set
            {
                _interface = value;
                OnPropertyChanged(nameof(Interface));
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
                string command = $"insert into Видеокарты values ('{Maker}', '{Model}', '{StorageType}', {VideoStorage}, '{Interface}', {Price}, {Count})";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Видеокарты закуплены и добавлены на склад!");

                ComponentConnector.Videocard.UpdateInfo("Видеокарты");
            }
            catch (Exception) { }

            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
