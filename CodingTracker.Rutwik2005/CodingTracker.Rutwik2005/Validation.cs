using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Rutwik2005
{
    public  class Validation
    {
        private static string connectionString = @"Data Source=CodingTracker.Rutwik2005.db";
        public static bool ValidateDateTimeFormat(string input, out DateTime dateTime)
        {
            return DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out dateTime);
        }


        public static bool ValidateSessionTimes(DateTime startTime,DateTime endTime)
        {
            return endTime > startTime;
        }
        public static bool SessionIdExists(int sessionId)
        {
            using (var connection = new SqliteConnection(connectionString)) // Ensure you're using SqliteConnection
            {
                connection.Open(); // Always open the connection
                string sql = "SELECT COUNT(1) FROM CodingSession WHERE Id = @Id;";
                return connection.ExecuteScalar<int>(sql, new { Id = sessionId }) > 0; // Dapper executes the query and returns the count
            }
        }

    }
}
