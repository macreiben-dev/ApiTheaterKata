using BestestTheater.WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestestTheater.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BusinessLayerServiceFacade _showService;
        public IEnumerable<Show> Shows { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, BusinessLayerServiceFacade showService)
        {
            _logger = logger;
            _showService = showService;
        }

        public void OnGet()
        {
            Shows = _showService.FetchShows();
        }
    }
}