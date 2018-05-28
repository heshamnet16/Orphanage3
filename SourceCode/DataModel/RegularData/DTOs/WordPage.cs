using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageDataModel.RegularData.DTOs
{
    public class WordPage
    {
        [Key]
        public int Id { get; set; }

        public string TemplateName { get; set; }

        public virtual WordTemplete Template { get; set; }

        public string OrphanId { get; set; }
        public string OrphanFirstName { get; set; }
        public string OrphanFatherName { get; set; }
        public string OrphanLastName { get; set; }
        public string OrphanFullName { get; set; }
        public string OrphanBirthday { get; set; }
        public string OrphanAge { get; set; }
        public string OrphanIdentityCardNumber { get; set; }
        public string OrphanIsBailed { get; set; }
        public string BailAmount { get; set; }
        public string GuarantorName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyShortcut { get; set; }
        public string OrphanConsanguinityToCaregiver { get; set; }
        public string OrphanBirthCertificatePhoto { get; set; }
        public string OrphanFamilyCardPhoto { get; set; }
        public string OrphanFacePhoto { get; set; }
        public string OrphanFullPhoto { get; set; }
        public string OrphanPlaceOfBirth { get; set; }
        public string OrphanCivilRegisterNumber { get; set; }
        public string OrphanGender { get; set; }
        public string OrphanRegDate { get; set; }
        public string OrphanStory { get; set; }
        public string IsSick { get; set; }
        public string SicknessName { get; set; }
        public string MedicineName { get; set; }
        public string TreatmentCost { get; set; }
        public string IsStuding { get; set; }
        public string StudyStage { get; set; }
        public string SchoolName { get; set; }
        public string DegreesRate { get; set; }
        public string EducationCost { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherLastName { get; set; }
        public string FatherFatherName { get; set; }
        public string FatherFullName { get; set; }
        public string FatherBirthday { get; set; }
        public string FatherDateOfDeath { get; set; }
        public string FatherJop { get; set; }
        public string FatherDeathReason { get; set; }
        public string FatherIdentityCardNumber { get; set; }
        public string FatherPhoto { get; set; }
        public string FatherStory { get; set; }
        public string FatherDeathCertificatePhoto { get; set; }
        public string OrphanBrothersCount { get; set; }
        public string FamilyOrphansCount { get; set; }
        public string FatherOrphansCount { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherLastName { get; set; }
        public string MotherFatherName { get; set; }
        public string MotherFullName { get; set; }
        public string MotherBirthday { get; set; }
        public string MotherDateOfDeath { get; set; }
        public string MotherIsMarried { get; set; }
        public string MotherJop { get; set; }
        public string MotherSalary { get; set; }
        public string MotherIdentityCardNumber { get; set; }
        public string MotherStory { get; set; }
        public string MotherIsDead { get; set; }
        public string MotherHusbandName { get; set; }
        public string MotherFullAddress { get; set; }
        public string MotherHasSheOrphans { get; set; }
        public string MotherMobileNumber { get; set; }
        public string MotherPhoneNumber { get; set; }
        public string CaregiverFirstName { get; set; }
        public string CaregiverLastName { get; set; }
        public string CaregiverFatherName { get; set; }
        public string CaregiverFullName { get; set; }
        public string CaregiverJop { get; set; }
        public string CaregiverSalary { get; set; }
        public string CaregiverIdentityCardNumber { get; set; }
        public string CaregiverFullAddress { get; set; }
        public string CaregiverMobileNumber { get; set; }
        public string CaregiverPhoneNumber { get; set; }
        public string FamilyIsBaild { get; set; }
        public string FamilyFinncialStatus { get; set; }
        public string FamilyCardNumber { get; set; }
        public string FamilyIsTheyRefugees { get; set; }
        public string FamilyResidenceStatus { get; set; }
        public string FamilyResidenceType { get; set; }
    }
}