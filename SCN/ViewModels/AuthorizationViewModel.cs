namespace SCN.ViewModels
{
    public class AuthorizationViewModel
    {
        private RelayCommand _entryAsClientCommand;
        private RelayCommand _registrationCommand;
        private RelayCommand _entryAsAdminCommand;
        
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

        private void EntryAsClient() { }
        
        private void RegisterClient() { }
        
        private void EntryAsAdmin() { }
    }
}