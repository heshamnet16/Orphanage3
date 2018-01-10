using DataModelCore.Persons;
using ImageSharp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace DataModelCore.RegularData
{
    [Table("Studies")]
    public class Study
    {
        [Key]
        [Column("ID")]
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

        [Column("Certificate_Photo1")]
        public byte[] CertificatePhotoFront { get; set; }


        [Column("Certificate_Photo2")]
        public byte[] CertificatePhotoBack { get; set; }

        public string Note { get; set; }


        [NotMapped]
        public Image CertificateImage { get => CertificatePhotoFront != null ? new Image(new MemoryStream(this.CertificatePhotoFront)) : null; }


        [NotMapped]
        public Image CertificateImage2 { get => CertificatePhotoBack != null ? new Image(new MemoryStream(this.CertificatePhotoBack)) : null; }


        public virtual ICollection<Orphan> Orphans { get; set; }

    }

}
