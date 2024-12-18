using System.ComponentModel.DataAnnotations;

namespace Odyseusz.domain
{
    public class ZgloszeniePodrozy
    {
        [Key] // Klucz główny
        public int Id { get; set; }

        [Required(ErrorMessage = "Data zgłoszenia jest wymagana.")]
        [DataType(DataType.Date, ErrorMessage = "Wprowadź poprawną datę.")]
        public DateTime DataZgloszenia { get; set; } = DateTime.Today;

        public int DaneOsoboweId { get; set; } // Klucz obcy

        [Required(ErrorMessage = "Dane osobowe są wymagane.")]
        public required DaneOsobowe DaneOsobowe { get; set; }

        [Required(ErrorMessage = "Lista etapów podróży jest wymagana.")]
        [MinLength(1, ErrorMessage = "Musisz dodać co najmniej jeden etap podróży.")]
        public required List<EtapPodrozy> EtapyPodrozy { get; set; } = new();
    }
}
