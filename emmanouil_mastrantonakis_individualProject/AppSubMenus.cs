using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmanouil_mastrantonakis_individualProject
{
    public static class AppSubMenus
    {
        //Admin Functionality
        public static void AdminCreatesUser(List<User> usersList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            Console.Write("Please insert the new user's data. Accepted roles are: Operator, Captain, User) \nRole: ");
            string EntryRole = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            while (EntryRole != "Operator" && EntryRole != "Captain" && EntryRole != "User")
            {
                Console.Write("Invalid role. Please try again: ");
                EntryRole = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            }
            Console.Write("First Name: ");
            string EntryFirstName = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            while (!EntryFirstName.All(Char.IsLetter))
            {
                Console.WriteLine("A human name can only contain letters. \nUnless you are a " +
                    "robot... In that case 01111011101011001010 ");
                Console.Write("Please retype the First name: ");
                EntryFirstName = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            }
            Console.Write("Last Name: ");
            string EntryLastName = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            while (!EntryLastName.All(Char.IsLetter))
            {
                Console.WriteLine("A human name can only contain letters. \nUnless you are a " +
                    "robot... In that case 01111011101011001010 ");
                Console.Write("Please retype the Last name: ");
                EntryLastName = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            }
            Console.Write("Email: ");
            string EntryEmail = Console.ReadLine();
            while (!EntryEmail.Contains("@"))
            {
                Console.Write("Please type a valid email: ");
                EntryEmail = Console.ReadLine();
            }
            Console.Write("Phone: ");
            string EntryPhone = Console.ReadLine();
            while (!EntryPhone.All(Char.IsNumber))
            {
                Console.Write("Please type a valid phone number: ");
                EntryPhone = Console.ReadLine();
            }
            Console.Write("Username: ");
            string EntryUsername = Console.ReadLine();
            while (usersList.Exists(x => x.Username == EntryUsername))
            {
                    Console.WriteLine("Username already exists please type onother one: ");
                    EntryUsername = Console.ReadLine();
            }
            Console.Write("Password: ");
            string EntryPassword = Console.ReadLine();
            dbLayer.CreateUser(EntryRole, EntryFirstName, EntryLastName, EntryEmail,
                EntryPhone, EntryUsername, EntryPassword);
            Console.Clear();
            Console.WriteLine($"User {EntryFirstName} {EntryLastName} created");
        }

        public static void AdminViewsUser(List<User> usersList)
        {
            Console.Clear();
            string viewUserDecision = "";
            while (viewUserDecision != "1" && viewUserDecision != "2")
            {
                Console.WriteLine("Press 1 to view a specific user: ");
                Console.WriteLine("Press 2 to view all the users of the system: ");
                viewUserDecision = Console.ReadLine();
                switch (viewUserDecision)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Please type the user's id: ");
                        int userToViewById = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                        User viewedUser = usersList.SingleOrDefault(x => x.Id == userToViewById);
                        if (viewedUser == null)
                        {
                            while (viewedUser == null)
                            {
                                Console.WriteLine("Sorry the given Id doesn't exist in the database!\nPlease try again: ");
                                userToViewById = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                                viewedUser = usersList.SingleOrDefault(x => x.Id == userToViewById);
                            }
                        }
                        Console.Clear();
                        Console.WriteLine($"{viewedUser.Id}: {viewedUser.FirstName} {viewedUser.LastName}, " +
                              $"{viewedUser.Role}, {viewedUser.Email}, {viewedUser.Phone}");
                        break;
                    case "2":
                        Console.Clear();
                        foreach (User user in usersList)
                        {
                            Console.WriteLine($"{user.Id}: {user.FirstName} {user.LastName}, " +
                            $"{user.Role}, {user.Email}, {user.Phone}");
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input try again");
                        break;
                }
            }
        }

        public static void AdminUpdatesUser(List<User> usersList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            Console.Write("Please type the user's id: ");
            int userToUpdateById = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
            User updatedUser = usersList.SingleOrDefault(x => x.Id == userToUpdateById);
            while (updatedUser == null)
            {
                Console.WriteLine("Sorry the given Id doesn't exist in the database!");
                Console.Write("Please re-type the user's id: ");
                userToUpdateById = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                updatedUser = usersList.SingleOrDefault(x => x.Id == userToUpdateById);
            }
            bool isFinishedUpdatingUser = false;
            while (!isFinishedUpdatingUser)
            {
                Console.Clear();
                Console.WriteLine($"You are updating {updatedUser.LastName} {updatedUser.FirstName}");
                Console.WriteLine("Please type what field you want to" +
                    " update: \nFirst Name, Last Name, Email, Phone, Password: ");
                string updateField = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
                while (updateField != "First name" && updateField != "Firstname" &&
                    updateField != "Last name" &&
                    updateField != "Lastname" && updateField != "Email" &&
                    updateField != "Phone" && updateField != "Password")
                {
                    Console.Write("Invalid update field\nPlease try again: ");
                    updateField = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
                }
                Console.Clear();
                Console.Write($"OK {updatedUser.FirstName} {updatedUser.LastName}" +
                $" will be updated.\nPlease type a new {updateField}: ");
                string updateValue = Console.ReadLine();
                dbLayer.UpdateUserSpecificFieldById(userToUpdateById, updateField, updateValue);
                Console.Write("Are you finised updating? (Y/N): ");
                string updateUserFinished = Console.ReadLine();
                if (updateUserFinished == "Y" || updateUserFinished == "y")
                {
                    isFinishedUpdatingUser = true;
                }
            }
        }

        public static void AdminAssignsRoleToUser(List<User> usersList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            Console.Write("Please type the user's id: ");
            int userToAssingRoleById = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
            User assignRoleUser = usersList.SingleOrDefault(x => x.Id == userToAssingRoleById);
            while (assignRoleUser == null)
            {

                Console.WriteLine("Sorry the given Id doesn't exist in the database!");
                Console.Write("Please re-type the user's id: ");
                userToAssingRoleById = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                assignRoleUser = usersList.SingleOrDefault(x => x.Id == userToAssingRoleById);
            }
            Console.Clear();
            Console.WriteLine($"You are assigning a new role to {assignRoleUser.FirstName} " +
                $"{assignRoleUser.LastName}\nPlease type a new role.\nOperator, Captain or User");
            string newRole = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            while (newRole != "Operator" && newRole != "Captain" && newRole != "User")
            {
                Console.Write("Invalid role\nPlease try again: ");
                newRole = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            }
            dbLayer.AssignRoleById(userToAssingRoleById, newRole);
            Console.WriteLine($"{assignRoleUser.FirstName} {assignRoleUser.LastName}" +
                $" is now a {newRole}");
        }

        public static void AdminDeletesUser(List<User> usersList, List<Message> messagesList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            Console.Write("Please type the user's id: ");
            int userToDeleteById = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
            User deletedUser = usersList.SingleOrDefault(x => x.Id == userToDeleteById);
            while (deletedUser == null)
            {
                Console.WriteLine("Sorry the given Id doesn't exist in the database!");
                Console.Write("Please retype the user's id: ");
                userToDeleteById = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                deletedUser = usersList.SingleOrDefault(x => x.Id == userToDeleteById);
            }
            Console.Clear();
            deletedUser.SentMessages = messagesList.FindAll(x => x.SenderId == deletedUser.Id);
            deletedUser.ReceivedMessages = messagesList.FindAll(x => x.ReceiverId == deletedUser.Id);
            foreach (Message message in deletedUser.SentMessages)
            {
                dbLayer.DeleteMessageById(message.Id);
            }
            foreach (Message message in deletedUser.ReceivedMessages)
            {
                dbLayer.DeleteMessageById(message.Id);
            }
            dbLayer.DeleteSystemUserById(userToDeleteById);
            Console.WriteLine($"{deletedUser.FirstName} {deletedUser.LastName} sunk into the deep waters!");   
        }

        public static void AdminCreatesShip(List<Ship> fleet)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            Console.WriteLine("Please insert the new ship's data");
            Console.Write("Imo Number: ");
            int shipImo = ToolsNconstraints.IntDynamicConverter(Console.ReadLine()); ;
            while (fleet.Exists(x => x.ImoNumber == shipImo))
            {
                Console.WriteLine("A ship with that Imo Number already exists please try again");
                shipImo = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
            }
            Console.Write("Name: ");
            string shipName = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            Console.Write("Vessel Type: ");
            string shipType = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            while (!shipType.All(char.IsLetter) && !shipType.Contains(""))
            {
                Console.Write("A Vessel Type can't contain digits\nPlease try again: ");
                shipType = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            }
            Console.Write("Deadweight: ");
            int shipDwt = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
            Console.Write("Flag: ");
            string shipFlag = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            while (!shipFlag.All(char.IsLetter) && !shipFlag.Contains(""))
            {
                Console.Write("Invalid input. Only letters are accepted \nPlease try again: ");
                shipFlag = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
            }
            dbLayer.CreateShip(shipImo, shipName, shipType, shipDwt,
                shipFlag);
            Console.WriteLine($"Ship {shipName} ({shipImo}) created");

        }

        public static void AdminViewsShip(List<Ship> fleet)
        {
            Console.Clear();
            string viewShipDecision = "";
            while (viewShipDecision != "1" && viewShipDecision != "2")
            {
                Console.WriteLine("Press 1 to view a specific ship: ");
                Console.WriteLine("Press 2 to view the whole fleet: ");
                viewShipDecision = Console.ReadLine();
                switch (viewShipDecision)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Please type the ship's Imo Number: ");
                        int shipToViewByImo = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                        Ship viewedShip = fleet.SingleOrDefault(x => x.ImoNumber == shipToViewByImo);
                        while (viewedShip == null)
                        {
                            Console.Write("Sorry the given Imo Number doesn't exist in the " +
                                "database!\nPlease try again: ");
                            shipToViewByImo = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                            viewedShip = fleet.SingleOrDefault(x => x.ImoNumber == shipToViewByImo);
                        }
                        Console.Clear();
                        Console.WriteLine($"Imo Number: {viewedShip.ImoNumber}, Name: {viewedShip.Name}, " +
                           $"Type: {viewedShip.VesselType}, Dwt: {viewedShip.Dwt}, Flag: {viewedShip.Flag}");
                        break;
                    case "2":
                        Console.Clear();
                        foreach (Ship ship in fleet)
                        {
                            Console.WriteLine($"Imo Number: {ship.ImoNumber}, Name: {ship.Name}, " +
                            $"Type: {ship.VesselType}, Deadweight: {ship.Dwt} tons , Flag: {ship.Flag}");
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input try again");
                        break;
                }
            }
        }

        public static void AdminUpdatesShip(List<Ship> fleet)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            Console.Write("Please type the ship's Imo Number: ");
            int shipToUpdateByImo = ToolsNconstraints.IntDynamicConverter(Console.ReadLine()); ;
            Ship updatedShip = fleet.SingleOrDefault(x => x.ImoNumber == shipToUpdateByImo);
            while (updatedShip == null)
            {
                Console.WriteLine("Sorry the given Imo Number doesn't exist in the database!" +
                        "\nPlease try again: ");
                shipToUpdateByImo = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                updatedShip = fleet.SingleOrDefault(x => x.ImoNumber == shipToUpdateByImo);
            }
            bool isFinishedUpdatingShip = false;
            while (!isFinishedUpdatingShip)
            {
                Console.Clear();
                Console.WriteLine("Please type what field you want to" +
                    " update: \nName or Flag: ");
                string updateField = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
                while (updateField != "Name" && updateField != "Flag")
                {
                    Console.Write("Invalid input. Please try again: ");
                    updateField = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
                }
                Console.WriteLine($"OK {updatedShip.Name} ({updatedShip.ImoNumber})" +
                $" {updateField} will be updated. \nPlease type the new {updateField}");
                string updateValue = ToolsNconstraints.WordCaseCorrector(Console.ReadLine());
                dbLayer.UpdateShipSpecificFieldByImoNumber(shipToUpdateByImo, updateField, updateValue);
                Console.Write("Are you finised updating? (Y/N): ");
                string updateShipFinished = Console.ReadLine();
                if (updateShipFinished == "Y" || updateShipFinished == "y")
                {
                    isFinishedUpdatingShip = true;
                }
                           
            }

        }

        public static void AdminDeletesShip(List<Ship> fleet)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            Console.Write("Please type the ship's Imo Number: ");
            int shipToDeleteByImo = ToolsNconstraints.IntDynamicConverter(Console.ReadLine()); ;
            Ship deletedShip = fleet.SingleOrDefault(x => x.ImoNumber == shipToDeleteByImo);
            while (deletedShip == null)
            {
                Console.WriteLine("Sorry the given Imo Number doesn't exist in the database!");
                Console.Write("Please retype the ship's Imo Number: ");
                shipToDeleteByImo = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                deletedShip = fleet.SingleOrDefault(x => x.ImoNumber == shipToDeleteByImo);
            }
            Console.Clear();
            dbLayer.DeleteShipByImoNumber(shipToDeleteByImo);
            Console.WriteLine($"{deletedShip.Name} ({deletedShip.ImoNumber}) deleted. Ahoy!");
        }

        //User Functionality
        public static void UserSendsMessageToUser(User inUser, List<User> usersList, List<Message> messagesList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            FileAccessLayer faLayer = new FileAccessLayer();
            Console.WriteLine("Please type receiver's Id");
            int receiverId = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
            User receiver = usersList.SingleOrDefault(x => x.Id == receiverId);
            while (receiver == null)
            {

                Console.WriteLine("Sorry the given Id doesn't exist in the database!");
                Console.Write("Please type the receiver's Id again: ");
                receiverId = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                receiver = usersList.SingleOrDefault(x => x.Id == receiverId);
            }
            Console.Clear();
            Console.WriteLine("Please type the subject (up to 50 characters)");
            string mSubject = ToolsNconstraints.CharLimiter(Console.ReadLine(), 50);
            Console.WriteLine("Please type the main message (up to 250 characters)");
            string mBody = ToolsNconstraints.CharLimiter(Console.ReadLine(), 250);
            dbLayer.CreateMessage(inUser.Id, receiver.Id, mSubject, mBody);
            Message lastDbMessage = messagesList.LastOrDefault();
            Message sentMessage = new Message(inUser.Id, receiver.Id, mSubject, mBody);
            if (lastDbMessage == null)
            {
                sentMessage.Id = 1;
            }
            else
            {
                sentMessage.Id = lastDbMessage.Id + 1;
            }
            faLayer.FileMessage(sentMessage, inUser, receiver);
            Console.WriteLine("Message sent");
        }
    
        public static void UserReadsMessage(User inUser, List<User> usersList, List<Message> messagesList)
        {
            Console.Clear();
            string readChoice = "";
            while (readChoice != "1" && readChoice != "2" && readChoice != "3")
            {
                Console.WriteLine("Press 1 for received messages");
                Console.WriteLine("Press 2 for sent messages");
                Console.WriteLine("Press 3 to read any message in the system");
                readChoice = Console.ReadLine();
                switch (readChoice)
                {
                    case "1":
                        Console.Clear();
                        IEnumerable<Message> receivedMessages = messagesList.Where(x => x.ReceiverId == inUser.Id).OrderByDescending(x => x.DateOfSubmission);
                        if (receivedMessages.Count() == 0)
                        {
                            Console.WriteLine("Your received messages folder is empty");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Received Messages:");
                            foreach (Message message in receivedMessages)
                            {
                                User fromUser = usersList.Single(x => x.Id == message.SenderId);
                                Console.WriteLine($"From {fromUser.FirstName} {fromUser.LastName} " +
                                    $"\nSent at {message.DateOfSubmission.ToString()}" +
                                    $"\nSubject: \"{message.Subject}\"" +
                                    $"\nMessage: ||{message.Body}|| \n");
                            }
                        }    
                        break;
                    case "2":
                        Console.Clear();
                        IEnumerable<Message> sentMessages = messagesList.Where(x => x.SenderId == inUser.Id).OrderByDescending(x=> x.DateOfSubmission);
                        if (sentMessages.Count() == 0)
                        {
                            Console.WriteLine("Your sent messages folder is empty");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Sent messages:");
                            foreach (Message message in sentMessages)
                            {
                                User toUser = usersList.Single(x => x.Id == message.ReceiverId);
                                Console.WriteLine($"To {toUser.FirstName} {toUser.LastName} " +
                                    $"\nSent at {message.DateOfSubmission.ToString()}" +
                                    $"\nSubject: \"{message.Subject}\"" +
                                    $"\nMessage: ||{message.Body}|| \n");
                            }
                        }
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Press 1 to read any single message: " +
                            "\nPress 0 to read all the messages of the system: ");
                        int messageReadChoice = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                        switch (messageReadChoice)
                        {
                            case 0:
                                Console.Clear();
                                if (messagesList.Count() == 0)
                                {
                                    Console.WriteLine("The are no system messages!");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Message:");
                                    foreach (Message message in messagesList.OrderByDescending(x=> x.DateOfSubmission))
                                    {
                                        User fromUser = usersList.Single(x => x.Id == message.SenderId);
                                        User toUser = usersList.Single(x => x.Id == message.ReceiverId);
                                        Console.WriteLine($"{message.Id}: From {fromUser.LastName} " +
                                        $"to {toUser.LastName}" +
                                        $"\nSent at {message.DateOfSubmission.ToString()}" +
                                        $"\nSubject: \"{message.Subject}\"" +
                                        $"\nMessage: ||{message.Body}|| \n");
                                    }
                                }
                                break;
                            case 1:
                                Console.Clear();
                                Console.WriteLine("Please type message's Id");
                                int mID = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                                Message singleMessage = messagesList.SingleOrDefault(x => x.Id == mID);
                                while (singleMessage == null)
                                {

                                    Console.WriteLine("Sorry the given message Id doesn't exist in the database!");
                                    Console.Write("Please type message's Id again: ");
                                    mID = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                                    singleMessage = messagesList.SingleOrDefault(x => x.Id == mID);
                                }
                                User fromUser2 = usersList.Single(x => x.Id == singleMessage.SenderId);
                                User toUser2 = usersList.Single(x => x.Id == singleMessage.ReceiverId);
                                Console.Clear();
                                Console.WriteLine("Messages: ");
                                Console.WriteLine($"{singleMessage.Id}: From {fromUser2.LastName} " +
                                $"to {toUser2.LastName}" +
                                $"\nSent at {singleMessage.DateOfSubmission.ToString()}" +
                                $"\nSubject: \"{singleMessage.Subject}\"" +
                                $"\nMessage: ||{singleMessage.Body}||");
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Invalid input. Program will exit");
                                Environment.Exit(1);
                                break;
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input please try again");
                        break;
                } 
            }
        }

        public static void UserEditsMessage(User inUser, List<User> usersList, List<Message> messagesList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            string editChoice = "";
            while (editChoice != "1" && editChoice !="2")
            {
                Console.WriteLine("Press 1 to if you want to edit your last sent message: ");
                Console.WriteLine("Press 2 if you want to edit any message in the database: ");
                editChoice = Console.ReadLine();
                switch (editChoice)
                {
                    case "1":
                        Console.Clear();
                        Message lastSentMessage = messagesList.LastOrDefault(x => x.SenderId == inUser.Id);
                        if(lastSentMessage == null)
                        {
                            Console.WriteLine("You have no sent messages");
                        }
                        else
                        {
                            User toUser = usersList.Single(x => x.Id == lastSentMessage.ReceiverId);
                            Console.WriteLine("This is your last message.");
                            Console.WriteLine($"To {toUser.FirstName} {toUser.LastName} " +
                            $"\nSent at {lastSentMessage.DateOfSubmission.ToString()}" +
                            $"\nSubject: \"{lastSentMessage.Subject}\"" +
                            $"\nMessage: ||{lastSentMessage.Body}|| \n");
                            Console.Write("Edit the Subject: ");
                            string newSubject = ToolsNconstraints.CharLimiter(Console.ReadLine(), 50);
                            Console.Write("Edit the message body: ");
                            string newBody = ToolsNconstraints.CharLimiter(Console.ReadLine(), 250);
                            dbLayer.UpdateMessageFullById(lastSentMessage.Id, newSubject, newBody);
                            Console.WriteLine($"Message with ID {lastSentMessage.Id} updated");
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Type the id of the message you want to update: ");
                        int updatedMessageId = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                        Message updatedMessage = messagesList.SingleOrDefault(x => x.Id == updatedMessageId);
                        while (updatedMessage == null)
                        {
                            Console.WriteLine("Sorry the given message ID doesn't exist in the database!");
                            Console.Write("Please type the message ID again: ");
                            updatedMessageId = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                            updatedMessage = messagesList.SingleOrDefault(x => x.Id == updatedMessageId);
                        }
                        User fromUserUpdated = usersList.Single(x => x.Id == updatedMessage.SenderId);
                        User toUserUpdated = usersList.Single(x => x.Id == updatedMessage.ReceiverId);
                        Console.Clear();
                        Console.WriteLine("Here is the message:");
                        Console.WriteLine($"{updatedMessage.Id}: From {fromUserUpdated.LastName} " +
                        $"to {toUserUpdated.LastName}" +
                        $"\nSent at {updatedMessage.DateOfSubmission.ToString()}" +
                        $"\nSubject: \"{updatedMessage.Subject}\"" +
                        $"\nMessage: ||{updatedMessage.Body}|| \n");
                        Console.Write("Edit the Subject: ");
                        string newUpdatedSubject = ToolsNconstraints.CharLimiter(Console.ReadLine(),250);
                        Console.Write("Edit the message body: ");
                        string newUpdatedBody = ToolsNconstraints.CharLimiter(Console.ReadLine(),250);
                        dbLayer.UpdateMessageFullById(updatedMessage.Id, newUpdatedSubject, newUpdatedBody);
                        Console.WriteLine($"Message with ID {updatedMessage.Id} updated");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input please try again");
                        break;
                }
            }
        }

        public static void UserDeletesMessage(User inUser, List<Message> messagesList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            string deleteChoice = "";
            while (deleteChoice != "1" && deleteChoice != "2" && deleteChoice != "3")
            {
                Console.WriteLine("Press 1 to delete all received messages");
                Console.WriteLine("Press 2 to delete all sent messages");
                Console.WriteLine("Press 3 to delete any message in the database");
                deleteChoice = Console.ReadLine();
                switch (deleteChoice)
                {
                    case "1":
                        Console.Clear();
                        IEnumerable<Message> receivedMessages = messagesList.Where(x => x.ReceiverId == inUser.Id);
                        if(receivedMessages.ToList().Count == 0)
                        {
                            Console.WriteLine("You received messages folder is already empty");
                        }
                        else
                        {
                            int deleteCount = 0;
                            foreach (Message message in receivedMessages)
                            {
                                dbLayer.DeleteMessageById(message.Id);
                                deleteCount++;
                            }
                            Console.WriteLine($"{deleteCount} messages deleted");
                            break;
                        }
                        break;
                    case "2":
                        Console.Clear();
                        IEnumerable<Message> sentMessages = messagesList.Where(x => x.SenderId == inUser.Id);
                        if(sentMessages.ToList().Count == 0)
                        {
                            int deleteCount = 0;
                            foreach (Message message in sentMessages)
                            {
                                dbLayer.DeleteMessageById(message.Id);
                                deleteCount++;
                            }
                            Console.WriteLine($"{deleteCount} messages deleted");
                        }  
                        break;
                    case "3":
                        Console.Clear();
                        if (messagesList.Count == 0)
                        {
                            Console.WriteLine("The database is already empty");
                        }
                        else
                        {
                            string messageDeleteChoice = "";
                            while (messageDeleteChoice != "0" &&  messageDeleteChoice != "1")
                            {
                                Console.WriteLine("Press 1 to delete one message: " +
                                                  "\nPress 0 to delete ALL the messages" +
                                " of the system (Beware: Function available only with super " +
                                "admin priveleges)");
                                messageDeleteChoice = Console.ReadLine();
                                switch (messageDeleteChoice)
                                {
                                    case "0":
                                        Console.Clear();
                                        Console.WriteLine("Super Admin re-Authentication needed");
                                        Console.Write("Retype username: ");
                                        string authUsername = Console.ReadLine();
                                        Console.Write("Retype password: ");
                                        string authPassword = Console.ReadLine();
                                        if (authUsername == "admin" && authPassword == "admin")
                                        {
                                            int allDeletedMessages = messagesList.Count;
                                            foreach (Message message in messagesList)
                                            {
                                                dbLayer.DeleteMessageById(message.Id);
                                            }
                                            Console.Clear();
                                            Console.WriteLine($"All messages deleted! ({allDeletedMessages})");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Authentication failed. " +
                                                "Program will be terminated");
                                            Environment.Exit(1);
                                        }
                                        break;
                                    case "1":
                                        Console.Clear();
                                        Console.WriteLine("Please type message's Id");
                                        int mDiD = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                                        Message deleteMessage = messagesList.SingleOrDefault(x => x.Id == mDiD);
                                        while (deleteMessage == null)
                                        {
                                            Console.WriteLine("Sorry the given message Id doesn't " +
                                                "exist in the database! Please retype the message id: ");
                                            mDiD = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                                            deleteMessage = messagesList.SingleOrDefault(x => x.Id == mDiD);
                                        }
                                        dbLayer.DeleteMessageById(deleteMessage.Id);
                                        Console.WriteLine("Message deleted...");
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine("Invalid input please try again");
                                        break;
                                }
                            }
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input please try again");
                        break;
                }
            }   
        }

        public static void UserGivesCharteringOrder(User inUser, List<User> usersList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            FileAccessLayer faLayer = new FileAccessLayer();
            Console.WriteLine("Please type captain's Id");
            int captainId = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
            User captain = usersList.SingleOrDefault(x => x.Id == captainId);
            while (captain == null)
            {
                Console.WriteLine("Sorry the given Id doesn't exist in the database!");
                Console.Write("Please type the captain's Id again: ");
                captainId = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                captain = usersList.SingleOrDefault(x => x.Id == captainId);        
            }
            Console.Clear();
            Console.WriteLine("Please give the chartering order details (up to 250 characters)");
            string coBody = ToolsNconstraints.CharLimiter(Console.ReadLine(), 250);
            dbLayer.CreateMessage(inUser.Id, captain.Id, "Chartering", coBody);
            Message sentCoOrder = new Message(inUser.Id, captain.Id, "Chartering", coBody);
            faLayer.FileMessage(sentCoOrder, inUser, captain);
            Console.WriteLine("Chartering Order Sent");
        }

        public static void UserGivesBunkeringOrder(User inUser, List<User> usersList)
        {
            Console.Clear();
            DbAccessLayer dbLayer = new DbAccessLayer();
            FileAccessLayer faLayer = new FileAccessLayer();
            Console.WriteLine("Please type operator's Id");
            int operatorId = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
            User Operator = usersList.SingleOrDefault(x => x.Id == operatorId);
            while (Operator == null)
            {
                Console.Write("Sorry the given Id doesn't exist in the database!" +
                    "/nPlease retype the the operators id: ");
                operatorId = ToolsNconstraints.IntDynamicConverter(Console.ReadLine());
                Operator = usersList.SingleOrDefault(x => x.Id == operatorId);
            }
            Console.Clear();
            Console.WriteLine("Please give the bunkering order details (up to 250 characters)");
            string boBody =ToolsNconstraints.CharLimiter(Console.ReadLine(),250);
            dbLayer.CreateMessage(inUser.Id, Operator.Id, "Bunkering", boBody);
            Message sentBoOrder = new Message(inUser.Id, Operator.Id, "Bunkering", boBody);
            faLayer.FileMessage(sentBoOrder, inUser, Operator);
            Console.WriteLine("Bunkering Order sent");   
        }

        public static void UserReadsAllCharteringOrders(User inUser, List<User> usersList, List<Message> messagesList)
        {
            Console.Clear();
            IEnumerable<Message> receivedCharteringOrders =
                    messagesList.OrderByDescending(x => x.DateOfSubmission).Where(x => x.Subject == "Chartering").Where(x => x.ReceiverId == inUser.Id);
            if(receivedCharteringOrders.ToList().Count == 0)
            {
                Console.WriteLine("You have no Chartering Orders");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Received Chartering Orders:");
                foreach (Message message in receivedCharteringOrders)
                {
                    User fromOperator = usersList.Single(x => x.Id == message.SenderId);
                    Console.WriteLine($"From captain {fromOperator.LastName} " +
                        $"\nSent at {message.DateOfSubmission.ToString()}" +
                        $"\nSubject: \"{message.Subject}\"" +
                        $"\nMessage: ||{message.Body}|| \n");
                }
            }   
        }

        public static void UserReadsAllBunkeringOrders(User inUser, List<User> usersList, List<Message> messagesList)
        {
            Console.Clear();
            IEnumerable<Message> receivedBunkeringOrders = messagesList.OrderByDescending(x => x.DateOfSubmission).Where(x => x.Subject == "Bunkering").Where(x=> x.ReceiverId == inUser.Id);
            if (receivedBunkeringOrders.ToList().Count == 0)
            {
                Console.WriteLine("You have no Bunkering Orders");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Received Bunkering Orders:");
                foreach (Message message in receivedBunkeringOrders)
                {
                    User fromCaptain = usersList.Single(x => x.Id == message.SenderId);
                    Console.WriteLine($"From captain {fromCaptain.LastName} " +
                        $"\nSent at {message.DateOfSubmission.ToString()}" +
                        $"\nSubject: \"{message.Subject}\"" +
                        $"\nMessage: ||{message.Body}|| \n");
                }
            }
        } 
    }

}
