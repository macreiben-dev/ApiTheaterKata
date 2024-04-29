using System.Data.SQLite;

namespace BestTheater.DontTouchMe.Kata;

public static class BootSeats
{
    public static void CreateSeatsAndFeed()
    {
        using var connection = new SQLiteConnection("Data Source=./database/BestestTheater.db");

        connection.Open();
        
        {
            using var cmd = connection.CreateCommand();

            cmd.CommandText =
                "CREATE TABLE seats (id INTEGER PRIMARY KEY, seat_number TEXT, show_id INTEGER, FOREIGN KEY(show_id) REFERENCES shows(id))";

            cmd.ExecuteNonQuery();
        }
        
        var transation =  connection.BeginTransaction();

        foreach (var show in AllShows())
        {

            using var cmd = connection.CreateCommand();
            
            for (int i = 0; i < 100; i++)
            {
                cmd.CommandText = "INSERT INTO seats (seat_number, show_id) VALUES (@seat_number, @show_id)";
                cmd.Parameters.AddWithValue("@seat_number", "A" + i.ToString("D2"));
                cmd.Parameters.AddWithValue("@show_id", show.Id);

                cmd.ExecuteNonQuery();
            }
            

        }
        
        transation.Commit();
        
        connection.Close();
    }

    private static IEnumerable<Show> AllShows()
    {
        using var connection = new SQLiteConnection("Data Source=./database/BestestTheater.db");

        connection.Open();
        
        using var cmd = connection.CreateCommand();

        cmd.CommandText = "SELECT * FROM shows";

        var reader = cmd.ExecuteReader();

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

        return shows;
    }
}