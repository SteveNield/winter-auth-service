using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winter.Auth.Model
{
    public class User
    {
        public User(string username, string role)
        {
            Username = username;
            Role = role;
        }

        public string Username { get; set; }
        public string Role { get; set; }
    }
}
