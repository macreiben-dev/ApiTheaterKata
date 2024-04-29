namespace BestestTheater.WebApp.Models;

public class BookingInfo
{
    public int ShowId { get; set; }
    public int UserId { get; set; }
    public List<string> Seats { get; set; }
}