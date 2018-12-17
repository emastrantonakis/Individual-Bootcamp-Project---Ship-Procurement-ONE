using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmanouil_mastrantonakis_individualProject
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime DateOfSubmission { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        //Overloaded Constructor
        public Message(int senderId, int receiverId, string subject, string body)
        {
            DateOfSubmission = DateTime.Now;
            SenderId = senderId;
            ReceiverId = receiverId;
            Subject = subject;
            Body = body;
        }

        //Default Constructor
        public Message()
        {
        }
    }
}
