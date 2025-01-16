using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Odyseusz.domain
{
    public class Ostrzezenie
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Kraj")]
        public required string KrajNazwa { get; set; }
        public required Kraj Kraj { get; set; }

        public DateTime Data { get; set; }
        public string? Tresc { get; set; }
    }
}
