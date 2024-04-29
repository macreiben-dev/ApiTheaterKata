using BestestTheater.WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestestTheater.WebApp.Pages
{
    public class BookModel : PageModel
    {
        public string Message { get; private set; }

        public List<Seat> AllSeats { get; set; }
        
        public string? ShowTitle { get; set; } 

        public void OnGet()
        {
            var session = HttpContext.Request.Query["sessionId"].First();
            
            var id = int.Parse(session);
            
            var show = ServiceBuilder.GetService<ShowRepository>().GetShow(id);

            if (show?.Title != null) ShowTitle = show?.Title;

            AllSeats = ServiceBuilder.GetService<SeatRepository>().GetAll(id);
        }
    }
}