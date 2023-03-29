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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ISecuritySystemDoorRequest DoorRequest { get; set; }
        public MainWindow(ISecuritySystemDoorRequest doorRequest)
        {
            InitializeComponent();
            DoorRequest = doorRequest;
        }

        private void onClick(object sender, RoutedEventArgs e)
        {
            DoorRequest.RequestDoorOpen();
            mainButton.IsChecked = true;
            mainButton.Content = "Waiting for response...";
        }
    }
}
