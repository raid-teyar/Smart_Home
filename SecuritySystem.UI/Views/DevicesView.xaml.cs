using SecuritySystem.UI.Controls;
using SecuritySystem.UI.Helpers;
using SmartHome.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SecuritySystem.UI.Views
{
    /// <summary>
    /// Interaction logic for DevicesView.xaml
    /// </summary>
    public partial class DevicesView : UserControl
    {
        private bool isFirstLoad;

        public List<Device> Devices { get; set; }

        public MainWindow MainWindowContext { get; set; }

        public DevicesView(MainWindow mainwindow)
        {
            isFirstLoad = true;
            InitializeComponent();
            MainWindowContext = mainwindow;
            DataContext = this;
            Devices = MainWindowContext.LoginContext.Services.Device.GetDevices();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            isFirstLoad = false;
        }

        private void SwitchButton_Changed(object sender, RoutedEventArgs e)
        {
            if (!isFirstLoad)
            {
                MainWindowContext.LoginContext.Services.Device.UpdateDevcies(Devices);
            }

            // add to the history
            SwitchButton switchButton = (SwitchButton)sender;
            string deviceState = (bool)switchButton?.IsChecked ? "on" : "off";
            Grid parentGrid = (Grid)VisualTreeHelper.GetParent(switchButton);

            TextBlock? nameTextBlock = parentGrid.Children.OfType<TextBlock>().ToList()[1];

            History history = new History()
            {
                Time = DateTime.Now,
                Action = "Turned " + deviceState + " " + nameTextBlock?.Text,
                User = MainWindowContext.LoginContext.LoggedUser
            };
            
            MainWindowContext.LoginContext.Services.History.AddHistoryItem(history);
        }
    }
}
