using SmartHome.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Contracts.Contracts
{
    [ServiceContract]
    public interface IAdministrationRoom
    {
        [OperationContract]
        List<Room> GetAllRooms();
    }
}
