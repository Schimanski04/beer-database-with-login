using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace BeerDatabase.Model
{
    public class BeerPub
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Vyberte pivo!")]
        public int BeerId { get; set; }
        public Beer Beer { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Vyberte hospodu!")]
        public int PubId { get; set; }
        public Pub Pub { get; set; }
    }
}
