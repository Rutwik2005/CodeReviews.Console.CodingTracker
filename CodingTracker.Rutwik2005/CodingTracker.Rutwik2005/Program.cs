using CodingTracker.Rutwik2005.Controlllers;
using CodingTracker.Rutwik2005;
using Microsoft.Data.Sqlite; // Adjust this to match your actual namespace
class Program
{
    public static string connectionString = @"Data Source=CodingTracker.Rutwik2005.db";

   public  static void Main(string[] args)
    {
        bool flag = true;

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand sqliteCommand = connection.CreateCommand();
            var tableCmd = sqliteCommand;
             tableCmd.CommandText = @"
CREATE TABLE IF NOT EXISTS CodingSession (
    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Duration INTEGER NOT NULL
);";


            tableCmd.ExecuteNonQuery();
            connection.Close();
        }



        DateTime startTime;
        DateTime endTime ;
        while (flag)
        {
            string inp;
            Display.DisplayMenu();
            inp = Console.ReadLine().Trim().ToLower();
            switch(inp)
            {
                case "v":
                         CodingController.DisplayAllSessions();
                         break;
                case "i":startTime = UserInput.GetSessionTime("Enter the starttime");
                         endTime = UserInput.GetSessionTime("Enter the endtime", startTime);
                         CodingController.CreateCodingSession(startTime,endTime);
                         break;
                case "u":CodingController.UpdateCodingSession();
                         break;
                case "d":CodingController.DeleteCodingSession();
                         break;
                case "e":Console.WriteLine("Exiting the menu");
                         flag = false;
                         break;
                default: Console.WriteLine("Invalid input");
                         break;
            }
        }
    }
   

}
