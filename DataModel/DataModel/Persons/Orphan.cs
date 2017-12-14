using OrphanageDataModel.FinancialData;
using OrphanageDataModel.RegularData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;

namespace OrphanageDataModel.Persons
{
    [Table("Orphans")]
    public class Orphan
    {
        [Key]
        [Column("ID", TypeName = "int")]
        public int Id { get; set; }


        [Column("Name", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Name")]
        public int NameId { get; set; }
        public virtual Name Name { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime? Birthday { get; set; }


        [Column("IsExcluded", TypeName = "bit")]
        public bool? IsExcluded { get; set; }


        [Column("Education_ID", TypeName = "int")]
        public int? EducationId { get; set; }


        [Column("Health_ID", TypeName = "int")]
        [ForeignKey("HealthStatus")]
        public int? HealthId { get; set; }
        public virtual Health HealthStatus { get; set; }


        [Column("FullPhoto", TypeName = "varbinary(max)")]
        public byte[] FullPhotoData { get; set; }


        [Column("FacePhoto", TypeName = "varbinary(max)")]
        public byte[] FacePhotoData { get; set; }


        [Column("IdentityNumber")]
        public decimal? IdentityCardNumber { get; set; }


        public byte? FootSize { get; set; }

        public int? Weight { get; set; }

        public int? Tallness { get; set; }

        [Column("Family_ID", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Family")]
        public int FamilyId { get; set; }
        public virtual Family Family { get; set; }


        [Column("IsBailed", TypeName = "bit")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public bool IsBailed { get; set; }


        [Column("Bail_ID", TypeName = "int")]
        [ForeignKey("Bail")]
        public int? BailId { get; set; }
        public virtual Bail Bail { get; set; }


        [Column("BondsMan_ID", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        [ForeignKey("Caregiver")]
        public int CaregiverId { get; set; }
        public virtual Caregiver Caregiver { get; set; }




        [Column("Supporter_ID", TypeName = "int")]
        public int? GuarantorId { get; set; }



        [Column("Color_Mark", TypeName = "bigint")]
        public long? ColorMark { get; set; }


        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public DateTime RegDate { get; set; }


        [Column("User_ID", TypeName = "int")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public int UserId { get; set; }


        public string Story { get; set; }


        public byte[] FingerPrint { get; set; }


        [Column("BondsManRel")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public string ConsanguinityToCaregiver { get; set; }



        [Column("BirthCertificate_Photo", TypeName = "varbinary(max)")]
        public byte[] BirthCertificatePhotoData { get; set; }


        [Column("FamilyCardPagePhoto", TypeName = "varbinary(max)")]
        public byte[] FamilyCardPagePhotoData { get; set; }


        [Column("Gender")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(string))]
        public string Gender { get; set; }


        [Column("Kaid", TypeName = "int")]
        public int? CivilRegisterNumber { get; set; }


        [Column("Birthplace")]
        public string PlaceOfBirth { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? Age { get; set; }


        [NotMapped]
        public Image FullPhoto { get => FullPhotoData != null ? Image.FromStream(new MemoryStream(this.FullPhotoData)) : null; }


        [NotMapped]
        public Image FacePhoto { get => FacePhotoData != null ? Image.FromStream(new MemoryStream(this.FacePhotoData)) : null; }

        [NotMapped]
        public Image BirthCertificatePhoto { get => BirthCertificatePhotoData != null ? Image.FromStream(new MemoryStream(this.BirthCertificatePhotoData)) : null; }


        [NotMapped]
        public Image FamilyCardPagePhoto { get => FacePhotoData != null ? Image.FromStream(new MemoryStream(this.FamilyCardPagePhotoData)) : null; }








        public virtual Study Education { get; set; }

        public virtual Guarantor Guarantor { get; set; }

        public virtual User ActingUser { get; set; }
    }
}
