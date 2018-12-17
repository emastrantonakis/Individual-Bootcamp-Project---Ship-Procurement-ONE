using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmanouil_mastrantonakis_individualProject
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Wellcome to Ship Procurement ONE!");
                DbAccessLayer dbLayer = new DbAccessLayer();
                FileAccessLayer faLayer = new FileAccessLayer();
                List<User> allUsers = dbLayer.ViewAllSystemUsers();
                List<Ship> allShips = dbLayer.ViewAllShips();
                List<Message> allMessages = dbLayer.ViewAllMessages();
                Login tryLogin = new Login(allUsers);
                switch (tryLogin.LoggedUser.Role)
                {
                    case "Admin":
                        ApplicationMenus.ShowAdminMenu(tryLogin.LoggedUser, allUsers, allShips, allMessages);
                        break;
                    case "Operator":
                        ApplicationMenus.ShowOperatorMenu(tryLogin.LoggedUser, allUsers, allMessages);
                        break;
                    case "Captain":
                        ApplicationMenus.ShowCaptainMenu(tryLogin.LoggedUser, allUsers, allMessages);
                        break;
                    case "User":
                        ApplicationMenus.ShowUserMenu(tryLogin.LoggedUser, allUsers, allMessages);
                        break;
                }
                Console.ReadKey();
            }  
        }
    }
}
