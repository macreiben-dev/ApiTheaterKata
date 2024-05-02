namespace BestTheater.DontTouchMe.Kata;

public static class BootShows
{
    public static void CreateShowAndFeed()
    {
        using var connection = ConnectionFactory.CreateConnection();

        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "CREATE TABLE Shows (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT) ";

        cmd.ExecuteNonQuery();

        var shows = new List<Show>();

        for (int i = 1; i <= 50; i++)
        {
            var show = new Show
            {
                Id = i,
                Title = $"Dummy Show {i}"
            };

            shows.Add(show);
        }

        {
            var transaction = connection.BeginTransaction();

            foreach (var show in shows)
            {
                cmd.CommandText ="INSERT INTO Shows (Title) VALUES (@title)";
                cmd.Parameters.AddWithValue("@title", show.Title);

                cmd.ExecuteNonQuery();
            }
            
            transaction.Commit();
        }
        connection.Close();
    }
}