using System.Data.SQLite;

namespace BestestTheater.WebApp.Models;

public class BusinessRepository
{
    public static List<Show> AllShows()
    {
        using var connection = new SQLiteConnection("Data Source=./database/BestestTheater.db");

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM shows";
        
        var reader = command.ExecuteReader();
        
        var shows = new List<Show>();
        
        while (reader.Read())
        {
            var show = new Show
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Date = reader.GetDateTime(2)
            };
            shows.Add(show);
        }
        
        connection.Close();

        return shows.ToList();
    }
}