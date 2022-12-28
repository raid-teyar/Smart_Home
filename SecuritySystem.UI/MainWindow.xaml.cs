using SecuritySystem.UI.Controls;
using SecuritySystem.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecuritySystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string LoggedUserInfo { get; set; }
        
        public MainWindow(LoginView loginView)
        {
            InitializeComponent();
            DataContext = this;

            NavBarList.SelectedItem = NavBarList.Items[0];
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
                    MainPage.Content = new DashboardView();
                    break;
                case "Devices":
                    MainPage.Content = new DevicesView();
                    break;
                case "Notes":
                    MainPage.Content = new NotesView();
                    break;
                case "History":
                    MainPage.Content = new HistoryView();
                    break;
                case "Settings":
                    MainPage.Content = new SettingsView();
                    break;
                default:
                    break;
            }
        }
    }
}
