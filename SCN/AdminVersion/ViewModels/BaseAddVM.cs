using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SCN.AdminVersion.ViewModels
{
    public class BaseAddVM
    {
        protected SqlConnection sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        public ObservableCollection<int> NumberOfComponent { get; set; } = new ObservableCollection<int> { };

        protected void Update(string component)
        {
            sqlConnection.Open();

            string command = $"select Код from {component}";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    NumberOfComponent.Add(Convert.ToInt32(reader.GetValue(0)));
                }
            }

            sqlConnection.Close();
        }
    }
}
