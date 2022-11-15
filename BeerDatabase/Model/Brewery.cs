using System.ComponentModel.DataAnnotations;

namespace BeerDatabase.Model
{
    public class Brewery
    {
        [Key]
        public int BreweryId { get; set; }

        [Required(ErrorMessage = "Zadejte název pivovaru!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zadejte adresu pivovaru!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Zadejte telefonní číslo do pivovaru!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Zadejte email do pivovaru!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zadejte adresu náhledového obrázku!")]
        public string ImgSrc { get; set; }

        [Required(ErrorMessage = "Zadejte popis náhledového obrázku!")]
        public string ImgTitle { get; set; }


        // Vztahy mezi tabulkami
        // 1:N
        public List<Beer> Beers { get; set; }
    }
}
