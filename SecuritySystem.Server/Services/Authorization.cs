using SmartHome.Contracts.Contracts;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SecuritySystem.UI.Models;
using MySql.Data.MySqlClient;
using System.ServiceModel;

namespace SecuritySystem.Server.Services
{
    public class Authorization : ISecurityServiceAuthorization
    {
        public string ConnectionString { get; set; }

        public Authorization()
        {
            ConnectionString = "server=localhost;user=root;database=smart home;port=3306;password=raid";
        }

        public User Login(string email, string password)
        {
            using (IDbConnection cnn = new MySqlConnection(ConnectionString))
            {
                User? user = cnn.Query<User>($"SELECT * FROM user WHERE email = '{email}' AND password = '{password}' ").FirstOrDefault();
                return user;
            }
        }
    }
}
