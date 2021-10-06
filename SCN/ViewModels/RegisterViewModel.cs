namespace SCN.ViewModels
{
    public class RegisterViewModel
    {
        private RelayCommand _registerCommand;

        public RelayCommand RegisterCommand
        {
            get => _registerCommand ??
                   (_registerCommand = new RelayCommand(obj => RegisterClient()));
        }

        private void RegisterClient() { }
    }
}