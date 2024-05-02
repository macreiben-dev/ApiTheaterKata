using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestestTheater.WebApp.Pages;

public class BookSite : PageModel
{
    public void OnGet()
    {
        var session = HttpContext.Request.Query["showId"].First();
        var session = HttpContext.Request.Query["seatId"].First();

    }
}