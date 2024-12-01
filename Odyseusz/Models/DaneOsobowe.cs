namespace Odyseusz.Models
{
    public class DaneOsobowe
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel {  get; set; }
        public string NrTelefonu { get; set; }
        public string Email { get; set; }
        public Adres Adres { get; set; }
    }
}
