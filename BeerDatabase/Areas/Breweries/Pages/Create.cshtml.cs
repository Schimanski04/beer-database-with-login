using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerDatabase.Areas.Breweries.Pages
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
        public Brewery Brewery { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Brewery.Beers");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                _context.Breweries.Add(Brewery);

                await _context.SaveChangesAsync();

                SuccessMessage = "Pivovar byl úspìšnì pøidaný do databáze";
                return RedirectToPage("./Index", new { area = "" });
            }
        }
    }
}
