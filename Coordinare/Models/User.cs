using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Models
{
    public class User
    {
        public int User_ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public bool Speaker { get; set; }

        public bool Specialaid { get; set; }
        public bool Admin { get; set; }

        public User()
        {
            
        }
        public User(int userId, string name, string username, string password, string phone, string email, bool speaker, bool specialaid, bool admin)
        {
            User_ID = userId;
            Name = name;
            Username = username;
            Password = password;
            Phone = phone;
            Email = email;
            Speaker = speaker;
            Specialaid = specialaid;
            Admin = admin;
        }

    }
}
