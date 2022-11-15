using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeerDatabase.Areas.BeerPubs.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        private readonly ApplicationDbContext _context;

        public CreateModel(ILogger<CreateModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string SuccessMessage { get; set; } = "";

        [BindProperty]
        public BeerPub BeerPub { get; set; }

        public List<Beer> Beers { get; set; }
        public List<Pub> Pubs { get; set; }

        public void OnGet()
        {
            Beers = _context.Beers.ToList();
            Pubs = _context.Pubs.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("BeerPub.Beer");
            ModelState.Remove("BeerPub.Pub");

            if (!ModelState.IsValid)
            {
                Beers = _context.Beers.ToList();
                Pubs = _context.Pubs.ToList();
                return Page();
            }
            else
            {
                _context.BeerPubs.Add(new BeerPub { BeerId = BeerPub.BeerId, PubId = BeerPub.PubId });

                await _context.SaveChangesAsync();

                SuccessMessage = "Vazba byla úspìšnì pøidaná do databáze";
                return RedirectToPage("./Index", new { area = "" });
            }
        }
    }
}
