using OrphanageService.DataContext.Persons;
using System;

namespace OrphanageService.DataContext.FinancialData
{
    public class AccountDto
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

        public virtual UserDto ActingUser { get; set; }


    }
}
