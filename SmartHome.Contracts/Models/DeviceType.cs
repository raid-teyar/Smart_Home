using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Contracts.Models
{
    [Serializable]
    public enum DeviceType
    {
        Temperature,
        Humidity,
        Camera
    }
}
