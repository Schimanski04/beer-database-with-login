using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BeerDatabase.Areas.Pubs.Pages
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
        public Pub? Pub { get; set; }

        public ActionResult OnGet(int id)
        {
            Pub = _context.Pubs.Find(id);
            if (Pub == null)
            {
                return NotFound();
            }

            _context.Entry(Pub).Collection(b => b.Beers).Load();

            ClaimsPrincipal cp = this.User;
            if (cp.IsInRole("Administrator"))
            {
                IsAdministrator = true;
            }
            else
            {
                IsAdministrator = false;
            }

            //Console.WriteLine(IsAdministrator);

            return Page();
        }
    }
}
