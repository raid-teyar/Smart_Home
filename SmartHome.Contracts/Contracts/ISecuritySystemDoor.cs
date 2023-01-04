using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Contracts.Contracts
{
    [ServiceContract]
    public interface ISecuritySystemDoor
    {
        [OperationContract]
        void OpenDoor();

        [OperationContract]
        void CloseDoor();

        [OperationContract]
        bool IsDoorClosed();
    }
}
