using OrphanageDataModel.Persons;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.RegularData
{
    [Table("Studies")]
    public class Study
    {
        [Key]
        [Column("ID",TypeName ="int")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Stage { get; set; }

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string School { get; set; }

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Univercity { get; set; }

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Collage { get; set; }

        public decimal? MonthlyCost { get; set; }

        public decimal? DegreesRate { get; set; }

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(string))]
        public string Reasons { get; set; }

        [Column("Certificate_Photo1", TypeName = "varbinary(max)")]
        public byte[] CertificatePhotoFront { get; set; }


        [Column("Certificate_Photo2", TypeName = "varbinary(max)")]
        public byte[] CertificatePhotoBack { get; set; }

        public string Note { get; set; }


        [NotMapped]
        public Image CertificateImageFace { get => CertificatePhotoFront != null ? Image.FromStream(new MemoryStream(this.CertificatePhotoFront)) : null; }


        [NotMapped]
        public Image CertificateImageBack { get => CertificatePhotoBack != null ? Image.FromStream(new MemoryStream(this.CertificatePhotoBack)) : null; }


        public virtual ICollection<Orphan> Orphans { get; set; }

    }
}
