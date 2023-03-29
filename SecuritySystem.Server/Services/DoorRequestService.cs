using Dapper;
using MySql.Data.MySqlClient;
using SmartHome.Contracts.Contracts;
using SmartHome.Contracts.Models;
using System.Data;

namespace SecuritySystem.Server.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class DoorRequestService : ISecuritySystemDoorRequest
    {
        public void RequestDoorOpen()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                History history = new History
                {
                    Time = DateTime.Now,
                    Action = "Door requested to open, check your camera!",
                    User = null
                };

                cnn.Execute($"INSERT INTO history (time, action) VALUES (@Time, @Action)",
                    new { history.Time, history.Action });
            }
        }
    }
}
