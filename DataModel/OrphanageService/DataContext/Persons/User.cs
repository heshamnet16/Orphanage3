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


        public virtual ICollection<BailDC> Bails { get; set; }

        public virtual ICollection<CaregiverDC> Caregivers { get; set; }

        public virtual ICollection<AccountDC> Accounts { get; set; }

        public virtual ICollection<FamilyDC> Famlies { get; set; }

        public virtual ICollection<FatherDC> Fathers { get; set; }

        public virtual ICollection<MotherDC> Mothers { get; set; }

        public virtual ICollection<OrphanDC> Orphans { get; set; }

        public virtual ICollection<GuarantorDC> Guarantors { get; set; }

    }
}
