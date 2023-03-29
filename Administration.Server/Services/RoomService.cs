using CoreWCF;
using Dapper;
using MySql.Data.MySqlClient;
using SecuritySystem.Server;
using SmartHome.Contracts.Contracts;
using SmartHome.Contracts.Models;
using System.Data;

namespace Administration.Server.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class RoomService : IAdministrationRoom
    {
        public List<Room> GetAllRooms()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                return cnn.Query<Room>($"SELECT * FROM room").ToList();
            }
        }
    }
}
