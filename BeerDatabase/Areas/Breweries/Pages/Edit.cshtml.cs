using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerDatabase.Areas.Breweries.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        private readonly ApplicationDbContext _context;

        public EditModel(ILogger<EditModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string SuccessMessage { get; set; } = "";

        [BindProperty]
        public Brewery Brewery { get; set; }

        public ActionResult OnGet(int id)
        {
            Brewery = _context.Breweries.Find(id);
            if (Brewery == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            ModelState.Remove("Brewery.Beers");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var change = await _context.Breweries.FindAsync(id);
                change.Name = Brewery.Name;
                change.Location = Brewery.Location;
                change.PhoneNumber = Brewery.PhoneNumber;
                change.Email = Brewery.Email;
                change.ImgSrc = Brewery.ImgSrc;
                change.ImgTitle = Brewery.ImgTitle;

                await _context.SaveChangesAsync();

                SuccessMessage = "Úspìšnì jste u pivovaru zmìnili údaje v databázi";
                return RedirectToPage("./Index", new { area = "" });
            }
        }
    }
}
