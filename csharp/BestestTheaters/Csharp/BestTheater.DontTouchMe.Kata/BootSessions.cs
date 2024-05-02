using System.Data.SQLite;

namespace BestTheater.DontTouchMe.Kata;

public class BootSessions
{
    public static void CreateAndFeed()
    {
        DateTime now = DateTime.Now.Date;
        
        using var connection = new SQLiteConnection("Data Source=./database/BestestTheater.db");
        
        connection.Open();
        
        {
            using var cmd = connection.CreateCommand();

            cmd.CommandText =
                "CREATE TABLE sessions (id INTEGER PRIMARY KEY, show_id INTEGER, show_datetime DATE, FOREIGN KEY(show_id) REFERENCES shows(id))";

            cmd.ExecuteNonQuery();
        }

        {
            var sessionTimes = CreateSessionTimes(now);

            var transaction = connection.BeginTransaction();
            
            using var cmd = connection.CreateCommand();
            
            cmd.CommandText = "SELECT Id FROM shows";
            
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var showId = reader.GetInt32(0);
                
                var cmdInsert = connection.CreateCommand();

                foreach (var sessionTime in sessionTimes)
                {
                    cmdInsert.Parameters.Clear();
                    
                    cmdInsert.CommandText = "INSERT INTO sessions (show_id, show_datetime) VALUES (@show_id, @show_datetime)";
                    cmdInsert.Parameters
                        .AddWithValue("@show_id", showId);
                    cmdInsert.Parameters
                        .AddWithValue("@show_datetime", sessionTime);
                    
                    cmdInsert.ExecuteNonQuery();
                }
                
                cmdInsert.ExecuteNonQuery();
            }
            
            transaction.Commit();
        }
    }

    private static List<DateTime> CreateSessionTimes(DateTime now)
    {
        var sessionTimes = new List<DateTime>();
        var random = new Random();
        for (int i = 0; i < 3; i++)
        {
            var showDate = now.AddDays(random.Next(0, 3));

            var showDateTime = new DateTime(
                showDate.Year,
                showDate.Month,
                showDate.Day, 17+i, 0, 0);
                    
            sessionTimes.Add(showDateTime);
        }

        return sessionTimes;
    }
}