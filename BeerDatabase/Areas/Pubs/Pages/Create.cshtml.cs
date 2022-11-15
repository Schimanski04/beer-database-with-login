using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BeerDatabase.Areas.Pubs.Pages
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
        public Pub Pub { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte hodnocení hospody!")]
        public string Rating { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";
                double rating = Convert.ToDouble(Rating, provider);
                Pub.Rating = rating;

                _context.Pubs.Add(Pub);

                await _context.SaveChangesAsync();

                SuccessMessage = "Hospoda byla úspìšnì pøidaná do databáze";
                return RedirectToPage("./Index", new { area = "" });
            }
        }
    }
}
