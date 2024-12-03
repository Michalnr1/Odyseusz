namespace Odyseusz.domain
{
    public class EtapPodrozy
    {
        public int Id { get; set; } // Klucz główny
        public required DateOnly DataPrzyjazdu {  get; set; }
        public required DateOnly DataWyjazdu { get; set; }
        public required OrganizatorPobytu OrganizatorPobytu { get; set; }
        public int AdresId { get; set; } // Klucz obcy
        public required Adres Adres { get; set; }
    }
}
