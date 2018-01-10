using OrphanageDataModel.FinancialData;
using OrphanageDataModel.Persons;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.RegularData
{
    [Table("Famlies")]
    public class Family
    {
        [Key]
        [Column("ID", TypeName = "int")]
        public int Id { get; set; }



        [Column("Mother_ID", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int MotherId { get; set; }

  


        [Column("Father_Id", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int FatherId { get; set; }


        
        [Column("Bail_ID", TypeName = "int")]
        public int? BailId { get; set; }




        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsBailed { get; set; }



        [Column("Address_ID", TypeName = "int")]
        public int? AddressId { get; set; }




        [Column("Address_ID2", TypeName = "int")]
        public int? AlternativeAddressId { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("isExcluded", TypeName = "bit")]
        public bool IsExcluded { get; set; }

        [Column("Finncial_State")]
        public string FinncialStatus { get; set; }


        [Column("FamilyCard_Num")]
        public string FamilyCardNumber { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("IsRefugee", TypeName = "bit")]
        public bool IsTheyRefugees { get; set; }


        [Column("Redidence_State")]
        public string ResidenceStatus { get; set; }


        [Column("Residence_Type")]
        public string ResidenceType { get; set; }


        [Column("Color_Mark", TypeName = "bigint")]
        public long? ColorMark { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }

        [Column("User_ID", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int UserId { get; set; }


        public string Note { get; set; }


        [Column("FamilyCardPhoto", TypeName = "varbinary(max)")]
        public byte[] FamilyCardPhotoFrontData { get; set; }


        [Column("FamilyCardPhotoP2", TypeName = "varbinary(max)")]
        public byte[] FamilyCardPhotoBackData { get; set; }


        [NotMapped]
        public Image IdentityCardImageFace { get => FamilyCardPhotoFrontData != null ? Image.FromStream(new MemoryStream(this.FamilyCardPhotoFrontData)) : null; }


        [NotMapped]
        public Image IdentityCardImageBack { get => FamilyCardPhotoBackData != null ? Image.FromStream(new MemoryStream(this.FamilyCardPhotoBackData)) : null; }




        public virtual User ActingUser { get; set; }

    }
}
