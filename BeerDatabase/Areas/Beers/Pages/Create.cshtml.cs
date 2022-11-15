using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BeerDatabase.Areas.Beers.Pages
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
        public Beer Beer { get; set; }

        public List<Brewery> Breweries { get; set; }
        public List<Kind> Kinds { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte množství alkoholu v pivu!")]
        public string AlcoholContent { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte cenu piva!")]
        public string PricePerHalfLitre { get; set; }

        public void OnGet()
        {
            Breweries = _context.Breweries.ToList();
            Kinds = _context.Kinds.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Beer.Brewery");
            ModelState.Remove("Beer.Kind");
            ModelState.Remove("Beer.Pubs");

            if (!ModelState.IsValid)
            {
                Breweries = _context.Breweries.ToList();
                Kinds = _context.Kinds.ToList();
                return Page();
            }
            else
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";
                double alcohol = Convert.ToDouble(AlcoholContent, provider);
                double price = Convert.ToDouble(PricePerHalfLitre, provider);
                Beer.AlcoholContent = alcohol;
                Beer.PricePerHalfLitre = price;

                _context.Beers.Add(Beer);

                await _context.SaveChangesAsync();

                SuccessMessage = "Pivo bylo úspìšnì pøidané do databáze";
                return RedirectToPage("./Index", new { area = "" });
            }
        }
    }
}
