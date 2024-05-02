namespace BestTheater.DontTouchMe.Kata;

public static class BootShows
{
    public static void CreateShowAndFeed()
    {
        using var connection = ConnectionFactory.CreateConnection();

        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "CREATE TABLE Shows (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Date TEXT) ";

        cmd.ExecuteNonQuery();

        var shows = new List<Show>();
        var random = new Random();

        for (int i = 1; i <= 50; i++)
        {
            var now = DateTime.Now;
            var show = new Show
            {
                Id = i,
                Title = $"Dummy Show {i}",
                Date = now.AddDays(random.Next(0, 3)) // Randomly assign a date within the next 3 days
            };

            shows.Add(show);
        }

        {
            string insertStatement = "INSERT INTO Shows (Date, Title) VALUES ";

            string values = shows.Aggregate("", (current,
                        show) => current + $"('{show.Date.ToString("yyyy-MM-dd")}', '{show.Title}'), ")
                .TrimEnd(',', ' ');

            var insertData = connection.CreateCommand();

            insertData.CommandText = insertStatement + values;

            insertData.ExecuteNonQuery();
        }
        connection.Close();
    }
}