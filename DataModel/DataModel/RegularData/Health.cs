using OrphanageDataModel.Persons;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OrphanageDataModel.RegularData
{
    [Table("Healthies")]
    public class Health
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }


        [Column("Name")]
        public string SicknessName { get; set; }

        [Column("Medicen")]
        public string Medicine { get; set; }

        public decimal? Cost { get; set; }


        [Column("SupervisorDoctor")]
        public string SupervisorDoctor { get; set; }

        public string Note { get; set; }



        [Column("ReporteFile")]
        public byte[] ReporteFileData { get; set; }



        [NotMapped]
        public MemoryStream ReporteFile { get => ReporteFileData != null ? new MemoryStream(this.ReporteFileData) : null; }

        [NotMapped]
        public string ReporteFileURI { get; set; }

        
        public virtual ICollection<Orphan> Orphans { get; set; }

    }
}
