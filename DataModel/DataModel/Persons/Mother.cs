using OrphanageDataModel.RegularData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.Persons
{
    [Table("Mothers")]
    public class Mother
    {
        [Key]
        [Column("ID", TypeName = "int")]
        public int Id { get; set; }

        [Column("Name_Id", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int NameId { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime Birthday { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsDead { get; set; }


        [Column("Dieday", TypeName = "datetime2")]
        public DateTime? DateOfDeath { get; set; }



        [Column("IdentityCard_Photo", TypeName = "varbinary(max)")]
        public byte[] IdentityCardPhotoFaceData { get; set; }


        [Column("IdentityCard_Photo2", TypeName = "varbinary(max)")]
        public byte[] IdentityCardPhotoBackData { get; set; }


        [Column("Address_ID", TypeName = "int")]
        public int? AddressId { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsMarried { get; set; }

        public string HusbandName { get; set; }



        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("IsOwnOrphans",TypeName ="bit")]
        public bool? HasSheOrphans { get; set; }


        public string Jop { get; set; }

    
        public decimal? Salary { get; set; }

        public string Story { get; set; }


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
        public Image IdentityCardFace { get => IdentityCardPhotoFaceData != null ? Image.FromStream(new MemoryStream(this.IdentityCardPhotoFaceData)) : null; }


        [NotMapped]
        public Image IdentityCardBack { get => IdentityCardPhotoBackData != null ? Image.FromStream(new MemoryStream(this.IdentityCardPhotoBackData)) : null; }


        public virtual User ActingUser { get; set; }


    }
}
