using MySql.Data.MySqlClient;
using SecuritySystem.Server;
using SmartHome.Contracts.Contracts;
using System.Data;
using Dapper;
using SmartHome.Contracts.Models;
using CoreWCF;

namespace Administration.Server.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class AuthorizationService : IAdministrationAuthorization
    {
        public User Login(string username, string password)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                User? user = cnn.Query<User>($"SELECT * FROM user WHERE email = '{username}' AND password = '{password}' ").FirstOrDefault();

                if (user != null)
                {
                    History history = new History
                    {
                        Time = DateTime.Now,
                        Action = "Logged in",
                        User = user
                    };
                    HistoryService historyService = new HistoryService();
                    historyService.AddHistoryItem(history);
                }
                return user;
            }
        }

        public void Logout(User user)
        {
            History history = new History
            {
                Time = DateTime.Now,
                Action = "Logged out",
                User = user
            };
            HistoryService historyService = new HistoryService();
            historyService.AddHistoryItem(history);
        }
    }
}
