﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using SCN.AdminVersion.Windows;

namespace SCN.AdminVersion.ViewModels
{
    public class AddCpuVM : BaseAddVM
    {
        private string _maker;
        private string _model;
        private string _socket;
        private int _cores;
        private int _freq;
        private string _storageType;
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

        public string Socket
        {
            get => _socket;
            set
            {
                _socket = value;
                OnPropertyChanged(nameof(Socket));
            }
        }

        public int Cores
        {
            get => _cores;
            set
            {
                _cores = value;
                OnPropertyChanged(nameof(Cores));
            }
        }

        public int Frequency
        {
            get => _freq;
            set
            {
                _freq = value;
                OnPropertyChanged(nameof(Frequency));
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
                string command = $"insert into Процессоры values ('{Maker}', '{Model}', '{Socket}', {Cores}, {Frequency}, '{StorageType}', {Price}, {Count})";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Процессоры закуплены и добавлены на склад!");

                ComponentConnector.Cpu.UpdateInfo("Процессоры");
            }
            catch (Exception) { }

            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
