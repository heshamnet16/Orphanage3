using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OrphanageDataModel.RegularData
{
    [Table("Healthies")]
    public class Health
    {
        [Key]
        [Column("ID", TypeName = "int")]
        public int Id { get; set; }


        [Column("Name")]
        public int? SicknessName { get; set; }

        [Column("Medicen")]
        public int? Medicine { get; set; }

        public decimal? Cost { get; set; }


        [Column("SupervisorDoctor")]
        public int? SupervisorDoctor { get; set; }

        public string Note { get; set; }



        [Column("ReporteFile", TypeName = "varbinary(max)")]
        public byte[] ReporteFileData { get; set; }



        [NotMapped]
        public MemoryStream ReporteFile { get => ReporteFileData != null ? new MemoryStream(this.ReporteFileData) : null; }

    }
}
