using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BeerDatabase.Areas.Beers.Pages
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

        public bool IsAdministrator { get; set; }
        public Beer? Beer { get; set; }
        public Brewery? Brewery { get; set; }
        public Kind? Kind { get; set; }

        public ActionResult OnGet(int id)
        {
            Beer = _context.Beers.Find(id);
            if (Beer == null)
            {
                return NotFound();
            }

            Brewery = _context.Breweries.Find(Beer.BreweryId);
            Kind = _context.Kinds.Find(Beer.KindId);

            _context.Entry(Beer).Collection(p => p.Pubs).Load();

            ClaimsPrincipal cp = this.User;
            if (cp.IsInRole("Administrator"))
            {
                IsAdministrator = true;
            }
            else
            {
                IsAdministrator = false;
            }

            return Page();
        }
    }
}
