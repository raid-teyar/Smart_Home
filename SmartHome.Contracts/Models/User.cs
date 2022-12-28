using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystem.UI.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string Password { get; set; }
        
        [DataMember]
        public UserType UserType { get; set; }


        public override string ToString()
        {
            return $"{Id} {Email} {FullName} {Password} {UserType}";
        }

    }
}
