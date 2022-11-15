using BeerDatabase.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerDatabase.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string InfoMessage { get; set; }

        public void OnGet()
        {
        }
    }
}
