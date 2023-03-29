using SmartHome.Contracts.Models;
using SmartHome.Contracts.Contracts;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using SecuritySystem.Server;
using CoreWCF;

namespace Administration.Server.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class UserService : IAdministrationUser
    {
        public void AddUser(User user)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                cnn.Execute("INSERT INTO user (email, password, fullname, usertype) VALUES (@Email, @Password, @FullName, @UserType)", user);
            }
        }

        public void DeleteUser(int id)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                cnn.Execute($"DELETE FROM user WHERE id = {id}");
            }
        }

        public List<User> GetAllUsers()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                return cnn.Query<User>("SELECT * FROM user").ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                return cnn.Query<User>($"SELECT * FROM user WHERE id = {id}").FirstOrDefault();
            }
        }

        public void UpdateUser(User user)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                cnn.Execute("UPDATE user SET email = @Email, password = @Password, fullname = @FullName, usertype = @UserType WHERE id = @Id", user);
            }
        }
    }
}
