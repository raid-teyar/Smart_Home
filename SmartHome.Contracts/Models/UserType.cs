using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystem.UI.Models
{
    [Serializable]
    public enum UserType
    {
        Admin,
        Resident,
        Guest
    }
}
