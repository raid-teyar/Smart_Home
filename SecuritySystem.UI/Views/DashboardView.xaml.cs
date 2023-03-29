using SecuritySystem.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.IO;
using SecuritySystem.UI.Helpers;
using SmartHome.Contracts.Models;

namespace SecuritySystem.UI.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public List<History> HistoryList { get; set; }
        private bool isFirstLoad;

        public List<Device> Devices
        {
            get;
            set;
        }

        public MainWindow MainWindowContext { get; set; }

        public VideoCaptureDevice videoCapture { get; set; }
        FilterInfoCollection filterInfo;

        void StartCamera()
        {
            try
            {
                filterInfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                videoCapture = new VideoCaptureDevice(filterInfo[0].MonikerString);
                videoCapture.NewFrame += new NewFrameEventHandler(Camera_On);
                videoCapture.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Camera_On(object sender, NewFrameEventArgs e)
        {
            // convert bitmap to imagesource
            try
            {
                ConvertBitmapToBitmapImage converter = new ConvertBitmapToBitmapImage();
                // execute on UI thread
                Dispatcher.Invoke(() =>
                {
                    CamPreview.ImageSource = converter.Convert((Bitmap)e.Frame.Clone());
                });
            }
            catch
            {
            }
        }

        

        public DashboardView(MainWindow mainwindow)
        {
            isFirstLoad = true;
            MainWindowContext = mainwindow;
            InitializeComponent();
            DataContext = this;

            tbWelcome.Text = "Welcome home, " + MainWindowContext.LoginContext.LoggedUser.FullName + "!";

            // uncomment this line to start the camera
            // StartCamera();

            // get only the first 5 history items
            HistoryList = MainWindowContext.LoginContext.Services.History.GetHistory().Take(5).ToList();

            // get only the first 4 devices
            Devices = MainWindowContext.LoginContext.Services.Device.GetDevices().Take(4).ToList();

            // get initial door state
            bool isDoorClosed = MainWindowContext.LoginContext.Services.Door.IsDoorClosed();
            if (isDoorClosed)
            {
                ipDoorClosed.Visibility = Visibility.Visible;
            }
            else
            {
                ipDoorOpen.Visibility = Visibility.Visible;
            }
        }

        private void SwitchButton_Checked(object sender, RoutedEventArgs e)
        {
            // card style switching
            Grid ownerGrid = (Grid)VisualTreeHelper.GetParent((SwitchButton)sender);
            Border ownerBorder = (Border)VisualTreeHelper.GetParent(ownerGrid);
            Style? style = FindResource("OnDeviceCard") as Style;
            ownerBorder.Style = style;

            if (!isFirstLoad)
            {
                // updating devices
                MainWindowContext.LoginContext.Services.Device.UpdateDevcies(Devices);

                // update the history
                SwitchButton switchButton = (SwitchButton)sender;
                Grid parentGrid = (Grid)VisualTreeHelper.GetParent(switchButton);

                TextBlock? nameTextBlock = parentGrid.Children.OfType<TextBlock>().FirstOrDefault();

                History history = new History()
                {
                    Time = DateTime.Now,
                    Action = "Turned on " + nameTextBlock?.Text,
                    User = MainWindowContext.LoginContext.LoggedUser
                };

                MainWindowContext.LoginContext.Services.History.AddHistoryItem(history);
            }
        }

        private void SwitchButton_UnChecked(object sender, RoutedEventArgs e)
        {
            // card style switching
            Grid ownerGrid = (Grid)VisualTreeHelper.GetParent((SwitchButton)sender);
            Border ownerBorder = (Border)VisualTreeHelper.GetParent(ownerGrid);
            Style? style = FindResource("OffDeviceCard") as Style;
            ownerBorder.Style = style;

            if (!isFirstLoad)
            {
                // updating devices
                MainWindowContext.LoginContext.Services.Device.UpdateDevcies(Devices);

                // update the history
                SwitchButton switchButton = (SwitchButton)sender;
                Grid parentGrid = (Grid)VisualTreeHelper.GetParent(switchButton);

                TextBlock? nameTextBlock = parentGrid.Children.OfType<TextBlock>().FirstOrDefault();

                History history = new History()
                {
                    Time = DateTime.Now,
                    Action = "Turned off " + nameTextBlock?.Text,
                    User = MainWindowContext.LoginContext.LoggedUser
                };

                MainWindowContext.LoginContext.Services.History.AddHistoryItem(history);
            }
        }

        private void CloseDoor_Clicked(object sender, RoutedEventArgs e)
        {
            ipDoorOpen.Visibility = Visibility.Collapsed;
            ipDoorClosed.Visibility = Visibility.Visible;
            MainWindowContext.LoginContext.Services.Door.CloseDoor();

            // add history item
            History history = new History()
            {
                Time = DateTime.Now,
                Action = "Closed the door",
                User = MainWindowContext.LoginContext.LoggedUser
            };
            MainWindowContext.LoginContext.Services.History.AddHistoryItem(history);
        }

        private void OpenDoor_Click(object sender, RoutedEventArgs e)
        {
            ipDoorClosed.Visibility = Visibility.Collapsed;
            ipDoorOpen.Visibility = Visibility.Visible;
            MainWindowContext.LoginContext.Services.Door.OpenDoor();

            // add history item
            History history = new History()
            {
                Time = DateTime.Now,
                Action = "Opened the door",
                User = MainWindowContext.LoginContext.LoggedUser
            };
            MainWindowContext.LoginContext.Services.History.AddHistoryItem(history);
        }

        private void ViewAllHistory_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.NavBarList.SelectedItem = MainWindowContext.NavBarList.Items[2];
        }

        private void ViewAllDevices_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.NavBarList.SelectedItem = MainWindowContext.NavBarList.Items[1];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            isFirstLoad = false;
        }
    }
}
