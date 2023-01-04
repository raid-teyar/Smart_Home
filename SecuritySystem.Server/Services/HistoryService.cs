using SmartHome.Contracts.Contracts;
using SmartHome.Contracts.Models;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using SecuritySystem.UI.Models;

namespace SecuritySystem.Server.Services
{
    public class HistoryService : ISecuritySystemHistory
    {


        public List<History> GetHistory()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                var historyData = cnn.Query<(int Id, DateTime Time, string Action, int UserID)>($"SELECT id, time, action, userid FROM history").ToList();
                
                var usersData = cnn.Query<User>($"SELECT * FROM user").ToList();

                var history = new List<History>();

                foreach (var item in historyData)
                {
                    var user = usersData.Where(x => x.Id == item.UserID).FirstOrDefault();
                    history.Add(new History
                    {
                        Id = item.Id,
                        Time = item.Time,
                        Action = item.Action,
                        User = user
                    });
                }

                // return the list sorted by time
                return history.OrderByDescending(o => o.Time).ToList();
            }
        }

        public void AddHistoryItem(History history)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                cnn.Execute($"INSERT INTO history (time, action, userid) VALUES (@Time, @Action, @UserID)",
                    new { history.Time, history.Action, UserID = history.User?.Id });
            }
        }
    }
}
