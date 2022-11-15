using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BeerDatabase.Areas.Pubs.Pages
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
        public Pub Pub { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte hodnocení hospody!")]
        public string Rating { get; set; }

        public ActionResult OnGet(int id)
        {
            Pub = _context.Pubs.Find(id);
            if (Pub == null)
            {
                return NotFound();
            }
            Rating = Convert.ToString(Pub.Rating);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
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

                var change = await _context.Pubs.FindAsync(id);
                change.Name = Pub.Name;
                change.Location = Pub.Location;
                change.PhoneNumber = Pub.PhoneNumber;
                change.Rating = Pub.Rating;

                await _context.SaveChangesAsync();

                SuccessMessage = "Úspìšnì jste u hospody zmìnili údaje v databázi";
                return RedirectToPage("./Index", new { area = "" });
            }
        }
    }
}
