using OrphanageDataModel.RegularData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.Persons
{
    [Table("Fathers")]
    public class Father
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Name")]
        public int NameId { get; set; }
        public virtual Name Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime Birthday { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("Dieday")]
        public DateTime DateOfDeath { get; set; }


        [Column("DeathCertificate_Photo")]
        public byte[] DeathCertificatePhotoData { get; set; }


        [Column("Photo")]
        public byte[] PhotoData { get; set; }


        public string Jop { get; set; }


        public string Story { get; set; }

        [Column("DeathResone")]
        public string DeathReason { get; set; }


        [Column("Color_Mark")]
        public long? ColorMark { get; set; }


        [Column("IdentityCard_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public decimal IdentityCardNumber { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("ActingUser")]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public string Note { get; set; }




        [NotMapped]
        public Image PersonalPhoto { get => PhotoData != null ? Image.FromStream(new MemoryStream(this.PhotoData)) : null; }


        [NotMapped]
        public Image DeathCertificateImage { get => DeathCertificatePhotoData != null ? Image.FromStream(new MemoryStream(this.DeathCertificatePhotoData)) : null; }


        public virtual ICollection<Family> Family { get; set; }

    }
}
