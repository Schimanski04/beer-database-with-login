using BeerDatabase.Data;
using BeerDatabase.Model;
using BeerDatabase.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerDatabase.Areas.Beers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Beer> Beers { get; set; }

        public void OnGet()
        {
            Beers = _context.Beers.ToList();
        }
    }
}
