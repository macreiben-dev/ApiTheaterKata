using BestestTheater.WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestestTheater.WebApp.Pages
{
    public class MyBookingsModel : PageModel
    {
        private readonly BusinessLayerServiceFacade _showService;

        public MyBookingsModel(BusinessLayerServiceFacade showService)
        {
            _showService = showService;
        }
        public IEnumerable<BkngData_3> BkngData { get; private set; }
     
        public void OnGet()
        {
            BkngData = _showService.GetMyBookings();
        }
    }
}
