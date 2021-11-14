using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SCN.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace SCN.ViewModels
{
    public class AuthorizationViewModel : DependencyObject
    {
        private SqlConnection _sqlConnection = 
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        private static readonly DependencyProperty LoginProperty = 
            DependencyProperty.Register("Login", typeof(string), typeof(AuthorizationViewModel));
        private static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(AuthorizationViewModel));

        private RelayCommand _entryAsClientCommand;
        private RelayCommand _registrationCommand;
        private RelayCommand _entryAsAdminCommand;

        public string Login
        {
            get => (string) GetValue(LoginProperty);
            set => SetValue(LoginProperty, value);
        }

        public string Password
        {
            get => (string) GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public AuthorizationViewModel()
        {
            _sqlConnection.Open();
        }

        private void EntryAsClient()
        {
            //bool isUserExists = false;

            //string command = $"select * from Client";
            //SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection);

            //using (SqlDataReader reader = sqlCommand.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        if ((reader.GetValue(0) as string) == Login && (reader.GetValue(1) as string) == Password)
            //        {
            //            isUserExists = true;

            //            User.Login = Login;
            //            User.Password = Password;
            //            User.FIO = reader.GetValue(2) as string;
            //            User.PhoneNumber = reader.GetValue(3) as string;
            //            User.IsAdmin = Convert.ToInt32(reader.GetValue(4));

            //            break;
            //        }
            //    }
            //}

            //if (isUserExists)
            //{
                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                window.Close();

                MainMenuWindow mw = new MainMenuWindow();
                mw.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Неверный логин или пароль!");
            //}
        }

        private void RegisterClient()
        {
            RegisterWindow w = new RegisterWindow();
            w.ShowDialog();
        }
        
        private void EntryAsAdmin() { }
        
        
        
        public RelayCommand EntryAsClientCommand
        {
            get => _entryAsClientCommand ?? 
                   (_entryAsClientCommand = new RelayCommand(obj => EntryAsClient()));
        }
        
        public RelayCommand RegistrationCommand
        {
            get => _registrationCommand ??
                   (_registrationCommand = new RelayCommand(obj => RegisterClient()));
        }

        public RelayCommand EntryAsAdminCommand
        {
            get => _entryAsAdminCommand ?? 
                   (_entryAsAdminCommand = new RelayCommand(obj => EntryAsAdmin()));
        }
    }
}