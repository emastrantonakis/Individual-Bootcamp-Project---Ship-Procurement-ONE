using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace emmanouil_mastrantonakis_individualProject
{
    public class Login
    {
        public bool isLoggedIn { get; set; }
        private int Lcounter { get; set; } = 0;
        public User LoggedUser = new User
        {
            Id = 0,
            FirstName = "",
            LastName = "",
            Role = "",
            Email = "",
            Phone = "",
            Username = "",
            Password = ""
        };
        
        public Login(List<User> allUsersList)
        {
            while (!isLoggedIn)
            {
                Console.Write("Please type your username: ");
                string username = Console.ReadLine();
                Console.Write("Please type your password: ");
                string password = Console.ReadLine();
                Console.Clear();
                if (isLoggedIn = allUsersList.Exists(x => x.Username == username && x.Password == password))
                {
                    User user = allUsersList.Single(x => x.Username == username && x.Password == password);
                    LoggedUser.Id = user.Id;
                    LoggedUser.FirstName = user.FirstName;
                    LoggedUser.LastName = user.LastName;
                    LoggedUser.Role = user.Role;
                    LoggedUser.Email = user.Email;
                    LoggedUser.Phone = user.Phone;
                    Console.Clear();
                    Console.WriteLine($"Wellcome {LoggedUser.Role} {LoggedUser.LastName} {LoggedUser.FirstName}");
                }
                else 
                {
                    Lcounter++;
                    if (Lcounter >= 3)
                    {
                        Console.WriteLine("3 Login Fails! \nSECURITY EXIT:");
                        Environment.Exit(1);
                    }
                    Console.WriteLine($"Validation failed.\nYou have {3 - Lcounter} tries left! ");
                }
            }  
        }
    }
}
