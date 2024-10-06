using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace CodingTracker.Rutwik2005.Models
{
    public class CodingSession
    {   public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration 
        { get { return EndTime - StartTime; } }
    }
}
    


