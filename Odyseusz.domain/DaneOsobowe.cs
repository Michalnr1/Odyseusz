using System.ComponentModel.DataAnnotations;

namespace Odyseusz.domain
{
    public class DaneOsobowe
    {
        [Key] // Klucz główny
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imię może zawierać maksymalnie 50 znaków.")]
        public required string Imie { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(100, ErrorMessage = "Nazwisko może zawierać maksymalnie 100 znaków.")]
        public required string Nazwisko { get; set; }

        [Required(ErrorMessage = "PESEL jest wymagany.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "PESEL musi składać się dokładnie z 11 cyfr.")]
        public required string Pesel { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [Phone(ErrorMessage = "Wprowadź poprawny numer telefonu.")]
        [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Numer telefonu musi mieć od 9 do 15 cyfr, opcjonalnie z '+' na początku.")]
        public required string NrTelefonu { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Wprowadź poprawny adres e-mail.")]
        [StringLength(100, ErrorMessage = "Adres e-mail może zawierać maksymalnie 100 znaków.")]
        public required string Email { get; set; }

        public int AdresId { get; set; } // Klucz obcy

        [Required(ErrorMessage = "Adres jest wymagany.")]
        public required Adres Adres { get; set; }
    }
}
