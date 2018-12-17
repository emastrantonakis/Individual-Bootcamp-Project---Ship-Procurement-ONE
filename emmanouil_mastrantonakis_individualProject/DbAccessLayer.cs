using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace emmanouil_mastrantonakis_individualProject
{
    public class DbAccessLayer
    {
        public const string ConnectionString = "Server=localhost\\SQLExpress; Database=ShipProcurement;Integrated Security=SSPI;";

        //Queries
        private const string _CreateShipQuery = "INSERT INTO Ships (ImoNumber, Name, VesselType, Dwt, Flag) VALUES(@ImoNumber, @Name, @VesselType, @Dwt, @Flag)";
        private const string _CreateSystemUserQuery = "INSERT INTO SystemUsers (FirstName, LastName, Role, Email, Phone, Username, Password) VALUES(@FirstName, @LastName, @Role, @Email, @Phone, @Username, @Password)";
        private const string _CreateMessageQuery = "INSERT INTO Messages (SenderId, ReceiverId, DateTime, Subject, Body) VALUES(@SenderId, @ReceiverId, @DateTime, @Subject, @Body)";
        private const string _getAllSystemUsersQuery = "SELECT * FROM SystemUsers";
        private const string _getAllShipsQuery = "SELECT * FROM Ships";
        private const string _getAllMessages = "SELECT * FROM Messages";
        private const string _DeleteSystemUserByIdQuery = "DELETE FROM SystemUsers WHERE Id =  @Id";
        private const string _DeleteShipByImoNumberQuery = "DELETE FROM Ships Where ImoNumber = @ImoNumber ";
        private const string _DeleteMessageByIdQuery = "DELETE FROM Messages WHERE Id = @Id";

        //Default Constructor
        public DbAccessLayer()
        {

        }

        //Create Methods
        public void CreateUser(string role, string firstName, string lastName, string email, string phone, string username, string password)
        {

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(_CreateSystemUserQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                }
            }

        }

        public void CreateShip(int imoNumber, string name, string vesselType, int dwt, string flag)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(_CreateShipQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@ImoNumber", imoNumber);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@VesselType", vesselType);
                    command.Parameters.AddWithValue("@Dwt", dwt);
                    command.Parameters.AddWithValue("@Flag", flag);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CreateMessage(int senderId, int ReceiverId, string subject, string body)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(_CreateMessageQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@SenderId", senderId);
                    command.Parameters.AddWithValue("@ReceiverId", ReceiverId);
                    command.Parameters.AddWithValue("@DateTime", DateTime.Now);
                    command.Parameters.AddWithValue("@Subject", subject);
                    command.Parameters.AddWithValue("@Body", body);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CreateBunkeringOrder(int senderId, int ReceiverId, string subject, string body)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(_CreateMessageQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@SenderId", senderId);
                    command.Parameters.AddWithValue("@ReceiverId", ReceiverId);
                    command.Parameters.AddWithValue("@DateTime", DateTime.Now);
                    command.Parameters.AddWithValue("@Subject", "Bunkering");
                    command.Parameters.AddWithValue("@Body", body);
                    command.ExecuteNonQuery();
                }
            }
        }

        //View Methods
        public List<User> ViewAllSystemUsers()
        {
            List<User> allUsers = new List<User>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(_getAllSystemUsersQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                User user = new User();
                                user.Id = reader.GetInt32(0);
                                user.FirstName = reader.GetString(1);
                                user.LastName = reader.GetString(2);
                                user.Role = reader.GetString(3);
                                user.Email = reader.GetString(4);
                                user.Phone = reader.GetString(5);
                                user.Username = reader.GetString(6);
                                user.Password = reader.GetString(7);
                                allUsers.Add(user);
                            }
                        }
                    }
                }
            }
            return allUsers;
        }

        public List<Ship> ViewAllShips()
        {
            List<Ship> ships = new List<Ship>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(_getAllShipsQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Ship ship = new Ship();
                                ship.ImoNumber = reader.GetInt32(0);
                                ship.Name = reader.GetString(1);
                                ship.VesselType = reader.GetString(2);
                                ship.Dwt = reader.GetInt32(3);
                                ship.Flag = reader.GetString(4);
                                ships.Add(ship);
                            }
                        }
                    }
                }
            }
            return ships;
        }

        public List<Message> ViewAllMessages()
        {
            List<Message> allMessages = new List<Message>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(_getAllMessages, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Message message = new Message();
                                message.Id = reader.GetInt32(0);
                                message.SenderId = reader.GetInt32(1);
                                message.ReceiverId = reader.GetInt32(2);
                                message.DateOfSubmission = reader.GetDateTime(3);
                                message.Subject = reader.GetString(4);
                                message.Body = reader.GetString(5);
                                allMessages.Add(message);
                            }
                        }
                    }
                }
            }
            return allMessages;
        }

        //Delete Methods
        public void DeleteSystemUserById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(_DeleteSystemUserByIdQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }

        }

        public void DeleteShipByImoNumber(int imoNumber)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(_DeleteShipByImoNumberQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    {
                        command.Parameters.AddWithValue("@ImoNumber", imoNumber);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeleteMessageById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(_DeleteMessageByIdQuery, sqlConnection))
                {
                    sqlConnection.Open();
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }                           
                }
            }

        }

        //Update user Methods
        //User's Id and Username cannot be changed 

        //Updates specific selected field for a specific user
        public void UpdateUserSpecificFieldById( int id, string field, string updateValue)
        {
            field = ToolsNconstraints.WordCaseCorrector(field);
            switch (field)
            {
                case "FirstName":
                case "Firstname":
                case "First Name":
                case "First name":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE SystemUsers SET FirstName = @FirstName WHERE Id = @Id", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@FirstName", updateValue);
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                case "LastName":
                case "Lastname":
                case "Last Name":
                case "Last name":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE SystemUsers SET LastName = @LastName WHERE Id = @Id", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@LastName", updateValue);
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                case "Role":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE SystemUsers SET Role = @Role WHERE Id = @Id", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@Role", updateValue);
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                case "Email":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE SystemUsers SET Email = @Email WHERE Id = @Id", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@Email", updateValue);
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                case "Phone":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE SystemUsers SET Phone = @Phone WHERE Id = @Id", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@Phone", updateValue);
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                case "Password":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE SystemUsers SET Password = @Password WHERE Id = @Id", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@Password", updateValue);
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        //Assigns a role to a specific user
        public void AssignRoleById(int id, string newRole)
        {
            switch (newRole)
            {
                case "User":
                case "Captain":
                case "Operator":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE SystemUsers SET Role = @Role WHERE Id = @Id", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@Role", newRole);
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
 
        }
        
        //Updates all possible fields for a specific user
        public void UpdateUserAllFieldsById(int aId, string aName, string aLastName, string aRole,  string aEmail, string aPhone, string aPassword)
        {
            UpdateUserSpecificFieldById(aId, "Name", aName);

            UpdateUserSpecificFieldById(aId, "LastName", aLastName);

            UpdateUserSpecificFieldById( aId, "Role", aRole);

            UpdateUserSpecificFieldById( aId, "Email", aEmail);

            UpdateUserSpecificFieldById( aId, "Phone", aPhone);

            UpdateUserSpecificFieldById( aId, "Password", aPassword);
        }

        //Update ship Methods
        //Ship's Imo Number, Type and Dwt cannot be changed

        //Updates specific selected field for a specific ship
        public void UpdateShipSpecificFieldByImoNumber(int imoNumber, string field, string updateValue)
        {
            switch (field)
            {
                case "Name":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE Ships SET Name = @Name WHERE ImoNumber = @ImoNumber", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@Name", updateValue);
                            command.Parameters.AddWithValue("@ImoNumber", imoNumber);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                case "Flag":
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command =
                            new SqlCommand("UPDATE Ships SET Flag = @Flag WHERE ImoNumber = @ImoNumber", sqlConnection))
                        {
                            sqlConnection.Open();
                            command.Parameters.AddWithValue("@Flag", updateValue);
                            command.Parameters.AddWithValue("@ImoNumber", imoNumber);
                            command.ExecuteNonQuery();
                        }
                    }
                    break;
                //case "Location":
                //case "location":
                //case "LOCATION":
                //    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                //    {
                //        using (SqlCommand command =
                //            new SqlCommand("UPDATE Ships SET Location = @Location WHERE ImoNumber = @ImoNumber", sqlConnection))
                //        {
                //            sqlConnection.Open();
                //            command.Parameters.AddWithValue("@Location", updateValue);
                //            command.Parameters.AddWithValue("@ImoNumber", imoNumber);
                //            command.ExecuteNonQuery();
                //        }
                //    }
                //    break;
                default:
                    throw new ArgumentException();
            }
        }

        public void UpdateShipAllFieldsByImoNumber(int imoNumber, string aName, string aFlag)
        {
            UpdateShipSpecificFieldByImoNumber(imoNumber, "Name", aName);
            UpdateShipSpecificFieldByImoNumber(imoNumber, "Flag", aFlag);
        }

        //Edit specific Message
        //Message's Id, SenderId, ReceiverId and DateOfSubmission can't be changed

        public void UpdateMessageSubjectById(int id, string newSubject)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand("UPDATE Messages SET Subject = @Subject WHERE Id = @Id", sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@Subject", newSubject);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMessageBodyById(int id, string newBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command =
                    new SqlCommand("UPDATE Messages SET Body = @Body WHERE Id = @Id", sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@Body", newBody);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMessageFullById(int id, string newSubject, string newBody)
        {
            UpdateMessageSubjectById(id, newSubject);
            UpdateMessageBodyById(id, newBody);
        }

    }
}

