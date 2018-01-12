using OrphanageDataModel.FinancialData;
using OrphanageDataModel.Persons;
using System;
using System.Collections.Generic;
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
        [Column("ID")]
        public int Id { get; set; }



        [Column("Mother_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int MotherId { get; set; }
        public virtual Mother Mother { get; set; }




        [Column("Father_Id")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int FatherId { get; set; }
        public virtual Father Father { get; set; }



        [Column("Bail_ID")]
        public int? BailId { get; set; }
        public virtual Bail Bail { get; set; }



        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsBailed { get; set; }



        [Column("Address_ID")]
        public int? AddressId { get; set; }
        public virtual Address PrimaryAddress { get; set; }




        [Column("Address_ID2")]
        public int? AlternativeAddressId { get; set; }
        public virtual Address AlternativeAddress { get; set; }



        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("isExcluded")]
        public bool IsExcluded { get; set; }

        [Column("Finncial_State")]
        public string FinncialStatus { get; set; }


        [Column("FamilyCard_Num")]
        public string FamilyCardNumber { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [Column("IsRefugee")]
        public bool IsTheyRefugees { get; set; }


        [Column("Redidence_State")]
        public string ResidenceStatus { get; set; }


        [Column("Residence_Type")]
        public string ResidenceType { get; set; }


        [Column("Color_Mark")]
        public long? ColorMark { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }

        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public string Note { get; set; }


        [Column("FamilyCardPhoto")]
        public byte[] FamilyCardPhotoFrontData { get; set; }


        [Column("FamilyCardPhotoP2")]
        public byte[] FamilyCardPhotoBackData { get; set; }


        [NotMapped]
        public Image FamilyCardImageFace { get => FamilyCardPhotoFrontData != null ? Image.FromStream(new MemoryStream(this.FamilyCardPhotoFrontData)) : null; }


        [NotMapped]
        public Image FamilyCardImageBack { get => FamilyCardPhotoBackData != null ? Image.FromStream(new MemoryStream(this.FamilyCardPhotoBackData)) : null; }


        [NotMapped]
        public string FamilyCardImageFaceURI { get; set; }


        [NotMapped]
        public string FamilyCardImageBackURI { get; set; }
        

        public virtual ICollection<Orphan> Orphans { get; set; }

    }
}
