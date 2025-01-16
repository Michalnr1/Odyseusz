using Microsoft.AspNetCore.Mvc.Rendering;
using Odyseusz.domain;

namespace Odyseusz.Models
{
    public class ReportRequestViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Countries { get; set; }
    }
}
