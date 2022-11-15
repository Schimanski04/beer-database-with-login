using System.ComponentModel.DataAnnotations;

namespace BeerDatabase.Model
{
    public class Pub
    {
        [Key]
        public int PubId { get; set; }

        [Required(ErrorMessage = "Zadejte název hospody!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Zadejte adresu hospody!")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Zadejte telefonní číslo do hospody!")]
        public string? PhoneNumber { get; set; }

        public double Rating { get; set; }


        // Vztahy mezi tabulkami
        // N:M
        public List<Beer>? Beers { get; set; }
    }
}
