using System.Data.SQLite;

namespace BestestTheater.WebApp.Models;

public class SeatRepository
{
    public List<Seat> GetAll(int i)
    {

	    var query = @"SELECT id,  show_id, seat_number
FROM seats
WHERE show_id = 1
AND seat_number NOT IN(
	SELECT seat_number 
	FROM Bookings 
	WHERE ShowId = @showId)";

	    using var connection = new SQLiteConnection("Data Source=./database/BestestTheater.db");

	    connection.Open();

	    using var command = connection.CreateCommand();

	    command.CommandText = query;
		command.Parameters.AddWithValue("@showId", i);
	    
	    var reader = command.ExecuteReader();

	    var seats = new List<Seat>();

	    while (reader.Read())
	    {
		    var seat = new Seat
		    {
			    Id = reader.GetInt32(0),
			    ShowId = reader.GetInt32(1),
			    SeatNumber = reader.GetString(2)
		    };
		    seats.Add(seat);
	    }

	    connection.Close();

	    return seats;
    }
}