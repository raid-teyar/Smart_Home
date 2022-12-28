using SecuritySystem.UI.Models;
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
using System.Windows.Shapes;

namespace SecuritySystem.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {

        public User LoggedUser { get; set; }
        
        public LoginView()
        {
         
            
            InitializeComponent();
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this);
            mainWindow.Show();
            Close();
        }
    }
}
