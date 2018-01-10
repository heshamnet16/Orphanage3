using OrphanageDataModel.RegularData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.Persons
{
    [Table("BondsMen")]
    public class Caregiver
    {
        [Key]
        [Column("ID", TypeName = "int")]
        public int Id { get; set; }

        [Column("Name_Id",TypeName ="int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int NameId { get; set; }


        [Column("Address_ID", TypeName = "int")]
        public int? AddressId { get; set; }


        [Column("IdentityCard_ID")]
        public int? IdentityCardId { get; set; }


        [Column("IdentityCard_Photo", TypeName = "varbinary(max)")]
        public byte[] IdentityCardPhotoFaceData { get; set; }


        [Column("IdentityCard_Photo2", TypeName = "varbinary(max)")]
        public byte[] IdentityCardPhotoBackData { get; set; }

        public string Jop { get; set; }

        public decimal? Income { get; set; }

        [Column("Color_Mark",TypeName ="bigint")]
        public long? ColorMark { get; set; }

        public byte[] FingerPrint { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID",TypeName ="int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int UserId { get; set; }


        public string Note { get; set; }


        [NotMapped]
        public Image IdentityCardImageFace { get => IdentityCardPhotoFaceData != null ? Image.FromStream(new MemoryStream(this.IdentityCardPhotoFaceData)) : null; }


        [NotMapped]
        public Image IdentityCardImageBack { get => IdentityCardPhotoBackData != null ? Image.FromStream(new MemoryStream(this.IdentityCardPhotoBackData)) : null; }


        public virtual User ActingUser { get; set; }

    }
}
