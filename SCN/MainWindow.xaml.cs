using SCN.Windows;

namespace SCN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AuthorizationWindow aw = new AuthorizationWindow();
            aw.ShowDialog();
            this.Close();
        }
    }
}