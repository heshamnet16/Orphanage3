using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using System;
using System.Collections.Generic;

namespace OrphanageService.DataContext.RegularData
{
    public class FamilyDto
    {
        public int Id { get; set; }

        public int MotherId { get; set; }

        public virtual MotherDto Mother { get; set; }

        public int FatherId { get; set; }

        public virtual FatherDto Father { get; set; }

        public int? BailId { get; set; }

        public virtual BailDto Bail { get; set; }

        public bool IsBailed { get; set; }

        public int? AddressId { get; set; }

        public virtual AddressDto PrimaryAddress { get; set; }

        public int? AlternativeAddressId { get; set; }

        public virtual AddressDto AlternativeAddress { get; set; }

        public bool IsExcluded { get; set; }

        public string FinncialStatus { get; set; }

        public string FamilyCardNumber { get; set; }

        public bool IsTheyRefugees { get; set; }

        public string ResidenceStatus { get; set; }

        public string ResidenceType { get; set; }

        public long? ColorMark { get; set; }

        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDto ActingUser { get; set; }

        public string Note { get; set; }

        public string FamilyCardImagePage1URI { get; set; }

        public string FamilyCardImagePage2URI { get; set; }
    }
}
