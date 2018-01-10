using OrphanageDataModel.RegularData;
using System;
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
        [Column("ID", TypeName = "int")]
        public int Id { get; set; }

        [Column("Name_ID", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int NameId { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime Birthday { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("Dieday", TypeName = "datetime")]
        public DateTime DateOfDeath { get; set; }


        [Column("DeathCertificate_Photo", TypeName = "varbinary(max)")]
        public byte[] DeathCertificatePhotoData { get; set; }

        public byte[] PhotoData { get; set; }


        public string Jop { get; set; }


        public string Story { get; set; }

        public string DeathReason { get; set; }


        [Column("Color_Mark", TypeName = "bigint")]
        public long? ColorMark { get; set; }


        [Column("IdentityCard_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int IdentityCardNumber { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int UserId { get; set; }


        public string Note { get; set; }




        [NotMapped]
        public Image PersonalPhoto { get => PhotoData != null ? Image.FromStream(new MemoryStream(this.PhotoData)) : null; }


        [NotMapped]
        public Image DeathCertificateImage { get => DeathCertificatePhotoData != null ? Image.FromStream(new MemoryStream(this.DeathCertificatePhotoData)) : null; }


        public virtual User ActingUser { get; set; }


    }
}
