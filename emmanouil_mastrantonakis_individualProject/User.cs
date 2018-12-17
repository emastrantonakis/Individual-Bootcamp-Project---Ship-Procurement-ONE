using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmanouil_mastrantonakis_individualProject
{
    public class User
    {
        public int Id { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Message> SentMessages = new List<Message>();
        public List<Message> ReceivedMessages = new List<Message>();

        //Overloaded constructor
        public User(string firstName, string lastName, string role ,string email, string phone, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            Email = email;
            Phone = phone;
            Username = username;
            Password = password;
        }

        //Default Constructor
        public User()
        {
        }
    }
}
