using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SCN.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using SCN.AdminVersion.Windows;

namespace SCN.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        private SqlConnection _sqlConnection = 
            new SqlConnection(ConfigurationManager.ConnectionStrings["SCNDB"].ConnectionString);

        private RelayCommand _entryAsClientCommand;
        private RelayCommand _registrationCommand;
        private RelayCommand _entryAsAdminCommand;

        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public AuthorizationViewModel()
        {
            _sqlConnection.Open();
        }

        private void EntryAsClient()
        {
            try
            {
                AuthorizeClient();

                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                window.Close();

                Window mw = new MainMenuWindow();
                mw.ShowDialog();
            }
            catch(Exception)
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private void AuthorizeClient()
        {
            bool isUserExists = false;

            string command = $"select * from Client";
            SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    if ((reader.GetValue(0) as string) == Login && (reader.GetValue(1) as string) == Password)
                    {
                        isUserExists = true;

                        User.Login = Login;
                        User.Password = Password;
                        User.FIO = reader.GetValue(2) as string;
                        User.PhoneNumber = reader.GetValue(3) as string;
                        User.IsAdmin = Convert.ToInt32(reader.GetValue(4));
                    }
                }
            }

            if (isUserExists == false)
                throw new Exception("Authorization error");
        }

        private void RegisterClient()
        {
            Window w = new RegisterWindow();
            w.ShowDialog();
        }

        private void EntryAsAdmin()
        {
            try
            {
                AuthorizeClient();

                if (User.IsAdmin == 1)
                {
                    var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    window.Close();

                    Window wn = new MainAdminWindow();
                    wn.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Пользователь не имеет прав администратора!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }
        
        
        
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



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}