using DataModelCore.RegularData;
using ImageSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace DataModelCore.Persons
{
    [Table("BondsMen")]
    public class Caregiver
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name_Id")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Name")]
        public int NameId { get; set; }
        public virtual Name Name { get; set; }

        [Column("Address_ID")]
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        [Column("IdentityCard_ID")]
        public string IdentityCardId { get; set; }


        [Column("IdentityCard_Photo")]
        public byte[] IdentityCardPhotoFaceData { get; set; }


        [Column("IdentityCard_Photo2")]
        public byte[] IdentityCardPhotoBackData { get; set; }

        public string Jop { get; set; }

        public decimal? Income { get; set; }

        [Column("Color_Mark")]
        public long? ColorMark { get; set; }

        public byte[] FingerPrint { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("ActingUser")]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public string Note { get; set; }


        [NotMapped]
        public Image IdentityCardImageFace { get => IdentityCardPhotoFaceData != null ? new Image(new MemoryStream(this.IdentityCardPhotoFaceData)) : null; }


        [NotMapped]
        public Image IdentityCardImageBack { get => IdentityCardPhotoBackData != null ? new Image(new MemoryStream(this.IdentityCardPhotoBackData)) : null; }


        public virtual ICollection<Orphan> Orphans { get; set; }

    }

}
