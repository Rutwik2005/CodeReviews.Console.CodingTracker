﻿using Spectre.Console;
using System;

namespace CodingTracker.Rutwik2005.Controlllers
{   public  class UserInput
    {

       public static DateTime GetSessionTime(string prompt, DateTime? minTime = null)
       {
        DateTime time;
        while (true)
        {
            // Display the prompt using Spectre.Console
            AnsiConsole.MarkupLine($"[yellow]{prompt}[/]");

            // Get the input from the user
            string input = AnsiConsole.Ask<string>("[green]Enter the date and time (yyyy-MM-dd HH:mm):[/]");

            // Try to parse the input date and time
            if (DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out time))
            {
                // Check if the input time is greater than minTime
                if (minTime == null || time > minTime)
                {
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]End time must be greater than start time.[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid date/time format. Please use yyyy-MM-dd HH:mm.[/]");
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

                if (int.TryParse(input, out sessionId))
                {
                    if (Validation.SessionIdExists(sessionId))
                    {
                        break; 
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

        public static bool Switchmenu()
        {
            bool flag = true;
            DateTime startTime;
            DateTime endTime;
            string inp;
            inp = Console.ReadLine().Trim().ToLower();
            switch (inp)
            {
                case "v":
                    CodingController.DisplayAllSessions();
                    break;
                case "i":
                    startTime = UserInput.GetSessionTime("Enter the starttime");
                    endTime = UserInput.GetSessionTime("Enter the endtime", startTime);
                    CodingController.CreateCodingSession(startTime, endTime);
                    break;
                case "u":
                    CodingController.UpdateCodingSession();
                    break;
                case "d":
                    CodingController.DeleteCodingSession();
                    break;
                case "sw":
                    CodingController.TrackCodingSessionWithStopwatch();
                    break;
                case "e":
                    Console.WriteLine("Exiting the menu");
                    flag = false;
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
            return flag;
        }
    }

}

