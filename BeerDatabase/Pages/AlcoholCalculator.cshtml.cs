using BeerDatabase.Data;
using BeerDatabase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace BeerDatabase.Pages
{
    public class AlcoholCalculatorModel : PageModel
    {
        private readonly ILogger<AlcoholCalculatorModel> _logger;
        private readonly ApplicationDbContext _context;

        public AlcoholCalculatorModel(ILogger<AlcoholCalculatorModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Vyberte pohlaví!")]
        public string Sex { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte vaší hmotnost!")]
        public int? Weight { get; set; }

        [BindProperty]
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Vyberte pivo, které jste pil nejvíce!")]
        public double? AlcoholContent { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte poèet piv, který jste vypil!")]
        public int? Amount { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Zadejte poèet hodin od požití!")]
        public string TimeSinceDrinking { get; set; }

        public double Promile { get; set; }
        public double TimeToSober { get; set; }
        public double RemainingTime { get; set; }
        public string RemainingTimeString { get; set; }

        public List<Beer> Beers { get; set; }

        public void OnGet()
        {
            Beers = _context.Beers.ToList();
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Beers = _context.Beers.ToList();
                return Page();
            }
            else
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";
                double timeSinceDrinking = Convert.ToDouble(TimeSinceDrinking, provider);

                if (Sex == "male")
                {
                    double weightOfAlcohol = ((Convert.ToDouble(Amount) * 500) * Convert.ToDouble(AlcoholContent) * 0.8) / 100;
                    double promile = weightOfAlcohol / (Convert.ToDouble(Weight) * 0.7);
                    double weightOfAlcoholKnockedDownPerHour = Convert.ToDouble(Weight) * 0.085;

                    Promile = Math.Round(promile, 2);
                    TimeToSober = Math.Round(weightOfAlcohol / weightOfAlcoholKnockedDownPerHour, 2);

                    if (Math.Round(TimeToSober - timeSinceDrinking, 2) < 0)
                    {
                        RemainingTime = 0;
                        RemainingTimeString = "0 h";
                    }
                    else
                    {
                        RemainingTime = TimeToSober - timeSinceDrinking;
                        RemainingTimeString = $"{Math.Floor(RemainingTime)} h {Math.Round(RemainingTime * 60 - Math.Floor(RemainingTime) * 60)} m ({Math.Round(RemainingTime, 2)} h)";
                    }
                }
                else
                {
                    double weightOfAlcohol = ((Convert.ToDouble(Amount) * 500) * Convert.ToDouble(AlcoholContent) * 0.8) / 100;
                    double promile = weightOfAlcohol / (Convert.ToDouble(Weight) * 0.6);
                    double weightOfAlcoholKnockedDownPerHour = Convert.ToDouble(Weight) * 0.1;

                    Promile = Math.Round(promile, 2);
                    TimeToSober = Math.Round(weightOfAlcohol / weightOfAlcoholKnockedDownPerHour, 2);

                    if (Math.Round(TimeToSober - timeSinceDrinking, 2) < 0)
                    {
                        RemainingTime = 0;
                        RemainingTimeString = "0 h";
                    }
                    else
                    {
                        RemainingTime = TimeToSober - timeSinceDrinking;
                        RemainingTimeString = $"{Math.Floor(RemainingTime)} h {Math.Round(RemainingTime * 60 - Math.Floor(RemainingTime) * 60)} m ({Math.Round(RemainingTime, 2)} h)";
                    }
                }

                Beers = _context.Beers.ToList();
                return Page();
            }
        }
    }
}
