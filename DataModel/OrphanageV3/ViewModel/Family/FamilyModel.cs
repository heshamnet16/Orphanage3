using System;

namespace OrphanageV3.ViewModel.Family
{
    public class FamilyModel
    {
        public int Id { get; set; }

        public string FamilyFullName { get; set; }

        public string MotherFullName { get; set; }

        public string FatherFullName { get; set; }

        public string GuarantorName { get; set; }

        public string BailAmount { get; set; }

        public bool IsBailed { get; set; }

        public string FullAddress { get; set; }

        public string AlternativeAddress { get; set; }

        public bool IsExcluded { get; set; }

        public string FinncialStatus { get; set; }

        public string FamilyCardNumber { get; set; }

        public bool IsTheyRefugees { get; set; }

        public string ResidenceStatus { get; set; }

        public string ResidenceType { get; set; }

        public long? ColorMark { get; set; }

        public DateTime RegDate { get; set; }

        public string UserName { get; set; }

        public string Note { get; set; }

        public int OrphansCount { get; set; }
    }
}