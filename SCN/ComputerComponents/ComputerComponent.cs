using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Xml.Serialization;

namespace SCN.ComputerComponents
{
    public abstract class ComputerComponent : INotifyPropertyChanged
    {
        private string _visible;
        private string _adminVisible;

        protected SqlConnection _sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        public string SourceUri { get; set; }

        private DataTable _component;
        public DataTable ComponentInfo
        {
            get => _component;
            set
            {
                _component = value;
                OnPropertyChanged(nameof(ComponentInfo));
            }
        }

        public string Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }

        public string AdminVisible
        {
            get => _adminVisible;
            set
            {
                _adminVisible = value;
                OnPropertyChanged(nameof(AdminVisible));
            }
        }

        public ComputerComponent()
        {
            _sqlConnection.Open();

            if (User.IsAdmin == 1)
            {
                Visible = "Hidden";
                AdminVisible = "Visible";
            }
            else
            {
                Visible = "Visible";
                AdminVisible = "Hidden";
            }
        }

        public void SetImage(string path)
        {
            SourceUri = Path.GetFullPath(path);
        }

        public void UpdateInfo(string nameComponent)
        {
            string executedCommand = $"select * from [{nameComponent}]";

            if (ComponentInfo == null)
                ComponentInfo = new DataTable();

            ComponentInfo.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(executedCommand, _sqlConnection);
            adapter.Fill(ComponentInfo);
        }

        public void FilterInfoGlobal(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection);

            ComponentInfo.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(command, _sqlConnection);
            adapter.Fill(ComponentInfo);
        }

        protected void AddOrder(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }

        protected void RemoveProduct(string componentName, DataRowView selectedComponent)
        {
            try
            {
                string model = selectedComponent.Row.ItemArray[2].ToString();
                string command = $"delete from [{componentName}] where Модель = '{model}'";
                SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection);
                sqlCommand.ExecuteNonQuery();
                UpdateInfo(componentName);
            }
            catch(Exception) { }
        }

        protected void TopUpProduct(string componentName, DataRowView selectedComponent)
        {
            try
            {
                string command = $"update [{componentName}] set [Кол-во] = [Кол-во] + 1 where Код = {Convert.ToInt32(selectedComponent.Row.ItemArray[0])}";
                SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection);
                sqlCommand.ExecuteNonQuery();
                UpdateInfo(componentName);
            }
            catch(Exception) { }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
