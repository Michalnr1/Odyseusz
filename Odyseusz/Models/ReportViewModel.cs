namespace Odyseusz.Models
{
    public class ReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime GeneratedDate { get; set; }
        public List<ReportEntry> ReportEntries { get; set; }
        public List<DateTime> MonthsInRange { get; set; }
    }

    public class ReportEntry
    {
        public int Id { get; set; }
        public string NazwaKraju { get; set; }
        public List<MonthlyData> MonthlyData { get; set; }
        public int TotalTrips { get; set; }
    }

    public class MonthlyData
    {
        public DateTime Month { get; set; }
        public int TripCount { get; set; }
    }

}
