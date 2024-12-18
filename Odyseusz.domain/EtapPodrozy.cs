using System.ComponentModel.DataAnnotations;

namespace Odyseusz.domain
{
    public class EtapPodrozy
    {
        [Key] // Klucz główny
        public int Id { get; set; }

        [Required(ErrorMessage = "Data przyjazdu jest wymagana.")]
        public required DateOnly DataPrzyjazdu { get; set; }

        [Required(ErrorMessage = "Data wyjazdu jest wymagana.")]
        [DateGreaterThan(nameof(DataPrzyjazdu), ErrorMessage = "Data wyjazdu musi być późniejsza niż data przyjazdu.")]
        public required DateOnly DataWyjazdu { get; set; }

        [Required(ErrorMessage = "Organizator pobytu jest wymagany.")]
        public required OrganizatorPobytu OrganizatorPobytu { get; set; }

        public int AdresId { get; set; } // Klucz obcy

        [Required(ErrorMessage = "Adres jest wymagany.")]
        public required Adres Adres { get; set; }
    }
}
