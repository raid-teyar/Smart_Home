using SmartHome.Contracts.Models;
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
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace SecuritySystem.UI.Views
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public List<History> HistoryList { get; set; }
        public MainWindow MainWindowContext { get; set; }
        public HistoryView(MainWindow mainwindow)
        {
            InitializeComponent();
            MainWindowContext = mainwindow;
            DataContext = this;

            HistoryList = MainWindowContext.LoginContext.Services.History.GetHistory();
        }
    }
}
