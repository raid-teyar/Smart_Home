using MySql.Data.MySqlClient;
using SmartHome.Contracts.Contracts;
using System.Data;
using Dapper;

namespace SecuritySystem.Server.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class DoorService : ISecuritySystemDoor
    {
        public void CloseDoor()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                cnn.Query($"UPDATE variables SET value = 1 WHERE name = 'isDoorClosed'");
            }
        }

        public void OpenDoor()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                cnn.Query($"UPDATE variables SET value = 0 WHERE name = 'isDoorClosed'");
            }
        }

        
        public bool IsDoorClosed()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                var isDoorClosed = cnn.Query<bool>($"SELECT value FROM variables WHERE name = 'isDoorClosed'").FirstOrDefault();
                return isDoorClosed;
            }
        }

    }
}
