using DataModelCore.RegularData;
using ImageSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace DataModelCore.Persons
{
    [Table("Mothers")]
    public class Mother
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name_Id")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Name")]
        public int NameId { get; set; }
        public virtual Name Name { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime Birthday { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsDead { get; set; }


        [Column("Dieday")]
        public DateTime? DateOfDeath { get; set; }



        [Column("IdentityCard_Photo")]
        public byte[] IdentityCardPhotoFaceData { get; set; }


        [Column("IdentityCard_Photo2")]
        public byte[] IdentityCardPhotoBackData { get; set; }


        [Column("Address_ID")]
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsMarried { get; set; }

        public string HusbandName { get; set; }



        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("IsOwnOrphans")]
        public bool? HasSheOrphans { get; set; }


        public string Jop { get; set; }


        public decimal? Salary { get; set; }

        public string Story { get; set; }


        [Column("Color_Mark")]
        public long? ColorMark { get; set; }


        [Column("IdentityCard_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public string IdentityCardNumber { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("ActingUser")]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public string Note { get; set; }




        [NotMapped]
        public Image IdentityCardFace { get => IdentityCardPhotoFaceData != null ? new Image(new MemoryStream(this.IdentityCardPhotoFaceData)) : null; }


        [NotMapped]
        public Image IdentityCardBack { get => IdentityCardPhotoBackData != null ? new Image(new MemoryStream(this.IdentityCardPhotoBackData)) : null; }


        public virtual ICollection<Family> Family { get; set; }

    }

}
