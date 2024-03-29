﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SCN.Models
{
    public class Order
    {
        protected SqlConnection _sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        protected string _executedCommand;

        private RelayCommand _addCountOrderCommand;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string SourceUri { get; set; }
        public int CountOrder { get; set; }
        public int _typeComponent { get; set; }

        public Order(string name, int price, int typeComponent, int id, int count)
        {
            Name = name;
            Price = price;
            _typeComponent = typeComponent;
            Id = id;
            CountOrder = count;
            LoadImage();
        }

        public void LoadImage()
        {
            _sqlConnection.Open();

            _executedCommand = $"select [Название комплетующего] from Комплектующие where id = {_typeComponent}";
            SqlCommand sqlCommand = new SqlCommand(_executedCommand, _sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                reader.Read();
                SourceUri = Path.GetFullPath($"../../img/{reader.GetValue(0) as string}.jpg");
            }

            _sqlConnection.Close();
        }
    }
}
