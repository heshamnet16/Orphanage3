using Newtonsoft.Json;
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
        [Column("ID")]
        public int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int NameId { get; set; }

        public virtual Name Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public DateTime? Birthday { get; set; }

        [Column("IsExcluded")]
        public bool? IsExcluded { get; set; }

        [Column("Education_ID")]
        public int? EducationId { get; set; }

        public virtual Study Education { get; set; }

        [Column("Health_ID")]
        public int? HealthId { get; set; }

        public virtual Health HealthStatus { get; set; }

        [Column("FullPhoto")]
        public byte[] FullPhotoData { get; set; }

        [Column("FacePhoto")]
        public byte[] FacePhotoData { get; set; }

        [Column("IdentityNumber")]
        [MinLength(13, ErrorMessageResourceName = "ErrorWrongData", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string IdentityCardNumber { get; set; }

        public byte? FootSize { get; set; }

        public int? Weight { get; set; }

        public int? Tallness { get; set; }

        [Column("Family_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int FamilyId { get; set; }

        public virtual Family Family { get; set; }

        [Column("IsBailed")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public bool IsBailed { get; set; }

        [Column("Bail_ID")]
        public int? BailId { get; set; }

        public virtual Bail Bail { get; set; }

        [Column("BondsMan_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int CaregiverId { get; set; }

        public virtual Caregiver Caregiver { get; set; }

        [Column("Supporter_ID")]
        public int? GuarantorId { get; set; }

        public virtual Guarantor Guarantor { get; set; }

        [Column("Color_Mark")]
        public long? ColorMark { get; set; }

        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public DateTime RegDate { get; set; }

        [Column("User_ID")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public int UserId { get; set; }

        public virtual User ActingUser { get; set; }

        public string Story { get; set; }

        public byte[] FingerPrint { get; set; }

        [Column("BondsManRel")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string ConsanguinityToCaregiver { get; set; }

        [Column("BirthCertificate_Photo")]
        public byte[] BirthCertificatePhotoData { get; set; }

        [Column("FamilyCardPagePhoto")]
        public byte[] FamilyCardPagePhotoData { get; set; }

        [Column("Gender")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Gender { get; set; }

        [Column("Kaid")]
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

        [NotMapped]
        public string FullPhotoURI { get; set; }

        [NotMapped]
        public string FacePhotoURI { get; set; }

        [NotMapped]
        public string BirthCertificatePhotoURI { get; set; }

        [NotMapped]
        public string FamilyCardPagePhotoURI { get; set; }
    }
}