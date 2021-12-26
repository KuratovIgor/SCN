using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SCN.AdminVersion.ViewModels
{
    public class ClientVM : INotifyPropertyChanged
    {
        private SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        private DataTable _clients;
        //private object _selectedClient;

        public DataTable Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }

        //public object SelectedClient
        //{
        //    get => _selectedClient;
        //    set
        //    {
        //        _selectedClient = value;
        //        OnPropertyChanged(nameof(SelectedClient));
        //    }
        //}

        public ClientVM()
        {
            UpdateClients();
        }

        private void UpdateClients()
        {
            _sqlConnection.Open();

            if (Clients == null)
                Clients = new DataTable();

            string command = "select FIO, Phone from Client";
            SqlDataAdapter adapter = new SqlDataAdapter(command, _sqlConnection);
            adapter.Fill(Clients);

            _sqlConnection.Close();
        }




        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
