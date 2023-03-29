using SecuritySystem.UI.Views;
using System;
using System.Windows;
using System.ServiceModel;
using SmartHome.Contracts.Contracts;
using SecuritySystem.UI.Helpers;

namespace SecuritySystem.UI
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

                autorizationChannel?.Open(TimeSpan.FromMinutes(_timeout));
                historyChannel?.Open(TimeSpan.FromMinutes(_timeout));
                deviceChannel?.Open(TimeSpan.FromMinutes(_timeout));
                doorChannel?.Open(TimeSpan.FromMinutes(_timeout));

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
