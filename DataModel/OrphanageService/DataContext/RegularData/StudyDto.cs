using OrphanageService.DataContext.Persons;
using System.Collections.Generic;

namespace OrphanageService.DataContext.RegularData
{
    public class StudyDto
    {
        public int Id { get; set; }

        public string Stage { get; set; }

        public string School { get; set; }

        public string Univercity { get; set; }

        public string Collage { get; set; }

        public decimal? MonthlyCost { get; set; }

        public decimal? DegreesRate { get; set; }

        public string Reasons { get; set; }

        public string Note { get; set; }

        public string CertificateImageURI { get; set; }

        public string CertificateImage2URI { get; set; }

    }
}
