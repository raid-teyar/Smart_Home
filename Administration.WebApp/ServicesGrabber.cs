using SmartHome.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Administration.WebApp
{
    public class ServicesGrabber : IServicesGrabber
    {
        private readonly string _address = "net.tcp://localhost:5001/";
        private readonly int _timeout = 30;

        public IAdministrationUser User { get; set; }
        public IAdministrationAuthorization Authorization { get; set; }
        public IAdministrationDevice Device { get; set; }
        public IAdministrationHistory History { get; set; }
        public IAdministrationRoom Room { get; set; }

        public ServicesGrabber()
        {
            var binding = new NetTcpBinding();
            var authorizationFactory = new ChannelFactory<IAdministrationAuthorization>(binding, new EndpointAddress(_address + "Authorization"));
            var userFactory = new ChannelFactory<IAdministrationUser>(binding, new EndpointAddress(_address + "User"));
            var deviceFactory = new ChannelFactory<IAdministrationDevice>(binding, new EndpointAddress(_address + "Device"));
            var historyFactory = new ChannelFactory<IAdministrationHistory>(binding, new EndpointAddress(_address + "History"));
            var roomFactory = new ChannelFactory<IAdministrationRoom>(binding, new EndpointAddress(_address + "Room"));

            authorizationFactory.Open();
            userFactory.Open();
            deviceFactory.Open();
            historyFactory.Open();
            roomFactory.Open();

            IAdministrationAuthorization authorizationClient = authorizationFactory.CreateChannel();
            IAdministrationUser userClient = userFactory.CreateChannel();
            IAdministrationDevice deviceClient = deviceFactory.CreateChannel();
            IAdministrationHistory historyClient = historyFactory.CreateChannel();
            IAdministrationRoom roomClient = roomFactory.CreateChannel();

            IClientChannel? userChannel = userClient as IClientChannel;
            IClientChannel? authorizationChannel = authorizationClient as IClientChannel;
            IClientChannel? deviceChannel = deviceClient as IClientChannel;
            IClientChannel? historyChannel = historyClient as IClientChannel;
            IClientChannel? roomChannel = roomClient as IClientChannel;

            userChannel?.Open(TimeSpan.FromMinutes(_timeout));
            authorizationChannel?.Open(TimeSpan.FromMinutes(_timeout));
            deviceChannel?.Open(TimeSpan.FromMinutes(_timeout));
            historyChannel?.Open(TimeSpan.FromMinutes(_timeout));
            roomChannel?.Open(TimeSpan.FromMinutes(_timeout));

            User = userClient;
            Authorization = authorizationClient;
            Device = deviceClient;
            History = historyClient;
            Room = roomClient;
        }
    }
}
