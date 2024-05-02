namespace BestTheater.DontTouchMe.Kata;

public class BootBookings
{
    public static void CreateAndFeed()
    {
        using var connection = ConnectionFactory.CreateConnection();
        
        connection.Open();

        using var cmd = connection.CreateCommand();
        
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Bookings (Id INTEGER PRIMARY KEY, ShowId INTEGER, UserId INTEGER, SeatNumber TEXT)";

        cmd.ExecuteNonQuery();
    }
}