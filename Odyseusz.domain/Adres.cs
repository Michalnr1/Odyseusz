using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odyseusz.domain
{
    public class Adres
    {
        [Key] // Klucz główny
        public int Id { get; set; }

        [Required(ErrorMessage = "Miejscowość jest wymagana.")]
        [StringLength(100, ErrorMessage = "Miejscowość może zawierać maksymalnie 100 znaków.")]
        public required string Miejscowosc { get; set; }

        [Required(ErrorMessage = "Ulica jest wymagana.")]
        [StringLength(100, ErrorMessage = "Ulica może zawierać maksymalnie 100 znaków.")]
        public required string Ulica { get; set; }

        [Required(ErrorMessage = "Numer domu jest wymagany.")]
        [StringLength(20, ErrorMessage = "Numer domu może zawierać maksymalnie 20 znaków.")]
        public required string NrDomu { get; set; }

        [StringLength(20, ErrorMessage = "Numer lokalu może zawierać maksymalnie 20 znaków.")]
        public string? NrLokalu { get; set; }

        [Required(ErrorMessage = "Kraj jest wymagany.")]
        public required string NazwaKraju { get; set; } // Klucz obcy

        public required Kraj Kraj { get; set; }
    }
}
