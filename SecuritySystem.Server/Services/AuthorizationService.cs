using SmartHome.Contracts.Contracts;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using MySql.Data.MySqlClient;
using System.ServiceModel;
using SmartHome.Contracts.Models;

namespace SecuritySystem.Server.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class AuthorizationService : ISecuritySystemAuthorization
    {

        public User Login(string email, string password)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                User? user = cnn.Query<User>($"SELECT * FROM user WHERE email = '{email}' AND password = '{password}' ").FirstOrDefault();


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
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
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
}
