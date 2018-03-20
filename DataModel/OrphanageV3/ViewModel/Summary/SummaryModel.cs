using System;

namespace OrphanageV3.ViewModel.Summary
{
    public class SummaryModel
    {
        public int OrphanID { get; set; }
        public int FatherID { get; set; }
        public int MotherID { get; set; }

        public string OrphanFirstName { get; set; }
        public string OrphanFatherName { get; set; }
        public string OrphanLastName { get; set; }
        public string OrphanEnglishFirstName { get; set; }
        public string OrphanEnglishFatherName { get; set; }
        public string OrphanEnglishLastName { get; set; }
        public string OrphanGrandFatherEnglishName { get; set; }
        public string GrandFatherName { get; set; }

        public string MotherFatherName { get; set; }
        public string MotherLastName { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherEnglishFirstName { get; set; }
        public string MotherEnglishFatherName { get; set; }
        public string MotherEnglishLastName { get; set; }

        public DateTime OrphanBirthday { get; set; }

        public string OrphanSex { get; set; }

        public string OrphanBirthplace { get; set; }

        public int OrphanAge { get; set; }

        public bool OrphanIsBailed { get; set; }

        public bool IsExcluded { get; set; }

        public decimal OrphanColor { get; set; }

        public DateTime RegisteryDate { get; set; }

        public int OrphanWeight { get; set; }

        public int OrphanTallness { get; set; }

        public string OrphanIdentityNumber { get; set; }

        public int OrphanFootSize { get; set; }

        public string OrphanStory { get; set; }

        public string StudyStage { get; set; }

        public string StudyReasons { get; set; }

        public bool IsSick { get; set; }

        public string CivilRegisterNumber { get; set; }

        public bool FamilyIsBailed { get; set; }

        public string FamilyResidentState { get; set; }

        public string FamilyResidentType { get; set; }

        public bool FamilyIsRefugee { get; set; }

        public string FamilyFinancailState { get; set; }

        public string FamilyCardNumber { get; set; }

        public string PrimaryAddressState { get; set; }
        public string PrimaryAddressCity { get; set; }
        public string PrimaryAddressTown { get; set; }
        public string PrimaryAddressStreet { get; set; }
        public string PrimaryAddressPhoneNumber { get; set; }
        public string PrimaryAddressMobileNumber { get; set; }
        public string PrimaryAddressFaxNumber { get; set; }
        public string PrimaryAddressEmail { get; set; }
        public string PrimrayAddressSkype { get; set; }
        public string PrimaryAddressFacebook { get; set; }

        public string SecondaryAddressState { get; set; }
        public string SecondaryAddressCity { get; set; }
        public string SecondaryAddressTown { get; set; }
        public string SecondaryAddressStreet { get; set; }
        public string SecondaryAddressPhoneNumber { get; set; }
        public string SecondaryAddressMobileNumber { get; set; }
        public string SecondaryAddressFaxNumber { get; set; }
        public string SecondaryAddressEmail { get; set; }
        public string SecondaryAddressSkype { get; set; }
        public string SecondaryAddressFacebook { get; set; }

        public bool FamilyIsExcluded { get; set; }

        public DateTime FatherDeathDate { get; set; }

        public string FatherJop { get; set; }

        public string FatherDeathReason { get; set; }

        public DateTime FatherBirthday { get; set; }

        public DateTime MotherBirthday { get; set; }

        public bool MotherIsDead { get; set; }

        public DateTime MotherDeathDate { get; set; }

        public string MotherIdentityNumber { get; set; }

        public bool MotherIsMarried { get; set; }

        public string MotherJop { get; set; }

        public string MotherMonthlyIncome { get; set; }

        public string CaregiverEnglishFirstName { get; set; }
        public string CaregiverEnglishFatherName { get; set; }
        public string CaregiverEnglishLastName { get; set; }
        public string CaregiverFatherName { get; set; }
        public string CaregiverLastName { get; set; }
        public string CaregiverFirstName { get; set; }

        public string OrphanConsanguinityToCaregiver { get; set; }

        public string CaregiverIdentityNumber { get; set; }

        public string CaregiverJop { get; set; }

        public string CaregiverMonthlyIncome { get; set; }

        public decimal FamilyColor { get; set; }
    }
}