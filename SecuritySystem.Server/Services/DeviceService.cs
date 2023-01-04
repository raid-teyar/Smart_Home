using MySql.Data.MySqlClient;
using SmartHome.Contracts.Contracts;
using SmartHome.Contracts.Models;
using System.Data;
using Dapper;

namespace SecuritySystem.Server.Services
{
    public class DeviceService : ISecuritySystemDevice
    {
        public List<Device> GetDevices()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                var devicesData = cnn.Query<(int id, string Name, DeviceType Type, bool State, string Value, int RoomId)>($"SELECT id, name, type, state, value, room FROM device").ToList();
                var roomsData = cnn.Query<Room>($"SELECT * FROM room ").ToList();

                var devices = new List<Device>();
                
                foreach (var item in devicesData)
                {
                    var room = roomsData.Where(x => x.Id == item.RoomId).FirstOrDefault();
                    devices.Add(new Device
                    {
                        Id = item.id,
                        Name = item.Name,
                        Type = item.Type,
                        State = item.State,
                        Value = item.Value,
                        Room = room
                    });
                }
                return devices;
            }
        }

        public void UpdateDevcies(List<Device> devices)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                foreach (var device in devices)
                {
                    cnn.Execute($"UPDATE device SET state = {device.State} WHERE id = {device.Id}");
                }
            }
        }
    }
}
