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
    public interface IAdministrationUser
    {
        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        void AddUser(User user);

        [OperationContract]
        void UpdateUser(User user);

        [OperationContract]
        void DeleteUser(int id);

        [OperationContract]
        User GetUserById(int id);
    }
}
