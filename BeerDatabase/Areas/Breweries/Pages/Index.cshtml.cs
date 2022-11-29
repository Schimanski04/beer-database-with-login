using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BeerDatabase.Areas.Breweries.Pages
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
        public List<Brewery> Breweries { get; set; }

        public void OnGet()
        {
            Breweries = _context.Breweries.ToList();

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
