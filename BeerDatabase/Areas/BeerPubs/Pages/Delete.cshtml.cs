using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BeerDatabase.Areas.BeerPubs.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;
        private readonly ApplicationDbContext _context;

        public DeleteModel(ILogger<DeleteModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string SuccessMessage { get; set; } = "";
        [TempData]
        public string InfoMessage { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Zvolte vazbu")]
        public string Select { get; set; }

        public List<BeerPub> BeerPubs { get; set; }
        public List<Beer> Beers { get; set; }
        public List<Pub> Pubs { get; set; }

        public void OnGet()
        {
            BeerPubs = _context.BeerPubs.ToList();
            Beers = _context.Beers.ToList();
            Pubs = _context.Pubs.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                string[] subs = Select.Split(';');

                var beerpub = new BeerPub { BeerId = Int32.Parse(subs[0]), PubId = Int32.Parse(subs[1]) };
                _context.BeerPubs.Remove(beerpub);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                InfoMessage = "Odstranìní vazby se nepodaøilo";
                return RedirectToPage("./Index", new { area = "" });
            }

            SuccessMessage = "Úspìšnì jste vazbu odstranili z databáze";
            return RedirectToPage("./Index", new { area = "" });
        }
    }
}
