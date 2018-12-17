using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmanouil_mastrantonakis_individualProject
{
    public static class ApplicationMenus
    {

        public static void ShowAdminMenu(User inUser, List<User> usersList, List<Ship> fleet, List<Message> messagesList)
        {
            //General Functionality Choice
            //Admin can use the platform with admin features AND user features
            DbAccessLayer dblayer = new DbAccessLayer();
            string funcChoice = "";
            while (funcChoice != "0" && funcChoice != "1" && funcChoice != "2")
            {
                Console.WriteLine("Press 1 for Admin functionality: ");
                Console.WriteLine("Press 2 for User funcionality: ");
                Console.WriteLine("Press 0 to Exit: ");
                funcChoice = (Console.ReadLine());
                if (funcChoice != "0" && funcChoice != "1" && funcChoice != "2")
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input please try again");
                }
            }
            switch (funcChoice)
            {
                case "1":
                    Console.Clear();
                    string adminChoice = ""; //Admin feature choice
                    while (adminChoice != "0" && adminChoice != "1" && adminChoice != "2"
                          && adminChoice != "3" && adminChoice != "4" && adminChoice != "5"
                        && adminChoice != "6" && adminChoice != "7" && adminChoice != "8"
                        && adminChoice != "9")
                    {
                        Console.WriteLine("Press 1 to create a new system user: ");
                        Console.WriteLine("Press 2 to view a specific system user or all system users: ");
                        Console.WriteLine("Press 3 to update an existing system user: ");
                        Console.WriteLine("Press 4 to assign a new role to an existing system user: ");
                        Console.WriteLine("Press 5 to delete a system user: ");
                        Console.WriteLine("Press 6 to create a new ship entry: ");
                        Console.WriteLine("Press 7 to view a ship entry or all ship entries: ");
                        Console.WriteLine("Press 8 to update a ship entry: ");
                        Console.WriteLine("Press 9 to delete a ship entry: ");
                        Console.WriteLine("Press 0 to exit: ");
                        adminChoice = Console.ReadLine();
                        switch (adminChoice)
                        {
                            case "1":
                                AppSubMenus.AdminCreatesUser(usersList);
                                break;
                            case "2":
                                AppSubMenus.AdminViewsUser(usersList);
                                break;
                            case "3":
                                AppSubMenus.AdminUpdatesUser(usersList);
                                break;
                            case "4":
                                AppSubMenus.AdminAssignsRoleToUser(usersList);
                                break;
                            case "5":
                                AppSubMenus.AdminDeletesUser(usersList, messagesList);
                                break;
                            case "6":
                                AppSubMenus.AdminCreatesShip(fleet);
                                break;
                            case "7":
                                AppSubMenus.AdminViewsShip(fleet);
                                break;
                            case "8":
                                AppSubMenus.AdminUpdatesShip(fleet);
                                break;
                            case "9":
                                AppSubMenus.AdminDeletesShip(fleet);
                                break;
                            case "0":
                                Console.Clear();
                                Console.WriteLine("Bye");
                                Environment.Exit(0);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Invalid input please try again!");
                                break;
                        }
                    }
                    break;
                case "2":
                    Console.Clear();
                    ShowOperatorMenu(inUser, usersList, messagesList);
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Bye");
                    Environment.Exit(0);
                    break;
            }

        }

        public static void ShowOperatorMenu(User inUser, List<User> usersList, List<Message> messagesList)
        {
            DbAccessLayer dbLayer = new DbAccessLayer();
            FileAccessLayer faLayer = new FileAccessLayer();
            string opChoice = "";
            while (opChoice != "0" && opChoice != "1" && opChoice != "2" && opChoice != "3" && opChoice != "4"
                && opChoice != "5" && opChoice != "6")
            {
                Console.WriteLine("Press 1 to send a message.");
                Console.WriteLine("Press 2 to read your received or sent messages or any other message in the system");
                Console.WriteLine("Press 3 to edit your sent messages or any other message in the system.");
                Console.WriteLine("Press 4 to delete received/sent messages or any other message in the system");
                Console.WriteLine("Press 5 to send a new Chartering Order");
                Console.WriteLine("Press 6 to read all Bunkering Orders");
                Console.WriteLine("Press 0 to exit");
                opChoice = Console.ReadLine();
                switch (opChoice)
                {
                    case "1":
                        AppSubMenus.UserSendsMessageToUser(inUser, usersList, messagesList);
                        break;
                    case "2":
                        AppSubMenus.UserReadsMessage(inUser, usersList, messagesList);
                        break;
                    case "3":
                        AppSubMenus.UserEditsMessage(inUser, usersList, messagesList);
                        break;
                    case "4":
                        AppSubMenus.UserDeletesMessage(inUser, messagesList);
                        break;
                    case "5":
                        AppSubMenus.UserGivesCharteringOrder(inUser, usersList);
                        break;
                    case "6":
                        AppSubMenus.UserReadsAllBunkeringOrders(inUser, usersList, messagesList);
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Bye");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input please try again");
                        break;
                }

            }
        }

        public static void ShowCaptainMenu(User inUser, List<User> usersList, List<Message> messagesList)
        {
            DbAccessLayer dbLayer = new DbAccessLayer();
            FileAccessLayer faLayer = new FileAccessLayer();
            string capChoice = "";
            while (capChoice != "0" && capChoice != "1" && capChoice != "2" && capChoice != "3" && capChoice != "4"
                && capChoice != "5")
            {
                Console.WriteLine("Press 1 to send a message.");
                Console.WriteLine("Press 2 to read your received or sent messages or any other message in the system");
                Console.WriteLine("Press 3 to edit your sent messages or any other message in the system.");
                Console.WriteLine("Press 4 to send a new Bunkering Order");
                Console.WriteLine("Press 5 to read all Chartering Orders");
                Console.WriteLine("Press 0 to exit");
                capChoice = Console.ReadLine();
                switch (capChoice)
                {
                    case "1":
                        AppSubMenus.UserSendsMessageToUser(inUser, usersList, messagesList);
                        break;
                    case "2":
                        AppSubMenus.UserReadsMessage(inUser, usersList, messagesList);
                        break;
                    case "3":
                        AppSubMenus.UserEditsMessage(inUser, usersList, messagesList);
                        break;
                    case "4":
                        AppSubMenus.UserGivesBunkeringOrder(inUser, usersList);
                        break;
                    case "5":
                        AppSubMenus.UserReadsAllCharteringOrders(inUser, usersList, messagesList);
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Bye");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input please try again");
                        break;
                }
            }
        }

        public static void ShowUserMenu(User inUser, List<User> usersList, List<Message> messagesList)
        {
            DbAccessLayer dbLayer = new DbAccessLayer();
            FileAccessLayer faLayer = new FileAccessLayer();
            string userChoice = "";
            while (userChoice != "0" && userChoice != "1" && userChoice != "2")
            {
                Console.Clear();
                Console.WriteLine("Press 1 to send a message.");
                Console.WriteLine("Press 2 to read your received or sent messages or any other message in the system");
                Console.WriteLine("Press 0 to exit");
                userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        AppSubMenus.UserSendsMessageToUser(inUser, usersList, messagesList);
                        break;
                    case "2":
                        AppSubMenus.UserReadsMessage(inUser, usersList, messagesList);
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Bye");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input please try again");
                        break;
                }
            }
        }
    }
}
