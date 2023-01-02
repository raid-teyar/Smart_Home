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

namespace SecuritySystem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // ovveride OnStartup
        protected override void OnStartup(StartupEventArgs e)
        {
            IClientChannel? channel = null;
            
            var binding = new NetTcpBinding();

            // create a channel factory
            var factory = new ChannelFactory<ISecurityServiceAuthorization>(binding, new EndpointAddress("net.tcp://localhost:5000/Authorization"));

            factory.Open();

            try
            {
                ISecurityServiceAuthorization client = factory.CreateChannel();
                channel = client as IClientChannel;
                channel?.Open();

                // create a login view
                var loginView = new LoginView(client);
                loginView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
