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
    public class AddPsuVM : BaseAddVM
    {
        private string _maker;
        private string _model;
        private string _formfactor;
        private int _power;
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

        public string FormFactor
        {
            get => _formfactor;
            set
            {
                _formfactor = value;
                OnPropertyChanged(nameof(FormFactor));
            }
        }

        public int Power
        {
            get => _power;
            set
            {
                _power = value;
                OnPropertyChanged(nameof(_power));
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
                string command = $"insert into [Блоки питания] values ('{Maker}', '{Model}', '{FormFactor}', {Power}, {Price}, {Count})";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Блоки питания закуплены и добавлены на склад!");

                ComponentConnector.Psu.UpdateInfo("Блоки питания");
            }
            catch (Exception) { }

            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
