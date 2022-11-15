using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerDatabase.Areas.Breweries.Pages
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

        public List<Beer> Beers { get; set; }
        public List<BeerPub> BeerPubs { get; set; }

        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            Beers = _context.Beers.ToList();
            BeerPubs = _context.BeerPubs.ToList();

            try
            {
                foreach (var b in Beers)
                {
                    if (b.BreweryId == id)
                    {
                        foreach (var bp in BeerPubs)
                        {
                            if (bp.BeerId == b.BeerId)
                            {
                                _context.BeerPubs.Remove(bp);
                            }
                        }
                        _context.Beers.Remove(b);
                    }
                }
                _context.Breweries.Remove(await _context.Breweries.FindAsync(id));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                InfoMessage = "Odstranìní pivovaru se nepodaøilo";
                return RedirectToPage("./Index", new { area = "" });
            }

            SuccessMessage = "Úspìšnì jste pivovar odstranili z databáze";
            return RedirectToPage("./Index", new { area = "" });
        }
    }
}
