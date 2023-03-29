using SecuritySystem.UI.Helpers;
using SmartHome.Contracts.Models;
using SmartHome.Contracts.Contracts;
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
        public ServicesGrabber Services { get; set; }

        public LoginView(ServicesGrabber services)
        {
            InitializeComponent();
            Services = services;
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            try
            {
                LoggedUser = Services.Authorization.Login(tbEmail.Text, pbPassword.Password);
                if (LoggedUser != null)
                {
                    var mainWindow = new MainWindow(this);
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
