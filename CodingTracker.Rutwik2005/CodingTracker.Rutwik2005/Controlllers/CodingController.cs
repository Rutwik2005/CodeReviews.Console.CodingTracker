using CodingTracker.Rutwik2005;
using CodingTracker.Rutwik2005.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Data;

public class CodingController
{
    private static string connectionString = @"Data Source=CodingTracker.Rutwik2005.db";
    public static void CreateCodingSession(DateTime startTime, DateTime endTime)
    {
        using (IDbConnection connection = new SqliteConnection(connectionString))
        {
            var duration = endTime - startTime;
            var sql = "INSERT INTO CodingSession (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration);";
            connection.Execute(sql, new { StartTime = startTime, EndTime = endTime, Duration = duration });
        }
    }

    public static  List<CodingSession> GetAllCodingSessions()
    {
        using (IDbConnection connection = new SqliteConnection(connectionString))
        {
            string sql = "SELECT * FROM CodingSession;";
            return connection.Query<CodingSession>(sql).AsList();
        }
    }
    public static  void UpdateCodingSession()
    {
        bool flag = true; 
        DateTime startTime;
        DateTime endTime;
        int sessionId;
        DisplayAllSessions();
        while (flag)
        {
            Console.WriteLine("Enter the Session ID you want to update:");
            if (!int.TryParse(Console.ReadLine(), out sessionId) || !Validation.SessionIdExists(sessionId))
            {
                Console.WriteLine("Invalid Session ID. Please try again.");
                continue; 
            }

            Console.WriteLine("Enter the new Start Time (format: yyyy-MM-dd HH:mm):");
            string startInput = Console.ReadLine();
            if (!Validation.ValidateDateTimeFormat(startInput, out startTime))
            {
                Console.WriteLine("Invalid Start Time format. Please use 'yyyy-MM-dd HH:mm'.");
                continue; 
            }

            Console.WriteLine("Enter the new End Time (format: yyyy-MM-dd HH:mm):");
            string endInput = Console.ReadLine();
            if (!Validation.ValidateDateTimeFormat(endInput, out endTime))
            {
                Console.WriteLine("Invalid End Time format. Please use 'yyyy-MM-dd HH:mm'.");
                continue; 
            }

            if (!Validation.ValidateSessionTimes(startTime, endTime))
            {
                Console.WriteLine("End Time must be after Start Time. Please try again.");
                continue; 
            }

            
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = "UPDATE CodingSession SET StartTime = @StartTime, EndTime = @EndTime WHERE Id = @Id;";
                connection.Execute(sql, new { Id = sessionId, StartTime = startTime, EndTime = endTime });
                Console.WriteLine("Session updated successfully.");
            }

            flag = false; 
        }
    }
    public static void DisplayAllSessions()
    {
        var sessions = GetAllCodingSessions();

        if (sessions.Count == 0)
        {
            Console.WriteLine("No coding sessions found.");
            return;
        }

   
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration (Minutes)");

        foreach (var session in sessions)
        {
            table.AddRow(
                session.Id.ToString(),
                session.StartTime.ToString("yyyy-MM-dd HH:mm"),
                session.EndTime.ToString("yyyy-MM-dd HH:mm"),
                ((int)(session.EndTime - session.StartTime).TotalMinutes).ToString()
            );
        }

        AnsiConsole.Render(table);
    }


    public static void DeleteCodingSession()
        {
       
            DisplayAllSessions();

            Console.WriteLine("\nEnter the Session ID you want to delete:");
            int sessionId;
            if (!int.TryParse(Console.ReadLine(), out sessionId))
            {
                Console.WriteLine("Invalid Session ID.");
                return;
            }
                   
            if (!Validation.SessionIdExists(sessionId)) 
            {
                Console.WriteLine("Session ID does not exist.");
                return;
            }
                    
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = "DELETE FROM CodingSession WHERE Id = @Id;";
                connection.Execute(sql, new { Id = sessionId });
                Console.WriteLine($"Session with ID {sessionId} deleted successfully.");
            }
        }
}
