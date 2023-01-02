using SecuritySystem.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace SmartHome.Contracts.Contracts
{
    [ServiceContract]
    public interface ISecurityServiceAuthorization
    {
        [OperationContract]
        User Login(string username, string password);
    }
}
