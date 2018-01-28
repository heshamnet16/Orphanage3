using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.RegularData;
using System;
using System.Collections.Generic;

namespace OrphanageService.DataContext.Persons
{
    public class GuarantorDto
    {
        public int Id { get; set; }

        public int NameId { get; set; }

        public virtual NameDto Name { get; set; }

        public int? AddressId { get; set; }

        public virtual AddressDto Address { get; set; }

        public int AccountId { get; set; }

        public virtual AccountDto Account { get; set; }

        public long? ColorMark { get; set; }

        public bool IsSupportingTheWholeFamily { get; set; }

        public bool IsPayingMonthly { get; set; }

        public bool IsStillPaying { get; set; }

        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDto ActingUser { get; set; }

        public string Note { get; set; }
    }
}
