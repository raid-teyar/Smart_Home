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

namespace SecuritySystem.UI.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {

        public LoginView Context { get; set; }

        VideoCaptureDevice videoCapture;
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
            catch (Exception ex)
            {
              
            }

        }

        public DashboardView(LoginView loginView)
        {
            Context = loginView;
            InitializeComponent();
            DataContext = this;

            tbWelcome.Text = "Welcome home, " + Context.LoggedUser.FullName + "!";
            StartCamera();
        }



        private void SwitchButton_Checked(object sender, RoutedEventArgs e)
        {
            Grid ownerGrid = (Grid)VisualTreeHelper.GetParent((SwitchButton)sender);
            Border ownerBorder = (Border)VisualTreeHelper.GetParent(ownerGrid);

            Style style = FindResource("OnDeviceCard") as Style;

            ownerBorder.Style = style;
        }

        private void SwitchButton_UnChecked(object sender, RoutedEventArgs e)
        {
            Grid ownerGrid = (Grid)VisualTreeHelper.GetParent((SwitchButton)sender);
            Border ownerBorder = (Border)VisualTreeHelper.GetParent(ownerGrid);

            Style style = FindResource("OffDeviceCard") as Style;

            ownerBorder.Style = style;
        }


        private void CloseDoor_Clicked(object sender, RoutedEventArgs e)
        {
            ipDoorOpen.Visibility = Visibility.Collapsed;
            ipDoorClosed.Visibility = Visibility.Visible;
        }

        private void OpenDoor_Click(object sender, RoutedEventArgs e)
        {
            ipDoorClosed.Visibility = Visibility.Collapsed;
            ipDoorOpen.Visibility = Visibility.Visible;
        }

    }
}
