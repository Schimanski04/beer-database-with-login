using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerDatabase.Areas.Breweries.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private readonly ApplicationDbContext _context;

        public DetailsModel(ILogger<DetailsModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Brewery? Brewery { get; set; }

        public ActionResult OnGet(int id)
        {
            Brewery = _context.Breweries.Find(id);
            if (Brewery == null)
            {
                return NotFound();
            }
            _context.Entry(Brewery).Collection(b => b.Beers).Load();

            return Page();
        }
    }
}
