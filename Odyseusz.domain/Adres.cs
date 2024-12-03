namespace Odyseusz.domain
{
    public class Adres
    {
        public int Id { get; set; } // Klucz główny
        public required string Miejscowosc {  get; set; }
        public required string Ulica {  get; set; }
        public required string NrDomu {  get; set; }
        public string? NrLokalu { get; set; }
        public required string NazwaKraju { get; set; } // Klucz obcy
        public required Kraj Kraj { get; set; }
    }
}
