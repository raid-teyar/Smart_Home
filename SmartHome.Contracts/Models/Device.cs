using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Contracts.Models
{
    [Serializable]
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public string Value { get; set; }
        public Room? Room { get; set; }
        public bool State { get; set; }
    }
}
