using OrphanageService.DataContext.FinancialData;
using OrphanageService.DataContext.RegularData;
using System;
using System.Collections.Generic;

namespace OrphanageService.DataContext.Persons
{
    public class UserDC
    {
        public int Id { get; set; }

        public int? NameId { get; set; }

        public virtual NameDC Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool CanDelete { get; set; }

        public bool CanRead { get; set; }

        public bool CanAdd { get; set; }

        public bool CanDeposit { get; set; }

        public bool CanDraw { get; set; }

        public int? AddressId { get; set; }

        public virtual AddressDC Address { get; set; }

        public DateTime RegDate { get; set; }

        public bool IsAdmin { get; set; }

        public string Note { get; set; }

    }
}
