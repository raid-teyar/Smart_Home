using SmartHome.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace SmartHome.Contracts.Contracts
{
    [ServiceContract]
    public interface ISecuritySystemAuthorization
    {
        [OperationContract]
        User Login(string username, string password);

        [OperationContract]
        void Logout(User user);
    }
}
