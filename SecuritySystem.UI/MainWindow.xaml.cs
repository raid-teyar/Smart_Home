using SecuritySystem.UI.Controls;
using SecuritySystem.UI.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SecuritySystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LoginView LoginContext { get; set; }
        public DashboardView DashboardView { get; set; }

        public MainWindow(LoginView loginView)
        {
            LoginContext = loginView;
            InitializeComponent();
            DataContext = this;
            NavBarList.SelectedItem = NavBarList.Items[0];

            tbFullName.Text = LoginContext.LoggedUser.FullName;
            tbEmail.Text = LoginContext.LoggedUser.Email;
        }

        private void ThemeChecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("Themes/Dark.xaml", UriKind.RelativeOrAbsolute);
        }

        private void ThemeUnchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("Themes/Light.xaml", UriKind.RelativeOrAbsolute);
        }

        private void NavBarSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavButton? selectedItem = NavBarList.SelectedItem as NavButton;

            switch (selectedItem?.Header)
            {
                case "Dashboard":
                    DashboardView = new DashboardView(this);
                    MainPage.Content = DashboardView;
                    break;
                case "Devices":
                    MainPage.Content = new DevicesView(this);
                    break;
                case "Notes":
                    MainPage.Content = new NotesView();
                    break;
                case "History":
                    MainPage.Content = new HistoryView(this);
                    break;
                case "Settings":
                    MainPage.Content = new SettingsView();
                    break;
                default:
                    break;
            }
        }

        private void OptionsNavbarSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavButton? selectedItem = OptionsNavbar.SelectedItem as NavButton;

            switch (selectedItem?.Header)
            {
                case "Log Out":
                    LoginView loginView = new LoginView(LoginContext.Services);
                    LoginContext.Services.Authorization.Logout(LoginContext.LoggedUser);
                    loginView.Show();
                    Close();
                    break;
                case "Help":
                    MessageBox.Show("Help me nigga!!!");
                    selectedItem.IsSelected = false;
                    break;
                default:
                    break;
            }
        }

        private void OnClosed(object sender, EventArgs e)
        {
            // closing the camera and freeing resources
            DashboardView.videoCapture.SignalToStop();
            DashboardView.videoCapture = null;    
        }
    }
}
