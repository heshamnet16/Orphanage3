using OrphanageService.DataContext.Persons;
using System.Collections.Generic;

namespace OrphanageService.DataContext.RegularData
{
    public class HealthDC
    {
        public int Id { get; set; }

        public string SicknessName { get; set; }

        public string Medicine { get; set; }

        public decimal? Cost { get; set; }

        public string SupervisorDoctor { get; set; }

        public string Note { get; set; }

        public string ReporteFileURI { get; set; }

        public virtual ICollection<OrphanDC> Orphans { get; set; }

    }
}
