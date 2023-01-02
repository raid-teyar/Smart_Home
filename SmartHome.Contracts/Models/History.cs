using SecuritySystem.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Contracts.Models
{
    public class History
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public User User { get; set; }
        public string Action { get; set; }
    }
}
