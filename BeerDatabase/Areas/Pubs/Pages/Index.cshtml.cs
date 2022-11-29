using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BeerDatabase.Areas.Pubs.Pages
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

        public bool IsAdministrator { get; set; }
        public List<Beer> Beers { get; set; }
        public List<Pub> Pubs { get; set; }

        public string NameSort { get; set; }
        public string RatingSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            RatingSort = sortOrder == "Rating" ? "rating_desc" : "Rating";

            CurrentFilter = searchString;

            IQueryable<Pub> pubsSort = from p in _context.Pubs
                                       select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                pubsSort = pubsSort.Where(p => p.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    pubsSort = pubsSort.OrderByDescending(s => s.Name);
                    break;
                case "Rating":
                    pubsSort = pubsSort.OrderBy(s => s.Rating);
                    break;
                case "rating_desc":
                    pubsSort = pubsSort.OrderByDescending(s => s.Rating);
                    break;
                default:
                    pubsSort = pubsSort.OrderBy(s => s.Name);
                    break;
            }

            Pubs = await pubsSort.AsNoTracking().ToListAsync();
            Beers = _context.Beers.ToList();

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
        }
    }
}
