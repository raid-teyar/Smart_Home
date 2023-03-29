using Administration.Server.Helpers;
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
    public class DeviceService : IAdministrationDevice
    {
        public void DeleteDevice(int id)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                cnn.Execute($"DELETE FROM device WHERE id = '{id}'");
            }
        }


        public List<Device> GetAllDevices()
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                var devicesData = cnn.Query<(int Id, string Name, DeviceType Type, bool State, string Value, int room)>($"SELECT * FROM device").ToList();
                List<Room> roomsData = cnn.Query<Room>($"SELECT * FROM room").ToList();

                var devices = new List<Device>();

                foreach (var item in devicesData)
                {
                    var room = roomsData.Where(x => x.Id == item.room).FirstOrDefault();
                    devices.Add(new Device
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Type = item.Type,
                        Value = item.Value,
                        State = item.State,
                        Room = room
                    });
                }

                return devices;
            }
        }

        public void UpdateDevice(Device device)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                int State = BoolToIntConverter.ConvertToInt(device.State);
                cnn.Execute($"UPDATE device SET name = @Name, type = @Type, state = @State, value = @Value, room = @Room WHERE id = @Id",
                    new { device.Name, device.Type, State, device.Value, Room = device.Room?.Id, device.Id });
            }
        }

        public void AddDevice(Device device)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                cnn.Execute($"INSERT INTO device (name, type, room, value, state) VALUES ('{device.Name}', '{device.Type}', '{device.Room.Id}', '{device.Value}', '{BoolToIntConverter.ConvertToInt(device.State)}')");
            }
        }


        public Device GetDeviceById(int id)
        {
            using (IDbConnection cnn = new MySqlConnection(Globals.ConnectionString))
            {
                var deviceData = cnn.Query<(int Id, string Name, DeviceType Type, bool State, string Value)>($"SELECT * FROM device WHERE id = '{id}'").FirstOrDefault();
                var roomData = cnn.Query<Room>($"SELECT * FROM room WHERE id = '{deviceData.Id}'").FirstOrDefault();

                return new Device
                {
                    Id = deviceData.Id,
                    Name = deviceData.Name,
                    Type = deviceData.Type,
                    Value = deviceData.Value,
                    State = deviceData.State,
                    Room = roomData
                };
            }
        }
    }
}
