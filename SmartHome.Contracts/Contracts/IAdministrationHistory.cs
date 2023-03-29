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
    public interface IAdministrationHistory
    {
        [OperationContract]
        List<History> GetHistory();

        [OperationContract]
        void AddHistoryItem(History history);
    }
}
