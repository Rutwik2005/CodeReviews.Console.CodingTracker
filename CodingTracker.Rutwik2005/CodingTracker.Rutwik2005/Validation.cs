using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Rutwik2005
{
    public  class Validation
    {
        private static string connectionString = @"Data Source=CodingTracker.Rutwik2005.db";
        public static bool ValidateDateTimeFormat(string input, out DateTime dateTime)
        {
            bool isValidFormat = DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm",
                                null, System.Globalization.DateTimeStyles.None,
                                out dateTime);
            if (isValidFormat) 
            {
                if (dateTime.Year < 1 || dateTime.Year > 9999 ||
                    dateTime.Month < 1 || dateTime.Month > 12 ||
                    dateTime.Day < 1 || dateTime.Day > DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
                {
                    isValidFormat = false;
                }
            }

            return isValidFormat;
        }


        public static bool ValidateSessionTimes(DateTime startTime, DateTime endTime)
        {
            return endTime > startTime;
        }

        public static bool SessionIdExists(int sessionId)
        {
            using (var connection = new SqliteConnection(connectionString)) 
            {
                connection.Open(); 
                string sql = "SELECT COUNT(1) FROM CodingSession WHERE Id = @Id;";
                return connection.ExecuteScalar<int>(sql, new { Id = sessionId }) > 0; 
            }
        }

    }
}
