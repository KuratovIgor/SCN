using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using  System.Data.SqlClient;
using System.Configuration;

namespace SCN.ViewModels
{
    public class RegisterViewModel : DependencyObject
    {
        private SqlConnection _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        private static readonly DependencyProperty LoginProperty = DependencyProperty.Register("Login", typeof(string), typeof(RegisterViewModel));
        private static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(RegisterViewModel));
        private static readonly DependencyProperty FIOProperty = DependencyProperty.Register("FIO", typeof(string), typeof(RegisterViewModel));
        private static readonly DependencyProperty PhoneProperty = DependencyProperty.Register("Phone", typeof(string), typeof(RegisterViewModel));

        private RelayCommand _continueCommand;

        public string Login
        {
            get => GetValue(LoginProperty) as string;
            set => SetValue(LoginProperty, value);
        }

        public string Password
        {
            get => GetValue(PasswordProperty) as string;
            set => SetValue(PasswordProperty, value);
        }

        public string FIO
        {
            get => GetValue(FIOProperty) as string;
            set => SetValue(FIOProperty, value);
        }

        public string Phone
        {
            get => GetValue(PhoneProperty) as string;
            set => SetValue(PhoneProperty, value);
        }

        public RelayCommand ContinueCommand { get => _continueCommand ?? (_continueCommand = new RelayCommand(obj => Continue())); }

        private void Continue()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password) ||
                    string.IsNullOrWhiteSpace(FIO) || string.IsNullOrWhiteSpace(Phone))
                {
                    throw new Exception("Заполнены не все данные!");
                }
                _sqlConnection.Open();

                using (_sqlConnection.OpenAsync())
                {
                    string command = $"insert into Client values (N'{Login}', N'{Password}', N'{FIO}', N'{Phone}', 0)";
                    SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Вы зарегистрированы!");

                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                window.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
