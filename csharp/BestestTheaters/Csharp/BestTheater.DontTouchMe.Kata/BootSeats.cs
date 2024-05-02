using System.Data.SQLite;

public static class BootSeats
{
    private const string CONNECTION_STRING = "Data Source=./database/BestestTheater.db";
    private const string CREATE_TABLE =
        "CREATE TABLE seats (id INTEGER PRIMARY KEY, seat_number TEXT, session_id INTEGER, FOREIGN KEY(session_id) REFERENCES sessions(id))";

    private const string INSERT = "INSERT INTO seats (seat_number, session_id) VALUES (@seat_number, @session_id)";

    public static void CreateSeatsAndFeed()
    {
        using var connection = new SQLiteConnection("Data Source=./database/BestestTheater.db");

        connection.Open();

        {
            using var cmd = connection.CreateCommand();

            cmd.CommandText = CREATE_TABLE;

            cmd.ExecuteNonQuery();
        }

        var transaction = connection.BeginTransaction();

        foreach (var session in AllSessions())
        {

            using var cmd = connection.CreateCommand();

            for (int i = 0; i < 100; i++)
            {
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@seat_number", "A" + i.ToString("D2"));
                cmd.Parameters.AddWithValue("@session_id", session.Id);

                cmd.ExecuteNonQuery();
            }
        }

        transaction.Commit();

        connection.Close();
    }

    private static IEnumerable<Session> AllSessions()
    {
        using var connection = new SQLiteConnection(CONNECTION_STRING);

        connection.Open();

        using var cmd = connection.CreateCommand();

        cmd.CommandText = "SELECT Id, show_id, show_datetime FROM sessions";

        var reader = cmd.ExecuteReader();

        var sessions = new List<Session>();

        while (reader.Read())
        {
            var session = new Session
            {
                Id = reader.GetInt32(0),
                ShowId = reader.GetInt32(1),
                ShowDateTime = reader.GetDateTime(2)
            };
            sessions.Add(session);
        }

        connection.Close();

        return sessions;
    }
}