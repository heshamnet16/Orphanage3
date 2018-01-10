using DataModelCore.FinancialData;
using DataModelCore.RegularData;
using ImageSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace DataModelCore.Persons
{
    [Table("Orphans")]
    public class Orphan
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }


        [Column("Name")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Name")]
        public int NameId { get; set; }
        public virtual Name Name { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime? Birthday { get; set; }


        [Column("IsExcluded")]
        public bool? IsExcluded { get; set; }


        [Column("Education_ID")]
        [ForeignKey("Education")]
        public int? EducationId { get; set; }
        public virtual Study Education { get; set; }


        [Column("Health_ID")]
        [ForeignKey("HealthStatus")]
        public int? HealthId { get; set; }
        public virtual Health HealthStatus { get; set; }


        [Column("FullPhoto")]
        public byte[] FullPhotoData { get; set; }


        [Column("FacePhoto")]
        public byte[] FacePhotoData { get; set; }


        [Column("IdentityNumber")]
        public string IdentityCardNumber { get; set; }


        public byte? FootSize { get; set; }

        public int? Weight { get; set; }

        public int? Tallness { get; set; }

        [Column("Family_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Family")]
        public int FamilyId { get; set; }
        public virtual Family Family { get; set; }


        [Column("IsBailed")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsBailed { get; set; }


        [Column("Bail_ID")]
        [ForeignKey("Bail")]
        public int? BailId { get; set; }
        public virtual Bail Bail { get; set; }


        [Column("BondsMan_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Caregiver")]
        public int CaregiverId { get; set; }
        public virtual Caregiver Caregiver { get; set; }




        [Column("Supporter_ID")]
        [ForeignKey("Guarantor")]
        public int? GuarantorId { get; set; }
        public virtual Guarantor Guarantor { get; set; }



        [Column("Color_Mark")]
        public long? ColorMark { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("ActingUser")]
        public int UserId { get; set; }
        public virtual User ActingUser { get; set; }


        public string Story { get; set; }


        public byte[] FingerPrint { get; set; }


        [Column("BondsManRel")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public string ConsanguinityToCaregiver { get; set; }



        [Column("BirthCertificate_Photo")]
        public byte[] BirthCertificatePhotoData { get; set; }


        [Column("FamilyCardPagePhoto")]
        public byte[] FamilyCardPagePhotoData { get; set; }


        [Column("Gender")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public string Gender { get; set; }


        [Column("Kaid")]
        public int? CivilRegisterNumber { get; set; }


        [Column("Birthplace")]
        public string PlaceOfBirth { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? Age { get; set; }


        [NotMapped]
        public Image FullPhoto { get => FullPhotoData != null ? new Image(new MemoryStream(this.FullPhotoData)) : null; }


        [NotMapped]
        public Image FacePhoto { get => FacePhotoData != null ? new Image(new MemoryStream(this.FacePhotoData)) : null; }

        [NotMapped]
        public Image BirthCertificatePhoto { get => BirthCertificatePhotoData != null ? new Image(new MemoryStream(this.BirthCertificatePhotoData)) : null; }


        [NotMapped]
        public Image FamilyCardPagePhoto { get => FacePhotoData != null ? new Image(new MemoryStream(this.FamilyCardPagePhotoData)) : null; }

    }

}
