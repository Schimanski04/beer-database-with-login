using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerDatabase.Areas.Beers.Pages
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

        public List<BeerPub> BeerPubs { get; set; }

        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            BeerPubs = _context.BeerPubs.ToList();

            try
            {
                foreach (var bp in BeerPubs)
                {
                    if (bp.BeerId == id)
                    {
                        _context.BeerPubs.Remove(bp);
                    }
                }
                _context.Beers.Remove(await _context.Beers.FindAsync(id));
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                InfoMessage = "Odstranìní piva se nepodaøilo";
                return RedirectToPage("./Index", new { area = "" });
            }

            SuccessMessage = "Úspìšnì jste pivo odstranili z databáze";
            return RedirectToPage("./Index", new { area = "" });
        }
    }
}
