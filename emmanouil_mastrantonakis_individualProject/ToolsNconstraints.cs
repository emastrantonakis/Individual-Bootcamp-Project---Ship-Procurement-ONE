using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emmanouil_mastrantonakis_individualProject
{
   public static  class ToolsNconstraints
    {
        //Takes a string and makes the first letter uppercase and the rest of the word lowercase
        public static string WordCaseCorrector(string word)
        {
            while (word.Length == 0 || word.All(char.IsNumber))
            {
                Console.Write("Invalid input please try again: ");
                word = Console.ReadLine();
            }
            string wordLowered = word.ToLower();
            string wordCorrected = wordLowered.First().ToString().ToUpper() + String.Join("", wordLowered.Skip(1));
            return wordCorrected;
        }

        //Takes a string input and if it consists only of numbers it makes it an Int
        //If not it asks for another console input and rechecks the value
        public static int IntDynamicConverter(string input)
        {
            while (!input.All(char.IsNumber) || input.Length == 0)
            {
                Console.Write("Invalid input please try again: ");
                input = Console.ReadLine();
            }
            return Convert.ToInt32(input);
        }
           
        //Takes a string and returns the chosen limited n of first characters
        public static string CharLimiter(string input, int limit)
        {
            string wordLimited = input.Substring(0, input.Length > limit ? limit : input.Length);
            return wordLimited;
        }
    }
}
