using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.Persons;
using System;
using System.Collections.Generic;

namespace OrphanageService.DataContext.RegularData
{
    public class FamilyDC
    {
        public int Id { get; set; }

        public int MotherId { get; set; }

        public virtual MotherDC Mother { get; set; }

        public int FatherId { get; set; }

        public virtual FatherDC Father { get; set; }

        public int? BailId { get; set; }

        public virtual BailDC Bail { get; set; }

        public bool IsBailed { get; set; }

        public int? AddressId { get; set; }

        public virtual AddressDC PrimaryAddress { get; set; }

        public int? AlternativeAddressId { get; set; }

        public virtual AddressDC AlternativeAddress { get; set; }

        public bool IsExcluded { get; set; }

        public string FinncialStatus { get; set; }

        public string FamilyCardNumber { get; set; }

        public bool IsTheyRefugees { get; set; }

        public string ResidenceStatus { get; set; }

        public string ResidenceType { get; set; }

        public long? ColorMark { get; set; }

        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDC ActingUser { get; set; }

        public string Note { get; set; }

        public string FamilyCardImageFaceURI { get; set; }

        public string FamilyCardImageBackURI { get; set; }

        public virtual ICollection<OrphanDC> Orphans { get; set; }

    }
}
