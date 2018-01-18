using OrphanageService.DataContext.Persons;
using System;
using System.Collections.Generic;

namespace OrphanageService.DataContext.FinancialData
{
    public class AccountDC
    {
        public int Id { get; set; }

        public string AccountName { get; set; }

        public string Currency { get; set; }

        public string CurrencyShortcut { get; set; }

        public decimal Amount { get; set; }

        public bool CanNotBeNegative { get; set; }

        public string Note { get; set; }

        public DateTime RegDate { get; set; }

        public int UserId { get; set; }

        public virtual UserDC ActingUser { get; set; }

        public virtual ICollection<BailDC> Bails { get; set; }

        public virtual ICollection<GuarantorDC> Guarantors { get; set; }
    }
}
