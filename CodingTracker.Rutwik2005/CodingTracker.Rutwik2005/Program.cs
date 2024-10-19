using CodingTracker.Rutwik2005.Controlllers;
using CodingTracker.Rutwik2005;
using Microsoft.Data.Sqlite; 
class Program
{
    public static string connectionString = @"Data Source=CodingTracker.Rutwik2005.db";

   public  static void Main(string[] args)
    {   using (var connection = new SqliteConnection(connectionString))
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
        bool flag = true;
        while (flag)
        {
            Display.DisplayMenu();
            flag = UserInput.Switchmenu();
        }
    }
   

}
