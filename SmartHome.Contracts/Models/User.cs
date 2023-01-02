using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystem.UI.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
        
        public UserType UserType { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Email: {1}, FullName: {2}, Password: {3}, UserType: {4}", Id, Email, FullName, Password, UserType);
        }
    }
}
