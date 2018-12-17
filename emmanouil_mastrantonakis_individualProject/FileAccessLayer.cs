using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace emmanouil_mastrantonakis_individualProject
{
    public class FileAccessLayer
    {

        public void FileMessage(Message message, User sender, User receiver)
        {
            string[] lines = new string[8];
            lines[0] = message.DateOfSubmission.ToString();
            lines[1] = sender.FirstName + " " + sender.LastName;
            lines[2] = "to";
            lines[3] = receiver.FirstName + " " + receiver.LastName;
            lines[4] = message.Subject;
            lines[5] = message.Body;
            lines[6] = " ";
            lines[7] = " ";

            
            //Saves a diffent txt file for every message (named by the message id)
            File.WriteAllLines(@"C:\\TestFiles\Message"+message.Id+".txt", lines);

            //Saves all the messages in one file
            File.AppendAllLines(@"C:\\TestFiles\AllMessages.txt", lines);

        }
    }
}
