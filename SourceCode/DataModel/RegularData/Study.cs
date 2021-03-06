﻿using OrphanageDataModel.Persons;
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
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Stage { get; set; }

        [MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string School { get; set; }

        //[MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Univercity { get; set; }

        //[MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Collage { get; set; }

        public decimal? MonthlyCost { get; set; }

        public decimal? DegreesRate { get; set; }

        //[MinLength(2, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Reasons { get; set; }

        [Column("Certificate_Photo1")]
        public byte[] CertificatePhotoFront { get; set; }

        [Column("Certificate_Photo2")]
        public byte[] CertificatePhotoBack { get; set; }

        public string Note { get; set; }

        [NotMapped]
        public Image CertificateImage { get => CertificatePhotoFront != null ? Image.FromStream(new MemoryStream(this.CertificatePhotoFront)) : null; }

        [NotMapped]
        public Image CertificateImage2 { get => CertificatePhotoBack != null ? Image.FromStream(new MemoryStream(this.CertificatePhotoBack)) : null; }

        [NotMapped]
        public string CertificateImageURI { get; set; }

        [NotMapped]
        public string CertificateImage2URI { get; set; }

        public virtual ICollection<Orphan> Orphans { get; set; }
    }
}