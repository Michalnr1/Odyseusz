namespace Odyseusz.domain
{
    public class ZgloszeniePodrozy
    {
        public int Id { get; set; } // Klucz główny
        public DateTime DataZgloszenia { get; set; } = DateTime.Today;
        public int DaneOsoboweId { get; set; } // Klucz obcy
        public required DaneOsobowe DaneOsobowe { get; set; }
        public required List<EtapPodrozy> EtapyPodrozy { get; set; } = new();
    }
}
