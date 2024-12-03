namespace Odyseusz.domain
{
    public class DaneOsobowe
    {
        public int Id {  get; set; } // Klucz główny
        public required string Imie {  get; set; }
        public required string Nazwisko { get; set; }
        public required string Pesel { get; set; }
        public required string NrTelefonu { get; set; }
        public required string Email { get; set; }
        public int AdresId { get; set; } // Klucz obcy
        public required Adres Adres { get; set; }
    }
}
