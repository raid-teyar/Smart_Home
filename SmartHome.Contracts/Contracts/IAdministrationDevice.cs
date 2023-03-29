using SmartHome.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Contracts.Contracts
{
    [ServiceContract]
    public interface IAdministrationDevice
    {
        [OperationContract]
        List<Device> GetAllDevices();

        [OperationContract]
        void AddDevice(Device device);

        [OperationContract]
        void UpdateDevice(Device device);

        [OperationContract]
        void DeleteDevice(int id);

        [OperationContract]
        Device GetDeviceById(int id);
    }
}
