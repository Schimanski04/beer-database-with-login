using System.ComponentModel.DataAnnotations;

namespace BeerDatabase.Model
{
    public class Kind
    {
        [Key]
        public int KindId { get; set; }

        public string Type { get; set; }


        // Vztahy mezi tabulkami
        // 1:N
        public List<Beer> Beers { get; set; }
    }
}
