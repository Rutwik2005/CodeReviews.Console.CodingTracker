using Spectre.Console;
using System;

namespace CodingTracker.Rutwik2005.Controlllers
{   public  class UserInput
    {
      public static DateTime GetSessionTime(string prompt, DateTime? minTime = null)
      {
        DateTime time;
         while (true)
         {
          Console.WriteLine(prompt);
          string input = Console.ReadLine();

             if (DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out time))
             {
                    if (minTime == null || time > minTime)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("End time must be greater than start time.");
                    }
             }
                    else
                    {
                        Console.WriteLine("Invalid date/time format. Please use yyyy-MM-dd HH:mm.");
                    }
         }
                return time;
      }

        public static int GetValidSessionId()
        {
            int sessionId;
            while (true)
            {
                Console.WriteLine("Enter the session ID:");
                string input = Console.ReadLine();

                // Validate if input is an integer
                if (int.TryParse(input, out sessionId))
                {
                    // Check if the session ID exists in the database
                    if (Validation.SessionIdExists(sessionId))
                    {
                        break; // Valid ID
                    }
                    else
                    {
                        Console.WriteLine("Session ID does not exist. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter a valid session ID.");
                }
            }
            return sessionId;
        }
    }

}

