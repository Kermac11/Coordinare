using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Coordinare.Models
{
    public class User
    {
        public int User_ID { get; set; }
        [BindProperty, Required(ErrorMessage = "{0} must be filled out")]
        public string Name { get; set; }
        [BindProperty, Required(ErrorMessage = "{0} must be filled out")]
        public string Username { get; set; }

        public string Password { get; set; }
        [BindProperty, Required(ErrorMessage = "{0} skal udfyldes"), DataType(DataType.Password), Display(Name = "Repeat password"), Compare(nameof(Password), ErrorMessage = "Password not the same")]
        public string Password2 { get; set; }
        [BindProperty, DataType(DataType.Password), Display(Name = "Password")]
        public string PasswordCheck { get; set; }

        public string Phone { get; set; }
        [BindProperty, Required(ErrorMessage = "{0} must be filled out")]
        public string Email { get; set; }
        [BindProperty, Required(ErrorMessage = "{0} must be filled out")]
        public bool Speaker { get; set; }
        [BindProperty, Required(ErrorMessage = "{0} must be filled out")]
        public bool Specialaid { get; set; }
        [BindProperty, Required(ErrorMessage = "{0} must be filled out")]
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

        public User(string name, string username, string password, string phone, string email, bool speaker, bool specialaid, bool admin)
        {

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
