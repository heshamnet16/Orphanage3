using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.RegularData;
using System;

namespace OrphanageService.DataContext.Persons
{
    public class OrphanDto
    {
        public int Id { get; set; }

        public int NameId { get; set; }

        public virtual NameDto Name { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? IsExcluded { get; set; }

        public int? EducationId { get; set; }

        public virtual StudyDto Education { get; set; }

        public int? HealthId { get; set; }

        public virtual HealthDto HealthStatus { get; set; }

        public string IdentityCardNumber { get; set; }

        public byte? FootSize { get; set; }

        public int? Weight { get; set; }

        public int? Tallness { get; set; }

        public int FamilyId { get; set; }

        public virtual FamilyDto Family { get; set; }

        public bool IsBailed { get; set; }

        public int? BailId { get; set; }

        public virtual BailDto Bail { get; set; }

        public int CaregiverId { get; set; }

        public virtual CaregiverDto Caregiver { get; set; }

        public int? GuarantorId { get; set; }

        public virtual GuarantorDto Guarantor { get; set; }

        public long? ColorMark { get; set; }

        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDto ActingUser { get; set; }

        public string Story { get; set; }

        public string ConsanguinityToCaregiver { get; set; }

        public string Gender { get; set; }

        public int? CivilRegisterNumber { get; set; }

        public string PlaceOfBirth { get; set; }

        public int? Age { get; set; }

        public string FullPhotoURI { get; set; }

        public string FacePhotoURI { get; set; }

        public string BirthCertificatePhotoURI { get; set; }

        public string FamilyCardPagePhotoURI { get; set; }
    }
}
