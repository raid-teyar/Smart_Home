using SecuritySystem.UI.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ServiceModel;
using SmartHome.Contracts.Contracts;
using SecuritySystem.UI.Models;
using SecuritySystem.UI.Helpers;
using SmartHome.Contracts.Models;

namespace SecuritySystem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string _address = "net.tcp://localhost:5000/";
        // ovveride OnStartup
        protected override void OnStartup(StartupEventArgs e)
        {
            var binding = new NetTcpBinding();

            // creating channel factories for all services
            var authorizationFactory = new ChannelFactory<ISecuritySystemAuthorization>(binding, new EndpointAddress(_address + "Authorization"));
            var historyFactory = new ChannelFactory<ISecuritySystemHistory>(binding, new EndpointAddress(_address + "History"));
            var deviceFactory = new ChannelFactory<ISecuritySystemDevice>(binding, new EndpointAddress(_address + "Device"));
            var doorFactory = new ChannelFactory<ISecuritySystemDoor>(binding, new EndpointAddress(_address + "Door"));

            authorizationFactory.Open();
            historyFactory.Open();
            deviceFactory.Open();
            doorFactory.Open();

            try
            {
                ISecuritySystemAuthorization authorizationClient = authorizationFactory.CreateChannel();
                ISecuritySystemHistory historyClient = historyFactory.CreateChannel();
                ISecuritySystemDevice deviceClient = deviceFactory.CreateChannel();
                ISecuritySystemDoor doorClient = doorFactory.CreateChannel();

                IClientChannel? autorizationChannel = authorizationClient as IClientChannel;
                IClientChannel? historyChannel = historyClient as IClientChannel;
                IClientChannel? deviceChannel = deviceClient as IClientChannel;
                IClientChannel? doorChannel = doorClient as IClientChannel;

                autorizationChannel?.Open();
                historyChannel?.Open();
                deviceChannel?.Open();
                doorChannel?.Open();

                ServicesGrabber services = new ServicesGrabber();
                services.Authorization = authorizationClient;
                services.History = historyClient;
                services.Device = deviceClient;
                services.Door = doorClient;

                // create a login view
                var loginView = new LoginView(services);
                loginView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
