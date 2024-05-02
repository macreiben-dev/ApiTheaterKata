using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestestTheater.WebApp.Pages;

public class BookSeat : PageModel
{
    public void OnGet()
    {
        var showId = HttpContext.Request.Query["showId"].First();
        var seatId = HttpContext.Request.Query["seatId"].First();

    }
}