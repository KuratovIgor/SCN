﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SCN.AdminVersion.Windows;

namespace SCN.AdminVersion.ViewModels
{
    public class AddHddVM : BaseAddVM
    {
        private string _maker;
        private string _model;
        private int _size;
        private string _interface;
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

        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
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
                string command = $"insert into [Жесткие диски] values ('{Maker}', '{Model}', {Size}, '{Interface}', {Price}, {Count})";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Жесткие диски закуплены и добавлены на склад!");

                ComponentConnector.Hdd.UpdateInfo("Жесткие диски");
            }
            catch (Exception) { }

            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
