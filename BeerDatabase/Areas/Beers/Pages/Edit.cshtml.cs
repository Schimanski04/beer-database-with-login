using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BeerDatabase.Areas.Beers.Pages
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
        public Beer Beer { get; set; }

        public List<Brewery> Breweries { get; set; }
        public List<Kind> Kinds { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte množství alkoholu v pivu!")]
        public string AlcoholContent { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte cenu piva!")]
        public string PricePerHalfLitre { get; set; }

        public ActionResult OnGet(int id)
        {
            Beer = _context.Beers.Find(id);
            if (Beer == null)
            {
                return NotFound();
            }

            Breweries = _context.Breweries.ToList();
            Kinds = _context.Kinds.ToList();
            AlcoholContent = Convert.ToString(Beer.AlcoholContent);
            PricePerHalfLitre = Convert.ToString(Beer.PricePerHalfLitre);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
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

                var change = await _context.Beers.FindAsync(id);
                change.Name = Beer.Name;
                change.BreweryId = Beer.BreweryId;
                change.KindId = Beer.KindId;
                change.Description = Beer.Description;
                change.AlcoholContent = Beer.AlcoholContent;
                change.PricePerHalfLitre = Beer.PricePerHalfLitre;
                change.ImgSrc = Beer.ImgSrc;
                change.ImgTitle = Beer.ImgTitle;

                await _context.SaveChangesAsync();

                SuccessMessage = "Úspìšnì jste u piva zmìnili údaje v databázi";
                return RedirectToPage("./Index", new { area = "" });
            }
        }
    }
}
