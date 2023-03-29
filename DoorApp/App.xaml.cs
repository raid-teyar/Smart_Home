using SmartHome.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace DoorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string _address = "net.tcp://localhost:5000/";
        private readonly int _timeout = 30;

         // ovveride OnStartup
        protected override void OnStartup(StartupEventArgs e)
        {
            var binding = new NetTcpBinding();

            // creating channel factories for all services
            var doorFactory = new ChannelFactory<ISecuritySystemDoorRequest>(binding, new EndpointAddress(_address + "DoorRequest"));
           
            doorFactory.Open();

            try
            {
                ISecuritySystemDoorRequest doorClient = doorFactory.CreateChannel();

                IClientChannel? doorChannel = doorClient as IClientChannel;

                doorChannel?.Open(TimeSpan.FromMinutes(_timeout));

                var mainwindow = new MainWindow(doorClient);
                mainwindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
